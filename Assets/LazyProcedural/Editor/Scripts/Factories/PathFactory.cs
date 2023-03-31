using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace LazyProcedural
{
    public class PathFactory
    {
        public static string absoluteToolPath { get; set; }
        public static string relativeToolPath { get; set; }

        public const string UI_PATH = "Editor/UI";
        public const string IMAGES_PATH = "Editor/Images";
        public const string MESHES_PATH = "Editor/Meshes";

        public const string IMAGE_TYPE = "png";

        public const string LAYOUT_TYPE = "uxml";
        public const string STYLE_TYPE = "uss";

        //UI Layouts
        public const string GRAPH_WINDOW_LAYOUT_FILE = "GraphWindow";

        public const string ICON_FULL_FILE = "Icon_F";
        public const string ICON_SMALL_FILE = "Icon_S";

        public const string DIALOGUE_LAYOUT_FILE = "DialogueLayout";

        public const string GRAPH_TYPE = "geograph";
        public const string MATERIAL_TYPE = "mat";


        public const string MARKDOWN_FILE = "AssetList";



        public static void Init(ScriptableObject scriptSource)
        {
            var currentPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(scriptSource)));
            relativeToolPath = Path.GetDirectoryName(Path.GetDirectoryName(currentPath)).RelativeFormat();
            //relativeToolPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(currentPath))).RelativeFormat();

            absoluteToolPath = $"{Path.GetDirectoryName(Application.dataPath)}\\{relativeToolPath}";
        }

        #region Local Paths

        public static string BuildUiFilePath(string fileName, bool layoutFile = true, bool absolute = false)
        {
            var rootPath = absolute ? absoluteToolPath : relativeToolPath;
            var fileType = layoutFile ? LAYOUT_TYPE : STYLE_TYPE;
            var path = $"{rootPath}/{UI_PATH}/{fileName}.{fileType}";

            if (absolute)
                path = path.AbsoluteFormat();

            return path;
        }


        public static string BuildImageFilePath(string fileName, bool absolute = false)
        {
            var rootPath = absolute ? absoluteToolPath : relativeToolPath;
            var path = $"{rootPath}/{IMAGES_PATH}/{fileName}.{IMAGE_TYPE}";

            if (absolute)
                path = path.AbsoluteFormat();

            return path;
        }

        #endregion Local Paths



    }
}
