using LazyProcedural.Gizmos;
using Sceelix.Core.Data;
using Sceelix.Paths.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sceelix.Processors
{
    [Processor(typeof(PathEntity))]
    public class PathEntityProcessor : IProcessor
    {
        public void Process(RecycleObject recycleObject)
        {
            PathEntity pathEntity = recycleObject.entity as PathEntity;

            recycleObject.gameObject.name = "Path Entity";
            recycleObject.gameObject.transform.position = pathEntity.BoxScope.Translation.ToVector3();


            var pathGizmo = recycleObject.gameObject.GetComponent<PathGizmo>();

            ////If a  doesn't exist, create it
            if (pathGizmo == null)
                pathGizmo = recycleObject.gameObject.AddComponent<PathGizmo>();

            pathGizmo.lines = pathEntity.Edges.Select(e => new PathGizmo.Line(e.Source.Position.FlipYZ().ToVector3(), e.Target.Position.FlipYZ().ToVector3())).ToArray();

            //var lineRenderer = recycleObject.gameObject.GetComponent<LineRenderer>();

            ////If a Mesh Filter doesn't exist, create it
            //if (lineRenderer == null)
            //    lineRenderer = recycleObject.gameObject.AddComponent<LineRenderer>();

            //var positions = pathEntity.Vertices.Select(v => v.Position.ToVector3()).ToArray();

            //lineRenderer.positionCount = positions.Length;
            //lineRenderer.SetPositions(positions);
            //lineRenderer.material = MaterialProcessor.CreateDefaultLineMaterial();
        }

        public IEnumerable<Type> Require(IEntity input)
        {
            return new[] { typeof(Transform), typeof(PathGizmo) };
        }
    }
}
