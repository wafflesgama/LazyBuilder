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
using System.Threading.Tasks;

public class ContextWindow : EditorWindow
{
    public GraphWindow graphWindow;

    private VisualElement _root;

    private EventCallback<ChangeEvent<string>> currentNameChangeCallback;


    private Label _typeLabel;
    private TextField _nameField;


    private ScrollView _parametersContainer;


    private async void OnEnable()
    {
        for (int i = 0; i < 5000; i++)
        {
            if (graphWindow == null)
                await Task.Delay(1);
            else
                break;
        }
        Setup();
    }


    private void Setup()
    {
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
        if (_typeLabel == null) return;

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
               if (x.newValue == "") return;
               node.title = x.newValue;
           };
        _nameField.RegisterValueChangedCallback(currentNameChangeCallback);


        var _headerContainer = _nameField.parent;




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
        Sceelix.Core.Parameters.Infos.ParameterInfo parameterInfo = parameter.ParameterInfo;
        bool isSelectList = parameterInfo.MetaType == "Select";

        if (!parameterInfo.IsPublic) return;

        if (parameter.Parameters.Count == 0)
        {
            //If a compound parameter does not have children do not add it
            if (parameterInfo.MetaType == "Compound") return;

            var field = BuildField(parameter,parameterInfo, accessingIndex, node);
            container.Add(field);
            return;
        }


        Foldout foldout = new Foldout();
        //A foldout parameter will not be created if it is the selected value of the parent List
        if (!descendedFromSelectList)
        {
            if (container != _root)
                foldout.style.marginLeft = 15;

            foldout.text = " ";

            var toggle = foldout.Q<Toggle>();
            var toggleChild = toggle.Q<VisualElement>();

            var toggleButton = toggleChild.Q<VisualElement>("unity-checkmark");
            toggleButton.style.marginTop = 3;


            //Add field in the Label element
            var toggleChildLabel = toggleChild.Q<Label>();


            var field2 = BuildField(parameter,parameterInfo, accessingIndex, node);
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

    //private VisualElement BuildField(ParameterReference parameterRef, List<int> acessingIndex, Node node)
    //{
    //    Procedure procedure = node.nodeData;
    //    VisualElement field = new VisualElement();

    //    Parameter parameter = parameterRef.Parameter;
    //    Type parameterType = parameter.GetType();
    //    object parameterValue = parameter.Get();

    //    var parameterValueType = parameterValue?.GetType();
    //    var currentAcessingIndex = acessingIndex.ToList();

    //    if (parameterInfo.MetaType == "Compound")
    //    {
    //        field = BuildCompoundField(parameter);
    //    }
    //    else if (parameterRef.IsExpression)
    //    {
    //        field = BuildExpressionField(parameter, parameterRef, currentAcessingIndex, node);
    //    }
    //    else if (parameterType.ToString().Contains(typeof(ObjectListParameter<>).ToString()))
    //    {
    //        field = BuildObjectListField(parameter, parameterValue, currentAcessingIndex, node);
    //    }
    //    else if (parameterType.ToString().StartsWith(typeof(ObjectParameter).ToString()))
    //    {
    //        field = BuildObjectField(parameter, parameterValue, currentAcessingIndex, node);
    //    }
    //    else if (parameterType.ToString().Contains(typeof(ListParameter).ToString()))
    //    {
    //        field = BuildListField(parameter, parameterValue, currentAcessingIndex, node);
    //    }
    //    else if (parameterType.ToString().Contains(typeof(SingleOrCollectiveInputChoiceParameter<IEntity>).ToString()))
    //    {
    //        field = BuildChoiceField(parameter, parameterValue, currentAcessingIndex, node);
    //    }
    //    else if (parameterType == typeof(ChoiceParameter))
    //    {
    //        field = BuildChoiceField(parameter, parameterValue, currentAcessingIndex, node);
    //    }
    //    else if (parameterType.ToString().Contains(typeof(SelectListParameter).ToString()))
    //    {
    //        field = BuildSelectListField(parameter, parameterValue, currentAcessingIndex, node);
    //    }
    //    else if (parameterValueType == typeof(Color))
    //    {
    //        field = BuildColorField(parameter, parameterValue, currentAcessingIndex, node);
    //    }
    //    else if (parameterValueType == typeof(Vector4D))
    //    {
    //        field = BuildVector4Field(parameter, parameterValue, currentAcessingIndex, node);
    //    }
    //    else if (parameterValueType == typeof(Vector3D))
    //    {
    //        field = BuildVector3Field(parameter, parameterValue, currentAcessingIndex, node);
    //    }
    //    else if (parameterValueType == typeof(Vector2D))
    //    {
    //        field = BuildVector2Field(parameter, parameterValue, currentAcessingIndex, node);
    //    }
    //    else if (parameterValueType != null && parameterValueType.IsEnum)
    //    {
    //        field = BuildEnumField(parameter, parameterValue, currentAcessingIndex, node);
    //    }
    //    else if (parameterValueType == typeof(int))
    //    {
    //        field = BuildIntegerField(parameter, parameterValue, currentAcessingIndex, node);
    //    }
    //    else if (parameterValueType == typeof(float) || parameterValueType == typeof(System.Single))
    //    {
    //        field = BuildFloatField(parameter, parameterValue, currentAcessingIndex, node);
    //    }
    //    else if (parameterValueType == typeof(bool))
    //    {
    //        field = BuildToggleField(parameter, parameterValue, currentAcessingIndex, node);
    //    }
    //    else if (parameterInfo.MetaType == "Attribute")
    //    {
    //        field = BuildAttributeField(parameter, parameterValue, currentAcessingIndex, node);
    //    }
    //    else
    //    {
    //        Debug.LogError($"Unsupported parameter type: {parameterType}");
    //    }

    //    field.AddToClassList("parameter");

    //    return field;
    //}

    private VisualElement BuildField(ParameterReference parameterRef, Sceelix.Core.Parameters.Infos.ParameterInfo parameterInfo, List<int> acessingIndex, Node node)
    {
        Procedure procedure = node.nodeData;

        Dictionary<string, Action<DropdownMenuAction>> contextMenuOperations = new Dictionary<string, Action<DropdownMenuAction>>();
        VisualElement field = new VisualElement();

        Parameter parameter = parameterRef.Parameter;
        Type parameterType = parameter.GetType();

        object parameterValue = parameter.Get();


        var parameterValueType = parameterValue?.GetType();

        var currentAcessingIndex = acessingIndex.ToList();


        //If it is a Compound Tree field (i.e the Attribute Header)
        if (parameterInfo.MetaType == "Compound")
        {
            TextField stringField = new TextField();


            stringField.label = parameter.Label;

            field = stringField;
            field.AddToClassList("compound");
        }
        else
    if (parameterRef.IsExpression)
        {
            string validExpressionClass = "valid-exp";
            string errorExpressionClass = "error-exp";

            TextField stringField = new TextField();

            stringField.label = parameter.Label;
            stringField.value = parameterRef.Parameter.RawExpression;
            stringField.RegisterValueChangedCallback(value =>
            {

                parameter.RawExpression = value.newValue;
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = true, value = value.newValue });
            });
            stringField.RegisterCallback<FocusOutEvent>(x =>
            {
                try
                {
                    parameterRef.ApplyRawExpression();

                }
                catch (Exception ex)
                {
                    parameter.ExpressionState = Parameter.ExpressionStates.ERROR;
                    field.AddToClassList(errorExpressionClass);

                    if (field.ClassListContains(validExpressionClass))
                        field.RemoveFromClassList(validExpressionClass);
                    throw ex;
                }


                parameter.ExpressionState = Parameter.ExpressionStates.VALID;

                field.AddToClassList(validExpressionClass);

                if (field.ClassListContains(errorExpressionClass))
                    field.RemoveFromClassList(errorExpressionClass);

                graphWindow.OnGraphValueUpdated();
            });
            field = stringField;

            field.AddToClassList("expression");

            switch (parameter.ExpressionState)
            {
                case Parameter.ExpressionStates.VALID:
                    if (!field.ClassListContains(validExpressionClass))
                        field.AddToClassList(validExpressionClass);
                    break; ;
                case Parameter.ExpressionStates.ERROR:
                    if (!field.ClassListContains(errorExpressionClass))
                        field.AddToClassList(errorExpressionClass);
                    break;
            }
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
                    graphWindow.OnGraphValueUpdated();

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
                    graphWindow.OnGraphValueUpdated();

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
                graphWindow.OnGraphValueUpdated();
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
                parameter.Set(value.newValue);

                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = value.newValue });
                BuildNodeInfo(node);
                node.RefreshNode();
                graphWindow.OnGraphValueUpdated();
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

                graphWindow.OnGraphValueUpdated();
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
                parameter.Set(value.newValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = value.newValue });
                BuildNodeInfo(node);
                node.RefreshNode();

                graphWindow.OnGraphValueUpdated();
            });

            field.AddToClassList("selectList");
            field = popupField;
        }
        else if (parameterValueType == typeof(Color))
        {
            ColorField colorField = new ColorField(parameter.Label + " (c)");
            colorField.value = (Color)parameterValue;
            colorField.RegisterValueChangedCallback(value =>
            {
                parameter.Set(value.newValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = value.newValue });
                graphWindow.OnGraphValueUpdated();
            });
            field = colorField;

        }
        else if (parameterValueType == typeof(Vector4D))
        {
            Vector4Field vector3Field = new Vector4Field(parameter.Label + " (v4)");
            vector3Field.value = ((Vector4D)parameterValue).ToVector4();
            vector3Field.RegisterValueChangedCallback(value =>
            {
                Vector4D convertedValue = new Vector4D(value.newValue);
                parameter.Set(convertedValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = convertedValue });
                graphWindow.OnGraphValueUpdated();
            });
            field = vector3Field;

        }
        else if (parameterValueType == typeof(Vector3D))
        {
            Vector3Field vector3Field = new Vector3Field(parameter.Label + " (v3)");
            vector3Field.value = ((Vector3D)parameterValue).FlipYZ().ToVector3();
            vector3Field.RegisterValueChangedCallback(value =>
            {
                Vector3D convertedValue = new Vector3D(value.newValue).FlipYZ();
                parameter.Set(convertedValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = convertedValue });
                graphWindow.OnGraphValueUpdated();
            });
            field = vector3Field;

        }
        else if (parameterValueType == typeof(Vector2D))
        {
            Vector2Field vector3Field = new Vector2Field(parameter.Label + " (v2)");
            vector3Field.value = ((Vector2D)parameterValue).ToVector2();
            vector3Field.RegisterValueChangedCallback(value =>
            {
                Vector2D convertedValue = new Vector2D(value.newValue);
                parameter.Set(convertedValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = convertedValue });
                graphWindow.OnGraphValueUpdated();
            });
            field = vector3Field;

        }
        else if (parameterValueType != null && parameterValueType.IsEnum)
        {
            EnumField enumField = new EnumField(parameter.Label, (System.Enum)parameterValue);
            enumField.RegisterValueChangedCallback(value =>
            {
                parameter.Set(value.newValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = value.newValue });
                BuildNodeInfo(node);
                node.RefreshNode();

                graphWindow.OnGraphValueUpdated();
            });
            field = enumField;
        }
        else if (parameterValueType == typeof(int))
        {
            IntegerField integerField = new IntegerField(parameter.Label + " (i)");
            integerField.value = (int)parameterValue;
            integerField.RegisterValueChangedCallback(value =>
            {
                parameter.Set(value.newValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = value.newValue });
                graphWindow.OnGraphValueUpdated();
            });
            field = integerField;
        }
        else if (parameterValueType == typeof(float) || parameterValueType == typeof(System.Single) || parameterValueType == typeof(double))
        {
            FloatField floatField = new FloatField(parameter.Label + " (f)");
            floatField.value = Convert.ToSingle(parameterValue);
            floatField.RegisterValueChangedCallback(value =>
            {
                parameter.Set(value.newValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = value.newValue });
                graphWindow.OnGraphValueUpdated();
            });
            field = floatField;

        }
        else if (parameterType.ToString().Contains(typeof(OptionalListParameter).ToString()))
        {

            OptionalListParameter optionalListParameter = (OptionalListParameter)parameter;

            if (optionalListParameter.GetAvailableFunctions().Count() == 1)
            {

                Toggle toggleField = new Toggle(parameter.Label + " (b)");
                toggleField.value = optionalListParameter.HasValue;
                toggleField.RegisterValueChangedCallback(value =>
                {
                    //Creation
                    if (value.newValue)
                    {
                        optionalListParameter.Add(optionalListParameter.GetAvailableFunctions().First());
                    }
                    //Deletion
                    else
                    {
                        optionalListParameter.Remove(optionalListParameter.Items[0]);
                    }
                    BuildNodeInfo(node);
                    //node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = value.newValue });
                    graphWindow.OnGraphValueUpdated();
                });
                field = toggleField;
            }

        }
        else if (parameterValueType == typeof(bool))
        {
            Toggle toggleField = new Toggle(parameter.Label + " (b)");
            toggleField.value = (bool)parameterValue;
            toggleField.RegisterValueChangedCallback(value =>
            {
                parameter.Set(value.newValue);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = value.newValue });
                graphWindow.OnGraphValueUpdated();
            });
            field = toggleField;
        }
        //If it is an Attribute parameter of an addable List
        else if (parameterInfo.MetaType == "Attribute")
        {
            TextField stringField = new TextField();

            stringField.label = parameter.Label;
            stringField.value = parameterValue != null ? parameterValue.ToString() : "";


            stringField.RegisterCallback<FocusOutEvent>(x =>
            {
                parameter.Set(stringField.value);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = stringField.value });
                graphWindow.OnGraphValueUpdated();
            });

            field = stringField;
        }
        else
        {
            TextField stringField = new TextField();

            stringField.label = parameter.Label;
            stringField.value = parameterValue != null ? parameterValue.ToString() : "";

            stringField.RegisterCallback<FocusOutEvent>(x =>
            {
                parameter.Set(stringField.value);
                node.ChangedDataParameter(new ChangedParameterInfo { accessIndex = currentAcessingIndex.ToArray(), isExpression = false, value = stringField.value });
                graphWindow.OnGraphValueUpdated();
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
