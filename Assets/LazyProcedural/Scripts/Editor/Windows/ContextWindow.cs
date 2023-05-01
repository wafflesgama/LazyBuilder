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

public class ContextWindow : EditorWindow
{
    private VisualElement _root;

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

        //var styleSheet = (StyleSheet)AssetDatabase.LoadAssetAtPath(PathFactory.BuildUiFilePath(PathFactory.SEARCH_WINDOW_LAYOUT_FILE, false), typeof(StyleSheet));
        //_root.styleSheets.Add(styleSheet);


    }
    public void ShowNode(Procedure procedure)
    {
        _root.Clear();

        var accessingIndex = new List<int>();
        accessingIndex.Add(0);

        for (int i = 0; i < procedure.Parameters.Count; i++)
        {
            accessingIndex[accessingIndex.Count - 1] = i;
            ShowParameter(_root, procedure.Parameters[i], false, accessingIndex, procedure);
        }
    }

    private void ShowParameter(VisualElement container, ParameterReference parameter, bool isDropdownChoice, List<int> accessingIndex, Procedure procedure)
    {
        if (!parameter.ParameterInfo.IsPublic) return;

        if (parameter.Parameters.Count == 0)
        {
            var field = CreateField(parameter, accessingIndex, procedure);
            container.Add(field);
            return;
        }


        Foldout foldout = new Foldout();
        if (!isDropdownChoice)
        {
            if (container != _root)
                foldout.style.marginLeft = 15;

            foldout.text = " ";
            //foldoutContainer.Add(foldout);


            var toggle = foldout.Q<Toggle>();
            var toggleChild = toggle.Q<VisualElement>();

            var toggleButton = toggleChild.Q<VisualElement>("unity-checkmark");
            toggleButton.style.marginTop = 3;


            //Remove Standard Label
            var toggleChildLabel = toggleChild.Q<Label>();


            var field2 = CreateField(parameter, accessingIndex, procedure);
            field2.style.marginLeft = 3;
            field2.style.paddingLeft = 2;
            field2.style.paddingTop = 0;

            toggleChildLabel.Add(field2);

            container.Add(foldout);
        }

        accessingIndex.Add(0);
        for (int i = 0; i < parameter.Parameters.Count; i++)
        {
            accessingIndex[accessingIndex.Count - 1] = i;
            var childParameter = parameter.Parameters[i];
            ShowParameter(isDropdownChoice ? container : foldout, childParameter, !isDropdownChoice, accessingIndex, procedure);
        }
    }

    private VisualElement CreateField(ParameterReference parameterRef, List<int> acessingIndex, Procedure procedure)
    {
        VisualElement field = new VisualElement();
        var parameter = parameterRef.Parameter;
        var parameterType = parameter.GetType();
        var parameterValue = parameter.Get();
        if (parameterValue == null) return field;

        var parameterValueType = parameterValue.GetType();

        var currentAcessingIndex= acessingIndex.ToList();

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
                ShowNode(procedure);
            });

            field = popupField;
        }
        else if (parameterValueType.IsEnum)
        {
            EnumField enumField = new EnumField(parameter.Label, (System.Enum)parameterValue);
            enumField.RegisterValueChangedCallback(value =>
            {
                var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                param.Set(value.newValue);
                ShowNode(procedure);
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
            });
            field = toggleField;
        }
        else
        {
            TextField stringField = new TextField();

            stringField.label = parameter.Label;
            stringField.value = parameterValue.ToString();
            stringField.RegisterValueChangedCallback(value =>
            {
                var param = GetParameterFromAcessingIndex(currentAcessingIndex, procedure);
                param.Set(value.newValue);
            });
            field = stringField;
        }

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
