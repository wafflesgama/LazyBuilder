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
using Sceelix.Core.Parameters;
using Sceelix.Meshes.Data;
using Sceelix.Meshes.Operations;
using Sceelix.Mathematics.Data;

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

            SceelixDomain.LoadAssembliesFrom($"{PathFactory.absoluteToolPath}\\{PathFactory.SCEELIX_PATH}");

            var checkTypes = SceelixDomain.Types.ToList();

            EngineManager.Initialize();

            ParameterManager.Initialize();

            //CallSystemProcedureSample(2);
        }


        public static void CallSystemProcedureSample(float extrude)
        {
            //you should create a LoadEnvironment instance and pass it to your procedures, so that it can find resources (files, textures, etc.)
            ProcedureEnvironment environment = new ProcedureEnvironment(new ResourceManager($"{PathFactory.absoluteToolPath}\\{PathFactory.SCEELIX_PATH}", Assembly.GetExecutingAssembly()));


            MeshCreateProcedure meshProc = new MeshCreateProcedure();


            meshProc.Parameters["Primitive"].Set("Cone");


            //create an instance of the system procedure and set the loadenvironment
            LogProcedure procedure = new LogProcedure();
            procedure.Environment = environment;

            //parameters are set in this way, using the same labels as viewed in the Sceelix Designer
            procedure.Parameters["Inputs"].Set("Single");

            //procedure.Inputs[0].Input.Enqueue

            meshProc.Execute();

            MeshModifyProcedure meshModifyProc = new MeshModifyProcedure();
            var op = meshModifyProc.Parameters["Operation"];
            op.Parameters[0].Parameters["Amount"].Set(extrude);

            var test = meshModifyProc.Inputs[0];
            //var test2 = meshModifyProc.Parameters[0].Inputs[0];

            meshModifyProc.Inputs[0].Enqueue(meshProc.Outputs[0].Peek());

            CopyProcedure copyProcedure = new CopyProcedure();

            //copyProcedure.Parameters["Operation"].Set("Standard");

            //copyProcedure.Parameters["Number of Copies"]
            //copyProcedure.Parameters["Operation"].Inputs[0].Enqueue(meshProc.Outputs[0].Peek());
            //copyProcedure.Inputs[0].Enqueue(meshProc.Outputs[0].Peek());

            //procedure.Outputs
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

            meshModifyProc.Execute();

            //now we can get the data from the outputs
            //this peeks (but not removes) one item from the first ouput
            IEntity entity = meshProc.Outputs[0].Peek();
            //IEntity entity2 = meshModifyProc.Outputs[0].Peek();

            MeshCreate((MeshEntity)entity);

            //this peeks all the data from all the output ports
            //List<IEntity> entities = meshProc.Outputs.PeekAll().ToList();

            //this gets (and removes) all the data from all the output ports
            IEnumerable<IEntity> poppedEntities = meshProc.Outputs.DequeueAll();
        }

        public static Material CreateDefaultMaterial()
        {
            if (UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset == null)
                return new Material(AssetDatabase.GetBuiltinExtraResource<Shader>("Standard.shader"));
            else
                return new Material(UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset.defaultMaterial);
        }

        public static GameObject MeshCreate(Sceelix.Meshes.Data.MeshEntity meshEntity)
        {
            Dictionary<Sceelix.Actors.Data.Material, int> materialToMaterialData = new Dictionary<Sceelix.Actors.Data.Material, int>();

            int indexerValue = 0;
            //GameObject obj = new GameObject("Test MeshConvert");
            //MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
            //MeshFilter meshFilter = obj.AddComponent<MeshFilter>();

            //meshRenderer.material = CreateDefaultMaterial();
            GameObject obj = GameObject.Find("Cube");
            //MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
            MeshFilter meshFilter = obj.GetComponent<MeshFilter>();

            //List<Material> materials = new List<Material>();

            //Materials = new List<Material>();

            //Mesh meshData = new Mesh();
            //Mesh.Id = meshEntity.GetHashCode();


            //UnityMesh.MeshEntity = meshEntity;

            List<List<int>> subMeshT = new List<List<int>>();
            List<Vector2> uv = new List<Vector2>();
            List<Vector3> v = new List<Vector3>();
            List<Vector3> n = new List<Vector3>();
            List<Vector4> t = new List<Vector4>();
            List<UnityEngine.Color> c = new List<UnityEngine.Color>();
            List<Sceelix.Actors.Data.Material> materials = new List<Sceelix.Actors.Data.Material>();

            var m = new Mesh();
            m.name = "Test Mesh";

            foreach (Face face in meshEntity)
            {
                int index;

                if (!materialToMaterialData.TryGetValue(face.Material, out index))
                {
                    index = materialToMaterialData.Count;

                    materialToMaterialData.Add(face.Material, index);

                    materials.Add(face.Material);
                    subMeshT.Add(new List<int>());
                    //UnityMesh.SubmeshTriangles.(new List<int>());
                }

                List<FaceTriangle> faceTriangles = face.Triangulate();


                foreach (FaceTriangle faceTriangle in faceTriangles)
                {
                    //faceTriangle.Vertices.Reverse();
                    foreach (Vertex vertex in faceTriangle.Vertices)
                    {
                        var normal = vertex[face].Normal;
                        var tangent = vertex[face].Tangent;
                        var binormal = vertex[face].Binormal;

                        v.Add(vertex.Position.FlipYZ().ToVector3());
                        n.Add(normal.FlipYZ().ToVector3());
                        c.Add(vertex[face].Color.ToUnityColor());
                        t.Add((new Vector4D(tangent, tangent.Cross(normal).Dot(binormal) > 0 ? 1f : -1f).ToVector4()));
                        uv.Add((vertex[face].UV0 * new Vector2D(1, -1)).ToVector2());

                        subMeshT[index].Add(indexerValue++);
                        //meshFilter.mesh.triangles
                        //UnityMesh.Normals.Add(normal.FlipYZ());
                        //UnityMesh.Colors.Add(vertex[face].Color);
                        //UnityMesh.Tangents.Add(new Vector4(tangent, tangent.Cross(normal).Dot(binormal) > 0 ? 1f : -1f));
                        //UnityMesh.Uvs.Add(vertex[face].UV0 * new Vector2(1, -1));
                        //UnityMesh.SubmeshTriangles[index].Add(indexerValue++);

                    }

                }

            }



            m.vertices = v.ToArray();
            m.normals = n.ToArray();
            m.colors = c.ToArray();
            m.uv = uv.ToArray();
            m.tangents = t.ToArray();
            m.subMeshCount = subMeshT.Count;
            m.RecalculateBounds();
            for (int i = 0; i < subMeshT.Count; i++)
            {
                m.SetTriangles(subMeshT[i], i);
            }

            ////meshFilter.mesh.subMeshCount= uv.ToArray();


            meshFilter.sharedMesh = m;

            return obj;
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
