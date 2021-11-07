using System;
using System.Collections;
using StageObject.Decoy;
using StateMachine.Context;
using StateMachine.State;
using UnityEngine;

namespace StageObject.Player.State
{
    public class SpawningDecoyState : IState
    {
        public static event Action<DecoyController> OnDecoySpawned;

        public static void InitOnDecoySpawned()
        {
            OnDecoySpawned = null;
        }
        
        private float spawnSeconds = 1f;
        private Context<PlayerController> context;
        
        public SpawningDecoyState(Context<PlayerController> context)
        {
            this.context = context;
        }

        IEnumerator SpawnDecoy()
        {
            yield return new WaitForSeconds(spawnSeconds);
            
            context.Client.ConsumeDecoyCount();
            
            Transform decoy = context.Client.Decoy;
            Vector3 position = context.Client.transform.position;
            Transform playerModelRoot = context.Client.transform.GetChild(0);
            Quaternion quaternion = Quaternion.Euler(playerModelRoot.eulerAngles);
            var ins = GameObject.Instantiate(decoy, position, quaternion);

            OnDecoySpawned(ins.GetComponent<DecoyController>());

            context.PopState();
        }
        
        public void Enter()
        {
            context.Client.TurnNonMovingModel(true);
            context.Client.StartCoroutine(SpawnDecoy());
        }

        public void Exit()
        {
            context.Client.TurnNonMovingModel(false);
        }

        public void HandleInput()
        {
        }

        public void UpdateLogic()
        {
        }

        public void UpdatePhysics()
        {
        }

        public string Name => "SpawningDecoy";
    }
}