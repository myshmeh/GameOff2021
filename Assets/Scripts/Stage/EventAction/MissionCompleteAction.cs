using System.Collections;
using Game;
using UnityEngine;

namespace Stage.EventAction
{
    public class MissionCompleteAction : MonoBehaviour
    {
        private void Start()
        {
            StageManager.OnMissionComplete += OnMissionComplete;
        }

        IEnumerator DoOnMissionComplete()
        {
            yield return new WaitForSeconds(1f);

            GameManager.Instance.OnStageCompleted();
        }

        void OnMissionComplete()
        {
            StartCoroutine(DoOnMissionComplete());
        }
    }
}