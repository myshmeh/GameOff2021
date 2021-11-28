using System;
using System.Collections;
using CMF;
using StageObject.Server;
using StateMachine.Context;
using StateMachine.State;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StageObject.Player.State
{
    public class MovingState : IState
    {
        private Context<PlayerController> context;
        private Transform transform;
        private float crackableDistance;
        private Rigidbody rigidbody;
        private PlayerAnimator animator;
        
        public MovingState(Context<PlayerController> context)
        {
            this.context = context;
            transform = context.Client.transform;
            crackableDistance = context.Client.crackableDistance;
            rigidbody = context.Client.GetComponent<Rigidbody>();
            animator = context.Client.animator;
        }

        public void Enter()
        {
            context.Client.TurnMovingModel(true);
            animator.Move();
            context.Client.StartCoroutine(DoFootSound());
        }

        public void Exit()
        {
            context.Client.TurnMovingModel(false);
            animator.Idle();
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
                        if (_serverController.Cracked) return;
                        
                        context.PushState(new CrackingState(context, _serverController, hitInfo.point, hitInfo.normal));

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
            if (rigidbody.velocity.sqrMagnitude < .25f)
            {
                animator.Idle();   
            }
            else
            {
                animator.Move();
            }
        }

        IEnumerator DoFootSound()
        {
            while (context.CurrentStateName == Name)
            {
                yield return new WaitForSeconds(.15f);
                yield return new WaitUntil(() => rigidbody.velocity.sqrMagnitude > .25f);
                
                context.Client.movingAudioSource.pitch = Random.Range(1f, 1.25f);
                context.Client.movingAudioSource.Play();    
            }
        }

        public string Name => "Moving";
    }
}