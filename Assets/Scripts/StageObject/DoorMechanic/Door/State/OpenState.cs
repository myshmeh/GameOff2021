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
            var _transform = context.Client.transform;
            var _position = _transform.position;
            _position = new Vector3(
                _position.x,
                0f,
                _position.z);
            _transform.position = _position;
        }

        public void Enter()
        {
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