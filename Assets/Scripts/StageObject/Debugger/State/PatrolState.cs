using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StageObject.Debugger.State.Attack;
using StageObject.Decoy;
using StageObject.Player;
using StateMachine.Context;
using StateMachine.State;
using UnityEngine;

namespace StageObject.Debugger.State
{
    public class PatrolState : IState
    {
        public static event Action OnPlayerFound;

        public static void InitOnPlayerFound()
        {
            OnPlayerFound = null;
        }

        private Context<DebuggerController> context;
        private Transform transform;
        private Vector3[] waypoints;
        private Transform player;
        private float sightDistance;
        private float sightAngle;
        private Light spotLight;
        private LayerMask obstacleMask;
        private Coroutine moveAlongPathsCoroutine = null;

        public PatrolState(Context<DebuggerController> context)
        {
            this.context = context;
            waypoints = TransformConverter.ToArray(context.Client.waypoints);
            spotLight = context.Client.spotLight;
            transform = this.context.Client.transform;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            sightDistance = context.Client.sight.SightDistance;
            sightAngle = context.Client.sight.Angle;
            obstacleMask = context.Client.obstacleMask;
        }

        public void Enter()
        {
            if (moveAlongPathsCoroutine != null) return;

            moveAlongPathsCoroutine = context.Client.StartCoroutine(MoveAlongPaths());
        }

        bool CollideWithDoor(Vector3 positionToMove)
        {
            return Physics.Linecast(transform.position, positionToMove, out RaycastHit _hitInfo) &&
                   _hitInfo.collider.CompareTag("Door");
        }

        IEnumerator MoveAlongPaths()
        {
            transform.position = waypoints[0];

            int _index = 1 % waypoints.Length;
            Vector3 _target = waypoints[_index];
            transform.LookAt(_target);
            while (true)
            {
                Vector3 _moveTowards = Vector3.MoveTowards(transform.position, _target, context.Client.speed * Time.deltaTime);

                if (CollideWithDoor(_moveTowards))
                    yield return new WaitUntil(() => !CollideWithDoor(_moveTowards));

                transform.position = _moveTowards;

                if (Mathf.Approximately(0f, (transform.position - _target).magnitude))
                {
                    _index = (_index + 1) % waypoints.Length;
                    _target = waypoints[_index];
                    transform.LookAt(_target);
                }

                yield return new WaitUntil(() => context.CurrentStateName == Name);
            }
        }

        public void Exit()
        {
        }

        public void HandleInput()
        {
        }

        bool CanSeePlayer()
        {
            if (Vector3.Distance(transform.position, player.position) > sightDistance)
                return false;

            Vector3 toPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, toPlayer) > sightAngle * .5f)
                return false;

            if (Physics.Linecast(transform.position, player.position, obstacleMask))
                return false;

            PlayerController _playerController = player.GetComponent<PlayerController>();
            if (_playerController.CurrentStateName != "Moving")
                return false;

            return true;
        }

        bool CanSeeDecoy(DecoyController decoy)
        {
            Vector3 decoyPosition = decoy.transform.position;

            if (Vector3.Distance(transform.position, decoyPosition) > sightDistance)
                return false;

            Vector3 toDecoy = (decoyPosition - transform.position).normalized;
            if (Vector3.Angle(transform.forward, toDecoy) > sightAngle * .5f)
                return false;

            if (Physics.Linecast(transform.position, decoyPosition, obstacleMask))
                return false;

            if (!decoy.Alive) return false;

            return true;
        }

        Transform GetVisibleDecoyTransform()
        {
            List<DecoyController> decoys = context.Client.decoys;
            DecoyController visibleDecoy = decoys.Where(CanSeeDecoy).FirstOrDefault();

            if (visibleDecoy == null) return null;
            return visibleDecoy.transform;
        }

        void OnPlayerFoundSafely()
        {
            if (OnPlayerFound == null) return;
            OnPlayerFound();
        }

        public void UpdateLogic()
        {
            Transform visibleDecoy;
            if ((visibleDecoy = GetVisibleDecoyTransform()) != null)
            {
                spotLight.color = Color.blue;
                IState attackState = new AttackState(context, visibleDecoy);
                context.PushState(attackState);
            }
            else if (CanSeePlayer())
            {
                spotLight.color = Color.yellow;
                IState attackState = new AttackState(context, player);
                context.PushState(attackState);
                OnPlayerFoundSafely();
            }
            else
            {
                spotLight.color = context.Client.PrimaryColor;
            }
        }

        public void UpdatePhysics()
        {
        }

        public string Name => "Patrol";
    }
}