using StateMachine.Context;
using StateMachine.State;

namespace StageObject.Player.State
{
    public class FrozenState: IState
    {
        private Context<PlayerController> context;
        
        public FrozenState(Context<PlayerController> context)
        {
            this.context = context;
            context.Client.TurnNonMovingModel(true);
        }
        
        public void Enter()
        {
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

        public string Name => "Frozen";
    }
}