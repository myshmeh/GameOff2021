using System;
using System.Collections;
using MonoBehaviourWatcher;
using StageObject.Debugger;
using UnityEngine;

namespace StageObject.Server
{
    public class ServerController : MonoBehaviour
    {
        [SerializeField] private float crackingSeconds = 5f;
        [SerializeField] private DebuggerController[] debuggers;
        [SerializeField] private Color primaryColor = Color.blue;
        [SerializeField] private ParticleSystem smokeParticle;
        [SerializeField] private ParticleSystem explosionParticle;

        [Watchable] private bool cracked;

        public bool Cracked => cracked;

         private void Awake()
        {
            cracked = false;
            BrandColor.SetupBrandColor(this, primaryColor);
            foreach (IClient d in debuggers)
            {
                d.OnServerSetup(primaryColor);
            }
        }

        public void Crack(Action afterCrack)
        {
            if (cracked) return;
            
            StartCoroutine(DoCrack(afterCrack));
        }

        IEnumerator DoCrack(Action afterCrack)
        {
            yield return new WaitForSeconds(crackingSeconds);

            cracked = true;
            BrandColor.SetupBrandColor(this, Color.gray);
            afterCrack();
            foreach (IClient d in debuggers)
            {
                d.OnServerDown();
            }
            
            smokeParticle.Play();
            explosionParticle.Play();
            var cameraShaker = CameraShaker.Instance;
            if (cameraShaker != null)
                cameraShaker.Shake(CameraShakeDuration.Middle, CameraShakeMagnitude.Medium);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = primaryColor;
            Gizmos.DrawSphere(transform.position + Vector3.up, .2f);

            if (debuggers == null) return;
            foreach (DebuggerController debugger in debuggers)
            {
                Vector3 position = debugger.transform.position;
                Gizmos.DrawSphere(position + Vector3.up * .75f, .2f);
            }
        }
    }
}