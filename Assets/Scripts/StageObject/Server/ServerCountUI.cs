using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using TMPro;

namespace StageObject.Server
{
    public class ServerCountUI : MonoBehaviour
    {
        [SerializeField] private TextMeshPro text;
        [SerializeField] private float textVisibleSeconds = 1f;
        [SerializeField] private string prefix = "serverX";
        private ServerController[] serverControllers;
        private int previousCount;

        void UpdateText(int count)
        {
            text.text = $"{prefix}{count}";
        }

        private void Start()
        {
            serverControllers = FindObjectsOfType<ServerController>();
            if (serverControllers.Length == 0)
                throw new Exception("no server controller found");
            
            previousCount = serverControllers.Count(s => !s.Cracked);
            UpdateText(previousCount);
            text.enabled = false;
        }

        IEnumerator Animate(int newCount)
        {
            text.enabled = true;
            
            yield return new WaitForSeconds(textVisibleSeconds * .2f);

            UpdateText(newCount);

            yield return new WaitForSeconds(textVisibleSeconds * .8f);

            text.enabled = false;
        }

        private void Update()
        {
            int _newCount = serverControllers.Count(s => !s.Cracked);
            
            if (previousCount == _newCount) return;

            previousCount = _newCount;
            
            StopAllCoroutines();
            StartCoroutine(Animate(_newCount));
        }
    }
}