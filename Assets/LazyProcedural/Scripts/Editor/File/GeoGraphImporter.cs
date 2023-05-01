using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AssetImporters;
using System.IO;
using UnityEditor;

namespace LazyProcedural
{
    //[CreateAssetMenuAttribute(fileName = "New Geograph." + PathFactory.GRAPH_TYPE, menuName = "Lazy Procedural/Empty Graph")]
    //public class GraphAssetAttirbute_Empty : GeoGraphAsset
    //{
    //    [SerializeField]
    //    private string TYPE = "Empty";

    //    public GraphAssetAttirbute_Empty(string data) : base(data)
    //    {
    //    }
    //}

    //[CreateAssetMenuAttribute(fileName = "New Geograph." + PathFactory.GRAPH_TYPE, menuName = "Lazy Procedural/Example Graph")]
    public class GraphAssetAttirbute_Example : Editor
    {
        //[SerializeField]
        //private string TYPE = "Example";


        [MenuItem("Assets/Create/Lazy Procedural/Example Graph", false, 1)]
        public static void CreateNewAsset()
        {
            GeoGraphAsset asset = new GeoGraphAsset("");
            ProjectWindowUtil.CreateAsset(asset, "Graph.geograph");
        }
    }


    [CreateAssetMenu]
    public class VehicleTypeInfo : ScriptableObject
    {
        // Class that represents a specific type of vehicle
        [Range(0.1f, 100f)]
        public float m_MaxSpeed = 0.1f;

        [Range(0.1f, 10f)]
        public float m_MaxAcceration = 0.1f;

        // This class could have many other vehicle parameters, such as Turning Radius, Range, Damage etc
    }


    ///// <summary>
    ///// A custom scripted importer that handles importing ".geograph" files as text assets.
    ///// </summary>
    //[ScriptedImporter(1, PathFactory.GRAPH_TYPE)]
    //public class GeoGraphImporter : ScriptedImporter
    //{
    //    [SerializeField] private GeoGraphDataAsset graph;

    //    public override void OnImportAsset(AssetImportContext ctx)
    //    {
    //        MainController.Init();

    //        string fileText = File.ReadAllText(ctx.assetPath);

    //        //Hardcoded to match the TYPE Variable name size (plus the " ")
    //        var type = fileText.Substring(fileText.IndexOf("TYPE:") + 5);

    //        //Remove paragraph characters
    //        type = type.Replace("\n", "");

    //        graph = new GeoGraphDataAsset(CreateDefaultGraph(type));

    //        Texture2D icon = Utils.FetchImage(PathFactory.BuildImageFilePath(PathFactory.ICON_FULL_FILE, true));
    //        ctx.AddObjectToAsset("Graph", graph, icon);
    //    }

    //    private string CreateDefaultGraph(string type)
    //    {
    //        switch (type)
    //        {
    //            case "Example":
    //                break;
    //            default:
    //                break;
    //        }
    //        return "";
    //    }

    //}

    public class GeoGraphAsset : ScriptableObject
    {
        
        public string test;

        public GeoGraphAsset(string data)
        {
            this.test = data;
        }
    }

    public class GeoGraphDataAsset : TextAsset
    {
        public string test;

        public GeoGraphDataAsset(string data)
        {
            this.test = data;
        }
    }


}
