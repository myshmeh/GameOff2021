using System;
using System.Collections;
using Game;
using UnityEngine;

namespace Stage
{
    public class StageIntroManager : MonoBehaviour
    {
        [SerializeField] private float waitSeconds = 5f;

        IEnumerator WaitAndGoToStage()
        {
            yield return new WaitForSeconds(waitSeconds);
            
            GameManager.Instance.OnStageIntroCompleted();
        }

        private void Start()
        {
            StartCoroutine(WaitAndGoToStage());
        }
    }
}