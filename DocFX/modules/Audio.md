# Core 
`GameJamStarterKit.Audio` is a package with useful audiio clips, components, pools, and other utilities to help with implementing audio features in a small scope game.

# Features
* AudioPool - Subclass of `Core.BasePool`providing an easy way to play 2d and 3d audio clips anywhere.
* Persistent/BackgroundMusic provides a simple class to play single background music clips / ClipCollections, either persisting through scene change or not.


### Audio Pool Example
```c#
public class MyObject : MonoBehaviour
{
    // set this in inspector
    public AudioClip Sound;

    private void MakeNoise()
    {
        AudioPool.Instance.Play3D(Sound, transform.position);
    }

    private void Make2DNoise()
    {
        AudioPool.Instance.Play2D(Sound);
    }
}
```
#### NOTE
If you plan to use AudioPool, it is recommended to initialize it in an awake somewhere with `AudioPool.GetNext()`. For Example:
```c#
public class MySetupClass : MonoBehaviour
{
    private void Awake()
    {
        // ensure audio pool is cached
        AudioPool.Instance.GetNext();
    }
}
