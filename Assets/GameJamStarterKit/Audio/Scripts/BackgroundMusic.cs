using UnityEngine;

namespace GameJamStarterKit.Audio
{
    /// <summary>
    /// A Singleton used to play audio clips or <see cref="AudioClipCollection"/> in the background.
    /// <para>Supports fade in, fade out, and crossfade between clips</para>
    /// </summary>
    public class BackgroundMusic : SingletonBehaviour<BackgroundMusic>
    {
        /// <summary>
        /// Current collection of clips being played
        /// </summary>
        [Tooltip("Current collection of clips being played")]
        public AudioClipCollection ClipCollection;

        /// <summary>
        /// Should this start playing on start?
        /// </summary>
        [Tooltip("Should this start playing on start?")]
        public bool PlayOnStart;

        /// <summary>
        /// Should the music be looping? This means the song will not change unless manually told to.
        /// </summary>
        [Tooltip("Should this collection loop?")]
        public bool Looping;
        /// <summary>
        /// Should new clips fade in?
        /// </summary>
        [Tooltip("Should new clips fade in?")]
        public bool FadeIn;
        
        /// <summary>
        /// How long in seconds fading in takes
        /// </summary>
        [Tooltip("How long in seconds fading in takes")]
        public float FadeInDuration = 2f;

        [Tooltip("This will force the background music to always fade in, even if the last clip was not faded out.")]
        public bool AlwaysFadeIn;

        /// <summary>
        /// Should clips fade out?
        /// </summary>
        [Tooltip("Should clips fade out?")]
        public bool FadeOut;
        
        /// <summary>
        /// How long in seconds fading out takes
        /// </summary>
        [Tooltip("How long in seconds fading out takes")]
        public float FadeOutDuration = 2f;

        /// <summary>
        /// Should cross fade between clips?
        /// </summary>
        [Tooltip("Should cross fade between clips?")]
        public bool CrossFade;

        /// <summary>
        /// How long cross fading takes
        /// </summary>
        [Tooltip("How long cross fading takes")]
        public float CrossFadeDuration = 2f;

        private AudioSource _currentSource;

        private AudioSource _primarySource;

        private AudioSource _secondarySource;

        private TimeSince _timeUntilFade;

        private void Start()
        {
            // setup primary source.
            _primarySource = gameObject.GetOrAddComponent<AudioSource>();
            SetupSource(_primarySource);
            var callback = gameObject.GetOrAddComponent<AudioSourceCallback>();
            callback.Source = _primarySource;

            // setup secondary source
            if (CrossFade)
            {
                CreateSecondarySource();
            }
            else
            {
                callback.OnStop.AddListener(OnSourceStop);
            }

            // set primary as the current.
            _currentSource = _primarySource;

            if (PlayOnStart && !ClipCollection.IsEmpty)
            {
                PlayCollection(ClipCollection);
            }
        }

        private void CreateSecondarySource()
        {
            _secondarySource = gameObject.AddComponent<AudioSource>();
            SetupSource(_secondarySource);
            var callback = gameObject.AddComponent<AudioSourceCallback>();
            callback.Source = _secondarySource;
        }

        /// <summary>
        /// Plays a collection of AudioClips
        /// </summary>
        /// <param name="clipCollection">collection to play</param>
        /// <param name="volume">volume to play the clips at.</param>
        public void PlayCollection(AudioClipCollection clipCollection, float volume = 1f)
        {
            if (volume <= 0f)
            {
                Debug.LogError("[PersistentBackgroundMusic] Volume must be higher than 0f.");
                return;
            }

            _currentSource.loop = Looping;
            ClipCollection = clipCollection;
            var clip = clipCollection.GetClip();

            PlayClip(clip, volume);
        }

        private void CrossFadeClip(AudioClip clip, float volume)
        {
            if (clip == null)
                return;
            
            if (_secondarySource == null)
            {
                CreateSecondarySource();
                // we're probably coming from not being a cross fader
                _primarySource.GetCallback().OnStop.RemoveListener(OnSourceStop);
            }

            AudioSource fadeOutSource;
            AudioSource fadeInSource;
            if (_currentSource == _primarySource && _currentSource.isPlaying)
            {
                fadeOutSource = _primarySource;
                fadeInSource = _secondarySource;
            }
            else
            {
                fadeOutSource = _secondarySource;
                fadeInSource = _primarySource;
            }
            
            if (fadeOutSource.clip != null)
                fadeOutSource.FadeOut(CrossFadeDuration);

            fadeInSource.clip = clip;
            fadeInSource.loop = Looping;
            fadeInSource.FadeIn(CrossFadeDuration, volume);

            _currentSource = fadeInSource;

            _timeUntilFade = -FindTimeUntilFade(clip);
        }

        /// <summary>
        /// Plays a one off audio clip. 
        /// </summary>
        /// <param name="clip">clip to play</param>
        /// <param name="volume">volume of the clip</param>
        /// <param name="looping">if the clip should loop</param>
        public void PlayOneShot(AudioClip clip, float volume = 1f, bool looping = false)
        {
            _currentSource.loop = looping;
            if (CrossFade && _currentSource.clip != null)
            {
                CrossFadeClip(clip, volume);
            }

            var startVolume = FadeIn || AlwaysFadeIn ? 0.01f : volume;

            _currentSource.PlayOneShot(clip, startVolume);
            if (FadeIn || AlwaysFadeIn)
            {
                _currentSource.FadeIn(FadeInDuration, volume);
            }
        }

        /// <summary>
        /// Immediately starts the next clip, respecting fade settings.
        /// </summary>
        public void StartNextClip()
        {
            PlayClip(ClipCollection.GetClip(), _primarySource.volume);
        }
        
        /// <summary>
        /// Immediately starts the clip, respecting fade settings.
        /// </summary>
        /// <param name="clip">clip to play</param>
        /// <param name="volume">volume of the clip</param>
        public void PlayClip(AudioClip clip, float volume)
        {
            if (CrossFade && _currentSource.clip != null)
            {
                CrossFadeClip(clip, volume);
                return;
            }

            _currentSource.clip = clip;
            if (FadeIn)
            {
                _currentSource.FadeIn(FadeInDuration, volume);
            }
            else
            {
                _currentSource.volume = volume;
                _currentSource.Play();
            }
        }

        private void OnSourceStop(AudioSource source)
        {
            if (!ClipCollection.IsEmpty)
            {
                source.clip = ClipCollection.GetClip();

                if (FadeIn && FadeOut || AlwaysFadeIn)
                {
                    source.FadeIn(FadeInDuration, source.volume);
                }
                else
                {
                    source.Play();
                }
            }
        }

        private void SetupSource(AudioSource source)
        {
            source.loop = Looping;
            source.spatialize = false;
            source.spatialBlend = 0f;
            source.playOnAwake = false;
        }

        private float FindTimeUntilFade(AudioClip clip)
        {
            return clip.length - FadeOutDuration;
        }

        private void Update()
        {
            if (Looping)
                return;
            
            if (_timeUntilFade >= 0)
            {
                var clip = ClipCollection.GetClip();
                CrossFadeClip(clip, _currentSource.volume);
            }
        }
    }
}