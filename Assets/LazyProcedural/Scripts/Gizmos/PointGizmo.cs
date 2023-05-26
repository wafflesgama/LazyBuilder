using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LazyProcedural.Gizmos
{
    [ExecuteAlways]
    public class PointGizmo : MonoBehaviour
    {
        public float pointSize = 0.04f;
        public float pointSizeGrowDistance = 0.015f;
        private void OnDrawGizmos()
        {
            UnityEngine.Gizmos.color = Color.cyan;

#if UNITY_EDITOR
            UnityEngine.Gizmos.DrawSphere(transform.position, pointSize + (pointSizeGrowDistance * (Vector3.Distance(transform.position, SceneView.currentDrawingSceneView.camera.transform.position))));
#else
   UnityEngine.Gizmos.DrawSphere(transform.position, pointSize);
#endif
        }
    }
}
