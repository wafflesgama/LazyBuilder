using System;
using System.Collections.Generic;
using System.Linq;
using Sceelix.Core.Data;
using Sceelix.Mathematics.Data;
using Sceelix.Meshes.Data;
using Sceelix.Meshes.Operations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sceelix.Processors
{
    //[Processor("MeshFilter")]
    public class MeshFilterProcessor
    {
        public void Process(IEntity entity, GameObject gameObject)
        {
            Dictionary<Material,int> materialSlots = new Dictionary<Material, int>();

            MeshEntity meshEntity = entity as MeshEntity;

            var meshFilter = gameObject.GetComponent<MeshFilter>();

            //If a Mesh Filter doesn't exist, create it
            if (meshFilter == null)
                meshFilter = gameObject.AddComponent<MeshFilter>();


            int indexerValue = 0;

            List<List<int>> subMeshT = new List<List<int>>();
            subMeshT.Add(new List<int>());

            List<Vector2> uv = new List<Vector2>();
            List<Vector3> v = new List<Vector3>();
            List<Vector3> n = new List<Vector3>();
            List<Vector4> t = new List<Vector4>();
            List<UnityEngine.Color> c = new List<UnityEngine.Color>();

            var m = new Mesh();
            m.name = "Mesh";

            foreach (Face face in meshEntity)
            {
                int index = 0;
                
                //Try getting the value of the entry
                if (face.Material != null && !materialSlots.TryGetValue(face.Material,out index))
                {
                    //If no value exists creates a neew slow

                    index = materialSlots.Count;
                    materialSlots.Add(face.Material,index);
                    
                    //Add only ir order is above 0, since the first array has been added beforehand
                    if (index > 0)
                        subMeshT.Add(new List<int>());
                }

                List<FaceTriangle> faceTriangles = face.Triangulate();


                foreach (FaceTriangle faceTriangle in faceTriangles)
                {
                    foreach (Vertex vertex in faceTriangle.Vertices)
                    {
                        var normal = vertex[face].Normal;
                        var tangent = vertex[face].Tangent;
                        var binormal = vertex[face].Binormal;

                        v.Add(vertex.Position.FlipYZ().ToVector3());
                        n.Add(normal.FlipYZ().ToVector3());
                        c.Add(vertex[face].Color);
                        t.Add((new Vector4D(tangent, tangent.Cross(normal).Dot(binormal) > 0 ? 1f : -1f).ToVector4()));
                        uv.Add((vertex[face].UV0 * new Vector2D(1, -1)).ToVector2());

                        subMeshT[index].Add(indexerValue++);
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

            meshFilter.sharedMesh = m;

        }
    }
}