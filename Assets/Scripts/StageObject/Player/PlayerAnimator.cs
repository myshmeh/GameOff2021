using UnityEngine;

namespace StageObject.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private bool idle = true;

        public void Move()
        {
            if (!idle) return;
            
            animator.SetBool("IsMoving", true);
            idle = false;
        }

        public void Idle()
        {
            if (idle) return;
            
            animator.SetBool("IsMoving", false);
            idle = true;
        }
    }
}