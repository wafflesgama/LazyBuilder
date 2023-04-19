using System;
using System.Linq;
using Sceelix.Core.IO;
using Sceelix.Core.Parameters;
using Sceelix.Mathematics.Data;
using Sceelix.Mathematics.Geometry;
using Sceelix.Mathematics.Spatial;
using Sceelix.Paths.Data;
using Sceelix.Surfaces.Data;
using Sceelix.Surfaces.Procedures;

namespace Sceelix.Paths.Parameters
{
    /// <summary>
    /// Paints paths on the surface.
    /// </summary>
    /// <seealso cref="SurfacePaintProcedure.SurfacePaintingParameter" />
    public class PathOnSurfacePainting : SurfacePaintProcedure.SurfacePaintingParameter
    {
        /// <summary>
        /// The paths that are to be painted on the terrain.
        /// </summary>
        private readonly CollectiveInput<PathEntity> _inputPath = new CollectiveInput<PathEntity>("Path");

        /// <summary>
        /// The paths that were painted on the terrain.
        /// </summary>
        private readonly Output<PathEntity> _outputPath = new Output<PathEntity>("Path");

        /// <summary>
        /// The distance around the path that should be painted. This value can be set as an expression based on edge properties. 
        /// The @@attributeName will refer to the attributes of each edge.
        /// </summary>
        private readonly FloatParameter _parameterWidth = new FloatParameter("Width", 1) {EntityEvaluation = true};

        /// <summary>
        /// The intensity of the texture painting.
        /// </summary>
        private readonly FloatParameter _parameterValue = new FloatParameter("Value", 0.5f)
        {
            MinValue = 0,
            MaxValue = 1,
            Increment = 0.01f
        };



        public PathOnSurfacePainting()
            : base("Path")
        {
        }



        public override Action<SurfaceEntity, float[][,]> GetApplyFunction()
        {
            var textureIndex = ParameterTextureIndex.Value;
            var value = _parameterValue.Value;

            var pathEntities = _inputPath.Read().ToList();

            _outputPath.Write(pathEntities);

            return (surfaceEntity, values) =>
            {
                float[,] finalValues = new float[surfaceEntity.NumColumns, surfaceEntity.NumRows];

                foreach (PathEntity pathEntity in pathEntities)
                foreach (var pathEdge in pathEntity.Edges)
                {
                    var halfWidth = _parameterWidth.Get(pathEdge) / 2f;

                    //var direction = pathEdge.Direction;
                    var lateralDirection = UnityEngine.Vector3.forward.Cross(pathEdge.Direction).normalized;
                    var lateralDirection2D = lateralDirection.ToVector2().normalized;

                    var direction2D = pathEdge.Direction.ToVector2().normalized;
                    var source2D = pathEdge.Source.Position.ToVector2();
                    var target2D = pathEdge.Target.Position.ToVector2();

                    //determine the distance between point given the direction of the line
                    var frontalDistance = GetMinDistanceBetweenCellPoints(surfaceEntity.CellSize, direction2D, lateralDirection2D);
                    var lateralDistance = GetMinDistanceBetweenCellPoints(surfaceEntity.CellSize, lateralDirection2D, direction2D);


                    var sizedlateralDirection2D = lateralDirection2D * (halfWidth + lateralDistance);

                    BoundingRectangle br = new BoundingRectangle();
                    br.AddPoint(pathEdge.Source.Position.ToVector2());
                    br.AddPoint(pathEdge.Target.Position.ToVector2());
                    br.AddPoint(pathEdge.Source.Position.ToVector2() + sizedlateralDirection2D);
                    br.AddPoint(pathEdge.Source.Position.ToVector2() - sizedlateralDirection2D);
                    br.AddPoint(pathEdge.Target.Position.ToVector2() + sizedlateralDirection2D);
                    br.AddPoint(pathEdge.Target.Position.ToVector2() - sizedlateralDirection2D);

                    br.Expand(surfaceEntity.CellSize);

                    if (!surfaceEntity.Contains(br.Min) && !surfaceEntity.Contains(br.Max))
                        continue;

                    var minCoords = surfaceEntity.ToCoordinates(br.Min);
                    var maxCoords = surfaceEntity.ToCoordinates(br.Max);


                    Line2D line = new Line2D(pathEdge.Direction.ToVector2(), pathEdge.Source.Position.ToVector2());

                    for (int i = minCoords.x; i <= maxCoords.x; i++)
                    for (int j = maxCoords.y; j <= minCoords.y; j++)
                    {
                        //var height surfaceEntity.Heights[i, j];
                        UnityEngine.Vector2 gridCornerPosition = surfaceEntity.ToWorldPosition(new Coordinate(i, j));

                        //must be within the bound of the line - direction2D* frontalDistance
                        if ((gridCornerPosition - (source2D - direction2D * frontalDistance)).Dot(direction2D) > 0 &&
                            (gridCornerPosition - (target2D + direction2D * frontalDistance)).Dot(-direction2D) > 0)
                        {
                            var distance = line.MinDistanceToPoint(gridCornerPosition);
                            if (distance <= halfWidth + lateralDistance)
                            {
                                var valueToSet = value;

                                if (distance > halfWidth)
                                {
                                    var fraction = 1 - (distance - halfWidth) / lateralDistance;
                                    valueToSet *= fraction;
                                }

                                if (valueToSet > finalValues[i, j])
                                {
                                    SetDataIndex(i, j, values, textureIndex, valueToSet);
                                    finalValues[i, j] = valueToSet;
                                }
                            }
                        }
                    }
                }
            };
        }



        private float GetMinDistanceBetweenCellPoints(float cellSize, UnityEngine.Vector2 direction, UnityEngine.Vector2 crossdirection)
        {
            var distancePoint = new UnityEngine.Vector2(Math.Sign(crossdirection.x) * cellSize, Math.Sign(crossdirection.y) * cellSize);
            Line2D distanceLine = new Line2D(direction, UnityEngine.Vector2.zero);

            return distanceLine.MinDistanceToPoint(distancePoint);
        }
    }
}