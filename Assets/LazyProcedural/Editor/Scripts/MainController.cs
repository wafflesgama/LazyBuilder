using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LazyProcedural
{
    public class MainController : EditorWindow
    {
        public static SceneView sceneWindow { get; private set; }

        [MenuItem("Tools/Lazy Procedural/Graph #g")]
        private static void GraphWindow()
        {
            if (Application.isPlaying) return;

            GetSceneWindow();
            var window = GetWindow<GraphWindow>();
            window.titleContent = new GUIContent("Lazy Procedural Graph");
            window.Show();
        }

        private static void GetSceneWindow()
        {
            if (sceneWindow != null) return;

            sceneWindow = GetWindow<SceneView>();
        }

    }
}
