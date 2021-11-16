using System;
using StageObject.Player;
using UnityEngine;

namespace UI.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private DialogueAnimator animator;
        [SerializeField] private float delaySeconds = 0f;
        [SerializeField] private float waitCommandSeconds = .5f;
        [SerializeField] private float waitResponseSeconds = 1f;
        [SerializeField] private string targetTagName = "Player";
        [SerializeField] private bool freezePlayer = false;

        private PlayerController playerController;
        private bool triggered = false;

        private void Start()
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(targetTagName)) return;
            if (triggered) return;

            Action _afterAnimation = null;
            if (freezePlayer)
            {
                _afterAnimation = () => { playerController.ChangeWithMovingState(); };
                playerController.Freeze();    
            }

            animator.Animate(delaySeconds, waitCommandSeconds, waitResponseSeconds, _afterAnimation);
            triggered = true;
        }
    }
}