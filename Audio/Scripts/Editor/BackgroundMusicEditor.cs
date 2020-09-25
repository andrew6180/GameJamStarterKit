using GameJamStarterKit.Editor;
using UnityEditor;

namespace GameJamStarterKit.Audio.Editor
{
    [CustomEditor(typeof(BackgroundMusic), true)]
    public class BackgroundMusicEditor : UnityEditor.Editor
    {
        private SerializedProperty _clipCollection;
        private SerializedProperty _playOnStart;
        private SerializedProperty _fadeIn;
        private SerializedProperty _fadeInDuration;
        private SerializedProperty _alwaysFadeIn;
        private SerializedProperty _fadeOut;
        private SerializedProperty _fadeOutDuration;
        private SerializedProperty _crossFade;
        private SerializedProperty _crossFadeDuration;
        private SerializedProperty _looping;

        private void OnEnable()
        {
            _clipCollection = serializedObject.FindProperty("ClipCollection");
            _playOnStart = serializedObject.FindProperty("PlayOnStart");
            _fadeIn = serializedObject.FindProperty("FadeIn");
            _fadeInDuration = serializedObject.FindProperty("FadeInDuration");
            _alwaysFadeIn = serializedObject.FindProperty("AlwaysFadeIn");
            _fadeOut = serializedObject.FindProperty("FadeOut");
            _fadeOutDuration = serializedObject.FindProperty("FadeOutDuration");
            _crossFade = serializedObject.FindProperty("CrossFade");
            _crossFadeDuration = serializedObject.FindProperty("CrossFadeDuration");
            _looping = serializedObject.FindProperty("Looping");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            _clipCollection.DrawLayout(true);
            _playOnStart.DrawLayout();
            _crossFade.DrawLayout();
            _looping.DrawLayout();
            if (_crossFade.boolValue)
            {
                _crossFadeDuration.DrawLayout();
            }
            else
            {
                _fadeIn.DrawLayout();
                if (_fadeIn.boolValue)
                {
                    _fadeInDuration.DrawLayout();
                    _alwaysFadeIn.DrawLayout();
                }

                _fadeOut.DrawLayout();
                if (_fadeOut.boolValue)
                {
                    _fadeOutDuration.DrawLayout();
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}