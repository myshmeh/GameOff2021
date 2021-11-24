using StageObject.Debugger;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Editor.StageObject.Debugger.Waypoint
{
    public class WaypointHolder : MonoBehaviour
    {
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (transform.childCount == 0) return;
            
            Gizmos.color = Color.gray;
            Handles.color = Color.gray;

            Vector3[] waypoints = TransformConverter.ToArray(transform);
            for (int i = 0; i < waypoints.Length; i++)
            {
                Vector3 waypoint = waypoints[i];
                Vector3 nextWaypoint = waypoints[(i + 1) % waypoints.Length];
                
                Handles.Label(waypoint, $"{i}");
                Gizmos.DrawSphere(waypoint, .05f);

                Gizmos.DrawLine(waypoint, nextWaypoint);
            }
        }
        #endif
    }
}