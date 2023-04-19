using System;
using System.Linq;
using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Geometry
{
    /// <summary>
    /// Defines an limited Line Segment in 3D Euclidean space.
    /// The segment defined by a point P0 a point P1
    /// 
    /// </summary>
    public struct LineSegment3D
    {
        public LineSegment3D(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1)
        {
            Point0 = point0;
            Point1 = point1;
        }



        public UnityEngine.Vector3 Point0
        {
            get;
        }


        public UnityEngine.Vector3 Point1
        {
            get;
        }


        public bool IsValid => !Point0.Equals(Point1);


        public float Length => (Point1 - Point0).Length;

        /// <summary>
        /// Non-normalized direction of the segment.
        /// </summary>
        public UnityEngine.Vector3 Direction => Point1 - Point0;


        /// <summary>
        /// Calculated middle point between the start and the end points
        /// </summary>
        public UnityEngine.Vector3 Middle => Point0 + Direction / 2f;



        public UnityEngine.Vector3? IntersectWith(Plane3D plane3D)
        {
            Line3D line = new Line3D(Point1 - Point0, Point0);
            float? intersection = line.IntersectsPlane(plane3D);
            if (intersection.HasValue)
                //if the returned value is between 0 and 1, the intersection is set between the values
                if (intersection.Value >= 0 && intersection.Value <= 1)
                    return line[intersection.Value];

            //otherwise, there is no intersection
            return null;
        }



        public bool Intersects(LineSegment3D value, bool includeEnds)
        {
            return !Intersection(value, includeEnds).IsNaN;
        }



        public UnityEngine.Vector3 Intersection(LineSegment3D line, bool includeEnds)
        {
            UnityEngine.Vector3 p1 = Point0;
            UnityEngine.Vector3 p3 = line.Point0;

            UnityEngine.Vector3 p21 = Direction;
            UnityEngine.Vector3 p43 = line.Direction;
            UnityEngine.Vector3 p13 = p1 - p3;

            if (Direction.Length < float.Epsilon || line.Direction.Length < float.Epsilon)
                return UnityEngine.Vector3.NaN;

            double d1343 = p13.Dot(p43);
            double d4321 = p43.Dot(p21);
            double d1321 = p13.x * (double) p21.x + (double) p13.y * p21.y + (double) p13.z * p21.z;
            double d4343 = p43.x * (double) p43.x + (double) p43.y * p43.y + (double) p43.z * p43.z;
            double d2121 = p21.x * (double) p21.x + (double) p21.y * p21.y + (double) p21.z * p21.z;

            double denom = d2121 * d4343 - d4321 * d4321;
            if (Math.Abs(denom) < float.Epsilon) return UnityEngine.Vector3.Infinity;
            double numer = d1343 * d4321 - d1321 * d4343;

            double a = numer / denom;
            double b = (d1343 + d4321 * a) / d4343;

            if (includeEnds)
            {
                if (a < 0 || a > 1 || b < 0 || b > 1)
                    return UnityEngine.Vector3.NaN;
            }
            else
            {
                if (a <= 0 || a >= 1 || b <= 0 || b >= 1)
                    return UnityEngine.Vector3.NaN;
            }

            return this[(float) a];
        }



        /// <summary>
        /// Determines the point at the location m on the line, according to the line equation P = P0 + m * D
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public UnityEngine.Vector3 this[float m] => Direction * m + Point0;



        public UnityEngine.Vector3 ClosestPointOnLine(UnityEngine.Vector3 point)
        {
            var lineLength = Length;
            var lineDir = Direction / lineLength;
            var distance = UnityEngine.Vector3.Dot(point - Point0, lineDir);

            distance = Math.Min(Math.Max(0, distance), lineLength);
            /*if (distance <= 0)
                return _start;

            if (distance >= lineLength)
                return _end;*/

            return Point0 + lineDir * distance;
        }



        /// <summary>
        /// Find the minimum distance from this line segment to the given point.
        /// </summary>
        public float MinDistanceTo(UnityEngine.Vector3 point)
        {
            return (ClosestPointOnLine(point) - point).Length;
        }



        /// <summary>
        /// Finds the intersection between this line and the given sphere.
        /// </summary>
        /// <param name="sphereCenter">The sphere center.</param>
        /// <param name="radius">The sphere radius.</param>
        /// <returns>The array of intersected locations. Could be empty (if there were no intersections), or have up to 2 elements/intersections.</returns>
        /// <remarks>See http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/ for original code.</remarks>
        public UnityEngine.Vector3[] FindSphereIntersectionPoints(UnityEngine.Vector3 sphereCenter, float radius)
        {
            var direction = Direction;
            var point0 = Point0;

            var line3D = new Line3D(direction, Point0);

            return line3D.FindSphereIntersectionValues(sphereCenter, radius).Where(t => t >= 0 && t <= 1).Select(t => point0 + direction * t).ToArray();
        }



        /// <summary>
        /// Indicates if this line segments intersects the sphere.
        /// </summary>
        /// <param name="sphereCenter">The sphere center.</param>
        /// <param name="radius">The radius.</param>
        /// <returns><c>true</c> if the line segment intersects the sphere, <c>false</c> otherwise.</returns>
        public bool IntersectsSphere(UnityEngine.Vector3 sphereCenter, float radius)
        {
            var line3D = new Line3D(Direction, Point0);

            return line3D.FindSphereIntersectionValues(sphereCenter, radius).Any(t => t >= 0 && t <= 1);
        }
    }
}