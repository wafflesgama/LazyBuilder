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
        public bool multiColor;
        public Color[] colors = new Color[] { new Color(0, .2f, .2f), new Color(0, .4f, .4f), new Color(0, .6f, .6f), new Color(0, .8f, .8f), Color.cyan };

        int colorIndex;
        private void OnDrawGizmos()
        {
            UnityEngine.Gizmos.color = Color.cyan;

            colorIndex = -1;
            foreach (var line in lines)
            {
                if (multiColor)
                {
                    colorIndex++;
                    if (colorIndex >= colors.Length)
                        colorIndex = 0;

                    UnityEngine.Gizmos.color = colors[colorIndex];
                }

                //UnityEditor.Handles.color = colors[colorIndex];
                DrawArrow(line.start, line.end);
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



        public static void DrawArrow(Vector3 start, Vector3 end, float arrowHeadLength = .3f, float arrowHeadAngle = 20.0f)
        {

            UnityEngine.Gizmos.DrawLine(start, end);

            var direction = end - start;

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            UnityEngine.Gizmos.DrawRay(end, right * arrowHeadLength);
            UnityEngine.Gizmos.DrawRay(end, left * arrowHeadLength);
        }
    }
}


