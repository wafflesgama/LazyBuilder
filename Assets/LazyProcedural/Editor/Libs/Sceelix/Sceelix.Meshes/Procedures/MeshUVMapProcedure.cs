using System;
using System.Collections.Generic;
using System.Linq;
using Sceelix.Collections;
using Sceelix.Core.Annotations;
using Sceelix.Core.Parameters;
using Sceelix.Core.Procedures;
using Sceelix.Extensions;
using Sceelix.Mathematics.Data;
using Sceelix.Mathematics.Parameters;
using Sceelix.Meshes.Data;

namespace Sceelix.Meshes.Procedures
{
    /// <summary>
    /// Sets up UV mapping for meshes.
    /// </summary>
    [Procedure("9470e48f-5ee8-4d84-a371-949f27786f0f", Label = "Mesh UV Map", Category = "Mesh")]
    public class MeshUVMapProcedure : TransferProcedure<MeshEntity>
    {
        /// <summary>
        /// The UV mapping operation/shape to apply.
        /// </summary>
        private readonly SelectListParameter<UVMappingParameter> _parameterUVMapping = new SelectListParameter<UVMappingParameter>("Operation", "Face UV");


        public override IEnumerable<string> Tags => base.Tags.Union(_parameterUVMapping.SubParameterLabels);



        protected override MeshEntity Process(MeshEntity entity)
        {
            foreach (var parameter in _parameterUVMapping.Items)
                entity = parameter.UVMap(entity);

            return entity;
        }



        /// <summary>
        /// Defines the coordinate sizing for the given coordinate.
        /// </summary>
        /// <seealso cref="Sceelix.Core.Parameters.CompoundParameter" />
        public class CoordinateSizingParameter : CompoundParameter
        {
            /// <summary>
            /// Scale of the coordinate mapping.
            /// </summary>
            public readonly FloatParameter ParameterSize = new FloatParameter("Size", 1);

            /// <summary>
            ///  Indicates if the defined size is an absolute value (in world space), or relative to the chosen surrounding shape's size.
            /// </summary>
            public readonly BoolParameter ParameterAbsolute = new BoolParameter("Absolute", true);



            public CoordinateSizingParameter(string label)
                : base(label)
            {
            }
        }

        #region Abstract Parameter

        public abstract class UVMappingParameter : CompoundParameter
        {
            protected UVMappingParameter(string label)
                : base(label)
            {
            }



            protected internal abstract MeshEntity UVMap(MeshEntity meshEntity);
        }

        #endregion

        #region FlipUV

        /// <summary>
        /// Flips the current UV mapping coordinates in the U or V axis.
        /// </summary>
        /// <seealso cref="Sceelix.Meshes.Procedures.MeshUVMapProcedure.UVMappingParameter" />
        public class FlipUVParameter : UVMappingParameter
        {
            /// <summary>
            /// Indicates if the U coordinate should be flipped.
            /// </summary>
            private readonly BoolParameter _parameterFlipU = new BoolParameter("Flip U", true);

            /// <summary>
            /// Indicates if the V coordinate should be flipped.
            /// </summary>
            private readonly BoolParameter _parameterFlipV = new BoolParameter("Flip V", true);



            public FlipUVParameter()
                : base("Flip UV")
            {
            }



            public static MeshEntity Apply(MeshEntity meshEntity, bool flipU, bool flipV)
            {
                foreach (Face face in meshEntity)
                foreach (HalfVertex halfVertex in face.HalfVertices)
                {
                    UnityEngine.Vector2 textureCoordinate = halfVertex.UV0;
                    float u = textureCoordinate.x, v = textureCoordinate.y;
                    if (flipU)
                        u = -u;
                    if (flipV)
                        v = -v;

                    halfVertex.UV0 = new UnityEngine.Vector2(u, v);
                }

                return meshEntity;
            }



            protected internal override MeshEntity UVMap(MeshEntity meshEntity)
            {
                return Apply(meshEntity, _parameterFlipU.Value, _parameterFlipV.Value);
            }
        }

        #endregion

        #region Scale UV

        /// <summary>
        /// Scale the UV coordinates.
        /// </summary>
        /// <seealso cref="Sceelix.Meshes.Procedures.MeshUVMapProcedure.UVMappingParameter" />
        public class ScaleUVParameter : UVMappingParameter
        {
            /// <summary>
            /// The value to multiply with the UV coordinates.
            /// </summary>
            private readonly Vector2Parameter _parameterAmount = new Vector2Parameter("Amount", new UnityEngine.Vector2(1, 1));



            public ScaleUVParameter()
                : base("Scale UV")
            {
            }



            public static MeshEntity Apply(MeshEntity meshEntity, UnityEngine.Vector2 amount)
            {
                foreach (Face face in meshEntity)
                foreach (HalfVertex halfVertex in face.HalfVertices)
                {
                    UnityEngine.Vector2 textureCoordinate = halfVertex.UV0;
                    halfVertex.UV0 = new UnityEngine.Vector2(textureCoordinate.x * amount.x, textureCoordinate.y * amount.y);
                }

                return meshEntity;
            }



            protected internal override MeshEntity UVMap(MeshEntity meshEntity)
            {
                return Apply(meshEntity, _parameterAmount.Value);
            }
        }

        #endregion

        #region Cylinder UV Mapping

        /// <summary>
        /// Applies UV mapping coordinates based on a surrounding cylinder shape.
        /// </summary>
        /// <seealso cref="Sceelix.Meshes.Procedures.MeshUVMapProcedure.UVMappingParameter" />
        public class CylinderUVMappingParameter : UVMappingParameter
        {
            /// <summary>
            /// U coordinate map sizing. 
            /// </summary>
            private readonly CoordinateSizingParameter _parameterU = new CoordinateSizingParameter("U");

            /// <summary>
            /// V coordinate map sizing. 
            /// </summary>
            private readonly CoordinateSizingParameter _parameterV = new CoordinateSizingParameter("V");



            public CylinderUVMappingParameter()
                : base("Cylinder UV")
            {
            }



            public static MeshEntity Apply(MeshEntity meshEntity, float uSize, bool absoluteU, float vSize, bool absoluteV)
            {
                UnityEngine.Vector3 centroid = meshEntity.Centroid;
                UnityEngine.Vector2 centroid2D = meshEntity.BoxScope.ToScopePosition(centroid).ToVector2();

                float perimeter = (float) (Math.Max(meshEntity.BoxScope.Sizes.x, meshEntity.BoxScope.Sizes.y) * Math.PI);

                /*foreach (var vertex in meshEntity.FaceVertices)
                {
                    UnityEngine.Vector3 direction = vertex.Position - centroid;
                    //UnityEngine.Vector3 scopeDirection2 = MeshEntity.BoxScope.ToScopeDirection(direction);
                    UnityEngine.Vector3 scopeDirection = meshEntity.BoxScope.ToScopeDirection(direction);

                    UnityEngine.Vector3 normalizedScopeDirection = scopeDirection.normalized;

                    float u = 0.5f + (float) (Math.Atan2(normalizedScopeDirection.x, normalizedScopeDirection.y)/(2.0*Math.PI));
                    float v = -meshEntity.BoxScope.ToScopePosition(vertex.Position).z/meshEntity.BoxScope.Sizes.z;

                    u *= absoluteU ? uSize*perimeter : uSize;
                    v *= absoluteV ? vSize*meshEntity.BoxScope.Sizes.z : vSize;

                    vertex.SetAttribute(new GlobalAttributeKey("Section",new HighlightMeta()), u);

                    foreach (HalfVertex halfVertex in vertex.HalfVertices)
                        halfVertex.UV0 = new UnityEngine.Vector2(u, v);
                }*/

                foreach (var face in meshEntity)
                foreach (var halfVertex in face.HalfVertices)
                {
                    UnityEngine.Vector3 scopeDirection = meshEntity.BoxScope.ToScopePosition(halfVertex.Vertex.Position) - centroid2D.ToVector3(halfVertex.Vertex.Position.z);
                    if (Math.Abs(scopeDirection.Length) < float.Epsilon)
                        scopeDirection = halfVertex.Face.Normal;

                    UnityEngine.Vector3 normalizedScopeDirection = scopeDirection.normalized;

                    float u = 1 - (0.5f + (float) (Math.Atan2(normalizedScopeDirection.x, normalizedScopeDirection.y) / (2.0 * Math.PI)));
                    float v = -meshEntity.BoxScope.ToScopePosition(halfVertex.Vertex.Position).z / meshEntity.BoxScope.Sizes.z;

                    u *= absoluteU ? uSize * perimeter : uSize;
                    v *= absoluteV ? vSize * meshEntity.BoxScope.Sizes.z : vSize;

                    halfVertex.UV0 = new UnityEngine.Vector2(u, v);
                }


                foreach (var face in meshEntity)
                foreach (var halfVertex in face.HalfVertices)
                {
                    Edge emanatingEdge = halfVertex.GetEmanatingEdge();

                    UnityEngine.Vector3 vector3D = emanatingEdge.Direction.Cross(halfVertex.Face.Normal);

                    UnityEngine.Vector3 d = meshEntity.BoxScope.ToScopeDirection(vector3D);

                    if (d.z >= 0 && halfVertex.UV0.x < emanatingEdge.V1[face].UV0.x)
                        halfVertex.UV0 = new UnityEngine.Vector2(halfVertex.UV0.x + (absoluteU ? perimeter : 1.0f), halfVertex.UV0.y);

                    else if (d.z < 0 && halfVertex.UV0.x > emanatingEdge.V1[face].UV0.x)
                        emanatingEdge.V1[face].UV0 = new UnityEngine.Vector2(emanatingEdge.V1[face].UV0.x + (absoluteU ? perimeter : 1.0f), emanatingEdge.V1[face].UV0.y);
                }

                return meshEntity;
            }



            protected internal override MeshEntity UVMap(MeshEntity meshEntity)
            {
                return Apply(meshEntity, _parameterU.ParameterSize.Value, _parameterU.ParameterAbsolute.Value, _parameterV.ParameterSize.Value, _parameterV.ParameterAbsolute.Value);
            }
        }

        #endregion

        #region FaceUV Mapping

        /// <summary>
        /// Applies UV mapping coordinates based on each face's surrounding plane scope shape.
        /// </summary>
        /// <seealso cref="Sceelix.Meshes.Procedures.MeshUVMapProcedure.UVMappingParameter" />
        public class FaceUVMappingParameter : UVMappingParameter
        {
            /// <summary>
            /// U coordinate map sizing. 
            /// </summary>
            private readonly CoordinateSizingParameter _parameterU = new CoordinateSizingParameter("U");

            /// <summary>
            /// V coordinate map sizing. 
            /// </summary>
            private readonly CoordinateSizingParameter _parameterV = new CoordinateSizingParameter("V");



            public FaceUVMappingParameter()
                : base("Face UV")
            {
            }



            public static MeshEntity Apply(MeshEntity meshEntity, float uSize, bool absoluteU, float vSize, bool absoluteV)
            {
                foreach (Face face in meshEntity)
                {
                    //PlaneScope planeScope = new PlaneScope(face);
                    BoxScope planeScope = face.GetDerivedScope(meshEntity.BoxScope);
                    UVMap(face, planeScope, absoluteU, absoluteV, uSize, vSize);

                    //face.CalculateTangentAndBinormal();
                }

                return meshEntity;
            }



            protected internal override MeshEntity UVMap(MeshEntity meshEntity)
            {
                return Apply(meshEntity, _parameterU.ParameterSize.Value, _parameterU.ParameterAbsolute.Value, _parameterV.ParameterSize.Value, _parameterV.ParameterAbsolute.Value);
            }



            public static void UVMap(Face face, BoxScope planeScope, bool absoluteSizingU, bool absoluteSizingV, float u, float v)
            {
                foreach (Vertex vertex in face.Vertices)
                    UVMap(vertex, planeScope, face, absoluteSizingU, absoluteSizingV, u, v);

                if (face.HasHoles)
                    foreach (CircularList<Vertex> vertexList in face.Holes)
                    foreach (Vertex vertex in vertexList)
                        UVMap(vertex, planeScope, face, absoluteSizingU, absoluteSizingV, u, v);
            }



            private static void UVMap(Vertex vertex, BoxScope planeScope, Face face, bool absoluteSizingU, bool absoluteSizingV, float u, float v)
            {
                UnityEngine.Vector3 relativePosition = vertex.Position - planeScope.Translation;

                if (relativePosition == UnityEngine.Vector3.zero)
                {
                    vertex[face].UV0 = new UnityEngine.Vector2(0, 0);
                    vertex[face].Tangent = planeScope.XAxis;
                    vertex[face].Binormal = -planeScope.YAxis;
                }
                else
                {
                    float xScopeLength = planeScope.Sizes.x;
                    float yScopeLength = planeScope.Sizes.y;

                    double dX = relativePosition.Dot(planeScope.XAxis);
                    double dY = relativePosition.Dot(planeScope.YAxis);

                    dX = absoluteSizingU ? dX / u : dX / xScopeLength * u;
                    dY = absoluteSizingV ? dY / v : dY / yScopeLength * v;


                    vertex[face].UV0 = new UnityEngine.Vector2((float) dX, -(float) dY);
                    vertex[face].Tangent = planeScope.XAxis;
                    vertex[face].Binormal = -planeScope.YAxis;
                }
            }
        }

        #endregion

        #region BoxUV Mapping

        /// <summary>
        /// Applies UV mapping coordinates based on a surrounding box shape.
        /// </summary>
        public class BoxUVMappingParameter : UVMappingParameter
        {
            /// <summary>
            /// U coordinate map sizing. 
            /// </summary>
            private readonly CoordinateSizingParameter _parameterU = new CoordinateSizingParameter("U");

            /// <summary>
            /// V coordinate map sizing. 
            /// </summary>
            private readonly CoordinateSizingParameter _parameterV = new CoordinateSizingParameter("V");



            public BoxUVMappingParameter()
                : base("Box UV")
            {
            }



            public static MeshEntity Apply(MeshEntity meshEntity, float uSize, bool absoluteU, float vSize, bool absoluteV)
            {
                List<BoxScope> sideScopes = GetSideScopes(meshEntity.BoxScope).ToList();

                foreach (Face face in meshEntity)
                {
                    KeyValuePair<BoxScope, float> keyValuePair = sideScopes.SelectMin(val => val.ZAxis.AngleTo(face.Normal));

                    //PlaneScope planeScope = new PlaneScope(face);
                    UVMap(face, keyValuePair.Key, absoluteU, absoluteV, uSize, vSize);
                }

                return meshEntity;
            }



            private static IEnumerable<BoxScope> GetSideScopes(BoxScope boxScope)
            {
                //top
                yield return boxScope;

                //bottom
                yield return new BoxScope(-boxScope.XAxis, boxScope.YAxis, -boxScope.ZAxis, boxScope.Translation + boxScope.SizedXAxis + boxScope.SizedZAxis, boxScope.Sizes);

                //front
                yield return new BoxScope(-boxScope.XAxis, boxScope.ZAxis, boxScope.YAxis, boxScope.Translation + boxScope.SizedXAxis, new UnityEngine.Vector3(boxScope.Sizes.x, boxScope.Sizes.z, boxScope.Sizes.y));

                //back
                yield return new BoxScope(boxScope.XAxis, boxScope.ZAxis, -boxScope.YAxis, boxScope.Translation + boxScope.SizedYAxis, new UnityEngine.Vector3(boxScope.Sizes.x, boxScope.Sizes.z, boxScope.Sizes.y));

                //left
                yield return new BoxScope(-boxScope.YAxis, boxScope.ZAxis, -boxScope.XAxis, boxScope.Translation + boxScope.SizedYAxis, new UnityEngine.Vector3(boxScope.Sizes.y, boxScope.Sizes.z, boxScope.Sizes.x));

                //right
                yield return new BoxScope(boxScope.YAxis, boxScope.ZAxis, boxScope.XAxis, boxScope.Translation + boxScope.SizedXAxis, new UnityEngine.Vector3(boxScope.Sizes.y, boxScope.Sizes.z, boxScope.Sizes.x));
            }



            protected internal override MeshEntity UVMap(MeshEntity meshEntity)
            {
                return Apply(meshEntity, _parameterU.ParameterSize.Value, _parameterU.ParameterAbsolute.Value, _parameterV.ParameterSize.Value, _parameterV.ParameterAbsolute.Value);
            }



            public static void UVMap(Face face, BoxScope planeScope, bool absoluteSizingU, bool absoluteSizingV, float u, float v)
            {
                foreach (Vertex vertex in face.Vertices)
                    UVMap(vertex, planeScope, face, absoluteSizingU, absoluteSizingV, u, v);

                if (face.HasHoles)
                    foreach (CircularList<Vertex> vertexList in face.Holes)
                    foreach (Vertex vertex in vertexList)
                        UVMap(vertex, planeScope, face, absoluteSizingU, absoluteSizingV, u, v);
            }



            private static void UVMap(Vertex vertex, BoxScope planeScope, Face face, bool absoluteSizingU, bool absoluteSizingV, float u, float v)
            {
                UnityEngine.Vector3 relativePosition = vertex.Position - planeScope.Translation;

                if (relativePosition == UnityEngine.Vector3.zero)
                {
                    vertex[face].UV0 = new UnityEngine.Vector2(0, 0);
                    vertex[face].Tangent = planeScope.XAxis;
                    vertex[face].Binormal = -planeScope.YAxis;
                }
                else
                {
                    float xScopeLength = planeScope.Sizes.x;
                    float yScopeLength = planeScope.Sizes.y;

                    double dX = relativePosition.Dot(planeScope.XAxis);
                    double dY = relativePosition.Dot(planeScope.YAxis);

                    dX = absoluteSizingU ? dX / u : dX / xScopeLength * u;
                    dY = absoluteSizingV ? dY / v : dY / yScopeLength * v;


                    vertex[face].UV0 = new UnityEngine.Vector2((float) dX, -(float) dY);
                    vertex[face].Tangent = planeScope.XAxis;
                    vertex[face].Binormal = -planeScope.YAxis;
                }
            }
        }

        #endregion

        #region Scope UV

        /// <summary>
        /// Applies UV mapping coordinates based on the surrounding scope shape.
        /// </summary>
        /// <seealso cref="Sceelix.Meshes.Procedures.MeshUVMapProcedure.UVMappingParameter" />
        public class ScopeUVMappingParameter : UVMappingParameter
        {
            /// <summary>
            /// U coordinate map sizing. 
            /// </summary>
            private readonly CoordinateSizingParameter _parameterU = new CoordinateSizingParameter("U");

            /// <summary>
            /// V coordinate map sizing. 
            /// </summary>
            private readonly CoordinateSizingParameter _parameterV = new CoordinateSizingParameter("V");



            public ScopeUVMappingParameter()
                : base("Scope UV")
            {
            }



            public static MeshEntity Apply(MeshEntity meshEntity, float uSize, bool absoluteU, float vSize, bool absoluteV)
            {
                foreach (Face face in meshEntity) UVMap(face, meshEntity.BoxScope, absoluteU, absoluteV, uSize, vSize);

                return meshEntity;
            }



            protected internal override MeshEntity UVMap(MeshEntity meshEntity)
            {
                return Apply(meshEntity, _parameterU.ParameterSize.Value, _parameterU.ParameterAbsolute.Value, _parameterV.ParameterSize.Value, _parameterV.ParameterAbsolute.Value);
            }



            public static void UVMap(Face face, BoxScope boxScope, bool absoluteSizingU, bool absoluteSizingV, float u, float v)
            {
                foreach (Vertex vertex in face.Vertices)
                    UVMap(vertex, boxScope, face, absoluteSizingU, absoluteSizingV, u, v);

                if (face.HasHoles)
                    foreach (CircularList<Vertex> vertexList in face.Holes)
                    foreach (Vertex vertex in vertexList)
                        UVMap(vertex, boxScope, face, absoluteSizingU, absoluteSizingV, u, v);
            }



            private static void UVMap(Vertex vertex, BoxScope boxScope, Face face, bool absoluteSizingU, bool absoluteSizingV, float u, float v)
            {
                UnityEngine.Vector3 relativePosition = vertex.Position - boxScope.Translation;

                if (relativePosition == UnityEngine.Vector3.zero)
                {
                    vertex[face].UV0 = new UnityEngine.Vector2(0, 0);
                    vertex[face].Tangent = boxScope.XAxis;
                    vertex[face].Binormal = -boxScope.YAxis;
                }
                else
                {
                    float xScopeLength = boxScope.Sizes.x;
                    float yScopeLength = boxScope.Sizes.y;

                    double dX = relativePosition.Dot(boxScope.XAxis);
                    double dY = relativePosition.Dot(boxScope.YAxis);

                    dX = absoluteSizingU ? dX / u : dX / xScopeLength * u;
                    dY = absoluteSizingV ? dY / v : dY / yScopeLength * v;


                    vertex[face].UV0 = new UnityEngine.Vector2((float) dX, -(float) dY);
                    vertex[face].Tangent = boxScope.XAxis;
                    vertex[face].Binormal = -boxScope.YAxis;
                }
            }
        }

        #endregion

        #region Sphere UV

        /// <summary>
        /// Applies UV mapping coordinates based on a surrounding sphere shape.
        /// </summary>
        /// <seealso cref="Sceelix.Meshes.Procedures.MeshUVMapProcedure.UVMappingParameter" />
        public class SphereUVMapping : UVMappingParameter
        {
            /// <summary>
            /// U coordinate map sizing. 
            /// </summary>
            private readonly CoordinateSizingParameter _parameterU = new CoordinateSizingParameter("U");

            /// <summary>
            /// V coordinate map sizing. 
            /// </summary>
            private readonly CoordinateSizingParameter _parameterV = new CoordinateSizingParameter("V");



            public SphereUVMapping()
                : base("Sphere UV")
            {
            }



            private MeshEntity Apply(MeshEntity meshEntity, float uSize, bool absoluteU, float vSize, bool absoluteV)
            {
                UnityEngine.Vector3 centroid = meshEntity.Centroid;
                float perimeter = (float) (Math.Max(meshEntity.BoxScope.Sizes.x, meshEntity.BoxScope.Sizes.y) * Math.PI);

                foreach (var vertex in meshEntity.FaceVertices)
                {
                    UnityEngine.Vector3 direction = vertex.Position - centroid;
                    UnityEngine.Vector3 scopeDirection = meshEntity.BoxScope.ToScopeDirection(direction).normalized;

                    float u = 0.5f + (float) (Math.Atan2(scopeDirection.x, scopeDirection.y) / (2.0 * Math.PI));
                    float v = 0.5f - (float) (Math.Asin(scopeDirection.z) / Math.PI);

                    u *= absoluteU ? uSize * perimeter : uSize;
                    v *= absoluteV ? vSize * meshEntity.BoxScope.Sizes.z : vSize;

                    foreach (HalfVertex halfVertex in vertex.HalfVertices)
                        halfVertex.UV0 = new UnityEngine.Vector2(u, v);

                    //if (position.z > 9f && position.z < 11f)
                    //    Console.WriteLine(new UnityEngine.Vector2(u,v));
                }

                foreach (var face in meshEntity)
                foreach (var halfVertex in face.HalfVertices)
                {
                    Edge emanatingEdge = halfVertex.GetEmanatingEdge();

                    UnityEngine.Vector3 vector3D = emanatingEdge.Direction.Cross(halfVertex.Vertex.Position - centroid);

                    UnityEngine.Vector3 d = meshEntity.BoxScope.ToScopeDirection(vector3D);

                    if (d.z < 0 && halfVertex.UV0.x < emanatingEdge.V1[face].UV0.x)
                        halfVertex.UV0 = new UnityEngine.Vector2(halfVertex.UV0.x + 1.0f, halfVertex.UV0.y);
                    else if (d.z > 0 && halfVertex.UV0.x > emanatingEdge.V1[face].UV0.x)
                        emanatingEdge.V1[face].UV0 = new UnityEngine.Vector2(emanatingEdge.V1[face].UV0.x + 1.0f, emanatingEdge.V1[face].UV0.y);
                }

                return meshEntity;
            }



            protected internal override MeshEntity UVMap(MeshEntity meshEntity)
            {
                return Apply(meshEntity, _parameterU.ParameterSize.Value, _parameterU.ParameterAbsolute.Value, _parameterV.ParameterSize.Value, _parameterV.ParameterAbsolute.Value);
            }
        }

        #endregion
    }
}