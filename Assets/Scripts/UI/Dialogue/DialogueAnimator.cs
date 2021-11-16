using System;
using System.Collections;
using UnityEngine;

namespace UI.Dialogue
{
    public class DialogueAnimator : MonoBehaviour
    {
        [SerializeField] private Dialogue dialogue;
        [SerializeField] private DialogueBoxUI dialogueBoxUI;

        private char[] nameLetters;
        private char[] commandLetters;
        private char[] bossNameLetters;

        private void Start()
        {
            nameLetters = dialogue.name.ToCharArray();
            commandLetters = dialogue.command.ToCharArray();
            bossNameLetters = dialogue.bossName.ToCharArray();
            ClearUI();
        }

        void ClearUI()
        {
            dialogueBoxUI.ClearResponse();
            dialogueBoxUI.ClearNameAndCommand();
        }

        public void Animate(float delaySeconds = .5f, float waitCommandSeconds = .5f, float waitResponseSeconds = 1f, Action afterAnimation = null)
        {
            StopAllCoroutines();
            ClearUI();
            StartCoroutine(DoAnimate(delaySeconds, waitCommandSeconds, waitResponseSeconds, afterAnimation));
        }

        public IEnumerator DoAnimate(float delaySeconds, float waitCommandSeconds, float waitResponseSeconds, Action afterAnimation)
        {
            yield return new WaitForSeconds(delaySeconds);
            
            dialogueBoxUI.AddToNameAndCommand(dialogue.name);
            dialogueBoxUI.AddToNameAndCommand("$ ");
            
            yield return new WaitForSeconds(waitCommandSeconds);
            
            foreach (char _letter in commandLetters)
            {
                dialogueBoxUI.AddToNameAndCommand(_letter);
                yield return null;
            }

            foreach (var _response in dialogue.responses)
            {
                yield return new WaitForSeconds(waitResponseSeconds);
                dialogueBoxUI.ClearResponse();
                dialogueBoxUI.AddToResponse($"[{dialogue.bossName}]: ");
                
                foreach (char _letter in _response)
                {
                    dialogueBoxUI.AddToResponse(_letter);
                    yield return null;
                }
            }

            afterAnimation?.Invoke();
        }
    }
}