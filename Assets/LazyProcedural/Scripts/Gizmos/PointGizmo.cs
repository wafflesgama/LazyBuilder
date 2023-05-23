using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazyProcedural.Gizmos
{
    [ExecuteAlways]
    public class PointGizmo : MonoBehaviour
    {
        public float pointSize = 0.2f;
        private void OnDrawGizmos()
        {
            UnityEngine.Gizmos.color = Color.cyan;
            UnityEngine.Gizmos.DrawSphere(transform.position, pointSize);
        }
    }
}
