using UnityEngine;
using UnityEngine.Events;

namespace GameJamStarterKit
{
    public class AudioSourceUnityEvent : UnityEvent<AudioSource> { }

    /// <summary>
    /// Adds callbacks for when an attached audio source starts and stops playback.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceCallback : MonoBehaviour
    {
        public AudioSource Source;

        /// <summary>
        /// invoked when the attached audio source begins playing
        /// </summary>
        public AudioSourceUnityEvent OnPlay = new AudioSourceUnityEvent();

        /// <summary>
        /// invoked when the attached audio source stops playing
        /// </summary>
        public AudioSourceUnityEvent OnStop = new AudioSourceUnityEvent();

        private bool _wasPlaying;

        private void Awake()
        {
            if (Source == null)
            {
                Source = GetComponent<AudioSource>();
            }
        }

        private void Update()
        {
            if (Source.isPlaying && !_wasPlaying)
            {
                _wasPlaying = true;
                OnPlay.Invoke(Source);
            }
            else if (!Source.isPlaying && _wasPlaying)
            {
                _wasPlaying = false;
                OnStop.Invoke(Source);
            }
        }
    }
}