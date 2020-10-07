using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameJamStarterKit.Components
{
    /// <summary>
    /// Component to attach to a UI providing access to some common button click actions
    /// </summary>
    public class CommonButtonActions : MonoBehaviour
    {
        /// <summary>
        /// Loads a single scene.
        /// </summary>
        /// <param name="scene">scene to load</param>
        public virtual void LoadScene(string scene)
        {
            var async = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
            async.allowSceneActivation = true;
        }

        /// <summary>
        /// Loads a scene additively
        /// </summary>
        /// <param name="scene">scene to load</param>
        public virtual void LoadSceneAdditive(string scene)
        {
            var async = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            async.allowSceneActivation = true;
        }

        /// <summary>
        /// Unloads a scene
        /// </summary>
        /// <param name="scene">scene to unload.</param>
        public virtual void UnloadScene(string scene)
        {
            SceneManager.UnloadSceneAsync(scene, UnloadSceneOptions.None);
        }

        /// <summary>
        /// Calls Application.Quit()
        /// </summary>
        public virtual void QuitApplication()
        {
            Application.Quit();
        }

        /// <summary>
        /// Opens a URL using Application.OpenURL
        /// </summary>
        /// <param name="url">url to open</param>
        public virtual void OpenURL(string url)
        {
            Application.OpenURL(url);
        }

        /// <summary>
        /// Prints text with Debug.Log. Useful for testing events
        /// </summary>
        public virtual void DebugLog(string message)
        {
            Debug.Log("CommonButtonActions.PrintDebugText()");
        }
    }
}