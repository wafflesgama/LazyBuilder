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
using UnityEngine.UIElements;

namespace LazyProcedural
{
    public delegate void NodeEvent(Node node);

    public class ChangedParameterInfo
    {
        public int[] accessIndex;
        public bool isExpression = false;
        public object value;
    }
    public class CreatedParameterInfo
    {
        public int[] accessIndex;
        public string parameterName;
    }
    public class Node : UnityGraph.Node
    {
        public string id { get; private set; }
        public string typeTitle { get; private set; }

        public List<Port> inPorts { get; private set; } = new List<Port>();
        public List<Port> outPorts { get; private set; } = new List<Port>();

        public Procedure nodeData { get; private set; }
        public List<ChangedParameterInfo> changedDataParams { get; private set; }
        public List<CreatedParameterInfo> createdDataParams { get; private set; }

        public event NodeEvent OnNodeSelected;

        private List<(Sceelix.Core.IO.InputReference, int[])> inputsToAdd;
        private List<(Sceelix.Core.IO.OutputReference, int[])> outputsToAdd;



        //New Node Constrcutor
        public Node(ProcedureInfo nodeDataInfo)
        {
            id = GUID.Generate().ToString();

            typeTitle = nodeDataInfo.Label;
            title = nodeDataInfo.Label;
            tooltip = nodeDataInfo.Label;

            changedDataParams = new List<ChangedParameterInfo>();
            createdDataParams = new List<CreatedParameterInfo>();
            nodeData = (Procedure)Activator.CreateInstance(nodeDataInfo.Type);

            SetupExtraUI();
            RefreshNode();
        }



        //Copy Constructor
        public Node(Node sourceNode, bool linkedCopy = false)
        {
            id = GUID.Generate().ToString();

            typeTitle = sourceNode.typeTitle;
            title = sourceNode.title;
            tooltip = sourceNode.tooltip;

            changedDataParams = new List<ChangedParameterInfo>();
            createdDataParams = new List<CreatedParameterInfo>();

            if (linkedCopy)
                nodeData = sourceNode.nodeData;
            else
            {
                nodeData = (Procedure)Activator.CreateInstance(sourceNode.nodeData.GetType());


                foreach (var createdDataParam in sourceNode.createdDataParams)
                {
                    var param = GetParameterFromAcessingIndex(createdDataParam.accessIndex.ToList());
                    ListParameter listParam = (ListParameter)param.Parameter;
                    listParam.Add(createdDataParam.parameterName);
                }

                foreach (var changeParam in sourceNode.changedDataParams)
                {
                    var param = GetParameterFromAcessingIndex(changeParam.accessIndex.ToList());

                    if (changeParam.isExpression)
                        param.SetExpression((string)changeParam.value);
                    else
                        param.Set(changeParam.value);

                    ChangedDataParameter(changeParam);
                }
            }

            SetupExtraUI();
            RefreshNode();
        }

        //Load Constructor
        public Node(string id, string name, Type nodeDataType, Vector2 position, CreatedParameterInfo[] createdParams, ChangedParameterInfo[] changedParams)
        {
            this.id = id;
            ProcedureInfo nodeDataInfo = ProcedureInfoManager.GetProcedure(nodeDataType);

            typeTitle = nodeDataInfo.Label;
            title = name != null ? name : nodeDataInfo.Label;
            tooltip = nodeDataInfo.Label;

            this.SetPosition(new Rect(position, GetPosition().size));

            nodeData = (Procedure)Activator.CreateInstance(nodeDataType);

            changedDataParams = new List<ChangedParameterInfo>();
            createdDataParams = new List<CreatedParameterInfo>();


            foreach (var createdDataParam in createdParams)
            {
                var param = GetParameterFromAcessingIndex(createdDataParam.accessIndex.ToList());
                ListParameter listParam = (ListParameter)param.Parameter;
                listParam.Add(createdDataParam.parameterName);
            }

            foreach (var changeParam in changedParams)
            {
                var param = GetParameterFromAcessingIndex(changeParam.accessIndex.ToList());

                if (changeParam.isExpression)
                    param.SetExpression((string)changeParam.value);
                else
                    param.Set(changeParam.value);

                ChangedDataParameter(changeParam);
            }

            SetupExtraUI();
            RefreshNode();
        }

        public void RefreshNode()
        {
            nodeData.UpdateParameterPorts();
            RegisterPorts();
            AppendPorts();
        }




        private void SetupExtraUI()
        {
            var titleLabel = titleContainer.Q("title-label");

            Label subTitleLabel = new Label();
            subTitleLabel.text = typeTitle;
            subTitleLabel.style.paddingLeft = 9;
            subTitleLabel.style.fontSize = 8;
            subTitleLabel.style.marginTop = -6;
            subTitleLabel.style.color = new Color(0.56f, 0.56f, 0.56f);


            var titlesContainer = new VisualElement();
            titlesContainer.style.flexDirection = FlexDirection.Column;
            titlesContainer.Add(titleLabel);
            titlesContainer.Add(subTitleLabel);


            var buttonsContainer = titleContainer.Q("title-button-container");

            titleContainer.Add(titlesContainer);
            titleContainer.Add(buttonsContainer);
            //titleContainer.Add(colapseButton);
        }

        //public void AddCo

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
                //Do not add if it is duplicate
                if (inPorts.Any(x => x.inputData.Identifier == input.Item1.Identifier)) continue;
                var port = new Port(input.Item1.Input, false, input.Item2);
                inPorts.Add(port);
            }

            foreach (var output in outputsToAdd)
            {
                //Do not add if it is duplicate
                if (outPorts.Any(x => x.outputData.Identifier == output.Item1.Identifier)) continue;
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

        public void ChangedDataParameter(ChangedParameterInfo changedParameterInfo)
        {
            if (!changedDataParams.Any(x => x.accessIndex.SequenceEqual(changedParameterInfo.accessIndex)))
                changedDataParams.Add(new ChangedParameterInfo { accessIndex = changedParameterInfo.accessIndex });

            var param = changedDataParams.FirstOrDefault(x => x.accessIndex.SequenceEqual(changedParameterInfo.accessIndex));
            param.value = changedParameterInfo.value;
            param.isExpression = changedParameterInfo.isExpression;
        }

        public void CreatedDataParameter(CreatedParameterInfo createdParameterInfo)
        {
            createdDataParams.Add(createdParameterInfo);
        }

        public void RemoveCreatedDataParameter(CreatedParameterInfo createdToRemoveParameterInfo)
        {
            var paramsOfRemoved = changedDataParams.Where(x => createdToRemoveParameterInfo.accessIndex.Except(x.accessIndex).Count() <= 0).ToList();
            changedDataParams = changedDataParams.Except(paramsOfRemoved).ToList();

            var paramInfo = createdDataParams.FirstOrDefault(x => x.accessIndex.SequenceEqual(createdToRemoveParameterInfo.accessIndex) && createdToRemoveParameterInfo.parameterName == x.parameterName);
            createdDataParams.Remove(paramInfo);

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


        private ParameterReference GetParameterFromAcessingIndex(List<int> acessingIndex)
        {
            if (acessingIndex == null || acessingIndex.Count == 0) return null;

            ParameterReference reference = nodeData.Parameters[acessingIndex[0]];
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
