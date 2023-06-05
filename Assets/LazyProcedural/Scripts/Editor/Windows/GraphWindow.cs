using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Sceelix.Core.Procedures;
using Sceelix.Processors;
using UnityEditor.Experimental.GraphView;

namespace LazyProcedural
{
    public class GraphWindow : UnityEditor.Experimental.GraphView.GraphViewEditorWindow
    {
        public string filePath;

        private VisualElement _root;
        private Graph graph;
        private Vector2 addMousePos;


        private GenerationManager generationManager;
        private ProcessorManager processorManager;
        private GeoGraphComponent graphComponent;
        private GraphPersistance graphPersistance;

        //-----Header Area
        private Button _saveButton;
        //private VisualElement _saveButtonIcon;
        private Button _showContextButton;
        private Button _runButton;

        //-----Main Area
        private VisualElement _graph;


        //-----Footer Area
        private TextField _focusTarget;

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
        private GlobalParametersWindow _globalParametersBoard;

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

        const string UNSAVED_CHANGES_TITLE = "Unsaved Changes";
        const string UNSAVED_CHANGES_DESC = "This Graph contains unsaved changes. Do you want to save them?";
        const string UNSAVED_CHANGES_OK = "Save changes";
        const string UNSAVED_CHANGES_NOK = "Don't save";
        const string UNSAVED_CHANGES_CANCEL = "Cancel";

        const string CONFIRM_MSG = "Go Ahead";
        const string CANCEL_MSG = "Cancel";

        private bool initialized;
        private bool unsavedChanges;

        private async void OnEnable()
        {
            for (int i = 0; i < 5000; i++)
            {
                if (string.IsNullOrEmpty(filePath))
                    await Task.Delay(1);
                else
                    break;
            }
            Setup();

        }


        private void Setup()
        {

            InitVariables();
            InitGraph();

            SetupBaseUI();
            SetupExtraWindows();


            SetupBindings();
            SetupPatchedDropdowns();
            SetupIcons();

            SetupCallbacks();
            SetupInputCallbacks();

            SetupFooterMenuItems();


            FocusOnGraph();

            LogMessage("Setup finished");

            initialized = true;

        }

        private void InitVariables()
        {
            PathFactory.Init();

            generationManager = new GenerationManager();

            processorManager = new ProcessorManager();

            graphPersistance = new GraphPersistance();

            ProcedureInfoManager.Init();

            graphComponent = GraphComponentFinder.FindOrCreateComponent();

        }
        private void InitGraph()
        {
            graph = new Graph();

            if (graphPersistance.filePath == null)
                graphPersistance.filePath = filePath;

            var loadedGraph = graphPersistance.LoadGraph();

            graph.AddNodes(loadedGraph.Item1);
            graph.AddEdges(loadedGraph.Item2);

            _globalParametersBoard = new GlobalParametersWindow(this, graph, loadedGraph.Item3);

            graph.OnNodeSelected += OnNodeSelected;
            graph.OnNodesUnselected += OnNodesUnselected;

            graph.OnGraphStructureChanged += OnGraphStructureChanged;
        }



        public void AddNode(ProcedureInfo nodeInfo)
        {
            var pos = addMousePos;
            pos = _root.ChangeCoordinatesTo(_root.parent, pos - this.position.position);
            var node = graph.AddNode(nodeInfo, pos);

            node.Select(graph, false);

            RunGraph();
        }


        private void OnNodeSelected(Node node)
        {
            if (graph.selection.Count > 1) return;

            OpenContextWindow();
            _contextWindow.BuildNodeInfo(node);

            node.Focus();
        }

        private void OnNodesUnselected()
        {
            if (_contextWindow != null)
                _contextWindow.ResetNodeInfo();
        }

        private async void OnGraphStructureChanged()
        {
            unsavedChanges = true;

            //Delaying to graph process the structure changes
            await Task.Delay(3);
            RunGraph();
        }

        public void OnGraphValueUpdated()
        {
            unsavedChanges = true;
            RunGraph();
        }

        public void OnGraphGlobalParamUpdated()
        {
            unsavedChanges = true;
            RunGraph();
        }

        private void SaveGraph()
        {
            unsavedChanges = false;

            if (graphPersistance.filePath == null)
                graphPersistance.filePath = filePath;

            graphPersistance.SaveGraph
            (
                graph.nodes.ToList().Select(x => (Node)x),
                _globalParametersBoard.Parameters.Select(x => ((x.Key, x.Value)))
            );

            LogMessage("Graph saved successfully!");
        }

        private void OpenCloseContextWindow()
        {
            if (_contextWindow != null)
                CloseContextWindow();
            else
                OpenContextWindow();

            //_contextWindow.visible = !_contextWindow.visible;
        }
        private void OpenContextWindow()
        {
            if (_contextWindow == null)
            {

                _contextWindow = EditorWindow.CreateInstance<ContextWindow>();
                _contextWindow.graphWindow = this;
                _contextWindow.Show();
                try
                {
                    WindowDocker.Dock(this, _contextWindow, WindowDocker.DockPosition.Right);
                }
                catch (System.Exception)
                {
                    WindowManager.DockToWindow(this, _contextWindow, 500, 500);
                }
            }
            else
            {
                _contextWindow.Show();

            }


            //_contextWindow.visible = true;
        }
        private void CloseContextWindow()
        {
            if (_contextWindow == null) return;

            _contextWindow.Close();
            //_contextWindow.visible = false;
        }





        private void OnDestroy()
        {
            if (unsavedChanges)
            {
                bool result = EditorUtility.DisplayDialog(UNSAVED_CHANGES_TITLE, UNSAVED_CHANGES_DESC, UNSAVED_CHANGES_OK, UNSAVED_CHANGES_NOK);
                //= DialogueWindow.DisplayDialogue(UNSAVED_CHANGES_TITLE, UNSAVED_CHANGES_DESC, UNSAVED_CHANGES_OK, UNSAVED_CHANGES_NOK);

                if (result)
                    SaveGraph();
            }

            CloseContextWindow();
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


            var styleSheet = (StyleSheet)AssetDatabase.LoadAssetAtPath(PathFactory.BuildUiFilePath(PathFactory.GRAPH_WINDOW_LAYOUT_FILE, false), typeof(StyleSheet));
            _root.styleSheets.Add(styleSheet);


        }

        private void SetupExtraWindows()
        {


            graph.Add(_globalParametersBoard);


            //MiniMap minimap = new MiniMap();
            //graph.Add(minimap);

            //_contextWindow = new ContextWindow(this);
            //_contextWindow.visible = false;
            //graph.Add(_contextWindow);
        }



        private void SetupBindings()
        {

            //-----Header Area
            _saveButton = (Button)_root.Q("SaveBttn");
            //_saveButtonIcon = _root.Q("SaveBttnIcon");
            _showContextButton = (Button)_root.Q("ShowContextBttn");
            _runButton = (Button)_root.Q("RunBttn");

            //-----Main Area




            //-----Footer Area

            _focusTarget = (TextField)_root.Q("FocusTarget");

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

            //_saveButtonIcon.style.backgroundImage = (Texture2D)EditorGUIUtility.IconContent("d_SaveAs").image;

            _settingsIcon.style.backgroundImage = (Texture2D)EditorGUIUtility.IconContent("d__Popup@2x").image;
            _aboutIcon.style.backgroundImage = (Texture2D)EditorGUIUtility.IconContent("d_console.infoicon.sml").image;

        }


        private void SetupCallbacks()
        {


            //Header Items
            _saveButton.clicked += SaveGraph;
            _showContextButton.clicked += OpenCloseContextWindow;
            _runButton.clicked += RunGraph;

            //Footer Menu Items
            _aboutDrop.RegisterValueChangedCallback(x => OnAboutMenuChanged(x.newValue));
            _settingdDrop.RegisterValueChangedCallback(x => OnSettingsMenuChanged(x.newValue));
        }


        private void SetupInputCallbacks()
        {
            _root.RegisterCallback<KeyDownEvent>(OnKeyboardKeyDown, TrickleDown.TrickleDown);
            //_root.RegisterCallback<MouseDownEvent>(OnMouseKeyDown, TrickleDown.TrickleDown);
        }


        private void OnKeyboardKeyDown(KeyDownEvent e)
        {


            if (e.keyCode == KeyCode.Escape)
                this.Close();


            //TODO: Real copy and paste
            if (e.ctrlKey && (e.keyCode == KeyCode.D || e.keyCode == KeyCode.V))
            {
                DuplicateSelection();
            }
            else
            if (e.ctrlKey && e.keyCode == KeyCode.S)
            {
                SaveGraph();
            }
            else
            if (e.ctrlKey && e.keyCode == KeyCode.G)
            {
                graph.GroupSelection();
            }

            else if (e.keyCode == KeyCode.Space)
            {
                OpenSearch();
                //_searchBar.Focus();
            }

            else if ((e.keyCode == KeyCode.Return))
            {
                RunGraph();
            }
        }

        //private void OnMouseKeyDown(MouseDownEvent e)
        //{

        //}


        #endregion BaseUI


        private void OpenSearch()
        {
            var searchWindow = EditorWindow.CreateInstance<SearchWindow>();

            searchWindow.procedures = ProcedureInfoManager.ProceduresInfo;
            searchWindow.graphWindow = this;
            searchWindow.paramsInit = true;

            addMousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            searchWindow.position = new Rect(addMousePos, searchWindow.position.size);
            searchWindow.ShowPopup();
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
            //switch (option)
            //{
            //    //case SETTINGS_RESETPREF_MSG:
            //    //    if (EditorUtility.DisplayDialog(SETTING_RESETPREF_TITLE, SETTING_RESETPREF_DESC, CONFIRM_MSG, CANCEL_MSG))
            //    //        ResetPreferences();
            //    //    break;

            //    //case SETTINGS_CLEARTEMP_MSG:
            //    //    if (EditorUtility.DisplayDialog(SETTING_DELETETEMP_TITLE, SETTING_DELETETEMP_DESC, CONFIRM_MSG, CANCEL_MSG))
            //    //    {
            //    //        RemoveAllTempFiles();
            //    //        AssetDatabase.Refresh();
            //    //    }
            //    //    break;

            //    //case SETTINGS_CLEARSTORED_MSG:
            //    //    if (EditorUtility.DisplayDialog(SETTING_DELETESTORED_TITLE, SETTING_DELETESTORED_DESC, CONFIRM_MSG, CANCEL_MSG))
            //    //    {
            //    //        RemoveAllStoredItems();
            //    //        AssetDatabase.Refresh();
            //    //    }
            //    //    break;
            //}

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




        private void DuplicateSelection()
        {
            var mousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            mousePos = _root.ChangeCoordinatesTo(_root.parent, mousePos - this.position.position);

            graph.DuplicateSelection(mousePos);
        }

        private void RunGraph()
        {
            if (graphComponent == null)
                graphComponent = GraphComponentFinder.FindComponent();

            var meshes = generationManager.ExecuteGraph(graph.nodes.ToList(), _globalParametersBoard.Parameters.Select(x => (x.Key, x.Value)));

            processorManager.Populate(meshes, graphComponent);
        }


        public void FocusOnGraph()
        {
            this.Focus();
            _focusTarget.Focus();
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
        public async void LogMessage(string message, int messageLevel = 0)
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
