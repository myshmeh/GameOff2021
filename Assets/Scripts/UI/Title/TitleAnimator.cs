using UnityEngine;

namespace UI.Title
{
    public class TitleAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string OpenStateName = "Open";
        
        public void Show()
        {
            animator.SetBool("IsOpen", true);
        }

        public bool IsShowing()
        {
            return IsPlaying(OpenStateName);
        }

        bool IsPlaying(string stateName)
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                   animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;
        }
    }
}