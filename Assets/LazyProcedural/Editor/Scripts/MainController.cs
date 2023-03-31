using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace LazyProcedural
{
    public class MainController : EditorWindow
    {

        public static SceneView sceneWindow { get; private set; }

        public static void Init()
        {
            //Empty window to assess where script is located
            ScriptableObject scriptSource = new MainController();
            PathFactory.Init(scriptSource);
        }


        //Handles correct behaviour when double-clicking a .graph asset assigned to a field
        [OnOpenAsset]
        public static bool OnOpenGraph(int instanceID, int line)
        {
            Object target = EditorUtility.InstanceIDToObject(instanceID);

            var path = AssetDatabase.GetAssetPath(instanceID);

            if (Path.GetExtension(path) != $".{PathFactory.GRAPH_TYPE}") return false;

            Selection.activeObject = target;

            string graphName = Path.GetFileNameWithoutExtension(path);
            GraphWindow(graphName);
            return true;
        }

        public void CreateGraph()
        {

        }

        private static void GraphWindow(string graphName = "")
        {
            if (Application.isPlaying) return;

            GetSceneWindow();
            var window = GetWindow<GraphWindow>();

            Texture2D icon = Utils.FetchImage(PathFactory.BuildImageFilePath(PathFactory.ICON_SMALL_FILE, true));

            window.titleContent = new GUIContent(graphName == "" ? "New Geo Graph" : graphName, icon);
            window.Show();
        }

        private static void GetSceneWindow()
        {
            if (sceneWindow != null) return;

            sceneWindow = GetWindow<SceneView>();
        }

        private void OnInspectorUpdate()
        {

        }

    }

   
}
