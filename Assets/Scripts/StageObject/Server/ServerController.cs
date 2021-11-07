using System;
using System.Collections;
using MonoBehaviourWatcher;
using StageObject.Debugger;
using StageObject.Player.State;
using UnityEditor;
using UnityEngine;

namespace StageObject.Server
{
    public class ServerController : MonoBehaviour
    {
        [SerializeField] private float crackingSeconds = 5f;
        [SerializeField] private DebuggerController[] debuggers;
        [SerializeField] private Color primaryColor = Color.blue;
        [SerializeField] private Shader shader;

        [Watchable] private bool cracked;

        public bool Cracked => cracked;

        void SetColorToAppearance(Color color)
        {
            MeshRenderer _meshRenderer = GetComponentInChildren<MeshRenderer>();
            Material newMaterial = new Material(shader);
            newMaterial.color = color;
            _meshRenderer.material = newMaterial;
        }

        private void Awake()
        {
            cracked = false;
            SetColorToAppearance(primaryColor);
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
            SetColorToAppearance(Color.gray);
            afterCrack();
            foreach (IClient d in debuggers)
            {
                d.OnServerDown();
            }
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