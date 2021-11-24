using System.Collections;
using Game;
using UI.BlackRect;
using UI.Dialogue;
using UnityEngine;

namespace Stage.EventAction
{
    public class TutorialMissionFailedAction : MonoBehaviour
    {
        [SerializeField] private DialogueAnimator dialogueAnimator;
        [SerializeField] private BlackRectAnimator blackRectAnimator;
        
        private void Start()
        {
            StageManager.OnMissionFailed += OnMissionFailed;
        }

        IEnumerator DoAfterDialogueAnimation()
        {
            blackRectAnimator.FadeOut();

            yield return new WaitForSeconds(1.5f);
            
            GameManager.Instance.OnStageFailed();
        }

        void AfterDialogueAnimation()
        {
            StartCoroutine(DoAfterDialogueAnimation());
        }

        void OnMissionFailed()
        {
            dialogueAnimator.Animate(afterAnimation: AfterDialogueAnimation);
        }
    }
}