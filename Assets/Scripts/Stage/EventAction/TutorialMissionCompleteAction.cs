using System;
using System.Collections;
using Game;
using UI.Dialogue;
using UI.Title;
using UnityEngine;

namespace Stage.EventAction
{
    public class TutorialMissionCompleteAction : MonoBehaviour
    {
        [SerializeField] private DialogueAnimator dialogueAnimator;
        [SerializeField] private DialogueBoxAnimator dialogueBoxAnimator;
        [SerializeField] private TitleAnimator titleAnimator;
        
        private void Start()
        {
            StageManager.OnMissionComplete += OnMissionComplete;
        }

        IEnumerator ShowTitle()
        {
            dialogueBoxAnimator.Hide();

            yield return new WaitForSeconds(1f);

            titleAnimator.Show();

            yield return new WaitForSeconds(3f);

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