using System.Collections.Generic;
using System.Linq;
using Sceelix.Mathematics.Geometry;
using Sceelix.Mathematics.Helpers;
using Sceelix.Mathematics.Spatial;

namespace Sceelix.Mathematics.Data
{
    /// <summary>
    /// The BoxScope is used for keeping track of the location, adation and size of entities.
    /// 
    /// Note: Due to being a struct, all the axes, sizes and translation are initialized
    /// with Zero vectors when the default BoxScope() constructor is used. For most practical
    /// matters, the BoxScope.Identity should be used.
    /// </summary>
    public struct BoxScope //: IDeepCloneable<BoxScope>
    {
        /// <summary>
        /// Creates an world axis oriented scope based on the given bounding box.
        /// </summary>
        public BoxScope(BoundingBox boundingBox)
        {
            XAxis = UnityEngine.Vector3.right;
            YAxis = UnityEngine.Vector3.up;
            ZAxis = UnityEngine.Vector3.forward;
            Sizes = new UnityEngine.Vector3(boundingBox.Width, boundingBox.Length, boundingBox.Height);
            Translation = boundingBox.Min;
        }



        /// <summary>
        /// Creates a custom boxscope based on the given axis directions and translation.
        /// Sizes are initialized to 0.
        /// </summary>
        /// <param name="xAxis"></param>
        /// <param name="yAxis"></param>
        /// <param name="zAxis"></param>
        /// <param name="translation"></param>
        public BoxScope(UnityEngine.Vector3 xAxis, UnityEngine.Vector3 yAxis, UnityEngine.Vector3 zAxis, UnityEngine.Vector3 translation)
        {
            XAxis = xAxis;
            YAxis = yAxis;
            ZAxis = zAxis;
            Translation = translation;
            Sizes = UnityEngine.Vector3.zero;
        }



        public BoxScope(UnityEngine.Vector3 xAxis, UnityEngine.Vector3 yAxis, UnityEngine.Vector3 zAxis, UnityEngine.Vector3 translation, UnityEngine.Vector3 sizes)
        {
            XAxis = xAxis;
            YAxis = yAxis;
            ZAxis = zAxis;
            Translation = translation;
            Sizes = sizes;
        }



        /// <summary>
        /// Creates a boxscope instance from a set of 
        /// </summary>
        /// <param name="xAxis">The x axis direction. If null, will be set to (1,0,0), unless the other two axes are not null, in which case it will be calculated from their cross product</param>
        /// <param name="yAxis">The y axis direction. If null, will be set to (0,1,0), unless the other two axes are not null, in which case it will be calculated from their cross product</param>
        /// <param name="zAxis">The z axis direction. If null, will be set to (0,0,1), unless the other two axes are not null, in which case it will be calculated from their cross product.</param>
        /// <param name="translation">The boxscope offset/translation. If null, will be set to (0,0,0).</param>
        /// <param name="sizes">The sizes of the 3 axes. If null, will be set to (0,0,0).</param>
        public BoxScope(UnityEngine.Vector3? xAxis = null, UnityEngine.Vector3? yAxis = null, UnityEngine.Vector3? zAxis = null, UnityEngine.Vector3? translation = null, UnityEngine.Vector3? sizes = null)
        {
            XAxis = xAxis ?? (yAxis.HasValue && zAxis.HasValue ? yAxis.Value.Cross(zAxis.Value) : UnityEngine.Vector3.right);
            YAxis = yAxis ?? (xAxis.HasValue && zAxis.HasValue ? zAxis.Value.Cross(xAxis.Value) : UnityEngine.Vector3.up);
            ZAxis = zAxis ?? (xAxis.HasValue && yAxis.HasValue ? xAxis.Value.Cross(yAxis.Value) : UnityEngine.Vector3.forward);

            /*_xAxis = xAxis ?? UnityEngine.Vector3.right;
            _yAxis = yAxis ?? UnityEngine.Vector3.up;
            _zAxis = zAxis ?? UnityEngine.Vector3.forward;*/
            Translation = translation ?? UnityEngine.Vector3.zero;
            Sizes = sizes ?? UnityEngine.Vector3.zero;
        }



        /// <summary>
        /// Creates an axis-oriented scope based on the given positions.
        /// </summary>
        /// <param name="positions"></param>
        public BoxScope(IEnumerable<UnityEngine.Vector3> positions)
            : this(BoundingBox.FromPoints(positions))
        {
        }



        public BoxScope(BoxScope boxScope, UnityEngine.Vector3? xAxis = null, UnityEngine.Vector3? yAxis = null, UnityEngine.Vector3? zAxis = null, UnityEngine.Vector3? translation = null, UnityEngine.Vector3? sizes = null)
        {
            XAxis = xAxis ?? boxScope.XAxis;
            YAxis = yAxis ?? boxScope.YAxis;
            ZAxis = zAxis ?? boxScope.ZAxis;
            Translation = translation ?? boxScope.Translation;
            Sizes = sizes ?? boxScope.Sizes;
        }



        #region Methods

        /// <summary>
        /// Grows the scope by, while keeping orientation, upgrades
        /// the translation and sizes so as to incorporate the new
        /// given points as well.
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public BoxScope Grow(IEnumerable<UnityEngine.Vector3> positions)
        {
            Plane3D planeX = new Plane3D(XAxis, Translation);
            Plane3D planeY = new Plane3D(YAxis, Translation);
            Plane3D planeZ = new Plane3D(ZAxis, Translation);

            var sizes = Sizes; //new UnityEngine.Vector3();

            //Different from above: this needs to check every point of the face
            foreach (UnityEngine.Vector3 vector3D in positions)
            {
                float x = Plane3DHelper.MovePlane(ref planeX, vector3D, sizes.x);
                float y = Plane3DHelper.MovePlane(ref planeY, vector3D, sizes.y);
                float z = Plane3DHelper.MovePlane(ref planeZ, vector3D, sizes.z);

                sizes = new UnityEngine.Vector3(x, y, z);
            }

            var xAxis = planeX.Normal;
            var yAxis = planeY.Normal;
            var zAxis = planeZ.Normal;

            var translation = Plane3D.CalculateIntersection(planeX, planeY, planeZ);

            return new BoxScope(xAxis, yAxis, zAxis, translation, sizes);
        }



        /// <summary>
        /// Maintains the orientation, but resets the translation and sizes
        /// and recalculates them based on the given set of points.
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public BoxScope Adjust(IEnumerable<UnityEngine.Vector3> positions)
        {
            var positionsList = positions.ToList();
            if (!positionsList.Any())
                return this;

            var resettedScope = new BoxScope(XAxis, YAxis, ZAxis, positionsList.First(), new UnityEngine.Vector3());

            return resettedScope.Grow(positionsList.Skip(1));
        }



        /// <summary>
        /// Converts a coordinate in ABSOLUTE world coordinates into this scope's ABSOLUTE coordinates.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public UnityEngine.Vector3 ToScopePosition(UnityEngine.Vector3 position)
        {
            UnityEngine.Vector3 relativePosition = position - Translation;

            float dX = relativePosition.Dot(XAxis);
            float dY = relativePosition.Dot(YAxis);
            float dZ = relativePosition.Dot(ZAxis);

            return new UnityEngine.Vector3(dX, dY, dZ);
        }



        /// <summary>
        /// Converts a coordinate in ABSOLUTE world coordinates into this scope's RELATIVE coordinates (0 - 1 range, relative to the scope sizes).
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public UnityEngine.Vector3 ToRelativeScopePosition(UnityEngine.Vector3 position)
        {
            return (ToScopePosition(position) / Sizes).MakeValid();
        }



        /// <summary>
        /// Converts a coordinate in ABSOLUTE scope coordinates into world ABSOLUTE coordinates.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public UnityEngine.Vector3 ToWorldPosition(UnityEngine.Vector3 position)
        {
            return XAxis * position.x + YAxis * position.y + ZAxis * position.z + Translation;
        }



        /// <summary>
        /// Converts a coordinate in RELATIVE scope coordinates into world ABSOLUTE coordinates.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public UnityEngine.Vector3 ToRelativeWorldPosition(UnityEngine.Vector3 position)
        {
            return ToWorldPosition(position * Sizes);
        }



        /// <summary>
        /// Converts a direction from world's orientation into this scope's orientation.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        public UnityEngine.Vector3 ToScopeDirection(UnityEngine.Vector3 direction)
        {
            float dX = direction.Dot(XAxis);
            float dY = direction.Dot(YAxis);
            float dZ = direction.Dot(ZAxis);

            return new UnityEngine.Vector3(dX, dY, dZ);
        }



        /// <summary>
        /// Converts a direction from this scope's orientation into world's orientation.
        /// </summary>
        /// <param name="scopeDirection"></param>
        /// <returns></returns>
        public UnityEngine.Vector3 ToWorldDirection(UnityEngine.Vector3 scopeDirection)
        {
            return XAxis * scopeDirection.x + YAxis * scopeDirection.y + ZAxis * scopeDirection.z;
        }



        /// <summary>
        /// Rotates the scope so that the first direction (in world space) will face the second direction (also in world space).
        /// </summary>
        /// <param name="firstDirection"></param>
        /// <param name="secondDirection"></param>
        /// <returns></returns>
        public BoxScope OrientTo(UnityEngine.Vector3 firstDirection, UnityEngine.Vector3 secondDirection, UnityEngine.Vector3 pivot)
        {
            float angleTo = firstDirection.AngleTo(secondDirection);

            UnityEngine.Vector3 rotationAxis = firstDirection.Cross(secondDirection).normalized;
            if (!rotationAxis.IsNumericallyZero && !rotationAxis.IsInfinityOrNaN)
            {
                var rotation = Matrix.CreateTranslation(pivot) * Matrix.CreateAxisAngle(rotationAxis, angleTo) * Matrix.CreateTranslation(-pivot);
                var newScope = Transform(rotation);
                return newScope;
            }

            return this;
        }



        /// <summary>
        /// Gets the coordinates, in world space, of the 8 corners of the scope.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UnityEngine.Vector3> CornerPositions
        {
            get
            {
                UnityEngine.Vector3 sizedXAxis = SizedXAxis;
                UnityEngine.Vector3 sizedYAxis = SizedYAxis;
                UnityEngine.Vector3 sizedZAxis = SizedZAxis;

                yield return Translation;
                yield return Translation + sizedXAxis;
                yield return Translation + sizedYAxis;
                yield return Translation + sizedZAxis;
                yield return Translation + sizedXAxis + sizedYAxis;
                yield return Translation + sizedXAxis + sizedZAxis;
                yield return Translation + sizedYAxis + sizedZAxis;
                yield return Translation + sizedXAxis + sizedYAxis + sizedZAxis;
            }
        }



        /// <summary>
        /// Gets a world-aligned bounding box that encloses this scope.
        /// </summary>
        public BoundingBox BoundingBox
        {
            get
            {
                BoundingBox boundingBox = new BoundingBox();

                foreach (UnityEngine.Vector3 point in CornerPositions)
                    boundingBox.AddPoint(point);

                return boundingBox;
            }
        }



        public BoxScope Transform(Matrix transformation)
        {
            //transform the origin point
            var newTranslation = transformation.Transform(Translation);

            //determine the corner points of a cube with 1 unit sizes and transform them
            var pointX = transformation.Transform(Translation + XAxis);
            var pointY = transformation.Transform(Translation + YAxis);
            var pointZ = transformation.Transform(Translation + ZAxis);

            //determine the new direction vectors from the vector between the transformed points
            var newSizedXAxis = pointX - newTranslation;
            var newSizedYAxis = pointY - newTranslation;
            var newSizedZAxis = pointZ - newTranslation;

            //these sizes are in a relative, percentage scale
            var percentageSizeX = newSizedXAxis.Length;
            var percentageSizeY = newSizedYAxis.Length;
            var percentageSizeZ = newSizedZAxis.Length;

            var transposeInverse = transformation.Inverse.Transpose;

            newSizedXAxis = transposeInverse.Transform(XAxis);
            newSizedYAxis = transposeInverse.Transform(YAxis);
            newSizedZAxis = transposeInverse.Transform(ZAxis);


            return new BoxScope(newSizedXAxis.normalized, newSizedYAxis.normalized, newSizedZAxis.normalized, newTranslation, new UnityEngine.Vector3(Sizes.x * percentageSizeX, Sizes.y * percentageSizeY, Sizes.z * percentageSizeZ).MakeValid());
        }



        public Matrix ToWorldPositionMatrix()
        {
            return new Matrix(XAxis.x, YAxis.x, ZAxis.x, Translation.x,
                XAxis.y, YAxis.y, ZAxis.y, Translation.y,
                XAxis.z, YAxis.z, ZAxis.z, Translation.z,
                0, 0, 0, 1);
        }



        public Matrix ToScopePositionMatrix()
        {
            return ToWorldPositionMatrix().Transpose;
        }



        public Matrix ToWorldDirectionMatrix()
        {
            return new Matrix(XAxis.x, YAxis.x, ZAxis.x, 0,
                XAxis.y, YAxis.y, ZAxis.y, 0,
                XAxis.z, YAxis.z, ZAxis.z, 0,
                0, 0, 0, 1);
        }



        public Matrix ToScopeDirectionMatrix()
        {
            return ToWorldDirectionMatrix().Transpose;
        }



        public BoxScope Translate(UnityEngine.Vector3 translation)
        {
            return new BoxScope(XAxis, YAxis, ZAxis, Translation + translation, Sizes);
        }



        public UnityEngine.Vector3[] ToRelativeMainPoints(BoxScope subScope)
        {
            UnityEngine.Vector3 sizedXAxis = subScope.SizedXAxis;
            UnityEngine.Vector3 sizedYAxis = subScope.SizedYAxis;
            UnityEngine.Vector3 sizedZAxis = subScope.SizedZAxis;

            UnityEngine.Vector3[] mainPoints = new UnityEngine.Vector3[7];
            mainPoints[0] = ToRelativeScopePosition(subScope.Translation);
            mainPoints[1] = ToRelativeScopePosition(subScope.Translation + sizedXAxis);
            mainPoints[2] = ToRelativeScopePosition(subScope.Translation + sizedYAxis);
            mainPoints[3] = ToRelativeScopePosition(subScope.Translation + sizedZAxis);
            mainPoints[4] = ToScopeDirection(subScope.XAxis);
            mainPoints[5] = ToScopeDirection(subScope.YAxis);
            mainPoints[6] = ToScopeDirection(subScope.ZAxis);

            return mainPoints;
        }



        public BoxScope FromRelativeMainPoints(UnityEngine.Vector3[] mainFeatures)
        {
            var translation = ToRelativeWorldPosition(mainFeatures[0]);
            var cornerX = ToRelativeWorldPosition(mainFeatures[1]);
            var cornerY = ToRelativeWorldPosition(mainFeatures[2]);
            var cornerZ = ToRelativeWorldPosition(mainFeatures[3]);

            var directionX = cornerX - translation;
            var directionY = cornerY - translation;
            var directionZ = cornerZ - translation;

            var sizeX = directionX.Length;
            var sizeY = directionY.Length;
            var sizeZ = directionZ.Length;

            //fix the directions
            directionX = ToWorldDirection(mainFeatures[4]);
            directionY = ToWorldDirection(mainFeatures[5]);
            directionZ = ToWorldDirection(mainFeatures[6]);

            return new BoxScope(directionX, directionY, directionZ,
                translation, new UnityEngine.Vector3(sizeX, sizeY, sizeZ));
        }



        public BoxScope ToRelativeScope(BoxScope subScope)
        {
            var newXAxis = ToScopeDirection(subScope.XAxis);
            var newYAxis = ToScopeDirection(subScope.YAxis);
            var newZAxis = ToScopeDirection(subScope.ZAxis);

            var newTranslation = ToRelativeScopePosition(subScope.Translation);


            var newSizeX = ToWorldDirection(subScope.XAxis) * Sizes.x;
            var newSizeY = ToWorldDirection(subScope.YAxis) * Sizes.y;

            //newSizeX.

            var newSizes = (subScope.Sizes / Sizes).MakeValid();

            return new BoxScope(newXAxis, newYAxis, newZAxis, newTranslation, newSizes);
        }



        public BoxScope FromRelativeScope(BoxScope subScope)
        {
            var newXAxis = ToWorldDirection(subScope.XAxis);
            var newYAxis = ToWorldDirection(subScope.YAxis);
            var newZAxis = ToWorldDirection(subScope.ZAxis);

            var newTranslation = ToRelativeWorldPosition(subScope.Translation);
            var newSizes = subScope.Sizes * Sizes;

            return new BoxScope(newXAxis, newYAxis, newZAxis, newTranslation, newSizes);
        }



        /*public BoxScope DeepClone()
        {
            return new BoxScope(XAxis, YAxis, ZAxis, Translation, Sizes);
        }*/



        public override string ToString()
        {
            //return String.Format("Sizes: {0}, Translation: {1}, XAxis: {2}, YAxis: {3}, ZAxis: {4}", _sizes, _translation, _xAxis, _yAxis, _zAxis);
            return string.Format("scope({0},{1},{2},{3},{4})", XAxis, YAxis, ZAxis, Sizes, Translation);
        }

        #endregion

        #region Properties

        public UnityEngine.Vector3 XAxis
        {
            get;
        }


        public UnityEngine.Vector3 SizedXAxis => XAxis * Sizes.x;


        public UnityEngine.Vector3 YAxis
        {
            get;
        }


        public UnityEngine.Vector3 SizedYAxis => YAxis * Sizes.y;


        public UnityEngine.Vector3 ZAxis
        {
            get;
        }


        public UnityEngine.Vector3 SizedZAxis => ZAxis * Sizes.z;


        public UnityEngine.Vector3 Translation
        {
            get;
        }


        public UnityEngine.Vector3 Sizes
        {
            get;
        }


        /// <summary>
        /// A scope located at the origin, coincident with the world axes, and with unit sizes.
        /// </summary>
        public static BoxScope Identity => new BoxScope(UnityEngine.Vector3.right, UnityEngine.Vector3.up, UnityEngine.Vector3.forward, UnityEngine.Vector3.zero, UnityEngine.Vector3.one);


        public Plane3D FrontPlane => new Plane3D(YAxis, Translation + SizedYAxis);


        public Plane3D BackPlane => new Plane3D(-YAxis, Translation);


        public Plane3D LeftPlane => new Plane3D(-XAxis, Translation);


        public Plane3D RightPlane => new Plane3D(XAxis, Translation + SizedXAxis);


        public Plane3D TopPlane => new Plane3D(ZAxis, Translation + SizedZAxis);


        public Plane3D BottomPlane => new Plane3D(-ZAxis, Translation);


        public UnityEngine.Vector3 Centroid => Translation + SizedXAxis / 2f + SizedYAxis / 2f + SizedZAxis / 2f;



        public BoundingRectangle BoundingRectangle
        {
            get
            {
                BoundingRectangle boundingRectangle = new BoundingRectangle();

                foreach (UnityEngine.Vector3 point in CornerPositions)
                    boundingRectangle.AddPoint(point.ToVector2());

                return boundingRectangle;
            }
        }



        public bool IsSkewed
        {
            get
            {
                var dot1 = XAxis.Dot(YAxis);
                var dot2 = XAxis.Dot(ZAxis);
                var dot3 = YAxis.Dot(ZAxis);

                return dot1 > 0.01f || dot2 > 0.01f || dot3 > 0.01f;
            }
        }

        #endregion
    }
}