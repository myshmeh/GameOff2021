using System;
using System.Collections;
using MonoBehaviourWatcher;
using StageObject.Debugger.State;
using UnityEngine;

namespace StageObject.Decoy
{
    public class DecoyController : MonoBehaviour, IAttackable
    {
        [SerializeField] private float speed = 1f;
        [Watchable] private bool alive = true;
        [Watchable] private bool thrown = false;

        public bool Alive => alive;

        public void OnAttack()
        {
            alive = false;
        }

        IEnumerator DoThrowTo(Vector3 direction)
        {
            yield return null;
            Vector3 _maxDestination = transform.position + direction * speed;
            if (Physics.Linecast(transform.position, _maxDestination, out RaycastHit _hitInfo))
            {
                Vector3 _toHitPosition = _hitInfo.point - transform.position;
                Vector3 _hitDirection = _toHitPosition.normalized;
                transform.position += _hitDirection * (_toHitPosition.magnitude * .8f);
            }
            else
                transform.position = _maxDestination;
        }

        public void ThrowTo(Vector3 direction)
        {
            if (thrown) return;

            thrown = true;
            StartCoroutine(DoThrowTo(direction));
        }
    }
}