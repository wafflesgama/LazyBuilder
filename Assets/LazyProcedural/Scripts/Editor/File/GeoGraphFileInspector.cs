using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LazyProcedural
{
    //[CustomEditor(typeof(GeoGraphImporter), true)]

    public class GeoGraphFileInspector : Editor
    {
        //private bool isCompatible => Path.HasExtension(".lua");
        public override void OnInspectorGUI()
        {
            //if (isCompatible)
            //{
            OnGUI();
            //    return;
            //}
            //base.OnInspectorGUI();
        }



        void OnGUI()
        {
            //EditorGUI.TextArea(Rect.zero, "Ssdadasds asdasd");
            //EditorGUIUtility.set(gameObject, (Texture2D)iconContent.image);
            //GUILayout.be
            if (
            GUILayout.Button("Open Graph"))
            {
                WindowManager.ShowGraph();
            }
            //GUILayout.Button("See Generated Code");

            //base.OnInspectorGUI();
        }
    }
}

