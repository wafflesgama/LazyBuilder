using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AssetImporters;
using System.IO;
using UnityEditor;

namespace LazyProcedural
{
    [CreateAssetMenuAttribute(fileName = "New Geograph." + PathFactory.GRAPH_TYPE, menuName = "Lazy Procedural/Empty Graph")]
    public class GraphAssetAttirbute_Empty : ScriptableObject
    {
        [SerializeField]
        private string TYPE = "Empty";
    }

    [CreateAssetMenuAttribute(fileName = "New Geograph." + PathFactory.GRAPH_TYPE, menuName = "Lazy Procedural/Example Graph")]
    public class GraphAssetAttirbute_Example : ScriptableObject
    {
        [SerializeField]
        private string TYPE = "Example";
    }


    /// <summary>
    /// A custom scripted importer that handles importing ".geograph" files as text assets.
    /// </summary>
    [ScriptedImporter(1, PathFactory.GRAPH_TYPE)]
    public class GeoGraphImporter : ScriptedImporter
    {
        [SerializeField] private TextAsset graph;

        public override void OnImportAsset(AssetImportContext ctx)
        {
            MainController.Init();

            string fileText = File.ReadAllText(ctx.assetPath);

            //Hardcoded to match the TYPE Variable name size (plus the " ")
            var type = fileText.Substring(fileText.IndexOf("TYPE:") + 5);

            //Remove paragraph characters
            type = type.Replace("\n", "");

            graph = new TextAsset(CreateDefaultGraph(type));

            Texture2D icon = Utils.FetchImage(PathFactory.BuildImageFilePath(PathFactory.ICON_FULL_FILE, true));
            ctx.AddObjectToAsset("Graph", graph, icon);
            ctx.SetMainObject(graph);
        }

        private string CreateDefaultGraph(string type)
        {
            switch (type)
            {
                case "Example":
                    break;
                default:
                    break;
            }
            return "";
        }

    }


}
