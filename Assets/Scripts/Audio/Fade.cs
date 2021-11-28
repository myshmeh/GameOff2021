using System;
using System.Collections;
using UnityEngine;

namespace Audio
{
    public static class Fade
    {
        public static IEnumerator In(AudioSource audioSource, float duration, float maxVolume = 1f)
        {
            float _timePassed = 0f;

            audioSource.volume = 0;
            
            while (!Mathf.Approximately(audioSource.volume, 1f))
            {
                _timePassed += Time.deltaTime;
                float _t = _timePassed / duration;
                audioSource.volume = Mathf.Clamp(Mathf.Lerp(0f, maxVolume, _t), 0f, 1f);
                yield return null;
            }
        }
        
        public static IEnumerator Out(AudioSource audioSource, float duration, float maxVolume = 1f)
        {
            float _timePassed = 0f;

            audioSource.volume = maxVolume;
            
            while (!Mathf.Approximately(audioSource.volume, 0f))
            {
                _timePassed += Time.deltaTime;
                float _t = _timePassed / duration;
                audioSource.volume = maxVolume - Mathf.Clamp(Mathf.Lerp(0f, maxVolume, _t), 0f, 1f);
                yield return null;
            }
        }
    }
}