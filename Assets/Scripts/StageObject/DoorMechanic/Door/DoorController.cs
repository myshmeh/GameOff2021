using System;
using MonoBehaviourWatcher;
using StageObject.DoorMechanic.Door.State;
using StateMachine.Context;
using UnityEditor;
using UnityEngine;

namespace StageObject.DoorMechanic.Door
{
    public class DoorController : MonoBehaviour
    {
        private Context<DoorController> context;
        [SerializeField] private DoorStateName initialDoorStateName;
        [SerializeField] private Animator animator;
        
        [Watchable] private string CurrentState => context.CurrentStateName;
        public Animator Animator => animator;

        private void Start()
        {
            context = new Context<DoorController>(this, ctx =>
            {
                switch (initialDoorStateName)
                {
                    case DoorStateName.Open:
                        return new OpenState(ctx);
                    case DoorStateName.Close:
                        return new CloseState(ctx);
                    default:
                        throw new Exception($"{initialDoorStateName} is not supported");
                }
            });
        }
        
        public void SwitchState()
        {
            if (context.CurrentStateName == DoorStateName.Open.ToString())
                context.PushState(new CloseState(context));
            else
                context.PushState(new OpenState(context));
        }

        private void OnDrawGizmos()
        {
            Handles.color = Color.gray;
            Handles.Label(transform.position, initialDoorStateName.ToString());
            // if (initialDoorStateName == DoorStateName.Open)
            //     transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            // else
            //     transform.position = new Vector3(transform.position.x, -1f, transform.position.z);
        }
    }
}