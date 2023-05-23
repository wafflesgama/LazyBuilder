using Sceelix.Core.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sceelix.Processors
{
    [Processor(typeof(Entity))]
    public class EntityProcessor : IProcessor
    {
        public void Process(RecycleObject recycleObject)
        {
            recycleObject.gameObject.name = "Entity";
        }

        public IEnumerable<Type> Require(IEntity input)
        {
            return new[] { typeof(Transform) };
        }
    }
}
