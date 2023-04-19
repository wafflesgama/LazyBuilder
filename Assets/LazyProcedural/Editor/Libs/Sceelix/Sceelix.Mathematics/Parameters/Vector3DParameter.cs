using Sceelix.Core.Parameters;
using Sceelix.Core.Parameters.Infos;
using Sceelix.Mathematics.Data;
using Sceelix.Mathematics.Parameters.Infos;

namespace Sceelix.Mathematics.Parameters
{
    public class Vector3Parameter : PrimitiveParameter<UnityEngine.Vector3>
    {
        /*public FloatParameter X = new FloatParameter("X", 0);
        public FloatParameter Y = new FloatParameter("Y", 0);
        public FloatParameter Z = new FloatParameter("Z", 0);*/



        public Vector3Parameter(string label)
            : base(label, UnityEngine.Vector3.zero)
        {
        }



        public Vector3Parameter(string label, UnityEngine.Vector3 vector3D)
            : base(label, vector3D)
        {
            //Value = vector3D;
        }



        protected internal override ParameterInfo ToParameterInfo()
        {
            return new Vector3ParameterInfo(this);
        }



        /*public new UnityEngine.Vector3 Value
        {
            get { return new UnityEngine.Vector3(X.Value, Y.Value, Z.Value); }
            set
            {
                X.Value = value.x;
                Y.Value = value.y;
                Z.Value = value.z;
            }
        }*/

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
    }
}