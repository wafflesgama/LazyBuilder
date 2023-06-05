using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace LazyProcedural
{
    public class DialogueWindow : EditorWindow
    {

        public static bool DisplayDialogue(string title, string message, string ok, string nok)
        {
            var window = GetWindow<DialogueWindow>(title);
            window.SetupInfo(title, message, ok, nok, null, null);
            window.ShowModalUtility();
            return window.status == 1;
        }

        public static int DisplayDialogue(string title, string message, string ok, string nok, string cancel)
        {
            var window = GetWindow<DialogueWindow>(title);
            window.SetupInfo(title, message, ok, nok, cancel, null);
            window.ShowModalUtility();
            return window.status;
        }

        public static (bool, string) DisplayInputDialogue(string title, string message, string ok, string nok, string defaultLabel)
        {
            var window = GetWindow<DialogueWindow>(title);
            window.SetupInfo(title, message, ok, nok, null, defaultLabel);
            window.ShowModalUtility();
            return (window.status == 1, window.GetInputValue());
        }

        private VisualElement _root;


        public int status = 0;

        private Label _message;
        private Button _okBttn;
        private Button _nokBttn;
        private Button _cancelBttn;
        private TextField _inputField;

        private string inputPlaceHolder;

        public void SetupInfo(string title, string message, string ok, string nok, string cancel, string defaultLabel)
        {
            titleContent.text = title;
            _message.text = message;
            _okBttn.text = ok;
            _nokBttn.text = nok;

            if (cancel != null)
            {
                _cancelBttn.text = cancel;
            }
            else
            {
                _cancelBttn.visible = false;
            }


            if (defaultLabel != null)
            {
                this.inputPlaceHolder = defaultLabel;
                _inputField.value = inputPlaceHolder;
            }
            else
            {
                _inputField.visible = false;
            }
        }
        private void OnEnable()
        {
            SetupBaseUI();
            SetupBindings();
            //SetupInfo();
            SetupCallbacks();

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
            var quickToolVisualTree = (VisualTreeAsset)AssetDatabase.LoadAssetAtPath(PathFactory.BuildUiFilePath(PathFactory.DIALOGUE_LAYOUT_FILE), typeof(VisualTreeAsset));
            quickToolVisualTree.CloneTree(_root);

        }

        private void SetupBindings()
        {

            _message = (Label)_root.Q("Message");
            _inputField = (TextField)_root.Q("InputField");
            _okBttn = (Button)_root.Q("OkBttn");
            _nokBttn = (Button)_root.Q("NOkBttn");
            _cancelBttn = (Button)_root.Q("CancelBttn");
        }



        private void SetupCallbacks()
        {
            _okBttn.RegisterCallback<ClickEvent>((x) => OkClicked());
            _nokBttn.RegisterCallback<ClickEvent>((x) => NOKClicked());
            _cancelBttn.RegisterCallback<ClickEvent>((x) => this.Close());
        }


        private string GetInputValue()
        {

            if (inputPlaceHolder == _inputField.value)
                return string.Empty;

            return _inputField.value;
        }

        private void OkClicked()
        {
            status = 1;
            this.Close();
        }

        private void NOKClicked()
        {
            status = -1;
            this.Close();
        }
    }
}
