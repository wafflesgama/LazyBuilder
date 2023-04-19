using System.Collections.Generic;
using System.Linq;
using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Spatial
{
    public class BoundingRectangle
    {
        private UnityEngine.Vector2 _max;
        private UnityEngine.Vector2 _min;



        public BoundingRectangle()
        {
            _min = UnityEngine.Vector2.Infinity;
            _max = -UnityEngine.Vector2.Infinity;
        }



        public BoundingRectangle(Rectangle rectangle)
        {
            _min = rectangle.Min;
            _max = rectangle.Max;
        }



        public BoundingRectangle(UnityEngine.Vector2 min, UnityEngine.Vector2 max)
        {
            _min = min;
            _max = max;
        }



        public BoundingRectangle(float x, float y, float width, float height)
        {
            _min = new UnityEngine.Vector2(x, y);
            _max = new UnityEngine.Vector2(x + width, y + height);
        }



        public BoundingRectangle(params UnityEngine.Vector2[] points)
            : this((IEnumerable<UnityEngine.Vector2>) points)
        {
        }



        public BoundingRectangle(IEnumerable<UnityEngine.Vector2> points) : this()
        {
            foreach (UnityEngine.Vector2 vector2D in points) AddPoint(vector2D);
        }



        public UnityEngine.Vector2 Center => _min + (_max - _min) / 2f;


        public float Height => _max.y - _min.y;


        public bool IsEmpty => Width == 0 || Height == 0;



        public UnityEngine.Vector2 Max
        {
            get { return _max; }
            set { _max = value; }
        }



        public UnityEngine.Vector2 Min
        {
            get { return _min; }
            set { _min = value; }
        }



        public UnityEngine.Vector2 Size => new UnityEngine.Vector2(Width, Height);


        public float Width => _max.x - _min.x;


        public static BoundingRectangle Zero => new BoundingRectangle(new UnityEngine.Vector2(0, 0), new UnityEngine.Vector2(0, 0));



        public void AddPoint(UnityEngine.Vector2 point)
        {
            if (!Contains(point))
            {
                _min = UnityEngine.Vector2.Minimize(point, _min);
                _max = UnityEngine.Vector2.Maximize(point, _max);
            }
        }



        /// <summary>
        /// Checks whether a point is inside the box
        /// </summary>
        /// <param name="point">The point to check</param>
        /// <returns></returns>
        public bool Contains(UnityEngine.Vector2 point)
        {
            return point.x >= _min.x && point.x <= _max.x && point.y >= _min.y && point.y <= _max.y;
        }



        public bool Contains(BoundingRectangle rectangle)
        {
            return Contains(rectangle.Min) && Contains(rectangle.Max);
        }



        public bool Equals(BoundingRectangle other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other._min.Equals(_min) && other._max.Equals(_max);
        }



        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(BoundingRectangle)) return false;
            return Equals((BoundingRectangle) obj);
        }



        public void Expand(float value)
        {
            _min -= new UnityEngine.Vector2(value, value);
            _max += new UnityEngine.Vector2(value, value);
        }



        public BoundingRectangle GetExpanded(float value)
        {
            return new BoundingRectangle(_min - new UnityEngine.Vector2(value, value), _max + new UnityEngine.Vector2(value, value));
        }



        public override int GetHashCode()
        {
            unchecked
            {
                return (_min.GetHashCode() * 397) ^ _max.GetHashCode();
            }
        }



        public BoundingRectangle Intersection(BoundingRectangle boundingRectangle)
        {
            var newMin = UnityEngine.Vector2.Maximize(boundingRectangle.Min, Min);
            var newMax = UnityEngine.Vector2.Minimize(boundingRectangle.Max, Max);

            if (newMin.x < newMax.x && newMin.y < newMax.y)
                return new BoundingRectangle(newMin, newMax);

            return null;
        }



        public static BoundingRectangle Intersection(IEnumerable<BoundingRectangle> boundingRectangles)
        {
            //start the aggregation with an infinite boundingrectangle, keep on going if null is returned (meaning no intersection exists)
            return boundingRectangles.Aggregate(new BoundingRectangle(), (valResult, val) => valResult == null ? null : valResult.Intersection(val));
        }



        public bool Intersects(BoundingRectangle target)
        {
            //combine
            UnityEngine.Vector2 combinedMin = UnityEngine.Vector2.Minimize(_min, target._min);
            UnityEngine.Vector2 combinedMax = UnityEngine.Vector2.Maximize(_max, target._max);

            if (
                combinedMax.x - combinedMin.x > Width + target.Width ||
                combinedMax.y - combinedMin.y > Height + target.Height
            ) return false;

            return true;
        }



        public BoundingRectangle Union(BoundingRectangle boundingBox)
        {
            return new BoundingRectangle(UnityEngine.Vector2.Minimize(boundingBox.Min, Min), UnityEngine.Vector2.Maximize(boundingBox.Max, Max));
        }



        public static BoundingRectangle Union(IEnumerable<BoundingRectangle> boundingRectangles)
        {
            return boundingRectangles.Aggregate(new BoundingRectangle(), (sum, val) => sum.Union(val));
        }
    }
}