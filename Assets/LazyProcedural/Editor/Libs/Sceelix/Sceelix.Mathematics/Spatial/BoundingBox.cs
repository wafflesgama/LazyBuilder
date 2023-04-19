using System;
using System.Collections.Generic;
using System.Linq;
using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Spatial
{
    /// <summary>
    /// This is a bounding box structure
    /// </summary>
    public class BoundingBox
    {
        private UnityEngine.Vector3 _max;
        private UnityEngine.Vector3 _min;



        /// <summary>
        /// Constructor for a boundingbox with infinite inverted sizes (minimum is +Inf and maximum is -Inf).
        /// </summary>
        public BoundingBox()
        {
            _min = UnityEngine.Vector3.Infinity;
            _max = -UnityEngine.Vector3.Infinity;
        }



        public BoundingBox(float left, float top, float front, float right, float bottom, float back)
        {
            _min = new UnityEngine.Vector3(left, top, front);
            _max = new UnityEngine.Vector3(right, bottom, back);
        }



        public BoundingBox(IEnumerable<UnityEngine.Vector3> points) : this()
        {
            foreach (UnityEngine.Vector3 vector3D in points) AddPoint(vector3D);
        }



        public BoundingBox(UnityEngine.Vector3 min, UnityEngine.Vector3 max)
        {
            _min = min;
            _max = max;

            if (min.x > max.x || min.y > max.y || min.z > max.z)
                throw new ArgumentException("Coordinates in 'min' cannot be greater than 'max'.");
        }



        public BoundingBox(float width, float height, float depth)
        {
            _min = new UnityEngine.Vector3(0, 0, 0);
            _max = new UnityEngine.Vector3(width, height, depth);
        }



        public BoundingRectangle BoundingRectangle => new BoundingRectangle(new UnityEngine.Vector2(_min), new UnityEngine.Vector2(_max));


        /// <summary>
        /// Gets a bounding sphere that fits inside this bounding box.
        /// </summary>
        /// <value>
        /// The bounding sphere.
        /// </value>
        public BoundingSphere BoundingSphere => new BoundingSphere(_min + UnityEngine.Vector3.Scale(_max - _min, 0.5f), UnityEngine.Vector3.Scale(_max - _min, 0.5f).Length);



        /// <summary>
        /// This property will translate the bounding box to the given center
        /// </summary>
        public UnityEngine.Vector3 Center
        {
            get
            {
                return _min + (_max - _min) / 2f; // boundingsphere.Center;
            }
            set
            {
                UnityEngine.Vector3 halfDiagonally = UnityEngine.Vector3.Scale(_max - _min, 0.5f);
                _min = value - halfDiagonally;
                _max = value + halfDiagonally;
                //_boundingsphere.Center = value;
            }
        }



        /// <summary>
        /// This will return the 8 corners
        /// </summary>
        public UnityEngine.Vector3[] Corners
        {
            get
            {
                //vectors
                UnityEngine.Vector3 diagonally = _max - _min;
                UnityEngine.Vector3 width = new UnityEngine.Vector3(diagonally.x, 0, 0);
                UnityEngine.Vector3 height = new UnityEngine.Vector3(0, diagonally.y, 0);
                UnityEngine.Vector3 depth = new UnityEngine.Vector3(0, 0, diagonally.z);
                UnityEngine.Vector3[] ret = new UnityEngine.Vector3[8];

                for (int index = 0; index < 8; index++)
                {
                    //coordinates
                    int x = index & 1;
                    int y = (index & 2) / 2;
                    int z = (index & 4) / 4;

                    //result
                    ret[index] = _min + width * x + height * y + depth * z;
                }

                return ret;
            }
        }



        /// <summary>
        /// Gets the height (size in Z) of the boundingbox.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public float Height => _max.z - _min.z;


        public bool IsInfinity => _min.IsInfinity || _max.IsInfinity;


        /// <summary>
        /// Gets the length (size in Y) of the bounding box.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public float Length => _max.y - _min.y;


        /// <summary>
        /// Gets the maximum of the bounding box.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        public UnityEngine.Vector3 Max => _max;


        /// <summary>
        /// Gets the minimum of the bounding box.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        public UnityEngine.Vector3 Min => _min;


        /// <summary>
        /// Gets the size of the box in the 3 dimensions.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public UnityEngine.Vector3 Size => _max - _min;


        /// <summary>
        /// Gets the volume of the box.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        public float Volume => Height * Width * Length;


        /// <summary>
        /// Gets the width (size in X) of the bounding box.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public float Width => _max.x - _min.x;



        /// <summary>
        /// Expands the bounding box to as to contain the given point.
        /// </summary>
        /// <param name="point">The point which must be contained in the box after the expansion.</param>
        public void AddPoint(UnityEngine.Vector3 point)
        {
            if (!Contains(point))
            {
                _min = UnityEngine.Vector3.Minimize(point, _min);
                _max = UnityEngine.Vector3.Maximize(point, _max);
            }
        }



        /// <summary>
        /// Checks whether a point is inside the box.
        /// </summary>
        /// <param name="point">The point to check</param>
        /// <returns></returns>
        public bool Contains(UnityEngine.Vector3 point)
        {
            return point.x >= _min.x && point.x <= _max.x
                                     && point.y >= _min.y && point.y <= _max.y
                                     && point.z >= _min.z && point.z <= _max.z;
        }



        /// <summary>
        /// Determines whether a given target bounding box is contained inside the current one.
        /// </summary>
        /// <param name="target">The target bounding box.</param>
        /// <returns>
        ///   <c>true</c> if this bounding box contains the specified target; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(BoundingBox target)
        {
            if (target._min.x >= _min.x &&
                target._min.y >= _min.y &&
                target._min.z >= _min.z &&
                target._max.x <= _max.x &&
                target._max.y <= _max.y &&
                target._max.z <= _max.z)
                return true;

            return false;
        }



        protected bool Equals(BoundingBox other)
        {
            return _min.Equals(other._min) && _max.Equals(other._max);
        }



        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BoundingBox) obj);
        }



        public void Expand(float size)
        {
            _min -= new UnityEngine.Vector3(size, size);
            _max += new UnityEngine.Vector3(size, size);
        }



        public void Expand(UnityEngine.Vector3 size)
        {
            _min -= size;
            _max += size;
        }



        public static BoundingBox FromPoints(IEnumerable<UnityEngine.Vector3> points)
        {
            BoundingBox boundingBox = new BoundingBox();

            foreach (UnityEngine.Vector3 point in points)
                boundingBox.AddPoint(point);

            return boundingBox;
        }



        /// <summary>
        /// Checks whether the box fully contains the target box
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [Obsolete("Replaced with contains method.")]
        public bool FullyContains(BoundingBox target)
        {
            if (target._min.x >= _min.x &&
                target._min.y >= _min.y &&
                target._min.z >= _min.z &&
                target._max.x <= _max.x &&
                target._max.y <= _max.y &&
                target._max.z <= _max.z)
                return true;

            return false;
        }



        public override int GetHashCode()
        {
            unchecked
            {
                return (_min.GetHashCode() * 397) ^ _max.GetHashCode();
            }
        }



        /// <summary>
        /// This function will return one of the 8 sub boxes
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public BoundingBox GetSubBox(int index)
        {
            //vectors
            UnityEngine.Vector3 halfDiagonally = UnityEngine.Vector3.Scale(_max - _min, 0.5f);
            UnityEngine.Vector3 halfWidth = new UnityEngine.Vector3(halfDiagonally.x, 0, 0);
            UnityEngine.Vector3 halfHeight = new UnityEngine.Vector3(0, halfDiagonally.y, 0);
            UnityEngine.Vector3 halfDepth = new UnityEngine.Vector3(0, 0, halfDiagonally.z);

            //coordinates
            int x = index & 1;
            int y = (index & 2) / 2;
            int z = (index & 4) / 4;

            //result
            UnityEngine.Vector3 newLeftTopFront = _min + halfWidth * x + halfHeight * y + halfDepth * z;
            return new BoundingBox(newLeftTopFront, newLeftTopFront + halfDiagonally);
        }



        /// <summary>
        /// this will return the index of the containing sub box
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public int GetSubBoxIndex(UnityEngine.Vector3 point)
        {
            //vectors
            UnityEngine.Vector3 halfDiagonally = UnityEngine.Vector3.Scale(_max - _min, 0.5f);
            double halfWidth = halfDiagonally.x;
            double halfHeight = halfDiagonally.y;
            double halfDepth = halfDiagonally.z;
            point -= _min;

            int x = 0;
            int y = 0;
            int z = 0;
            if (point.x >= halfWidth) x = 1;
            if (point.y >= halfHeight) y = 2;
            if (point.z >= halfDepth) z = 4;

            return x | y | z;
        }



        /// <summary>
        /// this function will return the indexes touched by the target box
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        public int[] GetSubBoxIndexes(BoundingBox box)
        {
            throw new Exception("Sadly this is not implemented yet");
        }



        public BoundingBox Intersection(BoundingBox boundingBox)
        {
            var newMin = UnityEngine.Vector3.Maximize(boundingBox.Min, Min);
            var newMax = UnityEngine.Vector3.Minimize(boundingBox.Max, Max);

            if (newMin.x < newMax.x && newMin.y < newMax.y && newMin.z < newMax.z)
                return new BoundingBox(newMin, newMax);

            return null;
        }



        public static BoundingBox Intersection(IEnumerable<BoundingBox> boundingRectangles)
        {
            //start the aggregation with an infinite boundingrectangle, keep on going if null is returned (meaning no intersection exists)
            return boundingRectangles.Aggregate(new BoundingBox(-UnityEngine.Vector3.Infinity, UnityEngine.Vector3.Infinity), (valResult, val) => valResult == null ? null : valResult.Intersection(val));
        }



        /// <summary>
        /// Checks wether the box intersects the target. The boxes must be axis aligned
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool Intersects(BoundingBox target)
        {
            //combine
            UnityEngine.Vector3 combinedMin = UnityEngine.Vector3.Minimize(_min, target._min);
            UnityEngine.Vector3 combinedMax = UnityEngine.Vector3.Maximize(_max, target._max);

            if (
                combinedMax.x - combinedMin.x > Width + target.Width ||
                combinedMax.y - combinedMin.y > Length + target.Length ||
                combinedMax.z - combinedMin.z > Height + target.Height
            ) return false;

            return true;
        }



        /// <summary>
        /// Always override this or the class will be boxed
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Bounding box";
        }



        /// <summary>
        /// Merges the current boundingBox with the indicated one and returns a merged copy.
        /// </summary>
        /// <param name="boundingBox"></param>
        public BoundingBox Union(BoundingBox boundingBox)
        {
            return new BoundingBox(UnityEngine.Vector3.Minimize(boundingBox.Min, Min), UnityEngine.Vector3.Maximize(boundingBox.Max, Max));
        }



        public static BoundingBox Union(IEnumerable<BoundingBox> boundingBoxes)
        {
            return boundingBoxes.Aggregate(new BoundingBox(), (sum, val) => sum.Union(val));
        }
    }
}