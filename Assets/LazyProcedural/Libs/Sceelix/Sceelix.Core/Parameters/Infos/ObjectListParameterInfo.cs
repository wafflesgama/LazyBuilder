using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sceelix.Core.Parameters.Infos
{

    public class ObjectListParameterInfo<T> : ParameterInfo
    {
        public T[] FixedValue
        {
            get;
            set;
        } = new T[0];

        public ObjectListParameterInfo(string label)
            : base(label)
        {
        }

        public ObjectListParameterInfo(ObjectListParameter<T> parameter)
            : base(parameter)
        {
            FixedValue = parameter.Value;
        }


        public override string MetaType => new T[0].GetType().Name;

        public override Parameter ToParameter()
        {
            return new ObjectListParameter<T>(this);
        }

    }
}
