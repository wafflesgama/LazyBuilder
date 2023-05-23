using LazyProcedural;
using Sceelix.Core.Parameters;
using Sceelix.Core.Parameters.Infos;
using Sceelix.Core.Procedures;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Reflection;
using UnityEditor.UIElements;
using Sceelix.Collections;
using System.Linq;
using Sceelix.Mathematics.Data;
using System;
using Sceelix.Core.Data;
using UnityGraph = UnityEditor.Experimental.GraphView;

public class ContextWindow : EditorWindow
{
    private VisualElement _root;

    private GraphWindow _graphWindow;


    private EventCallback<ChangeEvent<string>> currentNameChangeCallback;


    private Label _typeLabel;
    private TextField _nameField;


    private ScrollView _parametersContainer;

    private bool initialized = false;
    public ContextWindow(GraphWindow graphWindow)
    {
        _graphWindow = graphWindow;
    }

    private void OnFocus()
    {
        Setup();
    }

    private void Setup()
    {
        if (initialized) return;

        initialized = true;

        SetupBaseUI();
        StripDefaultElements();
        SetupBindings();
    }

    private void SetupBaseUI()
    {
        _root = rootVisualElement;


        if (_root == null) return;

        //subTitle = "";
        //title = "Context";

        // Loads and clones our VisualTree (eg. our UXML structure) inside the root.
        var visualTree = (VisualTreeAsset)AssetDatabase.LoadAssetAtPath(PathFactory.BuildUiFilePath(PathFactory.CONTEXT_WINDOW_LAYOUT_FILE), typeof(VisualTreeAsset));
        visualTree.CloneTree(_root);

        var styleSheet = (StyleSheet)AssetDatabase.LoadAssetAtPath(PathFactory.BuildUiFilePath(PathFactory.CONTEXT_WINDOW_LAYOUT_FILE, false), typeof(StyleSheet));
        _root.styleSheets.Add(styleSheet);

    }

    private void SetupBindings()
    {
        _parametersContainer = (ScrollView)_root.Q("Params");
        _nameField = (TextField)_root.Q("NameField");
        _typeLabel = (Label)_root.Q("TypeLabel");
    }

    private void StripDefaultElements()
    {
        //_root.Q("addButton").visible = false;
    }

    public void ResetNodeInfo()
    {
        _typeLabel.text = "";
        _nameField.value = "";

        if (currentNameChangeCallback != null)
            _nameField.UnregisterValueChangedCallback(currentNameChangeCallback);

        _parametersContainer.Clear();

    }
    public void BuildNodeInfo(Node node)
    {
        ResetNodeInfo();

        _typeLabel.text = node.typeTitle;
        _nameField.value = node.title;


        currentNameChangeCallback = x =>
         {
             node.title = x.newValue;
         };
        _nameField.RegisterValueChangedCallback(currentNameChangeCallback);




        //Build Paramters

        var accessingIndex = new List<int>();
        accessingIndex.Add(0);
        for (int i = 0; i < node.nodeData.Parameters.Count; i++)
        {
            accessingIndex[accessingIndex.Count - 1] = i;
            BuildNodeParameter(_parametersContainer, node.nodeData.Parameters[i], false, accessingIndex.ToList(), node);
        }
    }


    private void BuildNodeParameter(VisualElement container, ParameterReference parameter, bool descendedFromSelectList, List<int> accessingIndex, Node node)
    {
        Procedure procedure = node.nodeData;

        bool isSelectList = parameter.ParameterInfo.MetaType == "Select";
        bool isSingleCollectiveChoice = parameter.ParameterInfo.MetaType == "Choice";

        if (!parameter.ParameterInfo.IsPublic) return;

        if (parameter.Parameters.Count == 0)
        {
            var field = BuildField(parameter, accessingIndex, node);
            container.Add(field);
            return;
        }


        Foldout foldout = new Foldout();
        if (!descendedFromSelectList)
        {
            if (container != _root)
                foldout.style.marginLeft = 15;

            foldout.text = " ";
            //foldoutContainer.Add(foldout);


            var toggle = foldout.Q<Toggle>();
            var toggleChild = toggle.Q<VisualElement>();

            var toggleButton = toggleChild.Q<VisualElement>("unity-checkmark");
            toggleButton.style.marginTop = 3;


            //Add field in the Label element
            var toggleChildLabel = toggleChild.Q<Label>();


            var field2 = BuildField(parameter, accessingIndex, node);
            field2.AddToClassList("dropdown-parameter");

            toggleChildLabel.Add(field2);

            container.Add(foldout);
        }

        accessingIndex.Add(0);
        for (int i = 0; i < parameter.Parameters.Count; i++)
        {
            accessingIndex[accessingIndex.Count - 1] = i;
            var childParameter = parameter.Parameters[i];
            BuildNodeParameter(descendedFromSelectList ? container : foldout, childParameter, isSelectList, accessingIndex, node);
        }
    }

    private VisualElement BuildField(ParameterReference parameterRef, List<int> acessingIndex, Node node)
    {
        Procedure procedure = node.nodeData;

        Dictionary<string, Action<DropdownMenuAction>> contextMenuOperations = new Dictionary<string, Action<DropdownMenuAction>>();
        VisualElement field = new VisualElement();

        Parameter parameter = parameterRef.Parameter;
        Type parameterType = parameter.GetType();
        object parameterValue = parameter.Get();

        //if (parameterValue == null) return field;

        var parameterValueType = parameterValue?.GetType();

        var currentAcessingIndex = acessingIndex.ToList();


        //If it is a Compound Tree field (i.e the Attribute Header)
        if (parameterRef.ParameterInfo.MetaType == "Compound")
        {
            TextField stringField = new TextField();

            stringField.label = parameter.Label;

            //contextMenuOperations.Add("Delete Attribute", (x) =>
            //{
            //    var listParameter = (ListParameter)parameter;

            //    listParameter.Remove(parameter.Label);
            //    BuildNodeInfo(node);
            //});

            field = stringField;
        }
        else
    if (parameterRef.IsExpression)
        {
            TextField stringField = new TextField();

            stringField.label = parameter.Label;
            stringField.value = parameterRef.Parameter.RawExpression;
            stringField.RegisterValueChangedCallback(value =>
            {
                //var addedValues = value.newValue.Replace(value.previousValue, "");

                //if(addedValues == "\n")
                //{

                //}
                //else
                //{

                //}
                //var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                parameter.RawExpression = value.newValue;
                //parameterRef.SetExpression(value.newValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = true, value = value.newValue });
                //param.Set();
                //_graphWindow.OnGraphValueUpdated();
            });
            stringField.RegisterCallback<FocusOutEvent>(x =>
            {
                parameterRef.ApplyRawExpression();
                _graphWindow.OnGraphValueUpdated();
            });
            field = stringField;
            field.AddToClassList("expression");
        }

        else
        //If object list Parameter
        if (parameterType.ToString().Contains(typeof(ObjectListParameter<>).ToString()))
        {
            var objectListParam = (ObjectListParameter<object>)parameter;
            ObjectField objectField = new ObjectField();
            objectField.label = parameter.Label;

            var objectType = parameterType.GetGenericArguments()[0];

            objectField.objectType = objectType;
            objectField.value = (UnityEngine.Object)parameterValue;



            objectField.RegisterValueChangedCallback(value =>
            {
                parameter.Set(value.newValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = value.newValue });

                if (value.newValue != null)
                    _graphWindow.OnGraphValueUpdated();

            });


            field = objectField;
        }
        //If single object Paramter
        else if (parameterType.ToString().StartsWith(typeof(ObjectParameter).ToString()))
        {
            var objectParam = (ObjectParameter)parameter;
            ObjectField objectField = new ObjectField();
            objectField.label = parameter.Label;

            var objectType = parameterType.GetGenericArguments()[0];

            objectField.objectType = objectType;
            objectField.value = (UnityEngine.Object)parameterValue;

            objectField.RegisterValueChangedCallback(value =>
            {
                parameter.Set(value.newValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = value.newValue });

                if (value.newValue != null)
                    _graphWindow.OnGraphValueUpdated();

            });

            field = objectField;
        }
        else
            // If it is an Attribute addable List
            if (parameterType.ToString().Contains(typeof(ListParameter).ToString()))
        {

            var listParameter = (ListParameter)parameter;

            var choices = listParameter.GetAvailableFunctions();

            PopupField<string> popupField = new PopupField<string>();
            popupField.choices = choices.ToList();

            string defaultLabel = "+";
            popupField.value = defaultLabel;
            popupField.label = parameter.Label;
            popupField.AddToClassList("addList");



            //This value change callback acts like a context menu where the selected value creates a new attribute
            popupField.RegisterValueChangedCallback(value =>
            {
                if (value.newValue == defaultLabel) return;


                listParameter.Add(value.newValue);
                popupField.value = defaultLabel;
                BuildNodeInfo(node);

                node.CreatedDataParameter(new CreatedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), parameterName = value.newValue });
                node.RefreshNode();
                _graphWindow.OnGraphValueUpdated();
            });


            field = popupField;

        }
        else
        if (parameterType.ToString().Contains(typeof(SingleOrCollectiveInputChoiceParameter<IEntity>).ToString()))
        {
            var choiceParam = (SingleOrCollectiveInputChoiceParameter<IEntity>)parameter;
            var choices = choiceParam.GetAvailableFunctions();

            PopupField<string> popupField = new PopupField<string>();
            popupField.choices = choices.ToList();
            popupField.value = choiceParam.SelectedItem.Label;
            popupField.label = parameter.Label;

            popupField.RegisterValueChangedCallback(value =>
            {
                //var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                //param.Set(value.newValue);
                parameter.Set(value.newValue);

                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = value.newValue });
                BuildNodeInfo(node);
                node.RefreshNode();
                _graphWindow.OnGraphValueUpdated();
            });

            field.AddToClassList("selectList");
            field = popupField;
        }
        else if (parameterType == typeof(ChoiceParameter))
        {
            var choiceParam = (ChoiceParameter)parameter;
            PopupField<string> popupField = new PopupField<string>();
            popupField.choices = choiceParam.Choices.ToList();
            popupField.value = choiceParam.Value;
            popupField.label = parameter.Label;

            popupField.RegisterValueChangedCallback(value =>
            {
                parameter.Set(value.newValue);

                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = value.newValue });
                BuildNodeInfo(node);
                node.RefreshNode();

                _graphWindow.OnGraphValueUpdated();
            });
            field = popupField;

        }
        else
        if (parameterType.ToString().Contains(typeof(SelectListParameter).ToString()))
        {
            var choices = ((ListParameter)parameter).GetAvailableFunctions();

            var parameterList = (SceeList)parameterValue;
            PopupField<string> popupField = new PopupField<string>();
            popupField.choices = choices.ToList();
            popupField.value = parameterList.Keys[0];
            popupField.label = parameter.Label;

            popupField.RegisterValueChangedCallback(value =>
            {
                //var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                parameter.Set(value.newValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = value.newValue });
                BuildNodeInfo(node);
                node.RefreshNode();

                _graphWindow.OnGraphValueUpdated();
            });

            field.AddToClassList("selectList");
            field = popupField;
        }
        else if (parameterValueType == typeof(Sceelix.Mathematics.Data.Color))
        {
            ColorField colorField = new ColorField(parameter.Label + " (c)");
            colorField.value = ((Sceelix.Mathematics.Data.Color)parameterValue).ToUnityColor();
            colorField.RegisterValueChangedCallback(value =>
            {
                //var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                Sceelix.Mathematics.Data.Color convertedValue = new Sceelix.Mathematics.Data.Color(value.newValue);
                parameter.Set(convertedValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = convertedValue });
                _graphWindow.OnGraphValueUpdated();
            });
            field = colorField;

        }
        else if (parameterValueType == typeof(Vector4D))
        {
            Vector4Field vector3Field = new Vector4Field(parameter.Label + " (v4)");
            vector3Field.value = ((Vector4D)parameterValue).ToVector4();
            vector3Field.RegisterValueChangedCallback(value =>
            {
                //var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                Vector4D convertedValue = new Vector4D(value.newValue);
                parameter.Set(convertedValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = convertedValue });
                _graphWindow.OnGraphValueUpdated();
            });
            field = vector3Field;

        }
        else if (parameterValueType == typeof(Vector3D))
        {
            Vector3Field vector3Field = new Vector3Field(parameter.Label + " (v3)");
            vector3Field.value = ((Vector3D)parameterValue).ToVector3();
            vector3Field.RegisterValueChangedCallback(value =>
            {
                Vector3D convertedValue = new Vector3D(value.newValue).FlipYZ();
                parameter.Set(convertedValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = convertedValue });
                _graphWindow.OnGraphValueUpdated();
            });
            field = vector3Field;

        }
        else if (parameterValueType == typeof(Vector2D))
        {
            Vector2Field vector3Field = new Vector2Field(parameter.Label + " (v2)");
            vector3Field.value = ((Vector2D)parameterValue).ToVector2();
            vector3Field.RegisterValueChangedCallback(value =>
            {
                //var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                Vector2D convertedValue = new Vector2D(value.newValue);
                parameter.Set(convertedValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = convertedValue });
                _graphWindow.OnGraphValueUpdated();
            });
            field = vector3Field;

        }
        else if (parameterValueType != null && parameterValueType.IsEnum)
        {
            EnumField enumField = new EnumField(parameter.Label, (System.Enum)parameterValue);
            enumField.RegisterValueChangedCallback(value =>
            {
                //var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                parameter.Set(value.newValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = value.newValue });
                BuildNodeInfo(node);
                node.RefreshNode();

                _graphWindow.OnGraphValueUpdated();
            });
            field = enumField;
            //enumField.
        }
        else if (parameterValueType == typeof(int))
        {
            IntegerField integerField = new IntegerField(parameter.Label + " (i)");
            integerField.value = (int)parameterValue;
            integerField.RegisterValueChangedCallback(value =>
            {
                //var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                parameter.Set(value.newValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = value.newValue });
                _graphWindow.OnGraphValueUpdated();
            });
            field = integerField;
        }
        else if (parameterValueType == typeof(float) || parameterValueType == typeof(System.Single))
        {
            FloatField floatField = new FloatField(parameter.Label + " (f)");
            floatField.value = (float)parameterValue;
            floatField.RegisterValueChangedCallback(value =>
            {
                //var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                parameter.Set(value.newValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = value.newValue });
                //System.Single convertedvalue = value.newValue;
                //param.Set(convertedvalue);
                _graphWindow.OnGraphValueUpdated();
            });
            field = floatField;

        }
        else if (parameterValueType == typeof(bool))
        {
            Toggle toggleField = new Toggle(parameter.Label + " (b)");
            toggleField.value = (bool)parameterValue;
            toggleField.RegisterValueChangedCallback(value =>
            {
                //var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                parameter.Set(value.newValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = value.newValue });
                _graphWindow.OnGraphValueUpdated();
            });
            field = toggleField;
        }
        //If it is an Attribute parameter of an addable List
        else if (parameterRef.ParameterInfo.MetaType == "Attribute")
        {
            TextField stringField = new TextField();

            stringField.label = parameter.Label;
            stringField.value = parameterValue != null ? parameterValue.ToString() : "";
            stringField.RegisterValueChangedCallback(value =>
            {
                //ParameterReference param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                //AttributeParameter attrParam = (AttributeParameter)parameter;
                //attrParam.EntityEvaluation;
                parameter.Set(value.newValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = value.newValue });
                //param.Set(value.newValue);
                //procedure.
                //procedure.A
                _graphWindow.OnGraphValueUpdated();
            });
            field = stringField;
        }
        else
        {
            TextField stringField = new TextField();

            stringField.label = parameter.Label;
            stringField.value = parameterValue != null ? parameterValue.ToString() : "";
            stringField.RegisterValueChangedCallback(value =>
            {
                //var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                parameter.Set(value.newValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = value.newValue });
                _graphWindow.OnGraphValueUpdated();
            });



            field = stringField;
        }

        if (parameter.Root != parameter && parameter.Root.GetType().ToString().Contains(typeof(ListParameter).ToString()))
        {
            contextMenuOperations.Add("Delete Attribute", (x) =>
            {
                var listParameter = (ListParameter)parameter.Root;
                node.RemoveCreatedDataParameter(new CreatedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), parameterName = parameter.Label });
                listParameter.Remove(parameter);
                BuildNodeInfo(node);
                node.RefreshNode();

            });
        }

        contextMenuOperations.Add(parameterRef.IsExpression ? "Convert To Fixed Value" : "Convert To Expression", (x) =>
         {
             parameter.IsExpression = !parameter.IsExpression;
             BuildNodeInfo(node);
         });


        //Make Right Click Context Menu for Field
        field.AddManipulator(new ContextualMenuManipulator((ContextualMenuPopulateEvent evt) =>
        {
            foreach (var contextMenuOperation in contextMenuOperations)
            {
                evt.menu.AppendAction(contextMenuOperation.Key, contextMenuOperation.Value);
            }
        }));

        field.AddToClassList("parameter");

        return field;
    }




}
