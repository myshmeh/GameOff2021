using System;
using System.Collections;
using Game;
using MonoBehaviourWatcher;
using StageObject.Debugger.State;
using StageObject.Server;
using UnityEngine;

namespace Stage
{
    public class StageManager : MonoBehaviour
    {
        public static event Action OnMissionComplete;

        [SerializeField] private float completeAwaitSeconds = 1f;
        [SerializeField] private float failureAwaitSeconds = 1f;
        private ServerController[] serverControllers;

        [Watchable] private bool completed;
        [Watchable] private bool failed;

        private void OnDestroy()
        {
            OnMissionComplete = null;
        }
        
        IEnumerator WillFailStage()
        {
            yield return new WaitForSeconds(failureAwaitSeconds);
            
            GameManager.Instance.OnStageFailed();
        }

        private void Start()
        {
            PatrolState.OnPlayerFound += () =>
            {
                failed = true;
                StartCoroutine(WillFailStage());
            };

            serverControllers = FindObjectsOfType<ServerController>();
        }

        void OnMissionCompleteSafely()
        {
            if (OnMissionComplete == null) return;
            OnMissionComplete();
        }

        IEnumerator WillCompleteStage()
        {
            yield return new WaitForSeconds(completeAwaitSeconds);
            
            GameManager.Instance.OnStageCompleted();
        }

        private void Update()
        {
            if (!ShouldBeCompleted()) return; 
            if (completed) return;

            completed = true;
            OnMissionCompleteSafely();
            StartCoroutine(WillCompleteStage());
        }

        private bool ShouldBeCompleted() => ServerDownCount() == serverControllers.Length;

        [Watchable]
        private int ServerDownCount()
        {
            int num = 0;
            foreach (ServerController serverController in serverControllers)
            {
                if (!serverController.Cracked) continue;
                num++;
            }

            return num;
        }
    }
}