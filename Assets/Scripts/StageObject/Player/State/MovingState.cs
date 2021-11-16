using System;
using CMF;
using StageObject.Server;
using StateMachine.Context;
using StateMachine.State;
using UnityEngine;

namespace StageObject.Player.State
{
    public class MovingState : IState
    {
        private Context<PlayerController> context;
        private Transform transform;
        private float crackableDistance;
        
        public MovingState(Context<PlayerController> context)
        {
            this.context = context;
            transform = context.Client.transform;
            crackableDistance = context.Client.crackableDistance;
        }

        public void Enter()
        {
            context.Client.TurnMovingModel(true);
        }

        public void Exit()
        {
            context.Client.TurnMovingModel(false);
        }

        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // crack a server
                if (Physics.Raycast(
                    transform.position + Vector3.up * .5f, 
                    transform.GetChild(0).forward,
                    out RaycastHit hitInfo, 
                    crackableDistance))
                {
                    if (hitInfo.collider.CompareTag("Server"))
                    {
                        ServerController _serverController = hitInfo.collider.GetComponent<ServerController>();
                        if (_serverController == null) throw new Exception("ServerController not found");
                        context.PushState(new CrackingState(context, _serverController));

                        return;
                    }
                }
                
                // spawn a decoy
                if (context.Client.OutOfDecoy()) return;
                context.PushState(new SpawningDecoyState(context));
            }
        }

        public void UpdateLogic()
        {
        }

        public void UpdatePhysics()
        {
        }

        public string Name => "Moving";
    }
}