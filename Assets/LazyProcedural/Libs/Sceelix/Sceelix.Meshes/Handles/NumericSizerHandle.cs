using Sceelix.Core.Handles;
using Sceelix.Mathematics.Data;
using UnityEngine;

namespace Sceelix.Meshes.Handles
{
    public class NumericSizerHandle : VisualHandle
    {
        public NumericSizerHandle(Vector3D basePosition, Vector3D direction)
        {
            BasePosition = basePosition;
            Direction = direction;
        }



        public Vector3D BasePosition
        {
            get;
            set;
        }


        public Color Color
        {
            get;
            set;
        } = Color.black;


        public Vector3D Direction
        {
            get;
            set;
        }


        public float Scale
        {
            get;
            set;
        } = 1;
    }
}