using CMF;
using StateMachine.Context;
using StateMachine.State;

namespace StageObject.Player.State
{
    public class DeadState : IState
    {
        public DeadState(Context<PlayerController> context)
        {
            context.Client.TurnNonMovingModel(true);
            context.Client.explosionParticle.Play();
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

        public string Name => "Dead";
    }
}