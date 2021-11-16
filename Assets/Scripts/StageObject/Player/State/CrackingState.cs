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

        public CrackingState(Context<PlayerController> context, ServerController serverController)
        {
            this.context = context;
            serverController.Crack(() =>
            {
                context.Client.RechargeDecoy();
                context.PopState();
            });
        }
        
        public void Enter()
        {
            context.Client.TurnNonMovingModel(true);
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

        public string Name => "Cracking";
    }
}