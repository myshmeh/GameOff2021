using System;
using MonoBehaviourWatcher;
using StageObject.DoorMechanic.Button.State;
using StateMachine.Context;
using UnityEngine;

namespace StageObject.DoorMechanic.Button
{
    public class ButtonController : MonoBehaviour
    {
        private Context<ButtonController> context;
        [SerializeField] private Shader shader;

        [Watchable] private string CurrentState => context.CurrentStateName;

        private void Start()
        {
            context = new Context<ButtonController>(this,
                ctx => new UnpressedState(ctx));
        }

        void UnpressedToPushState()
        {
            if (context.CurrentStateName != ButtonStateName.Unpressed.ToString())
                return;

            context.PushState(new PressedState(context));
        }

        void AwaitingToUnpressedState()
        {
            if (context.CurrentStateName != ButtonStateName.Awaiting.ToString())
                return;
            
            context.PushState(new UnpressedState(context));
        }

        bool PlayerTagName(Collider other)
        {
            return other.CompareTag("Player");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!PlayerTagName(other)) return;
            UnpressedToPushState();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!PlayerTagName(other)) return;
            AwaitingToUnpressedState();
        }
        
        public bool IsPressed()
        {
            return context.CurrentStateName == ButtonStateName.Pressed.ToString();
        }

        public void MoveToAwaitingState()
        {
            context.PushState(new AwaitingState(context));
        }
    }
}