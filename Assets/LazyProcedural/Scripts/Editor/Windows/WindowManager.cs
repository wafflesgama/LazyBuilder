using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace LazyProcedural
{
    public class WindowManager : EditorWindow
    {
        public static SceneView sceneWindow { get; private set; }

        //Handles correct behaviour when double-clicking a Graph asset assigned to a field
        [OnOpenAsset]
        public static bool OnOpenGraph(int instanceID, int line)
        {
            UnityEngine.Object target = EditorUtility.InstanceIDToObject(instanceID);

            var path = AssetDatabase.GetAssetPath(instanceID);

            if (Path.GetExtension(path) != $".{PathFactory.GRAPH_TYPE}") return false;

            Selection.activeObject = target;

            OpenGraph(path);

            return true;
        }


        //[MenuItem("Tools/Lazy Procedural/Test Graph #g")]
        //public static void ShowGraph()
        //{
        //    OpenGraph("Test");
        //}


        public static void OpenGraph(string path)
        {
            OpenGraphWindow(path);
        }

        private static void OpenGraphWindow(string path)
        {
            var graphName = Path.GetFileNameWithoutExtension(path);
            //if (Application.isPlaying) return;

            //GetSceneWindow();

            GraphWindow graphWindow = new GraphWindow { filePath= path};
            //GraphWindow graphWindow = CreateWindow<GraphWindow>();
            //graphWindow.filePath = path;

            Texture2D icon = Utils.FetchImage(PathFactory.BuildImageFilePath(PathFactory.ICON_SMALL_FILE, true));

            graphWindow.titleContent = new GUIContent(graphName, icon);
            graphWindow.Show();
        }


        private static void GetSceneWindow()
        {
            if (sceneWindow != null) return;

            sceneWindow = GetWindow<SceneView>();
        }

    }


}
