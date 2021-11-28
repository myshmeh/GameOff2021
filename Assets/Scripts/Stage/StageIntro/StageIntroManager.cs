using System.Collections;
using Audio;
using Game;
using UI.BlackRect;
using UnityEngine;

namespace Stage.StageIntro
{
    public class StageIntroManager : MonoBehaviour
    {
        [SerializeField] private float waitSeconds = 6f;
        [SerializeField] private AudioSource bgmAudioSource;
        [SerializeField] private float bgmDuration = 1f;
        [SerializeField] private float bgmVolume = 1f;
        private BlackRectAnimator blackRectAnimator;

        IEnumerator WaitAndGoToStage()
        {
            yield return new WaitForSeconds(waitSeconds);
            
            blackRectAnimator.FadeOut();

            StartCoroutine(Fade.Out(bgmAudioSource, bgmDuration, bgmVolume));

            yield return new WaitForSeconds(1.5f);
            
            GameManager.Instance.OnStageIntroCompleted();
        }

        private void Start()
        {
            bgmAudioSource.Play();
            StartCoroutine(Fade.In(bgmAudioSource, bgmDuration, bgmVolume));
            blackRectAnimator = FindObjectOfType<BlackRectAnimator>();
            StartCoroutine(WaitAndGoToStage());
        }
    }
}