using System.Collections;
using UnityEngine;

namespace GameJamStarterKit.Camera
{
    /// <summary>
    /// A Component used to shake the camera using a <see cref="CameraShakeProfile"/>
    /// </summary>
    public class CameraShaker : MonoBehaviour
    {
        public CameraShakeProfile Profile;
        private bool _isShaking = false;
        
        private IEnumerator Shake(CameraShakeProfile profile)
        {
            _isShaking = true;
            var seed = Random.Range(-256, 256);
            TimeSince start = 0f;
            var initialPosition = transform.localPosition;
            var initialRotation = transform.localEulerAngles;
            while (start <= profile.Duration)
            {
                var positionOffset = profile.Resolve(seed, start);
                positionOffset.Scale(profile.PositionScalar);
                transform.localPosition = initialPosition + positionOffset;
                
                var rotationOffset = profile.Resolve(seed, start);
                rotationOffset.Scale(profile.RotationScalar);
                transform.localEulerAngles = initialRotation + rotationOffset;
                yield return null;
            }
            transform.localPosition = initialPosition;
            transform.localEulerAngles = initialRotation;
            _isShaking = false;
        }

        /// <summary>
        /// Starts a camera shake using the <see cref="Profile"/> field on this CameraShaker
        /// </summary>
        public void StartShake()
        {
            StartShake(Profile);
        }

        /// <summary>
        /// Starts a camera shake using the provided profile
        /// </summary>
        /// <param name="profile">profile to use</param>
        public void StartShake(CameraShakeProfile profile)
        {
            if (!_isShaking)
                StartCoroutine(Shake(profile));
        }
    }
}