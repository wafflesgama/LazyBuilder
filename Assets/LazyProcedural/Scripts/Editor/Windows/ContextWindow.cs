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

public class ContextWindow : EditorWindow
{
    private VisualElement _root;

    private GraphWindow _graphWindow;

    public ContextWindow(GraphWindow graphWindow)
    {
        _graphWindow = graphWindow;
    }

    private void OnEnable()
    {
        SetupBaseUI();
    }

    private void SetupBaseUI()
    {
        _root = rootVisualElement;

        // Loads and clones our VisualTree (eg. our UXML structure) inside the root.
        //var quickToolVisualTree = (VisualTreeAsset)AssetDatabase.LoadAssetAtPath(PathFactory.BuildUiFilePath(PathFactory.SEARCH_WINDOW_LAYOUT_FILE), typeof(VisualTreeAsset));
        //quickToolVisualTree.CloneTree(_root);

        var styleSheet = (StyleSheet)AssetDatabase.LoadAssetAtPath(PathFactory.BuildUiFilePath(PathFactory.CONTEXT_WINDOW_LAYOUT_FILE, false), typeof(StyleSheet));
        _root.styleSheets.Add(styleSheet);


    }
    public void BuildNodeParameters(Procedure procedure)
    {
        _root.Clear();

        var accessingIndex = new List<int>();
        accessingIndex.Add(0);

        for (int i = 0; i < procedure.Parameters.Count; i++)
        {
            accessingIndex[accessingIndex.Count - 1] = i;
            BuildParameter(_root, procedure.Parameters[i], false, accessingIndex, procedure);
        }
    }

    private void BuildParameter(VisualElement container, ParameterReference parameter, bool descendedFromSelectList, List<int> accessingIndex, Procedure procedure)
    {
        bool isSelectList = parameter.ParameterInfo.MetaType == "Select";

        if (!parameter.ParameterInfo.IsPublic) return;

        if (parameter.Parameters.Count == 0)
        {
            var field = BuildField(parameter, accessingIndex, procedure);
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


            var field2 = BuildField(parameter, accessingIndex, procedure);
            field2.AddToClassList("dropdown-parameter");

            toggleChildLabel.Add(field2);

            container.Add(foldout);
        }

        accessingIndex.Add(0);
        for (int i = 0; i < parameter.Parameters.Count; i++)
        {
            accessingIndex[accessingIndex.Count - 1] = i;
            var childParameter = parameter.Parameters[i];
            BuildParameter(descendedFromSelectList ? container : foldout, childParameter, isSelectList, accessingIndex, procedure);
        }
    }

    private VisualElement BuildField(ParameterReference parameterRef, List<int> acessingIndex, Procedure procedure)
    {
        Dictionary<string, Action<DropdownMenuAction>> contextMenuOperations = new Dictionary<string, Action<DropdownMenuAction>>();
        VisualElement field = new VisualElement();
        var parameter = parameterRef.Parameter;
        var parameterType = parameter.GetType();
        var parameterValue = parameter.Get();

        //if (parameterValue == null) return field;

        var parameterValueType = parameterValue?.GetType();

        var currentAcessingIndex = acessingIndex.ToList();

        ////If it is a Value Set field of an Attribute
      
        //else
        //If it is a Compound Tree field (i.e the Attribute Header)
        if (parameterRef.ParameterInfo.MetaType == "Compound")
        {
            TextField stringField = new TextField();

            stringField.label = parameter.Label;
            //stringField.value = parameterValue.ToString();
            stringField.RegisterValueChangedCallback(value =>
            {
                //var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                //param.Set(value.newValue);
                //_graphWindow.OnGraphValueUpdated();
            });

            contextMenuOperations.Add("Delete Attribute", (x) =>
            {
                var listParameter = (ListParameter)parameter;

                listParameter.Remove(parameter.Label);
                BuildNodeParameters(procedure);
            });

            field = stringField;
        }
        else
    if (parameterRef.IsExpression)
        {
            TextField stringField = new TextField();

            stringField.label = parameter.Label;
            stringField.value = parameterValue.ToString();
            stringField.RegisterValueChangedCallback(value =>
            {
                var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                param.SetExpression(value.newValue);
                //param.Set();
                _graphWindow.OnGraphValueUpdated();
            });
            field = stringField;
        }

        else
        // If it is an Attribute addable List
    if (parameterType.ToString().Contains(typeof(ListParameter).ToString()))
        {
            var listParameter = (ListParameter)parameter;

            var choices = listParameter.GetAvailableFunctions();

            Foldout foldout = new Foldout();
            foldout.style.marginLeft = 15;
            foldout.text = " ";

            //var parameterList = (SceeList)parameterValue;
            PopupField<string> popupField = new PopupField<string>();
            popupField.choices = choices.ToList();
            //popupField.value = parameterList.Keys[0];

            string defaultLabel = "+";
            popupField.value = defaultLabel;
            popupField.label = parameter.Label;
            popupField.AddToClassList("addList");



            popupField.RegisterValueChangedCallback(value =>
            {
                if (value.newValue == defaultLabel) return;


                listParameter.Add(value.newValue);
                //Parameter p= new Parameter();
                //var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                //param.Set(value.newValue);

                popupField.value = defaultLabel;
                BuildNodeParameters(procedure);
                _graphWindow.OnGraphValueUpdated();
            });


            field = popupField;
            //toggleChildLabel.Add(field);
            //field.AddToClassList("dropdown-parameter");

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
                var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                param.Set(value.newValue);
                BuildNodeParameters(procedure);
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
                var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                Sceelix.Mathematics.Data.Color convertedValue = new Sceelix.Mathematics.Data.Color(value.newValue);
                param.Set(convertedValue);
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
                var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                Vector4D convertedValue = new Vector4D(value.newValue);
                param.Set(convertedValue);
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
                var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                Vector3D convertedValue = new Vector3D(value.newValue).FlipYZ();
                param.Set(convertedValue);
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
                var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                Vector2D convertedValue = new Vector2D(value.newValue);
                param.Set(convertedValue);
                _graphWindow.OnGraphValueUpdated();
            });
            field = vector3Field;

        }
        else if (parameterValueType != null && parameterValueType.IsEnum)
        {
            EnumField enumField = new EnumField(parameter.Label, (System.Enum)parameterValue);
            enumField.RegisterValueChangedCallback(value =>
            {
                var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                param.Set(value.newValue);
                BuildNodeParameters(procedure);
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
                var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                param.Set(value.newValue);
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
                var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                System.Single convertedvalue = value.newValue;
                param.Set(convertedvalue);
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
                var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                param.Set(value.newValue);
                _graphWindow.OnGraphValueUpdated();
            });
            field = toggleField;
        }
        //If it is an Attribute parameter of an addable List
        else  if (parameterRef.ParameterInfo.MetaType == "Attribute")
        {
            TextField stringField = new TextField();

            stringField.label = parameter.Label;
            stringField.value = parameterValue != null ? parameterValue.ToString() : "";
            stringField.RegisterValueChangedCallback(value =>
            {
                var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                param.Set(value.newValue);
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
                var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                param.Set(value.newValue);
                _graphWindow.OnGraphValueUpdated();
            });

        
           
            field = stringField;
        }

        contextMenuOperations.Add(parameterRef.IsExpression ? "Convert To Fixed Value" : "Convert To Expression", (x) =>
         {
             parameter.IsExpression = !parameter.IsExpression;
             BuildNodeParameters(procedure);
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

    private ParameterReference GetParameterFromAcessingIndex(List<int> acessingIndex, Procedure procedure)
    {
        if (acessingIndex == null || acessingIndex.Count == 0) return null;

        ParameterReference reference = procedure.Parameters[acessingIndex[0]];
        bool firstElement = true;
        foreach (var index in acessingIndex)
        {
            if (firstElement)
            {
                firstElement = false;
                continue;
            }

            reference = reference.Parameters[index];
        }

        return reference;
    }


}
