using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor.Experimental.GraphView;
using UnityGraph = UnityEditor.Experimental.GraphView;
using UnityEditor;
using Sceelix.Core.Procedures;
using System.Linq;
using Sceelix.Core.Parameters;
using System;

namespace LazyProcedural
{
    public delegate void NodeEvent(Node node);

    public class ChangedParameterInfo
    {
        public bool isExpression = false;
        public object value;
    }
    public class Node : UnityGraph.Node
    {
        public GUID guid { get; private set; }

        public List<Port> inPorts { get; private set; } = new List<Port>();
        public List<Port> outPorts { get; private set; } = new List<Port>();

        public Procedure nodeData { get; private set; }
        public Dictionary<int[], ChangedParameterInfo> changedDataParams { get; private set; }

        public event NodeEvent OnNodeSelected;

        private List<(Sceelix.Core.IO.InputReference, int[])> inputsToAdd;
        private List<(Sceelix.Core.IO.OutputReference, int[])> outputsToAdd;


        public Node(ProcedureInfo nodeInfo, Procedure nodeData)
        {

            title = nodeInfo.Label;
            tooltip = nodeInfo.Label;

            changedDataParams = new Dictionary<int[], ChangedParameterInfo>();
            this.nodeData = nodeData;

            RefreshNode();
        }

        public Node(Node sourceNode, bool linkedCopy = false)
        {
            title = sourceNode.title;
            tooltip = sourceNode.tooltip;

            changedDataParams = new Dictionary<int[], ChangedParameterInfo>();

            if (linkedCopy)
                nodeData = sourceNode.nodeData;
            else
            {
                nodeData = (Procedure)Activator.CreateInstance(sourceNode.nodeData.GetType());
                foreach (var changeParam in sourceNode.changedDataParams)
                {
                    var param = GetParameterFromAcessingIndex(changeParam.Key.ToList(), nodeData);

                    if (changeParam.Value.isExpression)
                        param.SetExpression((string)changeParam.Value.value);
                    else
                        param.Set(changeParam.Value.value);

                    ChangedDataParameter(changeParam.Key, changeParam.Value);
                }
            }

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

        public void ChangedDataParameter(IEnumerable<int> accessingIndex, ChangedParameterInfo changedParameterInfo)
        {
            int[] key = accessingIndex.ToArray();

            if (!changedDataParams.ContainsKey(key))
                changedDataParams.Add(key, null);

            changedDataParams[key] = changedParameterInfo;
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


        private ParameterReference GetParameterFromAcessingIndex(List<int> acessingIndex, Procedure procedure)
        {
            if (acessingIndex == null || acessingIndex.Count == 0) return null;

            ParameterReference reference = procedure.Parameters[acessingIndex[0]];
            bool firstElement = true;
            foreach (var index in acessingIndex)
            {
                if (firstElement)
                {
                    firstElement = false;
                    continue;
                }

                reference = reference.Parameters[index];
            }

            return reference;
        }
    }
}
