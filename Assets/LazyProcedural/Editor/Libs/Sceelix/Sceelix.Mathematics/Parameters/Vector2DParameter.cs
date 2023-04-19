using Sceelix.Core.Parameters;
using Sceelix.Core.Parameters.Infos;
using Sceelix.Mathematics.Data;
using Sceelix.Mathematics.Parameters.Infos;

namespace Sceelix.Mathematics.Parameters
{
    public class Vector2Parameter : PrimitiveParameter<UnityEngine.Vector2>
    {
        /*public FloatParameter X = new FloatParameter("X", 0);
        public FloatParameter Y = new FloatParameter("Y", 0);*/



        public Vector2Parameter(string label)
            : base(label, UnityEngine.Vector2.zero)
        {
        }



        public Vector2Parameter(string label, UnityEngine.Vector2 vector2D)
            : base(label, vector2D)
        {
            Value = vector2D;
        }



        /*public UnityEngine.Vector3 GetVector3F()
        {
            return new UnityEngine.Vector3(X.Value, Y.Value, Z.Value);
        }

        public void SetVector3F(UnityEngine.Vector3 vector3F)
        {
            X.Value = vector3F.x;
            Y.Value = vector3F.y;
            Z.Value = vector3F.z;
        }*/



        protected internal override ParameterInfo ToParameterInfo()
        {
            return new Vector2ParameterInfo(this);
        }



        /*public new UnityEngine.Vector2 Value
        {
            get { return new UnityEngine.Vector2(X.Value, Y.Value); }
            set
            {
                X.Value = value.x;
                Y.Value = value.y;
            }
        }*/
    }
}