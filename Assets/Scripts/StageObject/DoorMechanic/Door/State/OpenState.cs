using System.Collections;
using StateMachine.Context;
using StateMachine.State;
using UnityEngine;

namespace StageObject.DoorMechanic.Door.State
{
    public class OpenState : IState
    {
        private Context<DoorController> context;
        
        public OpenState(Context<DoorController> context)
        {
            this.context = context;
        }

        private void Open()
        {
            context.Client.Animator.SetBool("IsOpen", true);
        }

        public void Enter()
        {
            context.Client.GetComponent<BoxCollider>().enabled = true;
            Open();
        }

        public void Exit()
        {
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

        public string Name => DoorStateName.Open.ToString();
    }
}