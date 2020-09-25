namespace GameJamStarterKit.Audio
{
    /// <summary>
    /// Extends <see cref="BackgroundMusic"/> calling DontDestroyOnLoad during awake
    /// allowing this to persist between scene transitions.
    /// </summary>
    public class PersistentBackgroundMusic : BackgroundMusic
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }
    }
}