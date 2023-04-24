using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor.Experimental.GraphView;
using UnityGraph = UnityEditor.Experimental.GraphView;
using UnityEditor;
using Sceelix.Core.Procedures;
using System.Linq;

namespace LazyProcedural
{
    public delegate void NodeEvent(Node node);

    public class Node : UnityGraph.Node
    {
        public GUID guid { get; private set; }

        public List<Port> inPorts { get; private set; } = new List<Port>();
        public List<Port> outPorts { get; private set; } = new List<Port>();

        public Procedure nodeData { get; private set; }

        public event NodeEvent OnNodeSelected;

        public Node(string title)
        {
            this.title = title;
            guid = GUID.Generate();

        }
        public Node(Procedure nodeData)
        {

            title = nodeData.GetType().Name;
            this.nodeData = nodeData;

            foreach (var input in nodeData.Inputs)
            {
                var port = new Port(input.Input);
                inPorts.Add(port);
            }

            foreach (var output in nodeData.Outputs)
            {
                var port = new Port(output.Output);
                outPorts.Add(port);
            }

            RefreshNode();
            //Add
        }

        public void RefreshNode()
        {
            foreach (var inPort in inPorts)
            {
                this.inputContainer.Add(inPort);
            }

            foreach (var outPort in outPorts)
            {
                this.outputContainer.Add(outPort);
            }

            this.RefreshExpandedState();
            this.RefreshPorts();
        }

        public override void OnUnselected()
        {
            base.OnUnselected();
        }
        public override void OnSelected()
        {
            if (OnNodeSelected != null)
                OnNodeSelected.Invoke(this);

            base.OnSelected();
        }

        public int GetPortIndex(Port port)
        {
            if (port.direction == Direction.Input)
                return inPorts.IndexOf(port);

            return outPorts.IndexOf(port);
        }

        public bool IsRootNode()
        {
            return inPorts.Count == 0;
        }
        public int GetTotalConnectedPorts(bool inPorts)
        {
            return inPorts ? this.inPorts.Where(x=> x.connected).Count() : this.outPorts.Where(x => x.connected).Count();
        }
    }
}
