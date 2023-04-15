using System;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityGraph = UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;


namespace LazyProcedural
{
    public class Graph : UnityGraph.GraphView
    {

        public Graph()
        {
            //Must be this order to properly work
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new FreehandSelector());
            this.AddManipulator(new ClickSelector()); 

            var zoomer = new ContentZoomer();
            zoomer.maxScale = 10;
            this.AddManipulator(zoomer);


            AddElement(ObjectFactory.InstantiateTestNode());
            AddElement(ObjectFactory.InstantiateTestNode());
            AddElement(ObjectFactory.InstantiateTestNode());
            AddElement(ObjectFactory.InstantiateTestNode());
            AddElement(ObjectFactory.InstantiateTestNode());
            AddElement(ObjectFactory.InstantiateTestNode());
            AddElement(ObjectFactory.InstantiateTestNode());
            AddElement(ObjectFactory.InstantiateTestNode());
            AddElement(ObjectFactory.InstantiateTestNode());
            AddElement(ObjectFactory.InstantiateTestNode());
            AddElement(ObjectFactory.InstantiateTestNode());
            AddElement(ObjectFactory.InstantiateTestNode());
            AddElement(ObjectFactory.InstantiateTestNode());
            AddElement(ObjectFactory.InstantiateTestNode());
            AddElement(ObjectFactory.InstantiateTestNode());
            AddElement(ObjectFactory.InstantiateTestNode());
            AddElement(ObjectFactory.InstantiateTestNode());
        }

        public override List<UnityGraph.Port> GetCompatiblePorts(UnityGraph.Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<UnityGraph.Port>();
            var checkedNodes = new Dictionary<Node, bool>();
            var startNode = (Node)startPort.node;

            ports.ForEach(endPort =>
            {
                if (startPort == endPort)
                    return;

                if (startPort.direction == Direction.Input && endPort.direction == Direction.Input || startPort.direction == Direction.Output && endPort.direction == Direction.Output)
                    return;

                if (!CheckTypeCompatiblity(startPort.portType, endPort.portType))
                    return;

                var endNode = (Node)endPort.node;

                if (!checkedNodes.ContainsKey(endNode))
                {
                    //Check if there recursiveness between the end and start nodes (if connecting them would create a loop)
                    var connectionResult = CheckConnection(endNode, startNode);
                    checkedNodes.Add(endNode, connectionResult);
                    if (connectionResult)
                        return;
                }
                else if (checkedNodes[endNode])
                    return;

                compatiblePorts.Add(endPort);
            });

            return compatiblePorts;
        }

        public bool CheckTypeCompatiblity(Type inType, Type outType)
        {
            return inType == outType;
        }


        /// <summary>
        /// Check with  Depth-First Search (DFS) if there is connection between the two nodes
        /// </summary>
        /// <param name="currentNode">The starting node</param>
        /// <param name="nodeToLook">The ending node</param>
        /// <returns>True if there is a connection</returns>
        public bool CheckConnection(Node currentNode, Node nodeToLook)
        {
            if (currentNode == nodeToLook) return true;

            foreach (var port in currentNode.outPorts)
            {
                foreach (var edge in port.connections)
                {
                    //The node that receives this connection
                    var nextNode = (Node)edge.input.node;
                    if (nextNode == currentNode) continue;
                    if (CheckConnection(nextNode, nodeToLook))
                        return true;
                }
            }
            return false;
        }


        //private Node CreateTestNode()
        //{
        //    var node = new Node(GUID.Generate().ToString())
        //    {
        //        title = "Teste Node",
        //        inPorts = new Port[] {
        //            new Port(true,true,typeof(string)) ,
        //            new Port(true,true,typeof(int)) ,
        //            new Port(true,true,typeof(string))
        //        },
        //        outPorts = new Port[] {
        //            new Port(false,true,typeof(string)) ,
        //            new Port(false,true,typeof(int)) ,
        //            new Port(false,true,typeof(string))
        //        }
        //    };

        //    node.Build();

        //    node.SetPosition(new Rect(10, 10, 200, 200));

        //    node.Add(new Label("Line Param 1"));
        //    node.Add(new Label("Line Param 2"));
        //    node.Add(new TextField("Line Param 3"));

        //    return node;
        //}

    }
}
