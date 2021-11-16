using System.Collections;
using Game;
using UnityEngine;

namespace Stage.EventAction
{
    public class MissionFailedAction : MonoBehaviour
    {
        private void Start()
        {
            StageManager.OnMissionFailed += OnMissionFailed;
        }

        IEnumerator DoOnMissionFailed()
        {
            yield return new WaitForSeconds(1f);

            GameManager.Instance.OnStageFailed();
        }

        void OnMissionFailed()
        {
            StartCoroutine(DoOnMissionFailed());
        }
    }
}