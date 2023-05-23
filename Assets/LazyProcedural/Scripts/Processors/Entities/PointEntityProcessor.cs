using LazyProcedural.Gizmos;
using Sceelix.Actors.Data;
using Sceelix.Core.Data;
using Sceelix.Points.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sceelix.Processors
{
    [Processor(typeof(PointEntity))]
    public class PointEntityProcessor : IProcessor
    {
        public void Process(RecycleObject recycleObject)
        {
            recycleObject.gameObject.name = "Point Entity";
            var baseEntity = (IActor)recycleObject.entity;
            recycleObject.gameObject.transform.position = baseEntity.BoxScope.Translation.FlipYZ().ToVector3();
        }

        public IEnumerable<Type> Require(IEntity input)
        {
            return new[] { typeof(Transform), typeof(PointGizmo) };
        }


    }
}
