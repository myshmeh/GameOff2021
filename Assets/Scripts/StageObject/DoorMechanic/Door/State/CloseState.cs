using System.Collections;
using StateMachine.Context;
using StateMachine.State;
using UnityEngine;

namespace StageObject.DoorMechanic.Door.State
{
    public class CloseState : IState
    {
        private Context<DoorController> context;

        public CloseState(Context<DoorController> context)
        {
            this.context = context;
        }

        void Close()
        {
            context.Client.Animator.SetBool("IsOpen", false);
        }
        
        public void Enter()
        {
            context.Client.GetComponent<BoxCollider>().enabled = false;
            Close();
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

        public string Name => DoorStateName.Close.ToString();
    }
}