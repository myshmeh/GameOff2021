using UnityEngine;

namespace StageObject.Debugger
{
    public class DebuggerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        // private bool idle = false;

        public void Idle()
        {
            // if (idle) return;
            
            animator.SetBool("IsMoving", false);
            // idle = true;
        }

        public void Move()
        {
            // if (!idle) return;
            
            animator.SetBool("IsMoving", true);
            // idle = false;
        }
    }
}