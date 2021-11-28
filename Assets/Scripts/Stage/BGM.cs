using System;
using System.Collections;
using Audio;
using UnityEngine;

namespace Stage
{
    public class BGM : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float fadeInDuration = 1f;
        [SerializeField] private float fadeOutDuration = 1f;
        [SerializeField] private float maxVolume = 1f;
        [SerializeField] private bool loop = true;

        private void Start()
        {
            audioSource.loop = loop;
            audioSource.Play();
            
            StopAllCoroutines();
            StartCoroutine(Fade.In(audioSource, fadeInDuration, maxVolume));
        }

        public void FadeOut()
        {
            StopAllCoroutines();
            StartCoroutine(Fade.Out(audioSource, fadeOutDuration, maxVolume));
        }
    }
}