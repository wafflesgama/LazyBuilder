using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazyProcedural
{

    public class Node : UnityEditor.Experimental.GraphView.Node
    {
        public string GUID;

        public override void OnUnselected()
        {
            Debug.Log("node unselected");
            base.OnUnselected();
        }
        public override void OnSelected()
        {
            Debug.Log("node selected");
            base.OnSelected();
        }
    }
}
