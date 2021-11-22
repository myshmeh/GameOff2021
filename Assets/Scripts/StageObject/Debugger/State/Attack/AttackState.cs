using System.Collections;
using System.Linq;
using StateMachine.Context;
using StateMachine.State;
using UnityEngine;

namespace StageObject.Debugger.State.Attack
{
    public class AttackState : IState
    {
        private Context<DebuggerController> context;
        private IAttackable target;
        private Vector3 targetPosition;

        public AttackState(Context<DebuggerController> context, Transform targetTr)
        {
            this.context = context; 
            target = targetTr.GetComponent<IAttackable>();
            targetPosition = targetTr.position;
        }

        IEnumerator Attack()
        {
            Laser();
            
            // camera shake
            var cameraShaker = CameraShaker.Instance;
            if (cameraShaker != null)
                cameraShaker.Shake(CameraShakeDuration.Short, CameraShakeMagnitude.Low);
            
            // take back fx
            Transform transform = context.Client.transform;
            Vector3 position = context.Client.transform.position;
            Vector3 takeBackDirection = -transform.forward;
            transform.position += takeBackDirection * .1f;
            yield return new WaitForSeconds(.2f);
            
            for (int i=0; i<2; i++)
            {
                transform.position += -takeBackDirection * .05f;
                yield return null;
            }
            transform.position = position;

            // recharging
            // smoke fx
            context.Client.smokeParticle.Play();
            // color blink fx
            var _meshRenderer = context.Client.GetComponentInChildren<MeshRenderer>();
            Color _color = _meshRenderer.material.color;
            BrandColor.SetupBrandColor(context.Client, Color.gray);
            int blinkCount = 100;
            for (int i = 0; i < blinkCount; i++)
            {
                Color temporaryColor = Random.Range(0, 2) == 0 ? _color : Color.gray;
                BrandColor.SetupBrandColor(context.Client, temporaryColor);
                yield return new WaitForSeconds(context.Client.rechargeSeconds / blinkCount);    
            }
            
            // bring back states
            BrandColor.SetupBrandColor(context.Client, _color);
            context.Client.smokeParticle.Stop();

            context.PopState();
        }

        void Laser()
        {
            Transform particlesParent = context.Client.attackParticlesParent;
            ParticleSystem[] particles = particlesParent.GetComponentsInChildren<ParticleSystem>();
            
            particlesParent.LookAt(targetPosition);
            foreach (var particle in particles)
            {
                particle.Play();
            }
        }
        
        public void Enter()
        {
            context.Client.spotLight.color = Color.gray;
            
            target.OnAttack();
            context.Client.StartCoroutine(Attack());
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

        public string Name => "Attack";
    }
}