using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityGraph = UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace LazyProcedural
{
    public class GlobalParametersWindow : UnityGraph.Blackboard
    {
        private VisualElement _root;

        private GraphWindow _graphWindow;

        private Dictionary<string, BlackboardField> _fields = new Dictionary<string, BlackboardField>();


        private EventCallback<ChangeEvent<string>> currentNameChangeCallback;

        private int currentCrearionIndex = 0;

        private Label _typeLabel;
        private TextField _nameField;


        private ScrollView _parametersContainer;
        public GlobalParametersWindow(GraphWindow graphWindow, Graph graph) : base(graph)
        {
            _graphWindow = graphWindow;
            Setup();
        }

        private void Setup()
        {
            SetupBaseUI();
            StripDefaultElements();
            SetupBindings();
            SetupCallbacks();
        }

        private void SetupBaseUI()
        {
            _root = contentContainer;


            if (_root == null) return;

            subTitle = "";
            title = "Global Parameters";



            //// Loads and clones our VisualTree (eg. our UXML structure) inside the root.
            //var visualTree = (VisualTreeAsset)AssetDatabase.LoadAssetAtPath(PathFactory.BuildUiFilePath(PathFactory.CONTEXT_WINDOW_LAYOUT_FILE), typeof(VisualTreeAsset));
            //visualTree.CloneTree(_root);

            //var styleSheet = (StyleSheet)AssetDatabase.LoadAssetAtPath(PathFactory.BuildUiFilePath(PathFactory.CONTEXT_WINDOW_LAYOUT_FILE, false), typeof(StyleSheet));
            //this.styleSheets.Add(styleSheet);
        }

        private void SetupBindings()
        {
            //_parametersContainer = (ScrollView)_root.Q("Params");
            //_nameField = (TextField)_root.Q("NameField");
            //_typeLabel = (Label)_root.Q("TypeLabel");
        }

        private void SetupCallbacks()
        {
            this.addItemRequested = AddGlobalParameter;
        }


        private void StripDefaultElements()
        {
            //_root.Q("addButton").visible = false;
        }


        public void AddGlobalParameter(Blackboard b)
        {
            currentCrearionIndex++;

            string varDefaultName = $"Global Param {currentCrearionIndex}";

            var field = new BlackboardField { text = varDefaultName, typeText = "Type test" };

            _fields.Add(varDefaultName, field);
            this.Add(field);
        }
    }
}
