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
        [SerializeField] private float waitResponseSeconds = 2.5f;
        [SerializeField] private float waitAtTheEndSeconds = 2.5f;
        [SerializeField] private float titleDisplayDelaySeconds = 1f;
        [SerializeField] private float titleDisplaySeconds = 4f;
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

            yield return new WaitForSeconds(titleDisplayDelaySeconds);

            titleAnimator.Show();

            yield return new WaitForSeconds(titleDisplaySeconds);
            
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