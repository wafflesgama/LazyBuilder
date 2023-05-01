using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace LazyProcedural
{
    public class WindowManager : EditorWindow
    {

        public static SceneView sceneWindow { get; private set; }

        [MenuItem("Tools/Lazy Procedural/Init #i")]
        public static void Init()
        {
            PathFactory.Init();
        }


        //Handles correct behaviour when double-clicking a .graph asset assigned to a field
        [OnOpenAsset]
        public static bool OnOpenGraph(int instanceID, int line)
        {
            UnityEngine.Object target = EditorUtility.InstanceIDToObject(instanceID);

            var path = AssetDatabase.GetAssetPath(instanceID);

            if (Path.GetExtension(path) != $".{PathFactory.GRAPH_TYPE}") return false;

            Selection.activeObject = target;

            string graphName = Path.GetFileNameWithoutExtension(path);

            ShowGraph();

            //var windows =UnityEditor.Experimental.GraphView.GraphViewEditorWindow.ShowGraphViewWindowWithTools<GraphWindow>();

            return true;
        }


        [MenuItem("Tools/Lazy Procedural/Test Graph #g")]
        public static void ShowGraph()
        {
            ShowGraph("Test");
        }
        public static void ShowGraph(string graphName)
        {
            GraphWindow(graphName);
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

    }


}
