using System.Collections.Generic;
using System.Linq;
using Sceelix.Core.Attributes;
using Sceelix.Core.Data;
using Sceelix.Mathematics.Data;
using Sceelix.Mathematics.Helpers;

namespace Sceelix.Meshes.Data
{
    public class HalfVertex : Entity
    {
        private static readonly FieldKey NormalKey = new FieldKey("Normal");
        private static readonly FieldKey ColorKey = new FieldKey("UnityEngine.Color");
        private static readonly FieldKey UV0Key = new FieldKey("UV0");
        private static readonly FieldKey TangentKey = new FieldKey("Tangent");
        private static readonly FieldKey BinormalKey = new FieldKey("Binormal");



        public HalfVertex(Face face, int faceIndex)
        {
            Face = face;
            FaceIndex = faceIndex;
        }



        public float Angle
        {
            get
            {
                float angleTo = MathHelper.ToDegrees((Previous.Position - Vertex.Position).AngleTo(Vertex.Position - Next.Position));
                return angleTo;
            }
        }



        public UnityEngine.Vector3 Binormal
        {
            get
            {
                var binormal = Attributes.TryGet(BinormalKey) as UnityEngine.Vector3?;
                if (binormal != null)
                    return binormal.Value;

                return UnityEngine.Vector3.zero;
            }
            set { Attributes.TrySet(BinormalKey, value, true); }
        }



        public UnityEngine.Color Color
        {
            get
            {
                var geometryNormal = Attributes.TryGet(ColorKey) as UnityEngine.Color?;
                if (geometryNormal.HasValue)
                    return geometryNormal.Value;

                UnityEngine.Color? vertexColor = Vertex.Color;

                return vertexColor ?? UnityEngine.Color.white;
            }
            set { Attributes.TrySet(ColorKey, value, true); }
        }



        public Face Face
        {
            get;
        }


        internal int FaceIndex
        {
            get;
            set;
        }


        public bool IsBoundary => !OtherFaces.Any();


        public Vertex Next => Face[FaceIndex + 1];



        public UnityEngine.Vector3 Normal
        {
            get
            {
                var geometryNormal = Attributes.TryGet(NormalKey) as UnityEngine.Vector3?;
                if (geometryNormal.HasValue)
                    return geometryNormal.Value;

                UnityEngine.Vector3? vertexNormal = Vertex.Normal;

                return vertexNormal ?? Face.Normal;
            }
            set { Attributes.TrySet(NormalKey, value, true); }
        }



        public IEnumerable<Face> OtherFaces
        {
            get
            {
                var currentVertex = Vertex;

                return Next.HalfVertices.Where(x => x.Next == currentVertex).Select(x => x.Face);
            }
        }



        public IEnumerable<HalfVertex> OtherHalfVertices
        {
            get
            {
                var currentVertex = Vertex;

                return Next.HalfVertices.Where(x => x.Next == currentVertex);
            }
        }



        public Vertex Previous => Face[FaceIndex - 1];



        public UnityEngine.Vector3 Tangent
        {
            get
            {
                var tangent = Attributes.TryGet(TangentKey) as UnityEngine.Vector3?;
                if (tangent != null)
                    return tangent.Value;

                return UnityEngine.Vector3.zero;
            }
            set { Attributes.TrySet(TangentKey, value, true); }
        }



        public UnityEngine.Vector2 UV0
        {
            get
            {
                var geometryNormal = Attributes.TryGet(UV0Key) as UnityEngine.Vector2?;
                if (geometryNormal.HasValue)
                    return geometryNormal.Value;

                return UnityEngine.Vector2.zero;
            }
            set { Attributes.TrySet(UV0Key, value, true); }
        }



        public HalfVertexUVCollection UVs => new HalfVertexUVCollection(this);


        public Vertex Vertex => Face[FaceIndex];



        /// <summary>
        /// Get edge that starts at this vertex
        /// </summary>
        /// <returns></returns>
        public Edge GetEmanatingEdge()
        {
            return new Edge(Vertex, Next);
        }



        public class HalfVertexUVCollection
        {
            public HalfVertexUVCollection(HalfVertex halfVertex)
            {
                HalfVertex = halfVertex;
            }



            private HalfVertex HalfVertex
            {
                get;
            }



            public UnityEngine.Vector2? this[int index]
            {
                get
                {
                    var uvCoordinate = HalfVertex.Attributes.TryGet(new FieldKey("UV" + index)) as UnityEngine.Vector2?;
                    if (uvCoordinate.HasValue)
                        return uvCoordinate.Value;

                    UnityEngine.Vector2? uv = HalfVertex.Vertex.UVs[index];

                    return uv ?? UnityEngine.Vector2.zero;
                }
                set { HalfVertex.Attributes.TrySet(new FieldKey("UV" + index), value, true); }
            }
        }
    }
}