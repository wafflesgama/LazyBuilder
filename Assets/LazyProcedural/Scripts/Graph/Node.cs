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

        private List<(Sceelix.Core.IO.InputReference, int[])> inputsToAdd;
        private List<(Sceelix.Core.IO.OutputReference, int[])> outputsToAdd;


        public Node(ProcedureInfo nodeInfo, Procedure nodeData)
        {

            title = nodeInfo.Label;
            tooltip = nodeInfo.Label;

            this.nodeData = nodeData;

            RefreshNode();
        }

        public Node(Node sourceNode)
        {
            title = sourceNode.title;
            tooltip = sourceNode.tooltip;

            nodeData = sourceNode.nodeData;

            RefreshNode();
        }

        public void RefreshNode()
        {
            RegisterPorts();
            AppendPorts();
        }

        private void RegisterPorts()
        {
            //First Add the direct access ports
            for (int i = 0; i < nodeData.Inputs.Count; i++)
            {
                var port = new Port(nodeData.Inputs[i].Input, true, new int[] { i });
                inPorts.Add(port);
            }

            for (int i = 0; i < nodeData.Outputs.Count; i++)
            {
                var port = new Port(nodeData.Outputs[i].Output, true, new int[] { i });
                outPorts.Add(port);
            }

            inputsToAdd = new List<(Sceelix.Core.IO.InputReference, int[])>();
            outputsToAdd = new List<(Sceelix.Core.IO.OutputReference, int[])>();
            List<int> accessIndex = new List<int>();
            accessIndex.Add(0);

            //Then search for additional ports nested in the parameter fields
            for (int i = 0; i < nodeData.Parameters.Count; i++)
            {
                accessIndex[0] = i;
                SearchForSubInputsOutputs(nodeData.Parameters[i], accessIndex);
            }


            //Then add the new ports of nested access
            foreach (var input in inputsToAdd)
            {
                var port = new Port(input.Item1.Input, false, input.Item2);
                inPorts.Add(port);
            }

            foreach (var output in outputsToAdd)
            {
                var port = new Port(output.Item1.Output, false, output.Item2);
                outPorts.Add(port);
            }

        }


        private void SearchForSubInputsOutputs(Sceelix.Core.Parameters.ParameterReference parameter, List<int> accessIndex)
        {

            for (int i = 0; i < parameter.Inputs.Count; i++)
            {
                var finalList = accessIndex.ToList();
                finalList.Add(i);
                inputsToAdd.Add((parameter.Inputs[i], finalList.ToArray()));
            }

            for (int i = 0; i < parameter.Outputs.Count; i++)
            {
                var finalList = accessIndex.ToList();
                finalList.Add(i);
                outputsToAdd.Add((parameter.Outputs[i], finalList.ToArray()));
            }

            for (int i = 0; i < parameter.Parameters.Count; i++)
            {

                var childParameter = parameter.Parameters[i];
                accessIndex.Add(i);
                SearchForSubInputsOutputs(childParameter, accessIndex);
                accessIndex.RemoveAt(accessIndex.Count - 1);
            }

        }


        private void AppendPorts()
        {
            this.inputContainer.Clear();
            foreach (var inPort in inPorts)
            {
                this.inputContainer.Add(inPort);
            }

            this.outputContainer.Clear();
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
            return inPorts ? this.inPorts.Where(x => x.connected).Count() : this.outPorts.Where(x => x.connected).Count();
        }
    }
}
