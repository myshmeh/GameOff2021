using System.Collections;
using Game;
using UI.Dialogue;
using UnityEngine;

namespace Stage.EventAction
{
    public class TutorialMissionFailedAction : MonoBehaviour
    {
        [SerializeField] private DialogueAnimator dialogueAnimator;
        
        private void Start()
        {
            StageManager.OnMissionFailed += OnMissionFailed;
        }

        void AfterDialogueAnimation()
        {
            GameManager.Instance.OnStageFailed();
        }

        void OnMissionFailed()
        {
            dialogueAnimator.Animate(afterAnimation: AfterDialogueAnimation);
        }
    }
}