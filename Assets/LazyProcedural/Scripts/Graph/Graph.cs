using System;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityGraph = UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using Sceelix.Core.Procedures;
using UnityEngine;
using Sceelix.Meshes.Procedures;
using System.Linq;

namespace LazyProcedural
{
    public class Graph : UnityGraph.GraphView
    {

        //public struct GraphChangedEvent
        //{
        //    public bool nodeDeletion;
        //}
        //public delegate void GraphChangedHandler(GraphChangedEvent e);

        public event NodeEvent OnNodeSelected;
        public event Action OnNodesUnselected;
        public event Action OnGraphStructureChanged;

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
            if (change.edgesToCreate == null && change.elementsToRemove == null) return change;

            if (change.edgesToCreate != null && change.edgesToCreate.Count > 0)
            {
                List<UnityGraph.Edge> edges = new List<UnityGraph.Edge>();
                foreach (var unityEdge in change.edgesToCreate)
                {
                    edges.Add(new Edge(unityEdge));
                }

                change.edgesToCreate = edges;
            }

            if (change.elementsToRemove != null)
            {
                foreach (var elementToRemove in change.elementsToRemove)
                {
                    if (elementToRemove.GetType() == typeof(UnityGraph.Group))
                    {
                        UnityGraph.Group castedGroup = (UnityGraph.Group)elementToRemove;

                        foreach (var node in castedGroup.containedElements)
                        {
                            var castedNode = (Node)node;
                            castedNode.group = null;
                            node.RemoveFromHierarchy();
                            Add(node);
                        }

                    }
                }
            }

            OnGraphStructureChanged.TryInvoke();

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
                node.OnNodeSelected += Node_OnSelected;
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

            node.OnNodeSelected += Node_OnSelected;
            node.OnNodeUnselected += Node_OnUnselected;
            AddElement(node);

            OnGraphStructureChanged.TryInvoke();
        }

        public void AddEdges(IEnumerable<Edge> edges)
        {
            foreach (var edge in edges)
            {
                //The node to be valid and visually functional it is required to be created via this function
                var trueEdge = edge.input.ConnectTo<Edge>(edge.output);
         
                AddElement(trueEdge);
            }
        }
        public void AddEdge(Port outPort, Port inPort)
        {
            var edge = new Edge(outPort, inPort);
            AddElement(edge);
        }

        public void GroupSelection()
        {
            var nodesToGroup = this.selection.Where(x => x.GetType() == typeof(Node) && ((Node)x).group == null);

            if (!nodesToGroup.Any()) return;

            UnityGraph.Group group = new UnityGraph.Group();
            group.title = "New Group";
            group.AddElements(nodesToGroup.Select(x => (Node)x));

            foreach (var nodeToGroup in nodesToGroup)
            {
                var castedNode = (Node)nodeToGroup;
                castedNode.group = group;
            }

            AddElement(group);
        }
        private void Node_OnSelected(Node node)
        {
            if (OnNodeSelected != null)
                OnNodeSelected.Invoke(node);

        }

        private void Node_OnUnselected(Node node)
        {
            if (selection.ToArray().Where(x => x != node && x.GetType() == typeof(Node)).Any()) return;

            //If there are no more elements in the list raise event
            OnNodesUnselected.TryInvoke();
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

            node.OnNodeSelected += Node_OnSelected;
            node.OnNodeUnselected += Node_OnUnselected;
            AddElement(node);

        }

    }
}
