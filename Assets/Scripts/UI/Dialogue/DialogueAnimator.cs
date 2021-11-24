using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Dialogue
{
    public class DialogueAnimator : MonoBehaviour
    {
        private static List<DialogueAnimator> instances = new List<DialogueAnimator>();
        
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
            
            instances.Add(this);
        }

        void ClearUI()
        {
            dialogueBoxUI.ClearResponse();
            dialogueBoxUI.ClearNameAndCommand();
        }

        void StopAllInstancesCoroutines()
        {
            foreach (var _instance in instances)
            {
                _instance.StopAllCoroutines();
            }
        }

        public void Animate(float delaySeconds = .5f, float waitCommandSeconds = .5f, float waitResponseSeconds = 1f, float waitAtTheEndSeconds = 0f, float waitNextLetterSeconds = .025f, Action afterAnimation = null)
        {
            StopAllInstancesCoroutines();
            ClearUI();
            StartCoroutine(DoAnimate(delaySeconds, waitCommandSeconds, waitResponseSeconds, waitAtTheEndSeconds, waitNextLetterSeconds, afterAnimation));
        }

        public IEnumerator DoAnimate(float delaySeconds, float waitCommandSeconds, float waitResponseSeconds, float waitAtTheEndSeconds, float waitNextLetterSeconds, Action afterAnimation)
        {
            yield return new WaitForSeconds(delaySeconds);
            
            dialogueBoxUI.AddToNameAndCommand(dialogue.name);
            dialogueBoxUI.AddToNameAndCommand("$ ");
            
            yield return new WaitForSeconds(waitCommandSeconds);
            
            foreach (char _letter in commandLetters)
            {
                dialogueBoxUI.AddToNameAndCommand(_letter);
                yield return new WaitForSeconds(waitNextLetterSeconds);
            }

            foreach (var _response in dialogue.responses)
            {
                yield return new WaitForSeconds(waitResponseSeconds);
                dialogueBoxUI.ClearResponse();
                dialogueBoxUI.AddToResponse($"[{dialogue.bossName}]: ");
                
                foreach (char _letter in _response)
                {
                    dialogueBoxUI.AddToResponse(_letter);
                    yield return new WaitForSeconds(waitNextLetterSeconds);
                }
            }

            yield return new WaitForSeconds(waitAtTheEndSeconds);

            afterAnimation?.Invoke();
        }

        private void OnDestroy()
        {
            instances.Remove(this);
        }
    }
}