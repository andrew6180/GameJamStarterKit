using UnityEngine;

namespace GameJamStarterKit.Camera.Samples
{
    public class ShakerSample : MonoBehaviour
    {
        private CameraShaker _shaker;
        private CameraShakeProfile _temporaryProfile;

        public float Magnitude = 1f;
        public float Roughness = 1f;
        public float FadeInTime = 1f;
        public float FadeOutTime = 1f;
        public Vector3 PositionScalar = Vector3.one;
        public Vector3 RotationScalar= Vector3.one;
        
        private void Start()
        {
            _shaker = GetComponent<CameraShaker>();
            _temporaryProfile = CameraShakeProfile.MakeProfile(Magnitude, Roughness, FadeInTime, FadeOutTime);
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Shake with Profile on CameraShaker"))
            {
                _shaker.StartShake();
            }

            if (GUILayout.Button("Shake with Inspector Values"))
            {
                _temporaryProfile.Magnitude = Magnitude;
                _temporaryProfile.Roughness = Roughness;
                _temporaryProfile.FadeInTime = FadeInTime;
                _temporaryProfile.FadeOutTime = FadeOutTime;
                _temporaryProfile.PositionScalar = PositionScalar;
                _temporaryProfile.RotationScalar = RotationScalar;
                _shaker.StartShake(_temporaryProfile);
            }
        }
    }
}