using System;
using System.Collections;
using Game;
using UI.BlackRect;
using UI.Dialogue;
using UnityEngine;

namespace Stage.EventAction
{
    public class TutorialMissionCompleteAction : MonoBehaviour
    {
        [SerializeField] private DialogueAnimator dialogueAnimator;
        [SerializeField] private DialogueBoxAnimator dialogueBoxAnimator;
        [SerializeField] private BlackRectAnimator blackRectAnimator;
        [SerializeField] private float waitResponseSeconds = 2.5f;
        [SerializeField] private float waitAtTheEndSeconds = 2.5f;
        [SerializeField] private float waitUntilFadeOutSeconds = 1f;
        [SerializeField] private float fadeOutSeconds = 1.5f;

        private BGM bgm;
        
        private void Start()
        {
            StageManager.OnMissionComplete += OnMissionComplete;
            bgm = FindObjectOfType<BGM>();
        }

        IEnumerator ShowTitle()
        {
            dialogueBoxAnimator.Hide();

            yield return new WaitForSeconds(waitUntilFadeOutSeconds);

            blackRectAnimator.FadeOut();
            
            if (bgm != null) bgm.FadeOut();
            
            yield return new WaitForSeconds(fadeOutSeconds);

            GameManager.Instance.OnStageCompleted();
        }

        void AfterDialogueAnimation()
        {
            StartCoroutine(ShowTitle());
        }

        void OnMissionComplete()
        {
            dialogueAnimator.Animate(waitResponseSeconds:waitResponseSeconds, waitAtTheEndSeconds:waitAtTheEndSeconds, afterAnimation:AfterDialogueAnimation);
        }
    }
}