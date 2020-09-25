namespace GameJamStarterKit
{
    /// <summary>
    /// A Base class for a singleton that will persist through scene changes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PersistentSingletonBehaviour<T> : SingletonBehaviour<T>
        where T : PersistentSingletonBehaviour<T>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }
    }
}