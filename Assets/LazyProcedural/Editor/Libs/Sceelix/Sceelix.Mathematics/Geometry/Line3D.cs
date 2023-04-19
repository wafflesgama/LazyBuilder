using System;
using System.Linq;
using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Geometry
{
    /// <summary>
    /// Defines an infinite Line in 3D Euclidean space.
    /// A line will be defined by a point P0 and a direction D, so that a point on the line has an equation P = P0 + m * D
    /// 
    /// Explanation at: http://paulbourke.net/geometry/lineline3d/
    /// </summary>
    public struct Line3D
    {
        public Line3D(UnityEngine.Vector3 direction, UnityEngine.Vector3 point0)
        {
            Direction = direction;
            Point0 = point0;
        }



        /// <summary>
        /// Finds the shortest line segment between two infinite lines
        /// Algorithm from: http://paulbourke.net/geometry/pointlineplane/
        /// </summary>
        /// <param name="line">The line to check for the distance</param>
        /// <returns></returns>
        public LineSegment3D? ShortestLineBetweenTwoLines(Line3D line)
        {
            UnityEngine.Vector3 p1 = Point0;
            UnityEngine.Vector3 p3 = line.Point0;

            UnityEngine.Vector3 p21 = Direction;
            UnityEngine.Vector3 p43 = line.Direction;
            UnityEngine.Vector3 p13 = p1 - p3;

            if (Direction.Length < float.Epsilon || line.Direction.Length < float.Epsilon)
                return null;

            double d1343 = p13.Dot(p43);
            double d4321 = p43.Dot(p21);
            double d1321 = p13.x * (double) p21.x + (double) p13.y * p21.y + (double) p13.z * p21.z;
            double d4343 = p43.x * (double) p43.x + (double) p43.y * p43.y + (double) p43.z * p43.z;
            double d2121 = p21.x * (double) p21.x + (double) p21.y * p21.y + (double) p21.z * p21.z;

            double denom = d2121 * d4343 - d4321 * d4321;
            if (Math.Abs(denom) < float.Epsilon) return null;
            double numer = d1343 * d4321 - d1321 * d4343;

            double mua = numer / denom;
            double mub = (d1343 + d4321 * mua) / d4343;

            return new LineSegment3D(this[(float) mua], line[(float) mub]);
        }



        /// <summary>
        /// Finds the shortest line segment between two infinite lines
        /// Algorithm from: http://paulbourke.net/geometry/pointlineplane/
        /// </summary>
        /// <param name="line">The line to check for the distance</param>
        /// <returns></returns>
        public float? ClosestRelativeHit(Line3D line)
        {
            UnityEngine.Vector3 p1 = Point0;
            UnityEngine.Vector3 p3 = line.Point0;

            UnityEngine.Vector3 p21 = Direction;
            UnityEngine.Vector3 p43 = line.Direction;
            UnityEngine.Vector3 p13 = p1 - p3;

            if (Direction.Length < float.Epsilon || line.Direction.Length < float.Epsilon)
                return null;

            double d1343 = p13.Dot(p43);
            double d4321 = p43.Dot(p21);
            double d1321 = p13.x * (double) p21.x + (double) p13.y * p21.y + (double) p13.z * p21.z;
            double d4343 = p43.x * (double) p43.x + (double) p43.y * p43.y + (double) p43.z * p43.z;
            double d2121 = p21.x * (double) p21.x + (double) p21.y * p21.y + (double) p21.z * p21.z;

            double denom = d2121 * d4343 - d4321 * d4321;
            if (Math.Abs(denom) < float.Epsilon) return null;
            double numer = d1343 * d4321 - d1321 * d4343;

            double mua = numer / denom;
            double mub = (d1343 + d4321 * mua) / d4343;

            return (float) mua;
        }



        /// <summary>
        /// Determines the point at the location m on the line, according to the line equation P = P0 + m * D
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public UnityEngine.Vector3 this[float m] => Direction * m + Point0;



        public enum LineIntersection
        {
            CollinearOverlapping,
            CollinearDisjoint
        }

        /*public float? Intersects(Line3D plane, bool includeEnd, bool include)
        {

    
    
        }*/



        private static bool LinesIntersect(UnityEngine.Vector3 A, UnityEngine.Vector3 B, UnityEngine.Vector3 C, UnityEngine.Vector3 D)
        {
            UnityEngine.Vector3 CmP = new UnityEngine.Vector3(C.x - A.x, C.y - A.y);
            UnityEngine.Vector3 r = new UnityEngine.Vector3(B.x - A.x, B.y - A.y);
            UnityEngine.Vector3 s = new UnityEngine.Vector3(D.x - C.x, D.y - C.y);

            float CmPxr = CmP.x * r.y - CmP.y * r.x;
            float CmPxs = CmP.x * s.y - CmP.y * s.x;
            float rxs = r.x * s.y - r.y * s.x;

            if (CmPxr == 0f)
                // Lines are collinear, and so intersect if they have any overlap

                return C.x - A.x < 0f != C.x - B.x < 0f
                       || C.y - A.y < 0f != C.y - B.y < 0f;

            if (rxs == 0f)
                return false; // Lines are parallel.

            float rxsr = 1f / rxs;
            float t = CmPxs * rxsr;
            float u = CmPxr * rxsr;

            return t >= 0f && t <= 1f && u >= 0f && u <= 1f;
        }



        /// <summary>
        /// Determines if the line intersects with the plane.
        /// Algorithm based on: http://paulbourke.net/geometry/planeline/
        /// </summary>
        /// <param name="plane">Plane to calculate the intersection with.</param>
        /// <returns>A value corresponding to m, in P = P0 + m * D. So to get the point, just use the [] operator. If the line is parallel or contained in the plane, then the result is null.</returns>
        public float? IntersectsPlane(Plane3D plane)
        {
            UnityEngine.Vector3 p1 = Point0;
            UnityEngine.Vector3 p2 = Point0 + Direction;

            float nominator = plane.Normal.Dot(plane.Point0 - p1);
            float denominator = plane.Normal.Dot(p2 - p1);

            //if it's 0, then the line is parallel to the plane, so no intersection
            if (Math.Abs(denominator) < float.Epsilon) return null;

            return nominator / denominator;
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

            return FindSphereIntersectionValues(sphereCenter, radius).Select(t => point0 + direction * t).ToArray();
        }



        /// <summary>
        /// Finds the intersection between this line and the given sphere, returning values between 0 and 1 relative to the line's points.
        /// </summary>
        /// <param name="sphereCenter">The sphere center.</param>
        /// <param name="radius">The sphere radius.</param>
        /// <returns>The array of intersected t values (between 0 and 1). Could be empty (if there were no intersections), or have up to 2 elements/intersections.</returns>
        /// <remarks>See http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/ for original code.</remarks>
        public float[] FindSphereIntersectionValues(UnityEngine.Vector3 sphereCenter, float radius)
        {
            float dx = Point1.x - Point0.x;
            float dy = Point1.y - Point0.y;
            float dz = Point1.z - Point0.z;

            float a = dx * dx + dy * dy + dz * dz;
            float b = 2 * (dx * (Point0.x - sphereCenter.x) + dy * (Point0.y - sphereCenter.y) + dz * (Point0.z - sphereCenter.z));
            float c = (Point0.x - sphereCenter.x) * (Point0.x - sphereCenter.x) + (Point0.y - sphereCenter.y) * (Point0.y - sphereCenter.y) + (Point0.z - sphereCenter.z) * (Point0.z - sphereCenter.z) - radius * radius;

            float det = b * b - 4 * a * c;
            if (a <= 0.0000001 || det < 0)
                // No real solutions.
                return new float[0];

            if (Math.Abs(det) < float.Epsilon)
            {
                // One solution.
                float t = -b / (2 * a);

                return new[] {t};
            }

            // Two solutions.
            float t1 = (float) ((-b + Math.Sqrt(det)) / (2 * a));
            float t2 = (float) ((-b - Math.Sqrt(det)) / (2 * a));

            return new[] {t1, t2};
        }



        public UnityEngine.Vector3 Direction
        {
            get;
        }


        public UnityEngine.Vector3 Point0
        {
            get;
        }


        public UnityEngine.Vector3 Point1 => Point0 + Direction;



        public static Line3D FromEndPoints(UnityEngine.Vector3 pointBegin, UnityEngine.Vector3 pointEnd)
        {
            return new Line3D(pointEnd - pointBegin, pointBegin);
        }



        public static Line3D FromPointAndDirection(UnityEngine.Vector3 direction, UnityEngine.Vector3 point0)
        {
            return new Line3D(direction, point0);
        }
    }
}