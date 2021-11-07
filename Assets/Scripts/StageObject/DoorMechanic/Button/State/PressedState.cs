using StateMachine.Context;
using StateMachine.State;

namespace StageObject.DoorMechanic.Button.State
{
    public class PressedState : IState
    {
        private Context<ButtonController> context;

        public PressedState(Context<ButtonController> context)
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

        public string Name => ButtonStateName.Pressed.ToString();
    }
}