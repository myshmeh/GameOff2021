using System;
using UnityEngine;

namespace Stage.StageIntro
{
    public class Doom : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        private void Start()
        {
            if (TryGetComponent<TextPopHandler>(out var _component))
            {
                _component.OnPop += () =>
                {
                    audioSource.Play();
                };
            }
        }
    }
}