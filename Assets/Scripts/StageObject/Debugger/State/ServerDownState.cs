using StateMachine.Context;
using StateMachine.State;
using UnityEngine;

namespace StageObject.Debugger.State
{
    public class ServerDownState : IState
    {
        public ServerDownState(Context<DebuggerController> context)
        {
            BrandColor.SetupBrandColor(context.Client, Color.white);
            context.Client.spotLight.color = Color.gray;
            context.Client.smokeParticle.Play();
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