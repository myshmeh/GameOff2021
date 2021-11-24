using System;
using System.Collections;
using StageObject.Player;
using UnityEngine;
using TMPro;

namespace StageObject.Decoy
{
    public class DecoyCountUI : MonoBehaviour
    {
        [SerializeField] private TextMeshPro text;
        [SerializeField] private float textVisibleSeconds = 1f;
        [SerializeField] private string prefix = "decoyX";
        private PlayerController playerController;
        private int previousCount;

        void UpdateText(int count)
        {
            text.text = $"{prefix}{count}";
        }

        private void Start()
        {
            playerController = FindObjectOfType<PlayerController>();
            if (playerController == null)
                throw new Exception("player controller not found");
            
            previousCount = playerController.DecoyCount;
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
            int _newCount = playerController.DecoyCount;
            
            if (previousCount == _newCount) return;

            previousCount = _newCount;
            
            StopAllCoroutines();
            StartCoroutine(Animate(_newCount));
        }
    }
}