using System;
using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Geometry
{
    /// <summary>
    /// Defines a line segment in 2D space.
    /// </summary>
    public class LineSegment2D
    {
        private UnityEngine.Vector2 _start;
        private UnityEngine.Vector2 _end;



        public LineSegment2D(UnityEngine.Vector2 start, UnityEngine.Vector2 end)
        {
            _start = start;
            _end = end;
        }



        /// <summary> 
        /// Gets the center of this line segment
        /// </summary>
        public UnityEngine.Vector2 Center => new UnityEngine.Vector2(0.5f * (_start.x + _end.x), 0.5f * (_start.y + _end.y));


        public UnityEngine.Vector2 Direction => _end - _start;


        public UnityEngine.Vector2 End => _end;


        /// <summary>
        /// Gets the length of this <see cref="LineSegment2D"/>.
        /// </summary>
        public float Length => Direction.Length;


        /// <summary>
        /// Gets the squared length of this <see cref="LineSegment2D"/>.
        /// </summary>
        public float LengthSquared => Direction.SquareLength;


        /// <summary> 
        /// Gets the normal of the line Segment. 
        /// </summary>
        public UnityEngine.Vector2 Normal => new UnityEngine.Vector2(_start.y - _end.y, _end.x - _start.x).normalized;


        public UnityEngine.Vector2 Start => _start;



        /// <summary>
        /// Find the closest point between <see cref="_start"/> and <see cref="_end"/>.
        /// </summary>
        public UnityEngine.Vector2 ClosestPointOnLine(UnityEngine.Vector2 point)
        {
            var lineLength = Length;
            var lineDir = Direction / lineLength;
            var distance = UnityEngine.Vector2.Dot(point - _start, lineDir);

            distance = Math.Min(Math.Max(0, distance), lineLength);
            /*if (distance <= 0)
                return _start;

            if (distance >= lineLength)
                return _end;*/

            return _start + lineDir * distance;
        }



        public UnityEngine.Vector2? Intersection(LineSegment2D value, bool includeEnds)
        {
            float x1 = _end.x - _start.x;
            float y1 = _end.y - _start.y;
            float x2 = value._end.x - value._start.x;
            float y2 = value._end.y - value._start.y;
            float d = x1 * y2 - y1 * x2;

            if (Math.Abs(d) < float.Epsilon)
                return null;

            float x3 = value._start.x - _start.x;
            float y3 = value._start.y - _start.y;
            float t = (x3 * y2 - y3 * x2) / d;
            float u = (x3 * y1 - y3 * x1) / d;

            if (includeEnds)
            {
                if (t < 0 || t > 1 || u < 0 || u > 1)
                    return null;
            }
            else
            {
                if (t <= 0 || t >= 1 || u <= 0 || u >= 1)
                    return null;
            }

            return new UnityEngine.Vector2(_start.x + t * x1, _start.y + t * y1);
        }



        public bool Intersects(LineSegment2D value, bool includeEnds)
        {
            return Intersection(value, includeEnds).HasValue;
        }



        /// <summary>
        /// Find the minimum distance from this line segment to the given point.
        /// </summary>
        public float MinDistanceTo(UnityEngine.Vector2 point)
        {
            return (ClosestPointOnLine(point) - point).Length;
        }



        /// <summary>
        /// Moves this <see cref="LineSegment2D"/> along its normal for the specified length.
        /// </summary>
        public void Offset(float length)
        {
            var normal = Normal;

            _start += normal * length;
            _end += normal * length;
        }
    }
}