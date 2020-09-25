using UnityEngine.SceneManagement;

namespace GameJamStarterKit
{
    /// <summary>
    /// Helpful scene management functions
    /// </summary>
    public static class KItSceneManager
    {
        /// <summary>
        /// Reloads the scene currently active.
        /// </summary>
        public static void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}