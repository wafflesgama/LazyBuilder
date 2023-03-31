using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using static UnityEditor.Experimental.GraphView.Port;
using UnityEngine;
using UnityEngine.UIElements;


namespace LazyProcedural
{
    public class Port : UnityEditor.Experimental.GraphView.Port
    {
        const Orientation DEF_ORIENTATION = Orientation.Horizontal;


        //public static Port Create(bool inPort, bool singleEntry, Type type)
        //{
        //    return UnityEditor.Experimental.GraphView.Port.Create<Edge>(DEF_ORIENTATION, inPort ? Direction.Input : Direction.Output, singleEntry ? Capacity.Single : Capacity.Multi, type);
        //}
        public Port(bool inPort, bool singleEntry, Type type) : base(DEF_ORIENTATION, inPort ? Direction.Input : Direction.Output, singleEntry ? Capacity.Single : Capacity.Multi, type)
        {
            EdgeConnectorListener listener = new EdgeConnectorListener();
            m_EdgeConnector = new EdgeConnector<Edge>(listener);
            this.AddManipulator(m_EdgeConnector);

        }

        //protected Port(Direction portDirection, Capacity portCapacity, Type type) : base(DEF_ORIENTATION, portDirection, portCapacity, type)
        //{

        //}


    }
}
