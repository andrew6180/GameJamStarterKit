using UnityEngine;

namespace GameJamStarterKit.Demo
{
    public class ReloadSceneButtonDemo : MonoBehaviour
    {
        private void OnGUI()
        {
            if (GUILayout.Button("Reload Scene"))
            {
                KItSceneManager.ReloadCurrentScene();
            }
        }
    }
}