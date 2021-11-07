using StateMachine.Context;
using StateMachine.State;

namespace StageObject.DoorMechanic.Button.State
{
    public class AwaitingState : IState
    {
        private Context<ButtonController> context;

        public AwaitingState(Context<ButtonController> context)
        {
            this.context = context;
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

        public string Name => ButtonStateName.Awaiting.ToString();
    }
}