using UnityEngine.SceneManagement;

namespace GameJamStarterKit
{
    /// <summary>
    /// A base class for a singleton that will not persist through scene changes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : new()
    {
        /// <summary>
        /// Returns the current instance of this singleton or creates one if one isn't found
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }

        private static T _instance;

        protected Singleton()
        {
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
        }

        private static void SceneManagerOnActiveSceneChanged(Scene currentScene, Scene nextScene)
        {
            SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;
            _instance = default;
        }
    }
}