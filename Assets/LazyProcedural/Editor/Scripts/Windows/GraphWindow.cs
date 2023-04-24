using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using Sceelix.Loading;
using Sceelix.Core;
using Sceelix.Core.Parameters;
using Sceelix.Core.Environments;
using System.Resources;
using System.Reflection;

namespace LazyProcedural
{
    public class GraphWindow : UnityEditor.Experimental.GraphView.GraphViewEditorWindow
    {

        private VisualElement _root;
        private Graph graph;

        //-----Header Area
        private Button _showContextButton;
        private Button _testPlusButton;
        private Button _testMinusButton;
        private Slider _testSlider;

        //-----Main Area
        private VisualElement _graph;


        //-----Footer Area

        //Debug Messager
        private TextElement _debugMssg;


        //Settings
        private PopupField<string> _settingdDrop;
        private VisualElement _settingsIcon;

        //About
        private PopupField<string> _aboutDrop;
        private VisualElement _aboutIcon;

        //Windows
        private ContextWindow _contextWindow;
        private bool isContextOpen;

        //Messages

        const string SETTINGS_RESETPREF_MSG = "Reset Preferences";
        const string SETTINGS_CLEARTEMP_MSG = "Delete Temp Items";
        const string SETTINGS_CLEARSTORED_MSG = "Delete Stored Items";


        const string ABOUT_TOOL_REPO_MSG = "About Lazy Procedural";
        const string ABOUT_REPORT_MSG = "Report an error";
        const string ABOUT_DONATE_MSG = "Donate and support the development";

        //const string SETTING_RESETPREF_TITLE = "Reset Preferences";
        //const string SETTING_RESETPREF_DESC = "Are you sure you want to reset this window's preferences";
        //const string SETTING_DELETETEMP_TITLE = "Delete Temp Items";
        //const string SETTING_DELETETEMP_DESC = "Are you sure you want to delete all the fetched temporary items?";
        //const string SETTING_DELETESTORED_TITLE = "Delete Stored Items";
        //const string SETTING_DELETESTORED_DESC = "Are you sure you want to delete all the fetched built/stored items?";

        const string CONFIRM_MSG = "Go Ahead";
        const string CANCEL_MSG = "Cancel";

        private void OnEnable()
        {
            PathFactory.Init(this);

            SceelixDomain.LoadAssembliesFrom($"{PathFactory.absoluteToolPath}\\{PathFactory.SCEELIX_PATH}");

            EngineManager.Initialize();

            ParameterManager.Initialize();

        


            InitVariables();
            MainController.OpenTest();
            //InitPreferences();

            SetupBaseUI();
            SetupExtraWindows();

            SetupBindings();
            SetupPatchedDropdowns();
            SetupIcons();

            SetupCallbacks();
            SetupInputCallbacks();

            SetupFooterMenuItems();

            graph.OnNodeSelected += OnNodeSelected;

            LogMessage("Setup finished");

            //_contextWindow.ShowNode(MainController.meshProc);

        }

        private void OnNodeSelected(Node node)
        {
            _contextWindow.ShowNode(node.nodeData);
        }

        private void OpenCloseContextWindow()
        {
            if (isContextOpen)
                CloseContextWindow();
            else
                OpenContextWindow();
        }
        private void OpenContextWindow()
        {
            if (isContextOpen) return;

            _contextWindow = new ContextWindow();
            _contextWindow.Show();
            isContextOpen = true;
        }
        private void CloseContextWindow()
        {
            if (!isContextOpen) return;

            _contextWindow.Close();
            isContextOpen = false;
        }

        private void OnDestroy()
        {
            _contextWindow.Close();
        }


        private void InitVariables()
        {
            graph = new Graph();

            //defaultButtonColor = new Color(0.345098f, 0.345098f, 0.345098f, 1);

            MainController.Init();
        }


        #region Base UI

        private void SetupBaseUI()
        {
            _root = rootVisualElement;

            // Loads and clones our VisualTree (eg. our UXML structure) inside the root.
            var visualTree = (VisualTreeAsset)AssetDatabase.LoadAssetAtPath(
                PathFactory.BuildUiFilePath(PathFactory.GRAPH_WINDOW_LAYOUT_FILE), typeof(VisualTreeAsset));

            visualTree.CloneTree(_root);

            graph.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
            graph.style.height = new StyleLength(new Length(100, LengthUnit.Percent));

            _graph = _root.Q("Graph");
            _graph.Add(graph);

            //graphvi

            //this.


            //var b = new UnityEditor.Experimental.GraphView.GraphViewBlackboardWindow();
            //b.SelectGraphViewFromWindow(this, graph);
            //b.Show();
            //b.SelectGraphViewFromWindow()
            //b.SelectGraphViewFromWindow(graph);
            //b.visible = true;
            //b.style.height = 220f;
            //b.style.width = 100f;
            //graph.ReleaseBlackboard(b);
            //var blackB = graph.GetBlackboard();
            //blackB.showInMiniMap = true;


        }

        private void SetupExtraWindows()
        {
            _contextWindow = new ContextWindow();

        }



        private void SetupBindings()
        {

            //-----Header Area
            _testPlusButton = (Button)_root.Q("Add");
            _testMinusButton = (Button)_root.Q("Remove");
            _testSlider = (Slider)_root.Q("Value");
            _showContextButton = (Button)_root.Q("ShowContextBttn");

            //-----Main Area




            //-----Footer Area


            //Debug Messager
            _debugMssg = (TextElement)_root.Q("Debug");

            //Settings
            _settingsIcon = _root.Q("SettingsIcon");

            //About
            _aboutIcon = _root.Q("AboutIcon");

        }




        private void SetupPatchedDropdowns()
        {

            ////Settings
            _settingdDrop = Utils.CreateDropdownField(_root.Q("SettingsDrop"));

            ////About
            _aboutDrop = Utils.CreateDropdownField(_root.Q("AboutDrop"));
        }


        private void SetupIcons()
        {
            //Acces icons list  here -> https://github.com/halak/unity-editor-icons

            //_searchBttnIcon.style.backgroundImage = (Texture2D)EditorGUIUtility.IconContent("d_Search Icon").image;

            _settingsIcon.style.backgroundImage = (Texture2D)EditorGUIUtility.IconContent("d__Popup@2x").image;
            _aboutIcon.style.backgroundImage = (Texture2D)EditorGUIUtility.IconContent("d_console.infoicon.sml").image;

        }

        private void SetupCallbacks()
        {
            //Server State Callbacks
            //ServerManager.On404 += () => SwitchToErrorPage(NOT_FOUND_MSG);
            //_serversDropdown.RegisterValueChangedCallback(x => ServerChanged(x.newValue));
            //_generateBttn.clicked += Generate;

            _testPlusButton.clicked += MoreGen;
            _testMinusButton.clicked += LessGen;
            _testSlider.RegisterValueChangedCallback(x => MainController.CallSystemProcedureSample(x.newValue));

            _showContextButton.clicked += OpenCloseContextWindow;
            //Footer Menu Items
            _aboutDrop.RegisterValueChangedCallback(x => OnAboutMenuChanged(x.newValue));
            _settingdDrop.RegisterValueChangedCallback(x => OnSettingsMenuChanged(x.newValue));
        }

        private float extruee = 1f;
        private const float dif = .1f;
        void MoreGen()
        {
            extruee += dif;
            MainController.CallSystemProcedureSample(extruee);
        }

        void LessGen()
        {
            extruee -= dif;
            MainController.CallSystemProcedureSample(extruee);
        }


        private void SetupInputCallbacks()
        {
            _root.RegisterCallback<KeyDownEvent>(OnKeyboardKeyDown, TrickleDown.TrickleDown);
            _root.RegisterCallback<MouseDownEvent>(OnMouseKeyDown, TrickleDown.TrickleDown);
        }


        private void OnKeyboardKeyDown(KeyDownEvent e)
        {
            if (e.keyCode == KeyCode.Escape)
                this.Close();

            else if (e.keyCode == KeyCode.Space)
            {
                OpenSearch();
                //_searchBar.Focus();
            }

            else if ((e.keyCode == KeyCode.Return))
            {
                //Generate();
            }
        }

        private void OnMouseKeyDown(MouseDownEvent e)
        {

        }


        #endregion BaseUI


        private void OpenSearch()
        {
            var searchWindow = new SearchWindow();
            searchWindow.ShowPopup();
            float windowsScale = 1.25f; // must be 1.25f if using 125%
            var pos = Event.current.mousePosition * windowsScale;
            Vector2 offset = new Vector2(-25, 100);

            var actualScreenPosition = new Vector2(
                pos.x,

            // the Y position is flipped, so we have to account for that
            // we also have to account for parts above the "Scene" window
            Screen.height - (pos.y + 25)
            );

            Vector2 mousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);

            //searchWindow.position = new Rect(mousePos.x, mousePos.y, position.width, position.height);
            searchWindow.position = new Rect(actualScreenPosition, searchWindow.position.size);
            //searchWindow.position = new Rect(actualScreenPosition, searchWindow.position.size);
        }


        private void OnAboutMenuChanged(string option)
        {
            if (option == "") return;

            switch (option)
            {
                case ABOUT_TOOL_REPO_MSG:
                    Application.OpenURL("https://github.com/wafflesgama/LazyProcedural");
                    break;

                case ABOUT_REPORT_MSG:
                    Application.OpenURL("https://github.com/wafflesgama/LazyProcedural/issues");
                    break;

                case ABOUT_DONATE_MSG:
                    Application.OpenURL("https://www.buymeacoffee.com/guilhermeGama");
                    break;
            }

            _aboutDrop.value = "";


        }
        private void OnSettingsMenuChanged(string option)
        {
            switch (option)
            {
                //case SETTINGS_RESETPREF_MSG:
                //    if (EditorUtility.DisplayDialog(SETTING_RESETPREF_TITLE, SETTING_RESETPREF_DESC, CONFIRM_MSG, CANCEL_MSG))
                //        ResetPreferences();
                //    break;

                //case SETTINGS_CLEARTEMP_MSG:
                //    if (EditorUtility.DisplayDialog(SETTING_DELETETEMP_TITLE, SETTING_DELETETEMP_DESC, CONFIRM_MSG, CANCEL_MSG))
                //    {
                //        RemoveAllTempFiles();
                //        AssetDatabase.Refresh();
                //    }
                //    break;

                //case SETTINGS_CLEARSTORED_MSG:
                //    if (EditorUtility.DisplayDialog(SETTING_DELETESTORED_TITLE, SETTING_DELETESTORED_DESC, CONFIRM_MSG, CANCEL_MSG))
                //    {
                //        RemoveAllStoredItems();
                //        AssetDatabase.Refresh();
                //    }
                //    break;
            }

            _settingdDrop.value = "";
        }

        private void SetupFooterMenuItems()
        {

            //About
            List<string> footerAboutMenuItems = new List<string>();
            footerAboutMenuItems.Add(ABOUT_TOOL_REPO_MSG);
            footerAboutMenuItems.Add(ABOUT_REPORT_MSG);
            footerAboutMenuItems.Add(ABOUT_DONATE_MSG);
            _aboutDrop.choices = footerAboutMenuItems;


            //Settings
            List<string> footerSettingsMenuItems = new List<string>();
            footerSettingsMenuItems.Add(SETTINGS_RESETPREF_MSG);
            footerSettingsMenuItems.Add(SETTINGS_CLEARTEMP_MSG);
            footerSettingsMenuItems.Add(SETTINGS_CLEARSTORED_MSG);
            _settingdDrop.choices = footerSettingsMenuItems;


        }

        #region Utils

        private static bool CheckIfMouseOverSreen(Rect r, bool relativePos = true, Vector2? offset = null)
        {
            Vector2 mousePos = relativePos
                ? Event.current.mousePosition
                : GUIUtility.GUIToScreenPoint(Event.current.mousePosition);

            var xMin = r.xMin + (offset.HasValue ? offset.Value.x : 0);
            var xMax = r.xMax + (offset.HasValue ? offset.Value.x : 0);
            var yMin = r.yMin + (offset.HasValue ? offset.Value.y : 0);
            var yMax = r.yMax + (offset.HasValue ? offset.Value.y : 0);

            var condition = mousePos.x >= xMin && mousePos.x <= xMax &&
                            mousePos.y >= yMin && mousePos.y <= yMax;

            return condition;
        }



        /// <summary>
        /// Logs Message to the footer panel, msg fades after some seconds
        /// </summary>
        /// <param name="message">The displayed message</param>
        /// <param name="messageLevel">0-Info,1-Warning, 2-Error</param>
        private async void LogMessage(string message, int messageLevel = 0)
        {
            _debugMssg.style.opacity = 1;
            _debugMssg.text = message;
            Color color = messageLevel switch
            {
                1 => Color.yellow,
                2 => Color.red,
                _ => Color.white
            };
            _debugMssg.style.color = color;


            for (int i = 0; i < 1000; i++)
            {
                await Task.Delay(5);
                if (_debugMssg.text != message) return;

                var subval = i * 0.01f;
                if (subval > 1) break;

                _debugMssg.style.opacity = 1 - subval;
            }

            _debugMssg.text = "";
        }

        #endregion Utils
    }
}
