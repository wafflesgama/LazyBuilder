using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

using Sceelix;
using Sceelix.Loading;
using Sceelix.Core;
using Sceelix.Core.Environments;
using Sceelix.Core.Procedures;
using System.Linq;
using System;
using Sceelix.Core.Attributes;
using Sceelix.Collections;
using Sceelix.Core.Data;
using System.Resources;
using System.Reflection;
using Sceelix.Meshes.Procedures;

namespace LazyProcedural
{
    public class MainController : EditorWindow
    {

        public static SceneView sceneWindow { get; private set; }

        public static void Init()
        {
            //Empty window to assess where script is located
            ScriptableObject scriptSource = new MainController();
            PathFactory.Init(scriptSource);
        }

        [MenuItem("Tools/Lazy Procedural/Test Sceelix #g")]
        public static void OpenTest()
        {
            ScriptableObject scriptSource = new MainController();
            PathFactory.Init(scriptSource);

            SceelixDomain.LoadAssembliesFrom("");

            EngineManager.Initialize();

            CallSystemProcedureSample();
        }


        private static void CallSystemProcedureSample()
        {
            //you should create a LoadEnvironment instance and pass it to your procedures, so that it can find resources (files, textures, etc.)
            ProcedureEnvironment environment = new ProcedureEnvironment(new ResourceManager(PathFactory.absoluteToolPath, Assembly.GetExecutingAssembly()));


            MeshCreateProcedure meshProc = new MeshCreateProcedure();
            meshProc.Parameters["Primitive"].Set("Cone");


            //create an instance of the system procedure and set the loadenvironment
            LogProcedure procedure = new LogProcedure();
            procedure.Environment = environment;

            //parameters are set in this way, using the same labels as viewed in the Sceelix Designer
            procedure.Parameters["Inputs"].Set("Single");

            //for lists, there are several options for settings data
            //1) Set a string, which will add an item with that label to the list
            //alternatively, you could use the same Set function for lists, which does the same as Add
            procedure.Parameters["Messages"].Set("Text");
            procedure.Parameters["Messages"].Parameters.Last().Set("Hello!");

            //2) Set an enumerable of strings, which will add several items with those labels to the list
            procedure.Parameters["Messages"].Set(new[] { "Text", "Text" });
            procedure.Parameters["Messages"].Parameters[1].Set("This is a second message.");
            procedure.Parameters["Messages"].Parameters[2].Set("This is a third message.");

            //You can also set data within a loop
            var listParameter = procedure.Parameters["Messages"];
            for (int i = 0; i < 5; i++)
            {
                listParameter.Set("Text");

                //you can set expressions (and reference attribute values) using the overload of the set function.
                listParameter.Parameters[i].SetExpression((inputData, currentEntity) => String.Format("The value of the attribute is '{0}'", inputData.Get(new GlobalAttributeKey("MyAttribute"), true)));
            }

            //to see what's stored in the parameter, you can always get it
            var data = listParameter.Get();
            Console.WriteLine(data);

            //3) You can use Sceelix's associative array, the Sceelist
            //bu setting it, you'll replace all the above
            SceeList sceelist = new SceeList(new KeyValuePair<string, object>("Text", "Message 4."), new KeyValuePair<string, object>("Text", "Message 5."), new KeyValuePair<string, object>("Text", "Message 6."));
            //Uncomment to see the result
            //listParameter.Set(sceelist);



            //Now, to set the inputs
            //let's create a new entity
            var newEntity = new Entity();
            newEntity.Attributes.Add(new GlobalAttributeKey("MyAttribute"), 123);

            //in this case, the input is tied to "Single" parameter, so you would need to do this.
            procedure.Parameters["Inputs"].Parameters["Single"].Inputs["Single"].Enqueue(newEntity);

            //or, after you have set all the parameters, you can call this function
            //to set all the ports and then use the procedure.Inputs field
            procedure.UpdateParameterPorts();

            var otherEntity = new Entity();
            otherEntity.Attributes.Add(new GlobalAttributeKey("MyAttribute"), "This is another value!");
            procedure.Inputs[0].Enqueue(otherEntity);

            //once we have finished the parameterization, execute it!
            //it will execute twice since we have added 2 entities to the input
            meshProc.Execute();

            //now we can get the data from the outputs
            //this peeks (but not removes) one item from the first ouput
            IEntity entity = meshProc.Outputs[0].Peek();

            //this peeks all the data from all the output ports
            IEnumerable<IEntity> entities = meshProc.Outputs.PeekAll();

            //this gets (and removes) all the data from all the output ports
            IEnumerable<IEntity> poppedEntities = meshProc.Outputs.DequeueAll();
        }


        //Handles correct behaviour when double-clicking a .graph asset assigned to a field
        [OnOpenAsset]
        public static bool OnOpenGraph(int instanceID, int line)
        {
            UnityEngine.Object target = EditorUtility.InstanceIDToObject(instanceID);

            var path = AssetDatabase.GetAssetPath(instanceID);

            if (Path.GetExtension(path) != $".{PathFactory.GRAPH_TYPE}") return false;

            Selection.activeObject = target;

            string graphName = Path.GetFileNameWithoutExtension(path);
            GraphWindow(graphName);
            return true;
        }

        public void CreateGraph()
        {

        }

        private static void GraphWindow(string graphName = "")
        {
            if (Application.isPlaying) return;

            GetSceneWindow();
            var window = GetWindow<GraphWindow>();

            Texture2D icon = Utils.FetchImage(PathFactory.BuildImageFilePath(PathFactory.ICON_SMALL_FILE, true));

            window.titleContent = new GUIContent(graphName == "" ? "New Geo Graph" : graphName, icon);
            window.Show();
        }

        private static void GetSceneWindow()
        {
            if (sceneWindow != null) return;

            sceneWindow = GetWindow<SceneView>();
        }

        private void OnInspectorUpdate()
        {

        }

    }


}
