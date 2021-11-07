using StateMachine.Context;
using StateMachine.State;

namespace StageObject.Player.State
{
    public class MissionCompleteState : IState
    {
        public MissionCompleteState(Context<PlayerController> context)
        {
            context.Client.TurnNonMovingModel(true);
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

        public string Name => "MissionComplete";
    }
}