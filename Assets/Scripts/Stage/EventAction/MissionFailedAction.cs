using System.Collections;
using Game;
using UI.BlackRect;
using UI.Dialogue;
using UnityEngine;

namespace Stage.EventAction
{
    public class MissionFailedAction : MonoBehaviour
    {
        [SerializeField] private DialogueAnimator dialogueAnimator;
        private DialogueBoxAnimator dialogueBoxAnimator;
        private BlackRectAnimator blackRectAnimator;
        
        private void Start()
        {
            StageManager.OnMissionFailed += OnMissionFailed;
            
            blackRectAnimator = FindObjectOfType<BlackRectAnimator>();
            dialogueBoxAnimator = FindObjectOfType<DialogueBoxAnimator>();
        }

        IEnumerator DoOnMissionFailed()
        {
            yield return new WaitForSeconds(.5f);
            
            dialogueBoxAnimator.Show();

            yield return new WaitForSeconds(1f);
            
            dialogueAnimator.Animate(0f, .5f, .5f);

            yield return new WaitForSeconds(3.5f);
            
            blackRectAnimator.FadeOut();
            
            yield return new WaitForSeconds(1.5f);

            GameManager.Instance.OnStageFailed();
        }

        void OnMissionFailed()
        {
            StartCoroutine(DoOnMissionFailed());
        }
    }
}