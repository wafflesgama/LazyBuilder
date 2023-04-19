using Sceelix.Core.Handles;
using Sceelix.Mathematics.Data;

namespace Sceelix.Meshes.Handles
{
    public class NumericSizerHandle : VisualHandle
    {
        public NumericSizerHandle(UnityEngine.Vector3 basePosition, UnityEngine.Vector3 direction)
        {
            BasePosition = basePosition;
            Direction = direction;
        }



        public UnityEngine.Vector3 BasePosition
        {
            get;
            set;
        }


        public UnityEngine.Color Color
        {
            get;
            set;
        } = UnityEngine.Color.black;


        public UnityEngine.Vector3 Direction
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