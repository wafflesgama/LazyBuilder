using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using static UnityEditor.Experimental.GraphView.Port;
using UnityEngine;
using UnityEngine.UIElements;

using SceelixData = Sceelix.Core.IO;
using System.Reflection;
using Sceelix.Core.Annotations;
using Sceelix.Core.Data;
using System.Linq;

namespace LazyProcedural
{
    public class Port : UnityEditor.Experimental.GraphView.Port
    {
        public const string GLOBAL_PARAM_PORTNAME = "Global Parameters";

        const Orientation DEF_ORIENTATION = Orientation.Horizontal;
        public string id;

        //If Port can be accessed through Inputs/Outputs or through Parameter's Inputs/Outputs
        public bool isDirectAccessPort { get; private set; }

        public bool isMuted { get; private set; } = false;


        //The index array to access through Parameter's Inputs/Outputs
        public int[] accessIndex;

        public SceelixData.Output outputData;
        public SceelixData.Input inputData;

        private ContextualMenuManipulator contextMenu;

        //public static Port Create(bool inPort, bool singleEntry, Type type)
        //{
        //    return UnityEditor.Experimental.GraphView.Port.Create<Edge>(DEF_ORIENTATION, inPort ? Direction.Input : Direction.Output, singleEntry ? Capacity.Single : Capacity.Multi, type);
        //}
        public Port(SceelixData.Output outputData, bool directAccess, int[] accessIndex) : base(DEF_ORIENTATION, Direction.Output, Capacity.Multi, outputData.EntityType)
        {
            id = Guid.NewGuid().ToString();
            //title = outputData.Description;
            this.outputData = outputData;
            this.isDirectAccessPort = directAccess;
            this.accessIndex = accessIndex;
            SetupUIPort();
            SetupLabel();
        }

        public Port(SceelixData.Input inputData, bool directAccess, int[] accessIndex) : base(DEF_ORIENTATION, Direction.Input, Capacity.Multi, inputData.EntityType)
        {
            id = Guid.NewGuid().ToString();
            //title = inputData.Description;
            this.inputData = inputData;
            this.isDirectAccessPort = directAccess;
            this.accessIndex = accessIndex;
            SetupUIPort();
            SetupLabel();

        }


        public bool IsTerminalPort()
        {
            if (direction == Direction.Input) return false;

            return !connections.Where(x => (x.input as Port).isMuted == false).Any() && !isMuted;
            //return direction== Direction.Input ? (x => x.connected && !x.isMuted).Count() : this.outPorts.Where(x => x.connected && !x.isMuted).Count();
        }

        public void ClearConnectionNumbers()
        {
            foreach (var connection in connections)
            {
                Edge castedConnection = connection as Edge;
                castedConnection.ClearNumbers();
            }
        }

        private void SetupUIPort()
        {
            EdgeConnectorListener listener = new EdgeConnectorListener();
            m_EdgeConnector = new EdgeConnector<Edge>(listener);
            this.AddManipulator(m_EdgeConnector);
            UpdatePortShape();
            UpdatePortColor();

            contentContainer.style.marginBottom = 5;
            contentContainer.style.alignItems = Align.FlexStart;

            contentContainer.Q("connector").style.marginTop = 3;

            UpdateContextMenu();

        }



        private void SetupLabel()
        {
            var attribute = this.portType.GetCustomAttribute<EntityAttribute>();
            var portLabel = outputData != null ? outputData.Label : inputData.Label;

            var portType = attribute != null ? attribute.Name : this.portType.Name;
            //this.portName = $"{portLabel} ({portType})";
            this.portName = portLabel;

            var portNameLabel = contentContainer.Q("type");



            Label portTypeLabel = new Label();
            portTypeLabel.text = $"({portType})";
            portTypeLabel.style.fontSize = 8;
            portTypeLabel.style.marginTop = -2;
            portTypeLabel.style.paddingRight = 3;
            portTypeLabel.style.paddingLeft = 3;
            portTypeLabel.style.color = new Color(0.56f, 0.56f, 0.56f);
            portTypeLabel.style.width = Length.Percent(100);

            var labelsContainer = new VisualElement();

            if (outputData != null)
            {
                labelsContainer.style.alignContent = Align.FlexEnd;
                portTypeLabel.style.unityTextAlign = TextAnchor.MiddleRight;
            }



            //Interactor to Tackle Unity issue of port not selectable via the connector (only on input ports)
            if (this.direction == Direction.Input)
            {
                var interactor = new VisualElement();
                interactor.name = "interactor";
                labelsContainer.Add(interactor);
            }

            labelsContainer.Add(portNameLabel);
            labelsContainer.Add(portTypeLabel);

            contentContainer.Add(labelsContainer);


        }

        private void UpdatePortShape()
        {
            if (direction == Direction.Output || inputData.InputNature == SceelixData.InputNature.Single) return;

            var connector = this.contentContainer.Q("connector");

            connector.style.borderBottomLeftRadius = 1;
            connector.style.borderBottomRightRadius = 1;
            connector.style.borderTopLeftRadius = 1;
            connector.style.borderTopRightRadius = 1;
        }
        public void UpdatePortColor(bool disabled = false)
        {
            Color color = Color.white;

            if (disabled)
                color = new Color(0.65f, 0.65f, 0.65f);
            else if (isMuted)
                color = new Color(0.3f, 0.3f, 0.3f);
            else
            if (portType == typeof(Sceelix.Meshes.Data.MeshEntity))
                color = new Color(1f, 0.52f, 0.52f);  //Redish pink
            else
            if (typeof(Sceelix.Actors.Data.IActor).IsAssignableFrom(portType))
                color = new Color(1f, 0.72f, 0.52f);  // Orange
            else
            if (typeof(IEntity).IsAssignableFrom(portType))
                color = new Color(1f, 0.87f, 0.52f);  //Yellow

            portColor = color;
        }

        private void MutePort()
        {
            isMuted = true;
            UpdatePortColor();
            ((Node)this.node).graph.GraphStructureChanged();

            UpdateContextMenu();
        }

        private void UnmutePort()
        {
            isMuted = false;
            UpdatePortColor();
            ((Node)this.node).graph.GraphStructureChanged();

            UpdateContextMenu();
        }

        private void UpdateContextMenu()
        {
            if (contextMenu != null)
                this.RemoveManipulator(contextMenu);

            contextMenu = new ContextualMenuManipulator((ContextualMenuPopulateEvent evt) =>
            {
                if (isMuted)
                    evt.menu.AppendAction("UnMute", (x) => UnmutePort());
                else
                    evt.menu.AppendAction("Mute", (x) => MutePort());
            });
            this.AddManipulator(contextMenu);

        }



        //override on

        //protected Port(Direction portDirection, Capacity portCapacity, Type type) : base(DEF_ORIENTATION, portDirection, portCapacity, type)
        //{

        //}


    }
}
