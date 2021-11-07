using StateMachine.Context;
using StateMachine.State;
using UnityEngine;

namespace StageObject.Debugger.State
{
    public class ServerDownState : IState
    {
        public ServerDownState(Context<DebuggerController> context)
        {
            context.Client.spotLight.color = Color.gray;
        }
        
        public void Enter()
        {
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

        public string Name => "ServerDown";
    }
}