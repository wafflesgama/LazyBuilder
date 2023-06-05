using Sceelix.Core.Annotations;
using Sceelix.Core.IO;
using Sceelix.Core.Parameters;
using Sceelix.Core.Procedures;
using Sceelix.Mathematics.Data;
using Sceelix.Meshes.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Sceelix.Meshes.Procedures
{

    /// <summary>
    /// Loads Meshes from disk from unity project
    /// </summary>
    /// <seealso cref="Sceelix.Core.Procedures.SystemProcedure" />
    [Procedure("3b9b4a33-8538-4760-85b1-b5e58e6aa149", Label = "Mesh Native Load", Category = "Mesh")]
    public class MeshNativeLoadProcedure : SystemProcedure
    {
        /// <summary>
        /// The loaded mesh entity.
        /// </summary>
        private readonly Output<MeshEntity> _output = new Output<MeshEntity>("Output");

        /// <summary>
        /// The Mesh components to be loaded 
        /// </summary>
        //private readonly ObjectParameter<Mesh> _parameterMesh = new ObjectParameter<Mesh>("Mesh");
        private readonly ListParameter<ObjectParameter<Mesh>> _parameterMeshes = new ListParameter<ObjectParameter<Mesh>>("Meshes");

        protected override void Run()
        {
            //GameObject ab = UnityEngine.Resources.Load<GameObject>("bag");    

            //Mesh body = ab.GetComponent<MeshFilter>().sharedMesh;

            ProcessMeshes(_parameterMeshes.Items.Select(x => x.Value));

        }

        private void ProcessMeshes(IEnumerable<Mesh> meshes)
        {
            List<Face> faces = new List<Face>();
            foreach (Mesh mesh in meshes)
            {
                if (mesh == null) continue;

                faces.AddRange(LoadMesh(mesh));
            }

            MeshEntity meshEntity = new MeshEntity(faces, BoxScope.Identity);
            _output.Write(meshEntity);
        }

        private List<Face> LoadMesh(Mesh mesh)
        {
            List<Face> faces = new List<Face>();

            int submeshCount = mesh.subMeshCount;
            for (int j = 0; j < submeshCount; j++)
            {
                int[] triangles = mesh.GetTriangles(j);
                Vector3[] mesh_P = mesh.vertices;
                Vector3[] mesh_N = mesh.normals;
                Vector4[] mesh_T = mesh.tangents;
                Vector2[] mesh_U = mesh.uv;
                Material mat = MaterialProcessor.CreateDefaultMaterial();

                for (int i = 0; i < triangles.Length; i += 3)
                {

                    var index0 = triangles[i];
                    var index1 = triangles[i + 1];
                    var index2 = triangles[i + 2];

                    List<Vector3D> p = new List<Vector3D>();
                    p.Add(new Vector3D(mesh_P[index0]).FlipYZ());
                    p.Add(new Vector3D(mesh_P[index1]).FlipYZ());
                    p.Add(new Vector3D(mesh_P[index2]).FlipYZ());


                    List<Vector3D> n = new List<Vector3D>();
                    n.Add(new Vector3D(mesh_N[index0]).FlipYZ());
                    n.Add(new Vector3D(mesh_N[index1]).FlipYZ());
                    n.Add(new Vector3D(mesh_N[index2]).FlipYZ());

                    List<Vector3D> t = new List<Vector3D>();
                    t.Add(new Vector3D(mesh_T[index0]).FlipYZ());
                    t.Add(new Vector3D(mesh_T[index1]).FlipYZ());
                    t.Add(new Vector3D(mesh_T[index2]).FlipYZ());

                    List<Vector2D> u = new List<Vector2D>();
                    u.Add(new Vector2D(mesh_U[index0]) * new Vector2D(1, -1));
                    u.Add(new Vector2D(mesh_U[index1]) * new Vector2D(1, -1));
                    u.Add(new Vector2D(mesh_U[index2]) * new Vector2D(1, -1));


                    List<Vertex> vertices = new List<Vertex>();
                    vertices.Add(new Vertex(p[0]));
                    vertices.Add(new Vertex(p[1]));
                    vertices.Add(new Vertex(p[2]));

                    vertices[0].Normal = n[0];
                    vertices[1].Normal = n[1];
                    vertices[2].Normal = n[2];

                    vertices[0].UV0 = u[0];
                    vertices[1].UV0 = u[1];
                    vertices[2].UV0 = u[2];



                    Face face = new Face(vertices);
                    face.Material = mat;
                    faces.Add(face);
                }

            }
            return faces;
        }


    }
}
