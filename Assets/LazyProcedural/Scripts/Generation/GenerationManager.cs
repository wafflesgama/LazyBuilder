using Sceelix.Core;
using Sceelix.Core.Data;
using Sceelix.Core.Environments;
using Sceelix.Core.IO;
using Sceelix.Core.Parameters;
using Sceelix.Loading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using UnityEngine;
using UnityGraph = UnityEditor.Experimental.GraphView;

namespace LazyProcedural
{
    public class GenerationManager
    {
        private static ProcedureEnvironment environment;
        private static bool initialized = false;


        private Dictionary<Node, int> executionOrderMatrix;
        public static void Init()
        {
            if (initialized) return;

            SceelixDomain.LoadAssembliesFrom($"{PathFactory.absoluteToolPath}\\{PathFactory.SCEELIX_PATH}");
            EngineManager.Init();
            ParameterManager.Init();

            environment = new ProcedureEnvironment(new ResourceManager($"{PathFactory.absoluteToolPath}\\{PathFactory.SCEELIX_PATH}", Assembly.GetExecutingAssembly()));
            initialized = true;
        }

        public GenerationManager()
        {
            Init();
        }

        public List<IEntity> ExecuteGraph(List<UnityGraph.Node> nodes)
        {
            return ExecuteGraph(nodes.Select(x => (Node)x).ToList());
        }

        public List<IEntity> ExecuteGraph(List<Node> nodes)
        {
            List<IEntity> output = new List<IEntity>();

            List<Node> rootNodes = GetRootNodes(nodes);

            if (rootNodes == null || rootNodes.Count == 0) return output;

            executionOrderMatrix = new Dictionary<Node, int>();

            //Firstly evaluate the execution orders
            foreach (Node rootNode in rootNodes)
                DFS_EvaluateExecutionOrder(rootNode, -1);

            List<List<Node>> nodesByExecutionOrder = ConvertMatrixToList();

            output = BFS_Execution(nodesByExecutionOrder);

            return output;
        }

        private List<List<Node>> ConvertMatrixToList()
        {

            var numOrders = executionOrderMatrix.Values.Max() + 1;
            List<List<Node>> nodesByExecutionOrder = new List<List<Node>>();
            for (int i = 0; i < numOrders; i++)
            {
                nodesByExecutionOrder.Add(new List<Node>());
            }


            foreach (var item in executionOrderMatrix)
            {
                //if (nodesByExecutionOrder[item.Value] == null)
                //    nodesByExecutionOrder.Add(new List<Node>());

                nodesByExecutionOrder[item.Value].Add(item.Key);
            }

            return nodesByExecutionOrder;
        }
        private static List<Node> GetRootNodes(List<Node> nodes)
        {
            return nodes.Where(x => x.IsRootNode()).ToList();
        }

        private void DFS_EvaluateExecutionOrder(Node currentNode, int order)
        {
            order++;

            if (!executionOrderMatrix.ContainsKey(currentNode))
                executionOrderMatrix.Add(currentNode, order);
            else
                //Keep only the highests orders
                executionOrderMatrix[currentNode] = executionOrderMatrix[currentNode] > order ? executionOrderMatrix[currentNode] : order;

            foreach (var outPort in currentNode.outPorts)
                foreach (var edge in outPort.connections)
                    DFS_EvaluateExecutionOrder((Node)edge.input.node, order);
        }

        private List<IEntity> BFS_Execution(List<List<Node>> nodesByExecutionOrder)
        {
            List<IEntity> result = new List<IEntity>();

            foreach (var nodesOfOrder in nodesByExecutionOrder)
            {
                foreach (var outNode in nodesOfOrder)
                {
                    outNode.nodeData.Execute();

                    //If is terminal Node then append all the output to results
                    if (outNode.GetTotalConnectedPorts(inPorts: false) == 0)
                    {
                        foreach (var outPort in outNode.outPorts)
                        {
                            if (outPort.outputData.Peek() == null) continue;

                            var resultEntry = outPort.outputData.DequeueAll();
                            result.AddRange(resultEntry);
                        }
                        //foreach (var output in outNode.nodeData.Outputs)
                        //{
                        //    if (output.Peek() == null) continue;

                        //    var resultEntry = output.Dequeue();
                        //    result.Add(resultEntry);
                        //}

                        //foreach (var output in outNode.nodeData.SubOutputs)
                        //{
                        //    if (output.Peek() == null) continue;

                        //    var resultEntry = output.Dequeue();
                        //    result.Add(resultEntry);
                        //}
                        continue;
                    }

                    foreach (var outPort in outNode.outPorts)
                    {
                        foreach (var edge in outPort.connections)
                        {

                            Node inNode = (Node)edge.input.node;
                            
                            Port castedInPort = (Port)edge.input;
                            Port castedOutPort = (Port)edge.output;

                            Edge castedEdge= edge as Edge;

                            var executionResult = GetOutput(outNode, castedOutPort).PeekAll();

                            castedEdge.SetInNumber(executionResult.Count());
                            castedEdge.SetOutNumber(executionResult.Count());

                            //Append a clone of the current Node result to the input of the connected Node
                            GetInput(inNode, castedInPort).Input.Enqueue(executionResult.Select(x=>x.DeepClone()));
                        }
                    }
                }
            }
            return result;
        }

        private OutputReference GetOutput(Node node, Port port)
        {
            if (port.isDirectAccessPort)
                return node.nodeData.Outputs[port.accessIndex[0]];

            ParameterReference param = node.nodeData.Parameters[port.accessIndex[0]];

            //Iterates until the pre-last element
            for (int i = 0; i < port.accessIndex.Length - 1; i++)
            {
                //First element skipped since its accessed through the node
                if (i == 0) continue;
                param = param.Parameters[port.accessIndex[i]];
            }
            //Access the Outputs through the last index of the array
            return param.Outputs[port.accessIndex[port.accessIndex.Length - 1]];
        }

        private InputReference GetInput(Node node, Port port)
        {
            if (port.isDirectAccessPort)
                return node.nodeData.Inputs[port.accessIndex[0]];

            ParameterReference param = node.nodeData.Parameters[port.accessIndex[0]];

            //Iterates until the pre-last element
            for (int i = 0; i < port.accessIndex.Length - 1; i++)
            {
                //First element skipped since its accessed through the node
                if (i == 0) continue;
                param = param.Parameters[port.accessIndex[i]];
            }
            //Access the Outputs through the last index of the array
            return param.Inputs[port.accessIndex[port.accessIndex.Length - 1]];
        }


        /// DEPRECATED but could be later used for graphs with non merging strucutres improving performance?
        /// 
        //private List<IEntity> DFS_Execution(Node currentNode)
        //{
        //    currentNode.nodeData.Execute();
        //    List<IEntity> result = new List<IEntity>();

        //    //If is terminal Node then execute and return
        //    if (currentNode.GetTotalConnectedPorts(inPorts: false) == 0)
        //    {
        //        foreach (var output in currentNode.nodeData.Outputs)
        //        {
        //            var resultEntry = output.Dequeue();
        //            if (resultEntry != null)
        //                result.Add(resultEntry);
        //        }
        //        return result;
        //    }

        //    foreach (var output in currentNode.nodeData.Outputs)
        //    {
        //        var resultEntry = output.Dequeue();
        //        if (resultEntry != null)
        //            result.Add(resultEntry);
        //    }
        //}



    }
}
