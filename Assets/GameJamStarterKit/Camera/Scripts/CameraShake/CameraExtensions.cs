namespace GameJamStarterKit.Camera
{
    /// <summary>
    /// Extension methods for UnityEngine.Camera
    /// </summary>
    public static class CameraExtensions
    {
        /// <summary>
        /// Starts a camera shake using the given profile
        /// </summary>
        /// <param name="camera">this</param>
        /// <param name="profile">profile to use for the shake</param>
        public static void StartShake(this UnityEngine.Camera camera, CameraShakeProfile profile)
        {
            var shaker = camera.gameObject.GetOrAddComponent<CameraShaker>();
            shaker.StartShake(profile);
        }
    }
}