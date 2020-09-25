using UnityEngine;

namespace GameJamStarterKit
{
    /// <summary>
    /// A base class for a MonoBehaviour singleton that will not persist through scene changes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        /// <summary>
        /// Returns the current instance of this singleton or creates one if one isn't found.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var script = FindObjectOfType<T>();

                    if (script == null)
                    {
                        var go = new GameObject("Singleton: " + typeof(T).Name);
                        script = go.AddComponent<T>();
                    }

                    _instance = script;
                }

                return _instance;
            }
        }

        private static T _instance;

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = (T) this;
            }
        }
    }
}