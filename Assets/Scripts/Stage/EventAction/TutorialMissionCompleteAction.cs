using System;
using System.Collections;
using Game;
using UI.BlackRect;
using UI.Dialogue;
using UI.Title;
using UnityEngine;
using UnityEngine.UI;

namespace Stage.EventAction
{
    public class TutorialMissionCompleteAction : MonoBehaviour
    {
        [SerializeField] private DialogueAnimator dialogueAnimator;
        [SerializeField] private DialogueBoxAnimator dialogueBoxAnimator;
        [SerializeField] private TitleAnimator titleAnimator;
        [SerializeField] private BlackRectAnimator blackRectAnimator;
        
        private void Start()
        {
            StageManager.OnMissionComplete += OnMissionComplete;
        }

        IEnumerator ShowTitle()
        {
            dialogueBoxAnimator.Hide();

            yield return new WaitForSeconds(1f);

            titleAnimator.Show();

            yield return new WaitForSeconds(2.5f);
            
            blackRectAnimator.FadeOut();
            
            yield return new WaitForSeconds(1.5f);

            GameManager.Instance.OnStageCompleted();
        }

        void AfterDialogueAnimation()
        {
            StartCoroutine(ShowTitle());
        }

        void OnMissionComplete()
        {
            dialogueAnimator.Animate(afterAnimation:AfterDialogueAnimation);
        }
    }
}