using UnityEngine;

namespace GameJamStarterKit.Audio.Samples
{
    public class PlaySoundButtonDemo : MonoBehaviour
    {
        public AudioClipCollection ClipCollection;
        public float PitchVariance = 0.05f;

        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Play 2D Sound"))
            {
                AudioPool.Play2D(ClipCollection.GetClip(), pitchVariance: PitchVariance);
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Play 3D Sound"))
            {
                AudioPool.Play3D(ClipCollection.GetClip(), transform.position, pitchVariance: PitchVariance);
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical("Box");
            GUILayout.Label("Pitch Variance: " + PitchVariance);
            PitchVariance = GUILayout.HorizontalSlider(PitchVariance, 0f, 1f);
            GUILayout.EndVertical();
            GUILayout.EndVertical();
        }
    }
}