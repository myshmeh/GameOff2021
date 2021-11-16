using System.Collections;
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

        IEnumerator Recharge()
        {
            yield return new WaitForSeconds(context.Client.rechargeSeconds);

            context.PopState();
        }
        
        public void Enter()
        {
            target.OnAttack();
            context.Client.StartCoroutine(Recharge());
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