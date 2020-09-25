using UnityEngine;

namespace GameJamStarterKit.Audio
{
    /// <summary>
    /// Extends <see cref="BasePool{T}"/> providing an easy singleton to play 2d and 3d sound clips or <see cref="AudioClipCollection"/>
    /// </summary>
    public class AudioPool : BasePool<AudioSource>
    {
        /// <summary>
        /// Returns the current instance of this audio pool
        /// </summary>
        public static AudioPool Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("[AudioPool]");
                    _instance = go.AddComponent<AudioPool>();
                }

                return _instance;
            }
        }

        protected void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private static AudioPool _instance;

        /// <summary>
        /// Plays an audio clip in 2D
        /// </summary>
        /// <param name="clip">clip to play</param>
        /// <param name="looping">is this clip looping?</param>
        /// <param name="volume">scale of the clips volume</param>
        /// <param name="pitch">pitch to play the sound at</param>
        /// <param name="pitchVariance">variance in pitch for this clip. 0 will have no variance.</param>
        /// <returns></returns>
        public static AudioSource Play2D(AudioClip clip,
            bool looping = false,
            float volume = 1f,
            float pitch = 1f,
            float pitchVariance = 0f)
        {
            var source = Instance.GetNext();
            source.spatialBlend = 0f;
            source.loop = looping;
            source.pitch = pitch + KitRandom.RandomSign() * pitchVariance;

            source.gameObject.SetActive(true);

            var callback = source.GetComponent<AudioSourceCallback>();

            callback.OnStop.AddListener(s =>
            {
                callback.OnStop.RemoveAllListeners();
                s.gameObject.SetActive(false);
            });
            source.clip = clip;
            source.volume = volume;
            source.Play();
            return source;
        }

        /// <summary>
        /// Plays an audio clip in 3D
        /// </summary>
        /// <param name="clip">clip to play</param>
        /// <param name="position">the position to play this sound at</param>
        /// <param name="looping">is this clip looping?</param>
        /// <param name="volume">scale the clips volume</param>
        /// <param name="pitch">pitch to play the sound at</param>
        /// <param name="pitchVariance">variance in pitch for this clip. 0 will have no variance.</param>
        /// <param name="spatialBlend">spatial blending. 0f = 2D, 1f = 3D</param>
        /// <param name="rollOffMode">the roll off mode to use for this source.</param>
        /// <returns></returns>
        public static AudioSource Play3D(AudioClip clip,
            Vector3 position,
            bool looping = false,
            float volume = 1f,
            float pitch = 1f,
            float pitchVariance = 0f,
            float spatialBlend = 1f,
            AudioRolloffMode rollOffMode = AudioRolloffMode.Logarithmic)
        {
            var source = Instance.GetNext();
            source.loop = looping;
            source.spatialBlend = spatialBlend;
            source.rolloffMode = rollOffMode;
            source.pitch = pitch + KitRandom.RandomSign() * pitchVariance;

            var go = source.gameObject;
            go.SetActive(true);

            var callback = source.GetComponent<AudioSourceCallback>();

            callback.OnStop.AddListener(s =>
            {
                callback.OnStop.RemoveAllListeners();
                s.gameObject.SetActive(false);
            });

            go.transform.position = position;

            source.clip = clip;
            source.volume = volume;
            source.Play();
            return source;
        }

        protected override void InitializeComponent(AudioSource component)
        {
            component.loop = false;
            component.playOnAwake = false;
            component.gameObject.AddComponent<AudioSourceCallback>();
        }
    }
}