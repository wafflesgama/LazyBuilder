using Sceelix.Core;
using Sceelix.Core.Attributes;
using Sceelix.Core.Data;
using Sceelix.Core.Environments;
using Sceelix.Core.IO;
using Sceelix.Core.Parameters;
using Sceelix.Loading;
using System;
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

        private List<(GlobalAttributeKey, object)> convertedGlobalParams;

        private Dictionary<Node, int> executionNodesOrder;
        private List<List<Node>> executionOrderMatrix;

        private Dictionary<Port, (Edge, int)> inPortNumbers;
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

        public List<IEntity> ExecuteGraph(IEnumerable<UnityGraph.Node> nodes, IEnumerable<(string, object)> globalParameters = null)
        {
            return ExecuteGraph(nodes.Select(x => (Node)x).ToList(), globalParameters);
        }
        public void CalculateGraphOrder(IEnumerable<UnityGraph.Node> nodes)
        {
            CalculateGraphOrder(nodes.Select(x => (Node)x).ToList());
        }

        public void CalculateGraphOrder(List<Node> nodes)
        {

            List<Node> rootNodes = GetRootNodes(nodes);

            executionNodesOrder = new Dictionary<Node, int>();
            executionOrderMatrix = new List<List<Node>>();

            if (rootNodes == null || rootNodes.Count == 0) return;


            //Firstly evaluate the execution orders
            foreach (Node rootNode in rootNodes)
                DFS_EvaluateExecutionOrder(rootNode, -1);

            //Then order the nodes by the execution order
            executionOrderMatrix = ConvertMatrixToList();
        }

        public List<IEntity> ExecuteGraph(List<Node> nodes, IEnumerable<(string, object)> globalParameters)
        {
            List<IEntity> output = new List<IEntity>();

            inPortNumbers = new Dictionary<Port, (Edge, int)>();

            foreach (var node in nodes)
                node.ClearProcessedState();

            //Create the Global Params to later append them
            if (globalParameters != null)
                convertedGlobalParams = globalParameters.Select(x => (new GlobalAttributeKey(x.Item1), x.Item2)).ToList();
            else
                convertedGlobalParams = new List<(GlobalAttributeKey, object)>();

            output = BFS_Execution(executionOrderMatrix);

            return output;
        }

        private List<List<Node>> ConvertMatrixToList()
        {

            var numOrders = executionNodesOrder.Values.Max() + 1;
            List<List<Node>> nodesByExecutionOrder = new List<List<Node>>();
            for (int i = 0; i < numOrders; i++)
            {
                nodesByExecutionOrder.Add(new List<Node>());
            }


            foreach (var item in executionNodesOrder)
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

            if (currentNode == null) return;

            if (!executionNodesOrder.ContainsKey(currentNode))
                executionNodesOrder.Add(currentNode, order);
            else
                //Keep only the highests orders
                executionNodesOrder[currentNode] = executionNodesOrder[currentNode] > order ? executionNodesOrder[currentNode] : order;

            foreach (var outPort in currentNode.outPorts)
            {
                if (outPort.isMuted) continue;

                foreach (var edge in outPort.connections)
                    DFS_EvaluateExecutionOrder((Node)edge.input.node, order);
            }
        }

        private List<IEntity> BFS_Execution(List<List<Node>> nodesByExecutionOrder)
        {
            List<IEntity> result = new List<IEntity>();
            bool rootNodePass = true;

            Entity globalEntity = new Entity();
            string globalImpulsePort = Port.GLOBAL_PARAM_PORTNAME;
            foreach (var globalParam in convertedGlobalParams)
                globalEntity.Attributes.Add(globalParam.Item1, globalParam.Item2);


            foreach (var nodesOfOrder in nodesByExecutionOrder)
            {
                foreach (var outNode in nodesOfOrder)
                {
                    if (rootNodePass && convertedGlobalParams.Count > 0)
                    {
                        if (!outNode.nodeData.HasInputs)
                            outNode.nodeData.SetupImpulsePorts(globalImpulsePort);

                        outNode.nodeData.Inputs[globalImpulsePort].Enqueue(globalEntity);
                    }
                    try
                    {
                        outNode.nodeData.Execute();
                        outNode.SetProcessedSate(success: true);
                    }
                    catch (Exception ex)
                    {
                        outNode.SetProcessedSate(success: false);
                        Debug.LogException(ex);
                    }

                    //If is terminal Node then append all the output to results
                    if (outNode.GetTotalConnectedPorts(inPorts: false) == 0)
                    {
                        foreach (var outPort in outNode.outPorts)
                        {
                            if (outPort.outputData.Peek() == null) continue;

                            var resultEntry = outPort.outputData.DequeueAll();
                            result.AddRange(resultEntry);
                        }

                        continue;
                    }

                    foreach (var outPort in outNode.outPorts)
                    {
                        foreach (var edge in outPort.connections)
                        {

                            Node inNode = (Node)edge.input.node;

                            Port castedInPort = (Port)edge.input;
                            Port castedOutPort = (Port)edge.output;

                            var executionResult = GetOutput(outNode, castedOutPort).PeekAll();

                            var resultEntityNum = executionResult.Count();

                            Edge castedEdge = (Edge)edge;

                            if (!inPortNumbers.ContainsKey(castedInPort))
                            {
                                inPortNumbers.Add(castedInPort, (castedEdge, resultEntityNum));
                                castedEdge.SetOutNumber(resultEntityNum);

                            }
                            else
                            {
                                //Add this result to the existing number
                                inPortNumbers[castedInPort] = (inPortNumbers[castedInPort].Item1, inPortNumbers[castedInPort].Item2 + resultEntityNum);
                                //And set it
                                inPortNumbers[castedInPort].Item1.SetOutNumber(inPortNumbers[castedInPort].Item2);
                            }

                            castedEdge.SetInNumber(executionResult.Count());

                            //Append a clone of the current Node result to the input of the connected Node
                            GetInput(inNode, castedInPort).Input.Enqueue(executionResult.Select(x => x.DeepClone()));
                        }
                    }
                }
                rootNodePass = false;
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
