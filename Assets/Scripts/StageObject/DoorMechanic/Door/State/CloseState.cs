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
            var _transform = context.Client.transform;
            var _position = _transform.position;
            _position = new Vector3(
                _position.x,
                -1f,
                _position.z);
            _transform.position = _position;
        }
        
        public void Enter()
        {
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