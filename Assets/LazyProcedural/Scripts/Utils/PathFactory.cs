using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace LazyProcedural
{
    public class PathFactory
    {
        public static string absoluteToolPath { get; set; }
        public static string relativeToolPath { get; set; }

        public const string UI_PATH = "UI";
        public const string IMAGES_PATH = "Images";
        public const string MESHES_PATH = "Meshes";

        public const string SCEELIX_PATH = "Libs/Sceelix";

        public const string IMAGE_TYPE = "png";

        public const string LAYOUT_TYPE = "uxml";
        public const string STYLE_TYPE = "uss";

        //UI Layouts
        public const string GRAPH_WINDOW_LAYOUT_FILE = "GraphWindow";
        public const string SEARCH_WINDOW_LAYOUT_FILE = "SearchWindow";
        public const string CONTEXT_WINDOW_LAYOUT_FILE = "ContextWindow";

        public const string ICON_FULL_FILE = "Icon_F";
        public const string ICON_SMALL_FILE = "Icon_S";

        public const string DIALOGUE_LAYOUT_FILE = "DialogueLayout";

        public const string GRAPH_TYPE = "geograph";
        public const string MATERIAL_TYPE = "mat";


        public const string MARKDOWN_FILE = "AssetList";

        public const string BASE_FOLDER = "LazyProcedural";

        public static void Init()
        {
            var result = FindLazyProceduralPath(Application.dataPath);

            absoluteToolPath = result.Item1.AbsoluteFormat();
            relativeToolPath = result.Item2.RelativeFormat();
        }

        private static (string, string) FindLazyProceduralPath(string startPath)
        {
            var folderPath = Directory.GetDirectories(startPath, BASE_FOLDER, SearchOption.AllDirectories)
                                        .FirstOrDefault();
            if (folderPath != null)
            {
                var absolutePath = folderPath;
                //Get Parent Folder to include Assets folder in relative path
                var relativePath = GetRelativePath(Path.GetDirectoryName(startPath), folderPath);
                return (absolutePath, relativePath);
            }
            else
            {
                Debug.LogError("Tool folder not found");
                return (null, null);
            }
        }

        private static string GetRelativePath(string startPath, string endPath)
        {
            var startUri = new Uri(startPath + Path.DirectorySeparatorChar);
            var endUri = new Uri(endPath);
            var relativeUri = startUri.MakeRelativeUri(endUri);
            return Uri.UnescapeDataString(relativeUri.ToString()).Replace('/', Path.DirectorySeparatorChar);
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
