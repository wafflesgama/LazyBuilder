using Sceelix.Mathematics.Data;
using Sceelix.Surfaces.Data;

namespace Sceelix.Surfaces.Extensions
{
    /// <summary>
    /// Performs UV calculation for surfaces.
    /// </summary>
    public static class SurfaceUVExtensions
    {
        public static UnityEngine.Vector2 CalculateBaseUV(this SurfaceEntity surfaceEntity, Coordinate surfaceCoordinate, bool flipU = false, bool flipV = false)
        {
            var u = surfaceCoordinate.x / (float) (surfaceEntity.NumColumns - 1);
            var v = (surfaceEntity.NumRows - 1 - surfaceCoordinate.y) / (float) (surfaceEntity.NumRows - 1);

            if (flipU)
                u = 1 - u;

            if (flipV)
                v = 1 - v;

            return new UnityEngine.Vector2(u, v);
        }



        public static UnityEngine.Vector2 CalculateUV(this SurfaceEntity surfaceEntity, Coordinate surfaceCoordinate)
        {
            var u = surfaceCoordinate.x / (float) (surfaceEntity.NumColumns - 1);
            var v = (surfaceEntity.NumRows - 1 - surfaceCoordinate.y) / (float) (surfaceEntity.NumRows - 1);

            return new UnityEngine.Vector2(u, v);
        }



        public static UnityEngine.Vector2 CalculateUV(this SurfaceEntity surfaceEntity, Coordinate surfaceCoordinate, UnityEngine.Vector2 tiling, UnityEngine.Vector2 offset, bool flipU = false, bool flipV = false)
        {
            var u = surfaceCoordinate.x / (float) (surfaceEntity.NumColumns - 1);
            var v = (surfaceEntity.NumRows - 1 - surfaceCoordinate.y) / (float) (surfaceEntity.NumRows - 1);

            if (flipU)
                u = 1 - u;

            if (flipV)
                v = 1 - v;

            return new UnityEngine.Vector2(u * tiling.x + offset.x, v * tiling.y + offset.y);
        }
    }
}