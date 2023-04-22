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
using Sceelix.Core.Graphs;

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

            environment = new ProcedureEnvironment(new ResourceManager($"{PathFactory.absoluteToolPath}\\{PathFactory.SCEELIX_PATH}", Assembly.GetExecutingAssembly()));
            //CallSystemProcedureSample(2);

            MeshCreateProcedure meshProc = new MeshCreateProcedure();
            meshProc.Parameters["Primitive"].Set("Cone");



            MeshModifyProcedure meshModifyProc = new MeshModifyProcedure();
            var op = meshModifyProc.Parameters["Operation"];
            op.Parameters[0].Parameters["Amount"].Set(1);


            var meshNode = new SystemNode(meshProc, new Point(0, 0));
            var meshModNode = new SystemNode(meshModifyProc, new Point(0, 0));

            graph = new Sceelix.Core.Graphs.Graph();


            graph.AddNode(meshNode);
            graph.AddNode(meshModNode);

            Sceelix.Core.Graphs.Edge edge = new Sceelix.Core.Graphs.Edge(meshNode.OutputPorts[0], meshModNode.InputPorts[0]);
            graph.AddEdge(edge);

        }

        static ProcedureEnvironment environment;
        static Sceelix.Core.Graphs.Graph graph;
        static long elapsedMs;


        public static void CallSystemProcedureSample(float extrude)
        {
            var test = graph != null;
            //graph.Nodes.First(x => x.ProcedureType is MeshModifyProcedure).Parameters.First(x => x.Identifier == "Operation").Parameters[0].Parameters[0].Parameters.First(x => x.Identifier == "Amount").

            IndependentGraphProcedure g2 = new IndependentGraphProcedure(graph, "aa", environment);

            g2.Execute();

            //watch.Stop();
            //elapsedMs = watch.ElapsedMilliseconds;
            //Debug.Log("Time took to calculate Total " + elapsedMs);

            //var entities = g2.Outputs.DequeueAll();
            if (g2.Outputs.Count > 0)
            {
                IEntity entity = g2.Outputs[0].Peek();
                MeshCreate((MeshEntity)entity);
            }

            //this peeks all the data from all the output ports
            //List<IEntity> entities = meshProc.Outputs.PeekAll().ToList();

            //this gets (and removes) all the data from all the output ports
            //IEnumerable<IEntity> poppedEntities = meshProc.Outputs.DequeueAll();
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
