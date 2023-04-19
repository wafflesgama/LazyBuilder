using Sceelix.Mathematics.Data;
using Sceelix.Surfaces.Data;

namespace Sceelix.Surfaces.Extensions
{
    public static class SurfaceHeightExtensions
    {
        public static UnityEngine.Vector3 CalculateFlatPosition(this SurfaceEntity surfaceEntity, Coordinate surfaceCoordinates)
        {
            return new UnityEngine.Vector3(surfaceEntity.ToWorldPosition(surfaceCoordinates), surfaceEntity.MinimumZ);
        }



        /// <summary>
        /// Gets the height value at the given column/row, or 0 if the heightlayer is null.
        /// </summary>
        /// <param name="heightLayer">The height layer.</param>
        /// <param name="surfaceColumn">The surface column.</param>
        /// <param name="surfaceRow">The surface row.</param>
        /// <returns></returns>
        public static float SafeGetHeight(this HeightLayer heightLayer, Coordinate surfaceCoordinate)
        {
            return heightLayer != null ? heightLayer.GetGenericValue(surfaceCoordinate) : 0;
        }



        /// <summary>
        /// Gets the height value at the given position, or 0 if the heightlayer is null.
        /// </summary>
        /// <param name="heightLayer">The height layer.</param>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public static float SafeGetHeight(this HeightLayer heightLayer, UnityEngine.Vector2 position)
        {
            return heightLayer != null ? heightLayer.GetGenericValue(position) : 0;
        }
    }
}