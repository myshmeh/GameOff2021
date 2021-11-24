using System.Collections;
using Game;
using UI.BlackRect;
using UI.Dialogue;
using UnityEngine;

namespace Stage.EventAction
{
    public class MissionCompleteAction : MonoBehaviour
    {
        [SerializeField] private DialogueAnimator dialogueAnimator;
        private DialogueBoxAnimator dialogueBoxAnimator;
        private BlackRectAnimator blackRectAnimator;
        
        private void Start()
        {
            StageManager.OnMissionComplete += OnMissionComplete;
            blackRectAnimator = FindObjectOfType<BlackRectAnimator>();
            dialogueBoxAnimator = FindObjectOfType<DialogueBoxAnimator>();
        }

        IEnumerator DoOnMissionComplete()
        {
            yield return new WaitForSeconds(.5f);
            
            dialogueBoxAnimator.Show();

            yield return new WaitForSeconds(1f);
            
            dialogueAnimator.Animate(0f);

            yield return new WaitForSeconds(6f);
            
            blackRectAnimator.FadeOut();
            
            yield return new WaitForSeconds(1.5f);

            GameManager.Instance.OnStageCompleted();
        }

        void OnMissionComplete()
        {
            StartCoroutine(DoOnMissionComplete());
        }
    }
}