using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using static UnityEditor.Experimental.GraphView.Port;
using Sceelix.Core.Procedures;
using System;

namespace LazyProcedural
{
    public class ObjectFactory
    {
        //static int counter = 0;
        //public static Node InstantiateTestNode()
        //{
        //    counter++;
        //    var node = new TestNode();
        //    node.Build();
        //    node.SetPosition(new Rect(10, 10, 200, 200));
        //    return node;
        //}


        public static Node CreateNode(Type procedureType)
        {

            Procedure procedure= (Procedure)Activator.CreateInstance(procedureType);
            Node node = new Node(procedure);

            return node;
        }

        //public class TestNode : Node
        //{
        //    const string TITLE = "Test Node ";
        //    public TestNode() : base(TITLE+counter)
        //    {

        //        inPorts = new Port[] {
        //                    new Port(true,true,typeof(string)) ,
        //                    new Port(true,true,typeof(int)) ,
        //                    new Port(true,true,typeof(string))
        //                };

        //        outPorts = new Port[] {
        //                    new Port(false,true,typeof(string)) ,
        //                    new Port(false,true,typeof(int)) ,
        //                    new Port(false,true,typeof(string))
        //                };

        //    }
        //}
    }
}
