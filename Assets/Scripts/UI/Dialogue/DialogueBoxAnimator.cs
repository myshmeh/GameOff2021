using UnityEngine;

namespace UI.Dialogue
{
    public class DialogueBoxAnimator : MonoBehaviour
    {
        public Animator animator;
        public AudioSource dialogueBoxUpAudioSource;

        public void Show()
        {
            dialogueBoxUpAudioSource.Play();
            animator.SetBool("IsOpen", true);
        }

        public void Hide()
        {
            animator.SetBool("IsOpen", false);
        }
    }
}