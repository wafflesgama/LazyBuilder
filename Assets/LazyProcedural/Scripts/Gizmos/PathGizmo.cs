using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazyProcedural.Gizmos
{
    [ExecuteAlways]
    public class PathGizmo : MonoBehaviour
    {
        public Line[] lines;
        private void OnDrawGizmos()
        {
            UnityEngine.Gizmos.color = Color.cyan;

            foreach (var line in lines)
            {
                UnityEngine.Gizmos.DrawLine(line.start, line.end);
            }
        }

        [Serializable]
        public class Line
        {
            [SerializeField] public Vector3 start;
            [SerializeField] public Vector3 end;

            public Line(Vector3 start, Vector3 end)
            {
                this.start = start;
                this.end = end;
            }

        }
    }
}


