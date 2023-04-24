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

        foreach (var parameter in procedure.Parameters)
            ShowParameter(_root, parameter, false);
    }

    private void ShowParameter(VisualElement container, ParameterReference parameter, bool isDropdownChoice)
    {
        if (!parameter.ParameterInfo.IsPublic) return;

        if (parameter.Parameters.Count == 0)
        {

            //TextField field = new TextField();
            ////field.
            ////field.text = parameter.Label;
            //field.label = parameter.Label;
            //field.value = parameter.Get().ToString();
            //container.Add(field);

            var field = CreateField(parameter);
            container.Add(field);
            return;
        }




        //VisualElement foldoutContainer = new VisualElement();
        //foldoutContainer.style.flexDirection = FlexDirection.Row;
        Foldout foldout = new Foldout();
        if (!isDropdownChoice)
        {
            if (container != _root)
                foldout.style.marginLeft = 15;

            foldout.text = " ";
            //foldoutContainer.Add(foldout);


            var toggle = foldout.Q<Toggle>();
            var toggleChild = toggle.Q<VisualElement>();

            var toggleButton= toggleChild.Q<VisualElement>("unity-checkmark");
            toggleButton.style.marginTop = 3;


            //Remove Standard Label
            var toggleChildLabel = toggleChild.Q<Label>();
            //toggleChildLabel.visible = false;
            //toggleChild.Remove(toggleChildLabel);
            //toggleChild.RemoveAt(1);

            var field2 = CreateField(parameter);
            field2.style.marginLeft = 3;
            field2.style.paddingLeft = 2;
            field2.style.paddingTop = 0;

            toggleChildLabel.Add(field2);

            container.Add(foldout);
        }

        for (int i = 0; i < parameter.Parameters.Count; i++)
        {
            var childParameter = parameter.Parameters[i];
            ShowParameter(isDropdownChoice ? container : foldout, childParameter, !isDropdownChoice);
            //bool isTerminal = parameter.Parameters.Count > 0;
        }
    }

    private VisualElement CreateField(ParameterReference parameterRef)
    {
        VisualElement field = new VisualElement();
        var parameter = parameterRef.Parameter;
        var parameterType = parameter.GetType();
        var parameterValue = parameter.Get();
        if (parameterValue == null) return field;

        var parameterValueType = parameterValue.GetType();
        var a = parameterType.ToString();
        var b = typeof(SelectListParameter).ToString();
        if (a.Contains(b))
        {
            var choices = ((ListParameter)parameter).GetAvailableFunctions();
            //var test = true;
            //}
            //if (parameterType == typeof(SceeList))
            //{
            var parameterList = (SceeList)parameterValue;
            PopupField<string> popupField = new PopupField<string>();
            popupField.choices = choices.ToList();
            //popupField.value= parameterValue;
            popupField.value = parameterList.Keys[0];

            popupField.label = parameter.Label;

            field = popupField;
            //field = new Label(parameter.Label);
        }
        else if (parameterValueType.IsEnum)
        {
            EnumField enumField = new EnumField(parameter.Label, (System.Enum)parameterValue);
            field = enumField;
            //enumField.
        }
        else if (parameterValueType == typeof(int))
        {
            IntegerField integerField = new IntegerField(parameter.Label + " (i)");
            integerField.value = (int)parameterValue;
            field = integerField;
        }
        else if (parameterValueType == typeof(float) || parameterValueType == typeof(System.Single))
        {
            FloatField floatField = new FloatField(parameter.Label + " (f)");
            floatField.value = (float)parameterValue;
            field = floatField;
        }
        else if (parameterValueType == typeof(bool))
        {
            Toggle toggleField = new Toggle(parameter.Label + " (b)");
            toggleField.value = (bool)parameterValue;
            field = toggleField;
        }
        else
        {
            TextField stringField = new TextField();

            //field.
            //field.text = parameter.Label;
            stringField.label = parameter.Label;
            stringField.value = parameterValue.ToString();
            field = stringField;
        }

        return field;
    }

}
