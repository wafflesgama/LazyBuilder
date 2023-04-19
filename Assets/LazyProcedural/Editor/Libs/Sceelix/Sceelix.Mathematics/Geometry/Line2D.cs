using System;
using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Geometry
{
    /// <summary>
    /// Defines an infinite Line in 3D Euclidean space.
    /// A line will be defined by a point P0 and a direction D, so that a point on the line has an equation P = P0 + m * D
    /// 
    /// Explanation at: http://paulbourke.net/geometry/lineline3d/
    /// </summary>
    public struct Line2D
    {
        private readonly UnityEngine.Vector2 _direction;
        private readonly UnityEngine.Vector2 _point0;



        public Line2D(UnityEngine.Vector2 direction, UnityEngine.Vector2 point0)
        {
            _direction = direction;
            _point0 = point0;
        }



        public float MinDistanceToPoint(UnityEngine.Vector2 point)
        {
            return Math.Abs(_direction.y * point.x - _direction.x * point.y + Point1.x * _point0.y - Point1.y * _point0.x) / _direction.Length;
        }



        /// <summary>
        /// Determines the point at the location m on the line, according to the line equation P = P0 + m * D
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public UnityEngine.Vector2 this[float m] => _direction * m + _point0;



        public UnityEngine.Vector2 Direction => _direction;


        public UnityEngine.Vector2 Point0 => _point0;


        public UnityEngine.Vector2 Point1 => _point0 + _direction;



        public static Line2D FromEndPoints(UnityEngine.Vector2 pointBegin, UnityEngine.Vector2 pointEnd)
        {
            return new Line2D(pointEnd - pointBegin, pointBegin);
        }



        public static Line2D FromPointAndDirection(UnityEngine.Vector2 direction, UnityEngine.Vector2 point0)
        {
            return new Line2D(direction, point0);
        }



        /// <summary>
        /// Finds the intersection between this line and the given circle.
        /// </summary>
        /// <param name="circleCenter">The circle center.</param>
        /// <param name="radius">The circle radius.</param>
        /// <returns>The array of intersected locations. Could be empty (if there were no intersections), or have up to 2 elements/intersections.</returns>
        /// <remarks>See http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/ for original code.</remarks>
        public UnityEngine.Vector2[] FindLineCircleIntersections(UnityEngine.Vector2 circleCenter, float radius)
        {
            var dx = Point1.x - Point0.x;
            var dy = Point1.y - Point0.y;

            var a = dx * dx + dy * dy;
            var b = 2 * (dx * (Point0.x - circleCenter.x) + dy * (Point0.y - circleCenter.y));
            var c = (Point0.x - circleCenter.x) * (Point0.x - circleCenter.x) + (Point0.y - circleCenter.y) * (Point0.y - circleCenter.y) - radius * radius;

            var det = b * b - 4 * a * c;
            if (a <= 0.0000001 || det < 0)
            {
                // No real solutions.
                return new UnityEngine.Vector2[0];
            }

            if (Math.Abs(det) < float.Epsilon)
            {
                // One solution.
                float t = -b / (2 * a);

                return new[] {new UnityEngine.Vector2(Point0.x + t * dx, Point0.y + t * dy)};
            }
            else
            {
                // Two solutions.
                float t = (float) ((-b + Math.Sqrt(det)) / (2 * a));
                var intersection1 = new UnityEngine.Vector2(Point0.x + t * dx, Point0.y + t * dy);

                t = (float) ((-b - Math.Sqrt(det)) / (2 * a));
                var intersection2 = new UnityEngine.Vector2(Point0.x + t * dx, Point0.y + t * dy);

                return new[] {intersection1, intersection2};
            }
        }
    }
}