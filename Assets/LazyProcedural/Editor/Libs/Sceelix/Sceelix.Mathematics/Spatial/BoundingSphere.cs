using System;
using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Spatial
{
    public struct BoundingSphere
    {
        private float _radius;
        private float _radiusSqr;
        public UnityEngine.Vector3 Center;



        public BoundingSphere(UnityEngine.Vector3 center) : this(center, 0)
        {
        }



        public BoundingSphere(UnityEngine.Vector3 center, float radius)
        {
            _radius = radius;
            _radiusSqr = radius * radius;
            Center = center;
        }



        public float Radius
        {
            get { return _radius; }
            set
            {
                _radius = value;
                _radiusSqr = _radius * _radius;
            }
        }



        public float RadiusSqr
        {
            get { return _radiusSqr; }
            set
            {
                _radiusSqr = value;
                _radius = (float) Math.Sqrt(_radiusSqr);
            }
        }



        /// <summary>
        /// Checks if this sphere intersects the target sphere
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool Intersects(BoundingSphere target)
        {
            double radiusSum = target.Radius + Radius;
            UnityEngine.Vector3 centerDifference = target.Center - Center;

            //fast check
            if (centerDifference.x > radiusSum || centerDifference.y > radiusSum || centerDifference.z > radiusSum)
                return false;

            //slow check
            if (centerDifference.Length > radiusSum)
                return false;

            return true;
        }
    }
}