using System;
using System.Collections;
using MonoBehaviourWatcher;
using StageObject.Debugger.State;
using UnityEngine;

namespace StageObject.Decoy
{
    public class DecoyController : MonoBehaviour, IAttackable
    {
        [SerializeField] private float vanishWaitSeconds = .2f;
        [SerializeField] private ParticleSystem explosionParticle;
        [SerializeField] private GameObject meshObject;
        [SerializeField] private float throwingAnimationWaitSeconds = .025f;
        [Watchable] private bool alive = true;
        [Watchable] private bool thrown = false;

        public bool Alive => alive;

        IEnumerator DoOnAttack()
        {
            explosionParticle.Play();
            yield return new WaitForSeconds(vanishWaitSeconds);
            meshObject.SetActive(false);
        }

        public void OnAttack()
        {
            alive = false;
            StartCoroutine(DoOnAttack());
        }

        IEnumerator DoThrowTo(Vector3 direction, float distance)
        {
            // get destination position
            Vector3 _maxDestination = transform.position + direction * distance;
            if (Physics.Linecast(transform.position, _maxDestination, out RaycastHit _hitInfo, LayerMask.GetMask("Obstacle")))
            {
                Vector3 _toHitPosition = _hitInfo.point - transform.position;
                Vector3 _hitDirection = _toHitPosition.normalized;
                _maxDestination = transform.position + _hitDirection * (_toHitPosition.magnitude * .8f);
            }

            // throwing decoy animation
            Vector3 _beforeThrowingPositin = transform.position;
            Vector3 _dist = _maxDestination - _beforeThrowingPositin;
            for (int i = 0; i < 20; i++)
            {
                float heightRate = Mathf.Sin(Mathf.PI * i * .05f) * .5f;
                transform.position = _beforeThrowingPositin + Vector3.up * heightRate + _dist * (i * .05f);
                yield return new WaitForSeconds(throwingAnimationWaitSeconds);
            }
            
            transform.position = _maxDestination;
        }

        public void ThrowTo(Vector3 direction, float distance)
        {
            if (thrown) return;

            thrown = true;
            StartCoroutine(DoThrowTo(direction, distance));
        }
    }
}