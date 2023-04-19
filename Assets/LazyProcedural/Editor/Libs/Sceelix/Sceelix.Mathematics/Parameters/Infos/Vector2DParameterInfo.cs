using System.Xml;
using Sceelix.Core.Parameters;
using Sceelix.Core.Parameters.Infos;
using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Parameters.Infos
{
    public class Vector2ParameterInfo : PrimitiveParameterInfo<UnityEngine.Vector2>
    {
        public Vector2ParameterInfo(string label)
            : base(label)
        {
            /*Fields.Add(new FloatParameterInfo("X"));
            Fields.Add(new FloatParameterInfo("Y"));*/
        }



        public Vector2ParameterInfo(Vector2Parameter parameter)
            : base(parameter)
        {
        }



        public Vector2ParameterInfo(XmlElement xmlNode)
            : base(xmlNode)
        {
        }



        public override string ValueLiteral
        {
            get
            {
                var fixedValue = FixedValue;
                return "new UnityEngine.Vector2(" + fixedValue.x + "," + fixedValue.y + ")";
            }
        }



        /*public UnityEngine.Vector2 FixedValue
        {
            get
            {
                float x = Fields[0].CastTo<FloatParameterInfo>().FixedValue;
                float y = Fields[1].CastTo<FloatParameterInfo>().FixedValue;

                return new UnityEngine.Vector2(x, y);
            }
            set
            {
                Fields[0].CastTo<FloatParameterInfo>().FixedValue = value.x;
                Fields[1].CastTo<FloatParameterInfo>().FixedValue = value.y;
            }
        }*/



        public override Parameter ToParameter()
        {
            return new Vector2Parameter(Label, FixedValue);
        }
    }
}