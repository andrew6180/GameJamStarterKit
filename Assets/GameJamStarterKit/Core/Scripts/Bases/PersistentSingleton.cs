namespace GameJamStarterKit
{
    /// <summary>
    /// A base class for a non MonoBehaviour singleton that will persist through scene changes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PersistentSingleton<T> : Singleton<T> where T : PersistentSingleton<T>, new()
    {
        protected PersistentSingleton()
        {
        }
    }
}