using System.Linq;
using Sceelix.Mathematics.Data;
using Sceelix.Mathematics.Geometry;

namespace Sceelix.Mathematics.Spatial
{
    public class BoundingPlanes
    {
        private readonly Plane3D[] _planes;



        public BoundingPlanes(params Plane3D[] planes)
        {
            _planes = planes;
        }



        /// <summary>
        /// Determines whether the given point is behind or on the given planes.
        /// </summary>
        /// <param name="point">The point to check.</param>
        /// <returns>
        ///   <c>true</c> if the point is behind or on the given planes; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(UnityEngine.Vector3 point)
        {
            return _planes.All(x => x.LocationToPlane(point) != PointToPlaneLocation.Above);
        }



        public bool Contains(BoundingBox boundingBox)
        {
            var corners = boundingBox.Corners;

            //ALL the planes have to have at least ONE corner point behind it 
            return Contains(corners);
        }



        public bool Contains(params UnityEngine.Vector3[] positions)
        {
            return _planes.All(plane => positions.Any(point => plane.LocationToPlane(point) != PointToPlaneLocation.Above));
        }
    }
}