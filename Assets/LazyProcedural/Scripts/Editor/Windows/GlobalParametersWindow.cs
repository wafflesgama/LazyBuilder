using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityGraph = UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Linq;
using UnityEditor.UIElements;

namespace LazyProcedural
{
    public class GlobalParametersWindow : UnityGraph.Blackboard
    {
        public Dictionary<string, object> Parameters = new Dictionary<string, object>();


        private GraphWindow _graphWindow;

        private VisualElement _root;

        private Dictionary<string, BlackboardField> _fields = new Dictionary<string, BlackboardField>();
        private Dictionary<BlackboardField, VisualElement> _fieldsContainers = new Dictionary<BlackboardField, VisualElement>();

        private PopupField<string> _addField;

        public GlobalParametersWindow(GraphWindow graphWindow, Graph graph, IEnumerable<(string, object)> globalParams) : base(graph)
        {
            _graphWindow = graphWindow;

            foreach (var param in globalParams)
            {
                CreateGlobalParameter(param.Item2.GetType().Name, param.Item1, param.Item2);
            }

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
            //this.addItemRequested = AddGlobalParameter;
            this.editTextRequested = EditGlobalParameter;

            //this.
        }



        private void StripDefaultElements()
        {
            //_root.Q("addButton").visible = false;
            this.Q("addButton").visible = false;


            var header = this.Q("header");

            _addField = new PopupField<string>();
            _addField.value = "+";

            //Center the "+" in the Dropdown
            var subElelment = _addField.Q(null, "unity-base-field__input");
            //subElelment.style.paddingLeft = 19;
            subElelment.style.fontSize = 16;
            subElelment.style.minWidth = 36;
            subElelment.style.maxWidth = 36;


            var labelElement = _addField.Q(null, "unity-text-element");
            labelElement.style.unityTextAlign = TextAnchor.MiddleCenter;
            labelElement.style.flexGrow = 1;

            //Remove Dropdown caret
            subElelment.RemoveAt(1);


            var choices = new List<string>();

            choices.Add(typeof(string).Name);
            choices.Add(typeof(int).Name);
            choices.Add(typeof(float).Name);
            choices.Add(typeof(bool).Name);
            choices.Add(typeof(Vector2).Name);
            choices.Add(typeof(Vector3).Name);
            choices.Add(typeof(Vector4).Name);
            choices.Add(typeof(Color).Name);

            _addField.choices = choices;

            _addField.RegisterValueChangedCallback((x) =>
            {
                if (x.newValue == "+") return;

                CreateGlobalParameter(x.newValue);
                _addField.value = "+";
            });

            header.Add(_addField);

        }


        public void CreateGlobalParameter(string paramType, string name = null, object value = null)
        {

            if (name == null)
            {
                for (int i = 0; i < int.MaxValue; i++)
                {
                    name = $"Global Param ({i})";

                    if (!_fields.ContainsKey(name)) break;

                }
            }

            VisualElement container = new VisualElement();

            var field = new BlackboardField { text = name, typeText = paramType, name= name };
            container.Add(field);

            VisualElement subField;
            object paramValue;
            (subField, paramValue) = CreateField(name, paramType, field, value);

            BlackboardRow row = new BlackboardRow(field, subField);

            field.RegisterCallback<DetachFromPanelEvent>(RemovedGlobalParameter);
            container.Add(row);

            this.Add(container);

            _fields.Add(name, field);
            _fieldsContainers.Add(field, container);

            Parameters.Add(name, paramValue);

            //_graphWindow.OnGraphGlobalParamUpdated();
        }

        private void RemovedGlobalParameter(DetachFromPanelEvent evnt)
        {
            var field = evnt.target as BlackboardField;

            Parameters.Remove(field.name);

            var container = _fieldsContainers[field];

            if (this.Contains(container))
                this.Remove(container);

            _graphWindow.OnGraphGlobalParamUpdated();
        }

        private (VisualElement, object) CreateField(string paramName, string paramType, BlackboardField parentField, object value)
        {
            VisualElement field;
            string defaultLabel = "Value:";

            if (paramType == typeof(int).Name)
            {
                IntegerField integerField = new IntegerField(defaultLabel);
                if (value == null)
                    value = 0;
                integerField.value = (int)value;

                integerField.RegisterValueChangedCallback(x => EditParameterValue(parentField, x.newValue));
                field = integerField;
            }
            else if (paramType == typeof(float).Name)
            {
                FloatField floatFtield = new FloatField(defaultLabel);
                if (value == null)
                    value = 0f;
                floatFtield.value = (float)value;

                floatFtield.RegisterValueChangedCallback(x => EditParameterValue(parentField, x.newValue));
                field = floatFtield;
            }
            else if (paramType == typeof(bool).Name)
            {
                Toggle toggleField = new Toggle(defaultLabel);
                if (value == null)
                    value = false;
                toggleField.value = (bool)value;

                toggleField.RegisterValueChangedCallback(x => EditParameterValue(parentField, x.newValue));
                field = toggleField;
            }
            else if (paramType == typeof(Vector2).Name)
            {
                Vector2Field vector2Field = new Vector2Field(defaultLabel);
                if (value == null)
                    value = Vector2.zero;
                vector2Field.value = (Vector2)value;

                vector2Field.RegisterValueChangedCallback(x => EditParameterValue(parentField, x.newValue));
                field = vector2Field;
            }
            else if (paramType == typeof(Vector3).Name)
            {
                Vector3Field vector3Field = new Vector3Field(defaultLabel);
                if (value == null)
                    value = Vector3.zero;
                vector3Field.value = (Vector3)value;

                vector3Field.RegisterValueChangedCallback(x => EditParameterValue(parentField, x.newValue));
                field = vector3Field;
            }
            else if (paramType == typeof(Vector4).Name)
            {
                Vector4Field vector4Field = new Vector4Field(defaultLabel);
                if (value == null)
                    value = Vector4.zero;
                vector4Field.value = (Vector4)value;

                vector4Field.RegisterValueChangedCallback(x => EditParameterValue(parentField, x.newValue));
                field = vector4Field;
            }
            else if (paramType == typeof(Color).Name)
            {
                ColorField colorField = new ColorField(defaultLabel);
                if (value == null)
                    value = Color.white;
                colorField.value = (Color)value;

                colorField.RegisterValueChangedCallback(x => EditParameterValue(parentField, x.newValue));
                field = colorField;
            }
            else
            {
                TextField textField = new TextField(defaultLabel);
                if (value == null)
                    value = "";
                textField.value = value.ToString();

                textField.RegisterValueChangedCallback(x => EditParameterValue(parentField, x.newValue));
                field = textField;
            }

          

            field.name = paramName;

            return (field, value);
        }

        public void EditGlobalParameter(Blackboard b, VisualElement field, string parameterName)
        {
            if (_fields.ContainsKey(parameterName))
            {
                _graphWindow.LogMessage("A parameter with that name already exists", 2);
                return;
            }

            BlackboardField blackboardField = (BlackboardField)field;
            string ogKey = blackboardField.text;

            _fields.Remove(ogKey);
            _fields.Add(parameterName, blackboardField);

            object paramValue = Parameters[ogKey];
            Parameters.Remove(ogKey);
            Parameters.Add(parameterName, paramValue);

            blackboardField.text = parameterName;
            blackboardField.name = parameterName;

            _graphWindow.OnGraphGlobalParamUpdated();
        }

        public void EditParameterValue(BlackboardField blackboardField, object value)
        {
            Parameters[blackboardField.name] = value;
            _graphWindow.OnGraphGlobalParamUpdated();
        }
    }
}
