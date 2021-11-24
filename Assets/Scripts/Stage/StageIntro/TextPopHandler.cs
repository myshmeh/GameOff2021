using System.Collections;
using UnityEngine;
using TMPro;

namespace Stage.StageIntro
{
    public class TextPopHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshPro text;
        [SerializeField] private float waitSeconds = 2f;

        IEnumerator Pop()
        {
            yield return new WaitForSeconds(waitSeconds);

            text.enabled = true;
        }
        
        private void Start()
        {
            text.enabled = false;
            StartCoroutine(Pop());
        }
    }
}