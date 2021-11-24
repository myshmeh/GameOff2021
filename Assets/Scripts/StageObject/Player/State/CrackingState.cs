using System;
using StageObject.Server;
using StateMachine.Context;
using StateMachine.State;
using UnityEngine;

namespace StageObject.Player.State
{
    public class CrackingState : IState
    {
        private Context<PlayerController> context;
        private Vector3 mountSurfacePoint;
        private Vector3 mountSurfaceNormal;

        public CrackingState(Context<PlayerController> context, ServerController serverController,
            Vector3 mountSurfacePoint, Vector3 mountSurfaceNormal)
        {
            this.context = context;
            serverController.Crack(() =>
            {
                context.Client.RechargeDecoy();
                context.PopState();
            });

            this.mountSurfaceNormal = mountSurfaceNormal;
            this.mountSurfacePoint = mountSurfacePoint;
        }

        public void Enter()
        {
            context.Client.TurnNonMovingModel(true);
            context.Client.TurnNonMovingModel(false);
            context.Client.TurnOnCrackingModel(mountSurfacePoint, mountSurfaceNormal);
        }

        public void Exit()
        {
            context.Client.TurnNonMovingModel(false);
            context.Client.TurnOffCrackingModel();
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

        public string Name => "Cracking";
    }
}