using Sceelix.Core;
using Sceelix.Core.Data;
using Sceelix.Core.Environments;
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

        private static Dictionary<Node, int> executionOrderMatrix;

        public static void Init()
        {
            SceelixDomain.LoadAssembliesFrom($"{PathFactory.absoluteToolPath}\\{PathFactory.SCEELIX_PATH}");
            EngineManager.Initialize();
            ParameterManager.Initialize();

            environment = new ProcedureEnvironment(new ResourceManager($"{PathFactory.absoluteToolPath}\\{PathFactory.SCEELIX_PATH}", Assembly.GetExecutingAssembly()));
        }

        public static List<IEntity> ExecuteGraph(List<UnityGraph.Node> nodes)
        {
            return ExecuteGraph(nodes.Select(x => (Node)x).ToList());
        }

        public static List<IEntity> ExecuteGraph(List<Node> nodes)
        {
            List<IEntity> output;

            List<Node> rootNodes = GetRootNodes(nodes);

            executionOrderMatrix = new Dictionary<Node, int>();

            //Firstly evaluate the execution orders
            foreach (Node rootNode in rootNodes)
                DFS_EvaluateExecutionOrder(rootNode, -1);

            List<List<Node>> nodesByExecutionOrder = ConvertMatrixToList();

            output = BFS_Execution(nodesByExecutionOrder);

            return output;
        }

        private static List<List<Node>> ConvertMatrixToList()
        {

            var numOrders=executionOrderMatrix.Values.Max()+1;
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

        private static void DFS_EvaluateExecutionOrder(Node currentNode, int order)
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

        private static List<IEntity> BFS_Execution(List<List<Node>> nodesByExecutionOrder)
        {
            List<IEntity> result = new List<IEntity>();

            foreach (var nodesOfOrder in nodesByExecutionOrder)
            {
                foreach (var node in nodesOfOrder)
                {
                    node.nodeData.Execute();

                    //If is terminal Node then append all the output to results
                    if (node.GetTotalConnectedPorts(inPorts: false) == 0)
                    {
                        foreach (var output in node.nodeData.Outputs)
                        {
                            var resultEntry = output.Dequeue();
                            if (resultEntry != null)
                                result.Add(resultEntry);
                        }
                        continue;
                    }

                    foreach (var outPort in node.outPorts)
                    {
                        foreach (var edge in outPort.connections)
                        {
                            Node inNode = (Node)edge.input.node;
                            int inIndex = inNode.GetPortIndex((Port)edge.input);
                            int outIndex = node.GetPortIndex((Port)edge.output);

                            //Append a clone of the current Node result to the input of the connected Node
                            inNode.nodeData.Inputs[inIndex].Input.Enqueue(node.nodeData.Outputs[outIndex].Peek().DeepClone());
                        }
                    }
                }
            }
            return result;
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
