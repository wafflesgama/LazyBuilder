using Sceelix.Core.Data;
using Sceelix.Loading;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sceelix.Processors
{
    public class ProcessorManager
    {
        private Dictionary<Type, Type> processors = new Dictionary<Type, Type>();

        public ProcessorManager()
        {
            SetProcessors();
        }

        private void SetProcessors()
        {
            var rawProcessors = SceelixDomain.Types.Where(type => type.GetCustomAttributes(typeof(ProcessorAttribute), true).Length > 0);
            processors = rawProcessors.ToDictionary(type => ((ProcessorAttribute)type.GetCustomAttributes(typeof(ProcessorAttribute), true)[0]).Type, type => type);
        }


        public void Process(IEnumerable<IEntity> entities)
        {
            foreach (IEntity entity in entities)
            {
                Type entityType = entity.GetType();
                bool hasProcessor = processors.ContainsKey(entityType);

                if (!hasProcessor)
                {
                    Debug.LogError($"{entityType.Name} Processor not implemented or is missing a ProcessorAttribute");
                    break;
                }

                IProcessor processor = (IProcessor)Activator.CreateInstance(processors[entityType]);
                processor.Process(entity);

            }

        }
    }
}
