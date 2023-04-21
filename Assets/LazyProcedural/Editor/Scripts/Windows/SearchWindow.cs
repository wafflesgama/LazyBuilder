using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace LazyProcedural
{
    public class SearchWindow : EditorWindow
    {
        private VisualElement _root;

        public bool sucess;

        private Label _message;
        private Button _okBttn;
        private Button _cancelBttn;
        private TextField _inputField;

        private string inputPlaceHolder;

        public SearchWindow()
        {
            titleContent.text = "Add Node";
        }

        //public void Init(string title, string message, string ok, string cancel, string defaultLabel)
        //{
        //    titleContent.text = title;
        //    this.message = message;
        //    this.ok = ok;
        //    this.cancel = cancel;
        //}

        public void SetupInfo(string title, string message, string ok, string cancel, string defaultLabel)
        {
            titleContent.text = title;
            _message.text = message;
            _okBttn.text = ok;
            _cancelBttn.text = cancel;

            this.inputPlaceHolder = defaultLabel;
            _inputField.value = inputPlaceHolder;
        }
        private void OnEnable()
        {
            SetupBaseUI();
            SetupBindings();
            //SetupInfo();
            SetupCallbacks();

        }

        private void OnLostFocus()
        {
            Close();
        }

        private void OnDestroy()
        {
            //if (this.OnClose != null)
            //    OnClose.Invoke(sucess, GetInputValue());
        }



        private void SetupBaseUI()
        {
            _root = rootVisualElement;

            // Loads and clones our VisualTree (eg. our UXML structure) inside the root.
            var quickToolVisualTree = (VisualTreeAsset)AssetDatabase.LoadAssetAtPath(PathFactory.BuildUiFilePath(PathFactory.SEARCH_WINDOW_LAYOUT_FILE), typeof(VisualTreeAsset));
            quickToolVisualTree.CloneTree(_root);

            var styleSheet = (StyleSheet)AssetDatabase.LoadAssetAtPath(PathFactory.BuildUiFilePath(PathFactory.SEARCH_WINDOW_LAYOUT_FILE, false), typeof(StyleSheet));
            _root.styleSheets.Add(styleSheet);


        }

        private void SetupBindings()
        {

            //_message = (Label)_root.Q("Message");
            //_inputField = (TextField)_root.Q("InputField");
            //_okBttn = (Button)_root.Q("OkBttn");
            //_cancelBttn = (Button)_root.Q("CancelBttn");
        }



        private void SetupCallbacks()
        {
            //_okBttn.RegisterCallback<ClickEvent>((x) => OkClicked());
            //_cancelBttn.RegisterCallback<ClickEvent>((x) => this.Close());
        }

    }
}
