using System.Xml;
using Sceelix.Core.Parameters;
using Sceelix.Core.Parameters.Infos;
using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Parameters.Infos
{
    public class Vector3ParameterInfo : PrimitiveParameterInfo<UnityEngine.Vector3> //CompoundParameterInfo
    {
        public Vector3ParameterInfo(string label)
            : base(label)
        {
            /*Fields.Add(new FloatParameterInfo("X"));
            Fields.Add(new FloatParameterInfo("Y"));
            Fields.Add(new FloatParameterInfo("Z"));*/
        }



        public Vector3ParameterInfo(Vector3Parameter parameter)
            : base(parameter)
        {
        }



        public Vector3ParameterInfo(XmlElement xmlNode)
            : base(xmlNode)
        {
        }



        public override string ValueLiteral
        {
            get
            {
                var fixedValue = FixedValue;
                return "new UnityEngine.Vector3(" + fixedValue.x + "," + fixedValue.y + "," + fixedValue.z + ")";
            }
        }



        /*public UnityEngine.Vector3 FixedValue
        {
            get
            {
                float x = Fields[0].CastTo<FloatParameterInfo>().FixedValue;
                float y = Fields[1].CastTo<FloatParameterInfo>().FixedValue;
                float z = Fields[2].CastTo<FloatParameterInfo>().FixedValue;

                return new UnityEngine.Vector3(x, y, z);
            }
            set
            {
                Fields[0].CastTo<FloatParameterInfo>().FixedValue = value.x;
                Fields[1].CastTo<FloatParameterInfo>().FixedValue = value.y;
                Fields[2].CastTo<FloatParameterInfo>().FixedValue = value.z;
            }
        }*/



        public override Parameter ToParameter()
        {
            return new Vector3Parameter(Label, FixedValue);
        }
    }
}