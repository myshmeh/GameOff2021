using System;
using UnityEngine;

namespace StageObject.Player.Guideline
{
    [ExecuteAlways]
    public class Guideline : MonoBehaviour
    {
        [SerializeField] private Transform square;
        [SerializeField] private Transform squareIdealPosition;
        [SerializeField] private float squareCenteringOffset = .5f;
        [SerializeField] private Transform line;

        void UpdateSquarePosition()
        {
            square.position = squareIdealPosition.position + Vector3.forward * squareCenteringOffset;
        }
        
        private void OnDrawGizmos()
        {
            UpdateSquarePosition();
        }

        // TODO leaving like this has a risk of no square position alignment to the line at all in a build
        // private void Update()
        // {
        //     TODO not working well (not perfect alignment
        //     UpdateSquarePosition();
        // }
    }
}