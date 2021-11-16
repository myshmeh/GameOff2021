using System;
using CMF;
using MonoBehaviourWatcher;
using Stage;
using StageObject.Debugger.State;
using StageObject.Player.State;
using StateMachine.Context;
using UnityEngine;

namespace StageObject.Player
{
    public class PlayerController : MonoBehaviour, IAttackable
    {
        public float crackableDistance = 1f;
        [SerializeField] private int maxDecoyCount = 3;
        [SerializeField] private Transform decoy;

        private Context<PlayerController> context;
        [Watchable] private int decoyCount;
        private Rigidbody moverRigidbody;
        private AdvancedWalkerController advancedWalkerController;

        public void ConsumeDecoyCount()
        {
            decoyCount -= 1;
        }

        public void RechargeDecoy()
        {
            decoyCount = maxDecoyCount;
        }

        public bool OutOfDecoy() => context.Client.DecoyCount <= 0;

        public int DecoyCount => decoyCount;
        public Transform Decoy => decoy;
        
        [Watchable] public string CurrentStateName => context.CurrentStateName;

        private void Awake()
        {
            moverRigidbody = GetComponent<Rigidbody>();
            advancedWalkerController = GetComponent<AdvancedWalkerController>();
        }

        private void Start()
        {
            StageManager.OnMissionComplete += () =>
            {
                context.PushState(new MissionCompleteState(context));
            };

            context = new Context<PlayerController>(
                this, 
                ctx => new MovingState(ctx));

            decoyCount = maxDecoyCount;
        }

        private void Update()
        {
            context.HandleInput();
            context.UpdateLogic();
        }

        private void FixedUpdate()
        {
            context.UpdatePhysics();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position + Vector3.up * .2f,
                transform.position + Vector3.up * .2f
                                   + transform.GetChild(0).forward * crackableDistance);
        }

        public void OnAttack()
        {
            context.PushState(new DeadState(context));
        }

        public void Freeze()
        {
            context.PushState(new FrozenState(context));
        }

        public void ChangeWithMovingState()
        {
            context.PushState(new MovingState(context));
        }
        
        public void TurnMovingModel(bool onoff)
        {
            advancedWalkerController.enabled = onoff;
            moverRigidbody.velocity = Vector3.zero;
            moverRigidbody.angularVelocity = Vector3.zero;
            transform.GetChild(0).gameObject.SetActive(onoff);
        }
        
        public void TurnNonMovingModel(bool onoff)
        {
            advancedWalkerController.enabled = false;
            moverRigidbody.velocity = Vector3.zero;
            moverRigidbody.angularVelocity = Vector3.zero;
            transform.GetChild(1).gameObject.SetActive(onoff);
        }
    }
}