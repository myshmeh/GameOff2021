using System.Collections;
using UnityEngine;

namespace UI.Dialogue
{
    public class DialogueBoxTrigger : MonoBehaviour
    {
        public float waitSeconds = 1f;
        public DialogueBoxAnimator dialogueBoxAnimator;

        private void Start()
        {
            StartCoroutine(ShowDialogueBox(waitSeconds));
        }

        IEnumerator ShowDialogueBox(float waitSeconds)
        {
            yield return new WaitForSeconds(waitSeconds);
            
            dialogueBoxAnimator.Show();
        }
    }
}