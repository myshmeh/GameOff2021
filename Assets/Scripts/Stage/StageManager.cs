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
        public static event Action OnMissionFailed;

        private ServerController[] serverControllers;

        [Watchable] private bool completed;
        [Watchable] private bool failed;
        
        private void OnDestroy()
        {
            OnMissionComplete = null;
            OnMissionFailed = null;
        }
        
        void OnMissionFailedSafely()
        {
            if (OnMissionFailed == null) return;
            OnMissionFailed();
        }

        private void Start()
        {
            PatrolState.OnPlayerFound += () =>
            {
                if (failed) return;
                
                failed = true;
                OnMissionFailedSafely();
            };

            serverControllers = FindObjectsOfType<ServerController>();
        }

        void OnMissionCompleteSafely()
        {
            if (OnMissionComplete == null) return;
            OnMissionComplete();
        }

        private void Update()
        {
            if (!ShouldBeCompleted()) return; 
            if (completed) return;

            completed = true;
            OnMissionCompleteSafely();
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