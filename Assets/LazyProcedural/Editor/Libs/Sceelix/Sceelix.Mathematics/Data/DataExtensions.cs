using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Sceelix.Mathematics.Data
{
 
    public static class DataExtensions
    {
        public const int PrecisionDigits = 5;

        #region Vector 2

        public static float[] ToArray(this Vector2 vector)
        {
            return new[] { vector.x, vector.y };
        }

        public static Vector2 Maximize(Vector2 v1, Vector2 v2)
        {
            float x = Math.Max(v1.x, v2.x);
            float y = Math.Max(v1.y, v2.y);

            return new Vector2(x, y);
        }

        public static Vector2 Minimize(Vector2 v1, Vector2 v2)
        {
            float x = Math.Min(v1.x, v2.x);
            float y = Math.Min(v1.y, v2.y);

            return new Vector2(x, y);
        }
        #endregion


    

        #region Vector 3

        public static Vector3 Maximize(Vector3 v1, Vector3 v2)
        {
            float x = Math.Max(v1.x, v2.x);
            float y = Math.Max(v1.y, v2.y);
            float z = Math.Max(v1.z, v2.z);

            return new Vector3(x, y, z);
        }



        public static Vector3 Minimize(Vector3 v1, Vector3 v2)
        {
            float x = Math.Min(v1.x, v2.x);
            float y = Math.Min(v1.y, v2.y);
            float z = Math.Min(v1.z, v2.z);

            return new Vector3(x, y, z);
        }



        public static float GetCommonMultiplier(Vector3 v1, Vector3 v2)
        {
            float a = 0;

            if (v2.x != 0)
                a = v1.x / v2.x;
            else if (v2.y != 0)
                a = v1.y / v2.y;
            else if (v2.z != 0)
                a = v1.z / v2.z;

            return a;
        }

        public static int LongAxis(ref Vector3 v)
        {
            int i = 0;
            if (Math.Abs(v.y) > Math.Abs(v.x)) i = 1;
            if (Math.Abs(v.z) > Math.Abs(i == 0 ? v.x : v.y)) i = 2;
            return i;
        }
        public static Vector3 New(float x, float y, float z)
        {
            return new Vector3(x, y, z);
        }


        public static bool IsOrthogonal(this Vector3 vector, Vector3 normal)
        {
            return Math.Abs(vector.Dot(normal) - 0) < float.Epsilon;
        }

        public static Vector2 ToVector2(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }

        public static float Dot(this Vector3 a, Vector3 b)
        {
            return Vector3.Dot(a, b);
        }

        public static Vector3 Cross(this Vector3 a, Vector3 b)
        {
            return Vector3.Cross(a, b);
        }

        public static float[] ToArray(this Vector3 vector)
        {
            return new[] { vector.x, vector.y, vector.z };
        }

        /// <summary>
        /// Calculates the angle, in radians, between this and the given vector.
        /// </summary>
        /// <param name="b">The vector against with the angle should be calculated.</param>
        /// <returns>The angle between the 2 vectors, in radians.</returns>
        public static float AngleTo(this Vector3 vector, Vector3 b)
        {
            float f = vector.Dot(b) / (vector.magnitude * b.magnitude);

            return (float)Math.Acos(Math.Round(f, PrecisionDigits));
        }


        public static Vector3 FlipYZ(this Vector3 vector)
        {
            return new Vector3(vector.x, vector.z, vector.y);
        }



        public static Vector3 FlipXY(this Vector3 vector)
        {
            return new Vector3(vector.y, vector.x, vector.z);
        }

        public static Vector3 FlipXZ(this Vector3 vector)
        {
            return new Vector3(vector.z, vector.y, vector.x);
        }

        #endregion
    }
}
