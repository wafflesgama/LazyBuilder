using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GraphComponentFinder : Editor
{

    public static GeoGraphComponent FindComponent()
    {
        GeoGraphComponent geoGraphComponent = null;
        if (Selection.activeGameObject != null)
        {
            geoGraphComponent = Selection.activeGameObject.GetComponent<GeoGraphComponent>();
        }

        if (geoGraphComponent == null || geoGraphComponent == null)
        {
            geoGraphComponent = GameObject.FindObjectOfType<GeoGraphComponent>();
        }


        if(geoGraphComponent == null)
        {
            Debug.LogError("No Graph Component Found in Scene");
        }

        return geoGraphComponent;
    }
}
