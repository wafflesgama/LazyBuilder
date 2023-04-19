using Sceelix.Mathematics.Data;
using Sceelix.Surfaces.Data;

namespace Sceelix.Surfaces.Extensions
{
    public static class SurfaceNormalExtensions
    {
        /// <summary>
        /// Calculates the normal from a heightlayer and surface coordinates.
        /// </summary>
        /// <param name="heightLayer">The height layer. If null, the default (0,0,1) vector will be returned.</param>
        /// <param name="surfaceColumn">The surface column.</param>
        /// <param name="surfaceRow">The surface row.</param>
        /// <returns></returns>
        public static UnityEngine.Vector3 CalculateNormal(this HeightLayer heightLayer, Coordinate surfaceCoordinate)
        {
            if (heightLayer == null)
                return UnityEngine.Vector3.forward;

            UnityEngine.Vector3[] directions = new UnityEngine.Vector3[4];
            UnityEngine.Vector3 centralPosition = heightLayer.GetPosition(surfaceCoordinate);

            directions[0] = heightLayer.GetPosition(new Coordinate(surfaceCoordinate.x, surfaceCoordinate.y - 1), SampleMethod.Clamp) - centralPosition;
            directions[1] = heightLayer.GetPosition(new Coordinate(surfaceCoordinate.x - 1, surfaceCoordinate.y), SampleMethod.Clamp) - centralPosition;
            directions[2] = heightLayer.GetPosition(new Coordinate(surfaceCoordinate.x, surfaceCoordinate.y + 1), SampleMethod.Clamp) - centralPosition;
            directions[3] = heightLayer.GetPosition(new Coordinate(surfaceCoordinate.x + 1, surfaceCoordinate.y), SampleMethod.Clamp) - centralPosition;

            UnityEngine.Vector3 normal = UnityEngine.Vector3.zero;
            for (int i = 0; i < 4; i++)
            {
                UnityEngine.Vector3 direction1 = directions[i];
                UnityEngine.Vector3 direction2 = i + 1 > 3 ? directions[0] : directions[i + 1];

                normal += UnityEngine.Vector3.Cross(direction1, direction2);
            }

            return normal.normalized;
        }
    }
}