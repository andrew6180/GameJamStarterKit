using UnityEngine;

namespace GameJamStarterKit.FXSystem.Samples
{
    public class FXSystemDemo : MonoBehaviour
    {
        public FXSpawner Spawner;

        private void OnGUI()
        {
            if (GUILayout.Button("Spawn And Forget Timeout FX"))
            {
                Spawner.SpawnAndForget("TimeoutUnit");
            }

            if (GUILayout.Button("Spawn and Retain Timeout FX"))
            {
                Spawner.SpawnAndRetain("TimeoutUnit");
            }

            if (GUILayout.Button("Spawn and Forget Manual FX"))
            {
                Spawner.SpawnAndForget("ManualUnit");
            }

            if (GUILayout.Button("Spawn and Retain Manual FX"))
            {
                Spawner.SpawnAndRetain("ManualUnit");
            }

            if (GUILayout.Button("Despawn Retained FX"))
            {
                Spawner.DespawnAndRelease("TimeoutUnit");
                Spawner.DespawnAndRelease("ManualUnit");
            }

            if (GUILayout.Button("Release Retained FX"))
            {
                Spawner.Release("TimeoutUnit");
                Spawner.Release("TimeoutUnit");
            }
        }
    }
}