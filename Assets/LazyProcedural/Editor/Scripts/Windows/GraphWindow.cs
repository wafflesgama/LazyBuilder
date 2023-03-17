using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace LazyProcedural
{
    public class GraphWindow : EditorWindow
    {
        Graph graph;

        private void OnEnable()
        {
            graph = new Graph();

            graph.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
            graph.style.height = new StyleLength(new Length(100, LengthUnit.Percent));
            rootVisualElement.Add(graph);
        }
    }
}
