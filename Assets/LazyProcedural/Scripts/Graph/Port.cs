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

namespace LazyProcedural
{
    public class Port : UnityEditor.Experimental.GraphView.Port
    {
        const Orientation DEF_ORIENTATION = Orientation.Horizontal;

        //If Port can be accessed through Inputs/Outputs or through Parameter's Inputs/Outputs
        public bool isDirectAccessPort;

        //The index array to access through Parameter's Inputs/Outputs
        public int[] accessIndex;

        public SceelixData.Output outputData;
        public SceelixData.Input inputData;

        //public static Port Create(bool inPort, bool singleEntry, Type type)
        //{
        //    return UnityEditor.Experimental.GraphView.Port.Create<Edge>(DEF_ORIENTATION, inPort ? Direction.Input : Direction.Output, singleEntry ? Capacity.Single : Capacity.Multi, type);
        //}
        public Port(SceelixData.Output outputData, bool directAccess, int[] accessIndex) : base(DEF_ORIENTATION, Direction.Output, Capacity.Multi, outputData.EntityType)
        {
            //title = outputData.Description;
            this.outputData = outputData;
            this.isDirectAccessPort = directAccess;
            this.accessIndex = accessIndex;
            SetupUIPort();
            SetupLabel();
        }

        public Port(SceelixData.Input inputData, bool directAccess, int[] accessIndex) : base(DEF_ORIENTATION, Direction.Input, inputData.InputNature == SceelixData.InputNature.Single ? Capacity.Single : Capacity.Multi, inputData.EntityType)
        {
            //title = inputData.Description;
            this.inputData = inputData;
            this.isDirectAccessPort = directAccess;
            this.accessIndex = accessIndex;
            SetupUIPort();
            SetupLabel();

        }
        //public Port(bool inPort, bool singleEntry, Type type) : base(DEF_ORIENTATION, inPort ? Direction.Input : Direction.Output, singleEntry ? Capacity.Single : Capacity.Multi, type)
        //{


        //}

        private void SetupUIPort()
        {
            EdgeConnectorListener listener = new EdgeConnectorListener();
            m_EdgeConnector = new EdgeConnector<Edge>(listener);
            this.AddManipulator(m_EdgeConnector);
        }

        private void SetupLabel()
        {
            var attribute = this.portType.GetCustomAttribute<EntityAttribute>();
            var portType = attribute != null ? attribute.Name : this.portType.Name;
            this.portName = portType;
        }


        //override on

        //protected Port(Direction portDirection, Capacity portCapacity, Type type) : base(DEF_ORIENTATION, portDirection, portCapacity, type)
        //{

        //}


    }
}
