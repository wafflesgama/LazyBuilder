using System.Collections.Generic;
using System.Linq;
using Sceelix.Core.IO;
using Sceelix.Core.Parameters;
using Sceelix.Mathematics.Data;
using Sceelix.Mathematics.Spatial;
using Sceelix.Paths.Algorithms;
using Sceelix.Paths.Data;
using Sceelix.Paths.Procedures;
using Sceelix.Points.Data;

namespace Sceelix.Points.Parameters
{
    #region From Points

    public abstract class PathFromPointsMethodParameter : CompoundParameter
    {
        protected PathFromPointsMethodParameter(string label)
            : base(label)
        {
        }



        public abstract PathEntity CreatePath(IEnumerable<PointEntity> pointEntities);
    }


    /// <summary>
    /// Connects all pairs of points.
    /// </summary>
    public class CrossPathConvertMethodParameter : PathFromPointsMethodParameter
    {
        public CrossPathConvertMethodParameter()
            : base("Cross")
        {
        }



        public override PathEntity CreatePath(IEnumerable<PointEntity> pointEntities)
        {
            List<PathVertex> pathVertices = pointEntities.Select(x =>
            {
                var pathVertex = new PathVertex(x.Position);
                x.Attributes.SetAttributesTo(pathVertex.Attributes);
                return pathVertex;
            }).ToList();

            List<PathEdge> pathEdges = new List<PathEdge>();

            for (int i = 0; i < pathVertices.Count; i++)
            for (int j = i + 1; j < pathVertices.Count; j++)
                pathEdges.Add(new PathEdge(pathVertices[i], pathVertices[j]));

            return new PathEntity(pathEdges);
        }
    }


    /// <summary>
    /// Connects all points in a sequence, in the same order as received in the input.
    /// </summary>
    public class SequencePathConvertMethodParameter : PathFromPointsMethodParameter
    {
        public SequencePathConvertMethodParameter()
            : base("Sequence")
        {
        }



        public override PathEntity CreatePath(IEnumerable<PointEntity> pointEntities)
        {
            List<PathVertex> pathVertices = pointEntities.Select(x =>
            {
                var pathVertex = new PathVertex(x.Position);
                x.Attributes.SetAttributesTo(pathVertex.Attributes);
                return pathVertex;
            }).ToList();

            return PathEntity.CreateSequence(pathVertices);
        }
    }

    /// <summary>
    /// Connects all points into a delaunay triangle mesh.
    /// </summary>
    public class DelaunayPathConvertMethodParameter : PathFromPointsMethodParameter
    {
        public DelaunayPathConvertMethodParameter()
            : base("Delaunay")
        {
        }



        public override PathEntity CreatePath(IEnumerable<PointEntity> pointEntities)
        {
            List<PathVertex> pathVertices = pointEntities.Select(x =>
            {
                var pathVertex = new PathVertex(x.Position);
                x.Attributes.SetAttributesTo(pathVertex.Attributes);
                return pathVertex;
            }).ToList();


            var boundingBox = new BoundingRectangle(pathVertices.Select(x => x.Position.ToVector2()));

            var doublesX = pathVertices.Select(x => (double) x.Position.x);
            var doublesY = pathVertices.Select(x => (double) x.Position.y);

            Voronoi voronoiGenerator = new Voronoi(0.1);
            List<PathEdge> newEdges = new List<PathEdge>();

            var points = voronoiGenerator.GenerateVoronoi(doublesX.ToArray(), doublesY.ToArray(), boundingBox.Min.x, boundingBox.Max.x, boundingBox.Min.y, boundingBox.Max.y);


            foreach (var graphEdge in points)
            {
                var newPathEdge = new PathEdge(pathVertices[graphEdge.site1], pathVertices[graphEdge.site2]);
                newEdges.Add(newPathEdge);
            }

            return new PathEntity(newEdges);
        }
    }

    /// <summary>
    /// Creates a voronoi pattern from the given points.
    /// </summary>
    public class VoronoiPathConvertMethodParameter : PathFromPointsMethodParameter
    {
        public VoronoiPathConvertMethodParameter()
            : base("Voronoi")
        {
        }



        public override PathEntity CreatePath(IEnumerable<PointEntity> pointEntities)
        {
            var pointEntityList = pointEntities as IList<PointEntity> ?? pointEntities.ToList();
            var boundingBox = new BoundingRectangle(pointEntityList.Select(x => x.Position.ToVector2()));

            var doublesX = pointEntityList.Select(x => (double) x.Position.x);
            var doublesY = pointEntityList.Select(x => (double) x.Position.y);

            Voronoi voronoiGenerator = new Voronoi(0.1);
            List<PathEdge> newEdges = new List<PathEdge>();

            var points = voronoiGenerator.GenerateVoronoi(doublesX.ToArray(), doublesY.ToArray(), boundingBox.Min.x, boundingBox.Max.x, boundingBox.Min.y, boundingBox.Max.y);

            Dictionary<UnityEngine.Vector3, PathVertex> vertices = new Dictionary<UnityEngine.Vector3, PathVertex>();
            foreach (var graphEdge in points)
            {
                UnityEngine.Vector3 v1 = new UnityEngine.Vector3((float) graphEdge.x1, (float) graphEdge.y1);
                UnityEngine.Vector3 v2 = new UnityEngine.Vector3((float) graphEdge.x2, (float) graphEdge.y2);

                PathVertex vertex1, vertex2;

                if (!vertices.TryGetValue(v1, out vertex1))
                    vertices.Add(v1, vertex1 = new PathVertex(v1));

                if (!vertices.TryGetValue(v2, out vertex2))
                    vertices.Add(v2, vertex2 = new PathVertex(v2));

                //for some reason, the algorithm can cause this, so let's check
                if (vertex1 != vertex2)
                {
                    var newPathEdge = new PathEdge(vertex1, vertex2);
                    //newPathEdge.AttachToVertices();
                    newEdges.Add(newPathEdge);
                }
            }

            return new PathEntity(newEdges);
        }
    }

    /// <summary>
    /// Creates a path from a set of given points.
    /// </summary>
    public class CreateFromPointsParameter
        : PathCreateProcedure.PrimitivePathParameter
    {
        /// <summary>
        /// Points from which to create path vertices.
        /// </summary>
        private readonly CollectiveInput<PointEntity> _inputPoints = new CollectiveInput<PointEntity>("Input");

        /// <summary>
        /// Method to connect the points into the path.
        /// </summary>
        private readonly SelectListParameter<PathFromPointsMethodParameter> _parameterMethod = new SelectListParameter<PathFromPointsMethodParameter>("Method", "Delaunay");



        public CreateFromPointsParameter()
            : base("From Points")
        {
        }



        protected internal override PathEntity CreatePath()
        {
            return _parameterMethod.SelectedItem.CreatePath(_inputPoints.Read());
        }
    }

    #endregion
}