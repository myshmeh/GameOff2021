using UnityEngine;

namespace UI.BlackRect
{
    public class BlackRectAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        public void FadeIn()
        {
            animator.SetBool("IsFadeIn", true);
        }

        public void FadeOut()
        {
            animator.SetBool("IsFadeIn", false);
        }
    }
}