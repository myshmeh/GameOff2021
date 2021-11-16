using System;
using UnityEngine;

namespace Rotator
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private Vector3 rotatingNormal = Vector3.up;

        private void Update()
        {
            transform.Rotate(rotatingNormal, speed * Time.deltaTime);
        }
    }
}