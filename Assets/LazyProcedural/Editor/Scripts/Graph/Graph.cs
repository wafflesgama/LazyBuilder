using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;


namespace LazyProcedural
{
    public class Graph : GraphView
    {

        public Graph()
        {
            //Must be this order to properly work
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());


            this.AddElement(CreateTestNode());
        }

        private Node CreateTestNode()
        {
            var node = new Node
            {
                GUID = GUID.Generate().ToString(),
                title = "Teste Node"
            };

            node.SetPosition(new Rect(10, 10, 200, 200));

            return node;
        }

    }
}
