using System;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityGraph = UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using Sceelix.Core.Procedures;
using UnityEngine;
using Sceelix.Meshes.Procedures;

namespace LazyProcedural
{
    public class Graph : UnityGraph.GraphView
    {
        public event NodeEvent OnNodeSelected;
        public event Action OnGraphChanged;

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


            this.graphViewChanged = new GraphViewChanged(OnGraphViewChanged);
        }


        private GraphViewChange OnGraphViewChanged(UnityGraph.GraphViewChange change)
        {


            if (OnGraphChanged != null)
                OnGraphChanged.Invoke();

            return change;
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

        //Loading from file
        public void AddNodes(IEnumerable<Node> nodes)
        {
            foreach (var node in nodes)
            {
                node.OnNodeSelected += Node_OnNodeSelected;
                AddElement(node);
            }
        }


        public void AddNode(ProcedureInfo procedureInfo, Vector2 pos)
        {
            //if (!procedureInfo.Type.IsSubclassOf(typeof(Procedure)))
            //{
            //    Debug.LogError("Cannot create non-procedure node");
            //    return;
            //}

            var node = new Node(procedureInfo);

            pos = contentViewContainer.WorldToLocal(pos);
            node.SetPosition(new Rect(pos, node.GetPosition().size));

            node.OnNodeSelected += Node_OnNodeSelected;
            AddElement(node);
        }

        public void AddEdges(IEnumerable<Edge> edges)
        {
            foreach (var edge in edges)
            {
                //AddElement(edge);
                var unityEdge = edge.input.ConnectTo(edge.output);
                AddElement(unityEdge);
            }
        }
        public void AddEdge(Port outPort, Port inPort)
        {
            var edge = new Edge { output = outPort, input = inPort };
            AddElement(edge);
        }

        private void Node_OnNodeSelected(Node node)
        {
            if (OnNodeSelected != null)
                OnNodeSelected.Invoke(node);

        }

        public bool CheckTypeCompatiblity(Type inType, Type outType)
        {
            return inType == outType || inType.IsAssignableFrom(outType) || outType.IsAssignableFrom(inType);
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

        public void DuplicateSelection(Vector2 pos)
        {
            foreach (var selectable in selection)
            {
                if (selectable.GetType() == typeof(Node))
                {
                    Node node = (Node)selectable;
                    DuplicateNode(node, pos);
                }
            }
        }

        public void DuplicateNode(Node sourceNode, Vector2 pos)
        {
            Node node = new Node(sourceNode);

            pos = contentViewContainer.WorldToLocal(pos);
            node.SetPosition(new Rect(pos, sourceNode.GetPosition().size));

            node.OnNodeSelected += Node_OnNodeSelected;
            AddElement(node);

        }

    }
}
