using System;
using System.Collections.Generic;
using MonoBehaviourWatcher;
using StageObject.Debugger.State;
using StageObject.Decoy;
using StageObject.Player.State;
using StageObject.Server;
using StateMachine.Context;
using StateMachine.State;
using UnityEngine;

namespace StageObject.Debugger
{
    public class DebuggerController : MonoBehaviour, IClient
    {
        public Transform waypoints;
        public Light spotLight;
        public Sight.Sight sight;
        public LayerMask obstacleMask;
        public List<DecoyController> decoys = new List<DecoyController>();

        private Context<DebuggerController> context;
        private Color primaryColor = Color.white;
        public Color PrimaryColor => primaryColor;

        [Watchable]
        private String CurrentStateName => context.CurrentStateName;

        void Makeup(Color color)
        {
            primaryColor = color;
            spotLight.color = primaryColor;
        }

        private void OnDestroy()
        {
            PatrolState.InitOnPlayerFound();
            SpawningDecoyState.InitOnDecoySpawned();
        }

        private void Awake()
        {
            SpawningDecoyState.OnDecoySpawned += decoyController => decoys.Add(decoyController);
            
            IState InstantiateInitialState(Context<DebuggerController> ctx) => new PatrolState(ctx);
            context = new Context<DebuggerController>(this, InstantiateInitialState);
        }

        private void Update()
        {
            context.HandleInput();
            context.UpdateLogic();
        }

        private void FixedUpdate()
        {
            context.UpdatePhysics();
        }

        public void OnServerSetup(Color serverColor)
        {
            Makeup(serverColor);
        }

        public void OnServerDown()
        {
            context.PushState(new ServerDownState(context));
        }
    }
}