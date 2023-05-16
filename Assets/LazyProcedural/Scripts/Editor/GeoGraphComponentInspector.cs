using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LazyProcedural
{
    [CustomEditor(typeof(GeoGraphComponent))]
    public class GeoGraphComponentInspector : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement myInspector = new VisualElement();

            myInspector.Add(new Label("This is a custom inspector"));

            ObjectField objectField = new ObjectField();

            objectField.label = "Asset Name";
            //objectField.objectType = typeof(GeoGraphAsset);
            myInspector.Add(objectField);

            return myInspector;
        }

        private void OnSceneGUI()
        {

        }
    }
}
