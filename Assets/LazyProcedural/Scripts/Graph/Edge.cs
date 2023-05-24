using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityGraph = UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace LazyProcedural
{
    using static UnityEditor.Experimental.GraphView.Port;
    public class Edge : UnityEditor.Experimental.GraphView.Edge
    {
        public Label inNumber;
        public Label outNumber;


        private const int NUMBER_MARGIN_VERTICAL = 10;
        private const int NUMBER_MARGIN_HORIZONTAL = 24;
        public Edge(Port output, Port input)
        {
            this.output = output;
            this.input = input;

            SetupExtraUI();
        }
        public Edge(UnityGraph.Edge edgeSource)
        {
            output = edgeSource.output;
            input = edgeSource.input;
            SetupExtraUI();
        }

        public Edge()
        {
            SetupExtraUI();
        }


        private void OnEdgeControlGeometryChanged(GeometryChangedEvent evt)
        {
            if (edgeControl.from.y < edgeControl.to.y)
            {
                inNumber.style.top = NUMBER_MARGIN_VERTICAL;
                inNumber.style.bottom = StyleKeyword.Auto;

                outNumber.style.top = StyleKeyword.Auto;
                outNumber.style.bottom = NUMBER_MARGIN_VERTICAL;
            }
            else
            {
                inNumber.style.top = StyleKeyword.Auto;
                inNumber.style.bottom = NUMBER_MARGIN_VERTICAL;

                outNumber.style.top = NUMBER_MARGIN_VERTICAL;
                outNumber.style.bottom = StyleKeyword.Auto;
            }
        }

        public void SetInNumber(int value)
        {
            inNumber.text= value.ToString();
        }

        public void SetOutNumber(int value)
        {
            outNumber.text= value.ToString();
        }

        public override void OnPortChanged(bool isInput)
        {
            base.OnPortChanged(isInput);

        }
        public void SetupExtraUI()
        {
            edgeControl.RegisterCallback<GeometryChangedEvent>(OnEdgeControlGeometryChanged);

            inNumber = new Label();
            inNumber.text = "";

            outNumber = new Label();
            outNumber.text = "";

            inNumber.style.position = Position.Absolute;
            outNumber.style.position = Position.Absolute;

            inNumber.style.left = NUMBER_MARGIN_HORIZONTAL;
            outNumber.style.right = NUMBER_MARGIN_HORIZONTAL;


            inNumber.style.color = edgeControl.inputColor;
            outNumber.style.color = edgeControl.outputColor;

            edgeControl.Add(inNumber);
            edgeControl.Add(outNumber);



            //contentContainer.Add(inNumber);
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
