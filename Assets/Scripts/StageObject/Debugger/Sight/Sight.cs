using System;
using UnityEditor;
using UnityEngine;

namespace StageObject.Debugger.Sight
{
    public class Sight : MonoBehaviour
    {
        [SerializeField] private float sightDistance = 1.5f;
        [SerializeField] private float angle = 45f;

        public float SightDistance => sightDistance;
        public float Angle => angle;

        private void OnDrawGizmos()
        {
            Handles.color = Color.green;
            float arcStartAngle = angle * .5f + -transform.eulerAngles.y + 90f;
            float x = Mathf.Cos(Mathf.Deg2Rad * arcStartAngle) * sightDistance;
            float y = Mathf.Sin(Mathf.Deg2Rad * arcStartAngle) * sightDistance;
            Vector3 arcStartVector = new Vector3(x, 0f, y);
            Handles.DrawWireArc(transform.position, Vector3.up, arcStartVector, angle, sightDistance);
            Handles.DrawLine(transform.position, transform.position + Vector3.forward * sightDistance);
        }
    }
}