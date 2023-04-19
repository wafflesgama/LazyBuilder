using System.Collections.Generic;
using Sceelix.Mathematics.Data;

namespace Sceelix.Meshes.Data
{
    public class Triangle
    {
        protected UnityEngine.Vector3 _normal;
        protected List<Vertex> vertices;



        public Triangle(Vertex v0, Vertex v1, Vertex v2)
        {
            vertices = new List<Vertex>(new[] {v0, v1, v2});

            CalculateNormal();
        }



        public Triangle(IEnumerable<Vertex> vertices)
        {
            this.vertices = new List<Vertex>(vertices);

            CalculateNormal();
        }



        public UnityEngine.Vector3 Normal => _normal;


        public Vertex V0 => vertices[0];


        public Vertex V1 => vertices[1];


        public Vertex V2 => vertices[2];



        public List<Vertex> Vertices
        {
            get { return vertices; }
            set { vertices = value; }
        }



        private void CalculateNormal()
        {
            _normal = UnityEngine.Vector3.Cross(V2.Position - V0.Position, V1.Position - V0.Position);
            _normal = _normal.normalized;
        }



        public static bool OtherSide(UnityEngine.Vector3 p1, UnityEngine.Vector3 a, UnityEngine.Vector3 b, UnityEngine.Vector3 c)
        {
            UnityEngine.Vector3 cp1 = UnityEngine.Vector3.Cross(b - a, p1 - a);
            UnityEngine.Vector3 cp2 = UnityEngine.Vector3.Cross(c - a, b - a);

            if (UnityEngine.Vector3.IsCollinear(cp1, cp2) && UnityEngine.Vector3.Dot(cp1, cp2) >= 0)
                //Console.WriteLine(UnityEngine.Vector3.Dot(cp1, cp2));
                //if ()
                return true;

            return false;
        }



        // http://softsurfer.com/Archive/algorithm_0104/algorithm_0104.htm
        //    Input:  P = a 3D point
        //            PL = a plane with point V0 and normal n
        //    Output: *B = base point on PL of perpendicular from P
        //    Return: the distance from P to the plane PL
        private double PointDistanceToPlane(UnityEngine.Vector3 point)
        {
            float sn = -UnityEngine.Vector3.Dot(_normal, point - V0.Position);
            float sd = UnityEngine.Vector3.Dot(_normal, _normal);
            float sb = sn / sd;

            UnityEngine.Vector3 pointPerpendicular = point + _normal * sb;

            return UnityEngine.Vector3.Distance(point, pointPerpendicular);
        }



        public bool PointInTriangle(UnityEngine.Vector3 p)
        {
            //first check if the point is on the same plane
            //if (PointDistanceToPlane(p) > 0)
            //    return false;

            //now, check for the boundaries of the triangle
            if (SameSide(p, V0.Position, V1.Position, V2.Position) &&
                SameSide(p, V1.Position, V2.Position, V0.Position) &&
                SameSide(p, V2.Position, V0.Position, V1.Position))
                return true;

            return false;
        }



        /// <summary>
        /// 
        /// http://www.blackpawn.com/texts/pointinpoly/default.html
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="c"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool SameSide(UnityEngine.Vector3 p1, UnityEngine.Vector3 a, UnityEngine.Vector3 b, UnityEngine.Vector3 c)
        {
            UnityEngine.Vector3 cp1 = UnityEngine.Vector3.Cross(b - a, p1 - a);
            UnityEngine.Vector3 cp2 = UnityEngine.Vector3.Cross(b - a, c - a);

            //if (UnityEngine.Vector3.Dot(cp1, cp2) >= 0)
            //Console.WriteLine(UnityEngine.Vector3.Dot(cp1, cp2));
            //if (UnityEngine.Vector3.IsCollinear(cp1,cp2))
            if (UnityEngine.Vector3.IsCollinear(cp1, cp2) && UnityEngine.Vector3.Dot(cp1, cp2) >= 0)
                return true;

            return false;
        }
    }
}