using System.Collections;
using Game;
using UI.BlackRect;
using UnityEngine;

namespace Stage.StageIntro
{
    public class StageIntroManager : MonoBehaviour
    {
        [SerializeField] private float waitSeconds = 6f;
        private BlackRectAnimator blackRectAnimator;

        IEnumerator WaitAndGoToStage()
        {
            yield return new WaitForSeconds(waitSeconds);
            
            blackRectAnimator.FadeOut();

            yield return new WaitForSeconds(1.5f);
            
            GameManager.Instance.OnStageIntroCompleted();
        }

        private void Start()
        {
            blackRectAnimator = FindObjectOfType<BlackRectAnimator>();
            StartCoroutine(WaitAndGoToStage());
        }
    }
}