using UnityEngine;
using Assimp;
using Sceelix.Mathematics.Data;
namespace Sceelix.Meshes.Extensions
{
    public static class AssimpExtension
    {
        public static UnityEngine.Color ToSceelixColor(this Color3D color)
        {
            return new UnityEngine.Color((byte) (color.R * 255), (byte) (color.B * 255), (byte) (color.G * 255));
        }



        public static UnityEngine.Color ToSceelixColor(this Color4D color)
        {
            return new UnityEngine.Color((byte) (color.R * 255), (byte) (color.B * 255), (byte) (color.G * 255), (byte) (color.A * 255));
        }



        public static Matrix ToSceelixMatrix(this Assimp.Matrix4x4 matrix)
        {
            return new Matrix(matrix.A1, matrix.A2, matrix.A3, matrix.A4,
                matrix.B1, matrix.B2, matrix.B3, matrix.B4,
                matrix.C1, matrix.C2, matrix.C3, matrix.C4,
                matrix.D1, matrix.D2, matrix.D3, matrix.D4);
        }



        public static UnityEngine.Vector2 ToSceelixVector2(this Assimp.Vector2D vector)
        {
            return new UnityEngine.Vector2(vector.x, vector.y);
        }



        public static UnityEngine.Vector3 ToSceelixVector3(this Assimp.Vector3D vector)
        {
            return new UnityEngine.Vector3(vector.x, vector.y, vector.z);
        }
    }
}