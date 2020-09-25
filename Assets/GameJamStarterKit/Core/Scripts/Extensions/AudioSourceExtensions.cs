using System.Collections;
using System.Linq;
using UnityEngine;

namespace GameJamStarterKit
{
    /// <summary>
    /// Useful extensions for AudioSources
    /// </summary>
    public static class AudioSourceExtensions
    {
        /// <summary>
        /// Fades out an audio source
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="duration">how long the fade out takes in seconds</param>
        public static void FadeOut(this AudioSource source, float duration)
        {
            FadeTo(source, duration, 0.0f);
        }

        /// <summary>
        /// Fades the audio source to the given volume
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="duration">how long the fade takes in seconds</param>
        /// <param name="targetVolume">the target volume</param>
        public static void FadeTo(this AudioSource source, float duration, float targetVolume)
        {
            source.GetComponent<MonoBehaviour>().StartCoroutine(Fade(source, duration, targetVolume));
        }

        /// <summary>
        /// Fades in an audio source
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="duration">how long the fade in takes in seconds</param>
        /// <param name="targetVolume">the target volume after fully faded in</param>
        public static void FadeIn(this AudioSource source, float duration, float targetVolume = 1.0f)
        {
            FadeTo(source, duration, targetVolume);
        }

        /// <summary>
        /// Coroutine to fade an audio source over a duration to the target volume.
        /// </summary>
        /// <param name="source">source to fade</param>
        /// <param name="duration">how long the fade takes in seconds</param>
        /// <param name="targetVolume">the target volume to fade to</param>
        /// <returns></returns>
        public static IEnumerator Fade(AudioSource source, float duration, float targetVolume)
        {
            if (!source.isPlaying)
            {
                source.volume = 0.01f;
                source.Play();
            }

            var startVolume = source.volume;

            UnscaledTimeSince timeSinceStart = 0f;

            // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
            while (timeSinceStart < duration)
            {
                source.volume = Mathf.Lerp(startVolume, targetVolume, timeSinceStart / duration);
                yield return null;
            }

            source.volume = targetVolume;

            if (Mathf.Approximately(source.volume, 0f))
            {
                source.Stop();
            }
        }

        /// <summary>
        /// Returns the attached AudioSourceCallback, creates one and attaches it if there is none.
        /// </summary>
        /// <param name="source">this</param>
        /// <returns><see cref="AudioSourceCallback"/></returns>
        public static AudioSourceCallback GetCallback(this AudioSource source)
        {
            var callback = source.GetComponents<AudioSourceCallback>()
                .FirstOrDefault(cb => cb.Source == source);

            if (callback == null)
            {
                callback = source.gameObject.AddComponent<AudioSourceCallback>();
                callback.Source = source;
            }

            return callback;
        }
    }
}