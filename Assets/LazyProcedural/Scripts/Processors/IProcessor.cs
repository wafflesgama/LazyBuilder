using Sceelix.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Sceelix.Processors
{
    interface IProcessor
    {
        public GameObject Process(IEntity input);
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ProcessorAttribute : Attribute
    {
        public Type Type
        {
            get;
            private set;
        }


        public ProcessorAttribute(Type type)
        {
            this.Type = type;
        }
    }
}

