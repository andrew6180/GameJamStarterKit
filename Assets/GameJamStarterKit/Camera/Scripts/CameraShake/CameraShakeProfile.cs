using UnityEngine;

namespace GameJamStarterKit.Camera 
{
    /// <summary>
    /// Scriptable Object that holds the parameters of a camera shake.
    /// </summary>
    [CreateAssetMenu(fileName = "New CameraShake Profile", menuName = "GameJamStarterKit/Camera/Create CameraShake Profile")]
    public class CameraShakeProfile : ScriptableObject
    {
        /// <summary>
        /// Magnitude of the shake. More intense with higher values.
        /// </summary>
        [Tooltip("Magnitude of the shake. More intense with higher values.")]
        public float Magnitude;
        
        /// <summary>
        /// Roughness of the shake. Higher values are more jerky / violent.
        /// </summary>
        [Tooltip("Roughness of the shake. Higher values are more jerky / violent.")]
        public float Roughness;
        
        /// <summary>
        /// How long to fade in a shake, in seconds.
        /// </summary>
        [Tooltip("How long to fade in a shake, in seconds.")]
        public float FadeInTime;
        
        /// <summary>
        /// How long to fade out a shake, in seconds.
        /// </summary>
        [Tooltip("How long to fade out a shake, in seconds.")]
        public float FadeOutTime;
        
        /// <summary>
        /// How much this shake influences the position per axis.
        /// </summary>
        [Tooltip("How much this shake influences the position per axis.")]
        public Vector3 PositionScalar = Vector3.one;
        
        /// <summary>
        /// How much this shake influences the rotation per axis.
        /// </summary>
        [Tooltip("How much this shake influences the rotation per axis.")]
        public Vector3 RotationScalar = Vector3.one;

        /// <summary>
        /// Returns the duration of this shake (fade in + fade out)
        /// </summary>
        public float Duration => FadeInTime + FadeOutTime;

        /// <summary>
        /// Scales the entire shake. Useful for doing distance based shake intensity
        /// </summary>
        [HideInInspector]
        public float GlobalScalar = 1f;

        /// <summary>
        /// Resolve the shake offsets for the given seed and timeStarted
        /// </summary>
        /// <param name="seed">the initial seed used for this shake</param>
        /// <param name="timeStarted">when this shake was started</param>
        /// <returns>returns the new offset</returns>
        internal Vector3 Resolve(float seed, TimeSince timeStarted)
        {
            var offset = seed + timeStarted;
            offset *= Roughness;
            
            var position = Vector3.zero;
            position.x = Mathf.PerlinNoise(offset, 0) - 0.5f;
            position.y = Mathf.PerlinNoise(0, offset) - 0.5f;
            position.z = Mathf.PerlinNoise(offset, offset) - 0.5f;
            var fadeInScaled = KitMath.NormalizeToRange(timeStarted, 0, FadeInTime, 0, 1);
            if (fadeInScaled > 1 || FadeInTime == 0)
            {
                fadeInScaled = KitMath.NormalizeToRange((Duration) - timeStarted, 0, Duration, 0, 1);
            }
            
            if (fadeInScaled <= 0)
                return Vector3.zero;
            return position * (Magnitude * Roughness * fadeInScaled * GlobalScalar);
        }

        /// <summary>
        /// Creates a temporary camera shake profile using the parameters given
        /// </summary>
        /// <param name="magnitude">Magnitude of the shake. More intense with higher values.</param>
        /// <param name="roughness">Roughness of the shake. Higher values are more jerky / violent.</param>
        /// <param name="fadeInTime">Time to fade in the shake, in seconds.</param>
        /// <param name="fadeOutTime">Time to fade out the shake, in seconds.</param>
        /// <returns>returns a temporary shake profile</returns>
        public static CameraShakeProfile MakeProfile(float magnitude = 1f, float roughness = 1f, float fadeInTime = 1f, float fadeOutTime = 1f)
        {
            return MakeProfile(magnitude, roughness, fadeInTime, fadeOutTime, Vector3.one, Vector3.one);
        }

        /// <summary>
        /// Creates a temporary camera shake profile using the parameters given
        /// </summary>
        /// <param name="magnitude">Magnitude of the shake. More intense with higher values.</param>
        /// <param name="roughness">Roughness of the shake. Higher values are more jerky / violent.</param>
        /// <param name="fadeInTime">Time to fade in the shake, in seconds.</param>
        /// <param name="fadeOutTime">Time to fade out the shake, in seconds.</param>
        /// <param name="positionScalar">Shake positional influence per axis</param>
        /// <param name="rotationScalar">Shake rotation influence per axis</param>
        /// <returns>returns a temporary shake profile</returns>
        public static CameraShakeProfile MakeProfile(float magnitude, float roughness, float fadeInTime, float fadeOutTime, Vector3 positionScalar, Vector3 rotationScalar)
        {
            var profile = CreateInstance<CameraShakeProfile>();
            profile.Magnitude = magnitude;
            profile.Roughness = roughness;
            profile.FadeInTime = fadeInTime;
            profile.FadeOutTime = fadeOutTime;
            profile.PositionScalar = positionScalar;
            profile.RotationScalar = rotationScalar;

            return profile;
        }
    }
}
