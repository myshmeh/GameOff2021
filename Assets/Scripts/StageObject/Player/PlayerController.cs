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
        public ParticleSystem explosionParticle;
        public PlayerAnimator animator;
        public AudioSource movingAudioSource;
        public AudioSource explosionAudioSource;
        public AudioSource throwingAudioSource;
        public AudioSource crackingAudioSource;
        public AudioSource attachingAudioSource;

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
            decoyCount = maxDecoyCount;
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
            
            if (!onoff) return;
            Transform nonMovingModel = transform.GetChild(1);
            Transform movingModel = transform.GetChild(0);
            movingModel.position = nonMovingModel.position;
            movingModel.eulerAngles = nonMovingModel.eulerAngles;
        }
        
        public void TurnNonMovingModel(bool onoff)
        {
            advancedWalkerController.enabled = false;
            moverRigidbody.velocity = Vector3.zero;
            moverRigidbody.angularVelocity = Vector3.zero;
            transform.GetChild(1).gameObject.SetActive(onoff);

            if (!onoff) return;
            Transform nonMovingModel = transform.GetChild(1);
            Transform movingModel = transform.GetChild(0);
            nonMovingModel.position = movingModel.position;
            nonMovingModel.eulerAngles = movingModel.eulerAngles;
        }

        public void TurnCrackingModel(bool onoff, Vector3 mountSurfacePoint, Vector3 mountSurfaceNormal)
        {
            advancedWalkerController.enabled = false;
            moverRigidbody.velocity = Vector3.zero;
            moverRigidbody.angularVelocity = Vector3.zero;
            Transform _transform = transform.GetChild(2);
            _transform.gameObject.SetActive(onoff);

            if (!onoff) return;
            _transform.position = mountSurfacePoint;
            _transform.LookAt(mountSurfacePoint + mountSurfaceNormal);
        }

        public void TurnOnCrackingModel(Vector3 mountSurfacePoint, Vector3 mountSurfaceNormal)
        {
            TurnCrackingModel(true, mountSurfacePoint, mountSurfaceNormal);
        }

        public void TurnOffCrackingModel()
        {
            TurnCrackingModel(false, Vector3.zero, Vector3.zero);
        }
    }
}