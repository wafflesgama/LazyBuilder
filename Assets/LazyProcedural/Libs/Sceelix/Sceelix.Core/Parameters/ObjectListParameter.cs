using Sceelix.Conversion;
using Sceelix.Core.Data;
using Sceelix.Core.Parameters.Infos;
using Sceelix.Core.Procedures;
using System.Linq;

namespace Sceelix.Core.Parameters
{
    /// <summary>
    /// A parameter that accepts a list of any type of data.
    public class ObjectListParameter<T> : Parameter
    {
        protected T[] _value;

        public T[] Value
        {
            get { return (T[])Get(); }
            set { Set(value); }
        }

        public ObjectListParameter(string label)
            : base(label)
        {
            _value = new T[0];
        }

        public ObjectListParameter(ParameterInfo parameterInfo)
       : base(parameterInfo)
        {
            _value = new T[0];
        }

        protected internal override object GetData()
        {
            return _value;
        }

        public new T Get(IEntity entity)
        {
            return (T)base.Get(entity);
        }


        protected internal override void Set(ParameterInfo argument, Procedure masterProcedure, Procedure currentProcedure)
        {
            var primitiveArgument = (ObjectListParameterInfo<T>)argument;

            Set(primitiveArgument.FixedValue);
        }


        protected override void SetData(object value)
        {
            object[] values = (object[])value;
            _value = values.Select(x => ConvertHelper.Convert<T>(x)).ToArray();
        }

        protected internal override ParameterInfo ToParameterInfo()
        {
            return new ObjectListParameterInfo<T>(this);
        }
    }

}