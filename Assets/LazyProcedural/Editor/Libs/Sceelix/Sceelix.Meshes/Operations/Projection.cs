using System.Collections.Generic;
using Sceelix.Mathematics.Data;
using Sceelix.Meshes.Data;

namespace Sceelix.Meshes.Operations
{
    public static class Projection
    {
        public static Projection2DInfo ProjectTo2D(this Face face)
        {
            return ProjectTo2D(face, UnityEngine.Vector3.forward);
        }



        public static Projection2DInfo ProjectTo2D(this Face face, UnityEngine.Vector3 planeNormal)
        {
            List<UnityEngine.Vector3> projectedPoints = new List<UnityEngine.Vector3>();
            float angle = face.Normal.AngleTo(planeNormal);

            UnityEngine.Vector3 axis = UnityEngine.Vector3.Cross(face.Normal, planeNormal);
            foreach (Vertex vertex in face.Vertices) projectedPoints.Add(vertex.Position.Rotate(axis, angle));

            return new Projection2DInfo(projectedPoints, angle, axis);
        }



        public struct Projection2DInfo
        {
            public List<UnityEngine.Vector3> ProjectedPoints
            {
                get;
            }


            public float Angle
            {
                get;
            }


            public UnityEngine.Vector3 Axis
            {
                get;
            }



            public Projection2DInfo(List<UnityEngine.Vector3> projectedPoints, float angle, UnityEngine.Vector3 axis)
                : this()
            {
                ProjectedPoints = projectedPoints;
                Angle = angle;
                Axis = axis;
            }
        }
    }
}