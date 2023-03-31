using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor;

namespace LazyProcedural
{

    public class Node : UnityEditor.Experimental.GraphView.Node
    {
        public GUID guid;

        public UnityEditor.Experimental.GraphView.Port[] inPorts = new UnityEditor.Experimental.GraphView.Port[0];
        public UnityEditor.Experimental.GraphView.Port[] outPorts = new UnityEditor.Experimental.GraphView.Port[0];

        public Node(string title)
        {
            this.title = title;

            guid = GUID.Generate();


        }

        public void Build()
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
            base.OnSelected();
        }
    }
}
