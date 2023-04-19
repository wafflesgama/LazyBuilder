using System;
using Sceelix.Mathematics.Data;
using Sceelix.Mathematics.Geometry;

namespace Sceelix.Paths.Data
{
    /// <summary>
    /// Define an edge connecting two street vertices.
    /// It is called "static", because it precalculates a couple of measurements, assuming that the vertex positions are not changed.
    /// This optimizes the process for many procedures.
    /// </summary>
    internal class StaticPathEdge
    {
        //precalculated data



        public StaticPathEdge(PathEdge edge)
        {
            Edge = edge;

            Direction = Target.Position - Source.Position;
            NormalizedDirection = Direction.normalized;
            CenterPoint = Source.Position + NormalizedDirection * Length;
            Length = Direction.Length;
        }



        public UnityEngine.Vector3 CenterPoint
        {
            get;
        }


        public UnityEngine.Vector3 Direction
        {
            get;
        }


        public PathEdge Edge
        {
            get;
        }


        public float Length
        {
            get;
        }


        public LineSegment3D LineSegment => new LineSegment3D(Source.Position, Target.Position);


        public UnityEngine.Vector3 NormalizedDirection
        {
            get;
        }


        public PathVertex Source => Edge.Source;


        public PathVertex Target => Edge.Target;



        /// <summary>
        /// Introduces a new vertex into the street and returns the 2 new static street edges
        /// </summary>
        /// <param name="vNew"></param>
        /// <returns></returns>
        /*public StaticStreetEdge[] IntroduceVertex(StreetVertex vNew)
        {
            float distanceFromV0 = (vNew.Position - V0.Position).Length;

            int v0Index = _v0[_street].StreetIndex;
            int v1Index = _v1[_street].StreetIndex;

            //in the returning/last connecting vertex
            if (v0Index > v1Index)
                v1Index = _street.Vertices.Count;

            for (int i = v0Index + 1; i <= v1Index; i++)
            {
                if (distanceFromV0 > (_street[i - 1].Position - V0.Position).Length &&
                    distanceFromV0 < (_street[i].Position - V0.Position).Length)
                {
                    _street.Vertices.Insert(i, vNew);
                    vNew.CreateHalfStreetVertex(_street, i);
                    break;
                }
            }

            _street.ResetHalfStreetVertices();

            return new []{new StaticStreetEdge(_street,_v0,vNew),new StaticStreetEdge(_street,vNew,_v1)};
        }*/
        public UnityEngine.Vector3 CalculateIntersection(StaticPathEdge otherEdge, bool includingEnds)
        {
            return LineSegment.Intersection(otherEdge.LineSegment, includingEnds);
        }



        /*public float MiddleIntersectionValue(StaticPathEdge otherEdge, bool includingEnds)
        {
            UnityEngine.Vector3 p1 = Source.Position;
            UnityEngine.Vector3 p2 = otherEdge.Source.Position;
            UnityEngine.Vector3 v1 = Target.Position - p1;
            UnityEngine.Vector3 v2 = otherEdge.Target.Position - p2;

            UnityEngine.Vector3 v1v2 = v1.Cross(v2);

            if (v1v2.ExactlyEquals(UnityEngine.Vector3.zero))
            {
                return float.PositiveInfinity;
            }


            UnityEngine.Vector3 p1p2 = p2 - p1;
            UnityEngine.Vector3 p1p2v2 = p1p2.Cross(v2);

            if (!v1v2.IsCollinear(p1p2v2))
                return float.NaN;

            //REFACTORING NEEDED!!!
            float a = UnityEngine.Vector3.GetCommonMultiplier(p1p2v2, v1v2);

            UnityEngine.Vector3 aV1p1p2 = v1*a - p1p2;

            double b = UnityEngine.Vector3.GetCommonMultiplier(aV1p1p2, v2);

            if (includingEnds)
            {
                if (a >= 0 && a <= 1 && b >= 0 && b <= 1)
                    return a;

                return float.NaN;
            }

            if (a > 0 && a < 1 && b > 0 && b < 1)
                return a;

            return float.NaN;
        }*/


        /*public PathEdge[] IntroduceVertex(PathVertex vNew)
        {
            return _edge.IntroduceVertex(vNew);
        }*/



        public bool CoincidentWith(StaticPathEdge edge)
        {
            return Source == edge.Source && Target == edge.Target
                   || Source == edge.Target && Target == edge.Source;
        }



        public bool HasPointInBetween(UnityEngine.Vector3 position)
        {
            return Math.Abs((position - Source.Position).normalized.Dot((position - Target.Position).normalized) - -1) < UnityEngine.Vector3.Precision;
        }



        public bool HasPointInBetween(UnityEngine.Vector3 position, float precision)
        {
            return Math.Abs((position - Source.Position).normalized.Dot((position - Target.Position).normalized) - -1) < precision;
        }



        public bool IsAtCollidableRange(StaticPathEdge se1)
        {
            return (se1.CenterPoint - CenterPoint).Length < se1.Length + Length;
        }



        /*public bool IsAtSameLine(StaticStreetEdge second)
        {
            float dotDirection = this.NormalizedDirection.Dot(second.NormalizedDirection);
            float dotStartingPoints = ( - second.V0.Position).normalized.Dot(NormalizedDirection);

            this.V0.Position.MultipleOf(second.V0.Position);


            return  == 1 && 

        }*/



        public bool IsConnectedTo(PathVertex pathVertex)
        {
            return Source == pathVertex || Target == pathVertex;
        }
    }
}