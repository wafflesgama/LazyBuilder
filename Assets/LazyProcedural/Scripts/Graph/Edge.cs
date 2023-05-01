using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace LazyProcedural
{
    using static UnityEditor.Experimental.GraphView.Port;

    public class Edge : UnityEditor.Experimental.GraphView.Edge
    {
        public override void OnPortChanged(bool isInput)
        {
            base.OnPortChanged(isInput);
        }
    }

    public class EdgeConnectorListener : IEdgeConnectorListener
    {
        private GraphViewChange m_GraphViewChange;

        private List<UnityEditor.Experimental.GraphView.Edge> m_EdgesToCreate;

        private List<GraphElement> m_EdgesToDelete;

        public EdgeConnectorListener()
        {
            m_EdgesToCreate = new List<UnityEditor.Experimental.GraphView.Edge>();
            m_EdgesToDelete = new List<GraphElement>();
            m_GraphViewChange.edgesToCreate = m_EdgesToCreate;
        }

        public void OnDropOutsidePort(UnityEditor.Experimental.GraphView.Edge edge, Vector2 position)
        {
        }

        public void OnDrop(GraphView graphView, UnityEditor.Experimental.GraphView.Edge edge)
        {
            //Graph graph= (Graph)graphView;

            m_EdgesToCreate.Clear();
            m_EdgesToCreate.Add(edge);
            m_EdgesToDelete.Clear();


            if (edge.input.capacity == Capacity.Single)
            {
                foreach (Edge connection in edge.input.connections)
                {
                    if (connection != edge)
                    {
                        m_EdgesToDelete.Add(connection);
                    }
                }
            }

            if (edge.output.capacity == Capacity.Single)
            {
                foreach (Edge connection2 in edge.output.connections)
                {
                    if (connection2 != edge)
                    {
                        m_EdgesToDelete.Add(connection2);
                    }
                }
            }

            if (m_EdgesToDelete.Count > 0)
            {
                graphView.DeleteElements(m_EdgesToDelete);
            }

            List<UnityEditor.Experimental.GraphView.Edge> edgesToCreate = m_EdgesToCreate;
            if (graphView.graphViewChanged != null)
            {
                edgesToCreate = graphView.graphViewChanged(m_GraphViewChange).edgesToCreate;
            }

            foreach (Edge item in edgesToCreate)
            {
                graphView.AddElement(item);
                edge.input.Connect(item);
                edge.output.Connect(item);
            }
        }
    }
}
