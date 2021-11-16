using UnityEngine;

namespace UI.Dialogue
{
    public class DialogueBoxAnimator : MonoBehaviour
    {
        public Animator animator;

        public void Show()
        {
            animator.SetBool("IsOpen", true);
        }

        public void Hide()
        {
            animator.SetBool("IsOpen", false);
        }
    }
}