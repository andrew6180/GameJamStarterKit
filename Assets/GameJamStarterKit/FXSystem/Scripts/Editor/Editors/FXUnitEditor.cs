using GameJamStarterKit.Editor;
using UnityEditor;

namespace GameJamStarterKit.FXSystem.Editor
{
    [CustomEditor(typeof(FXUnit), true)]
    public class FXUnitEditor : UnityEditor.Editor
    {
        private SerializedProperty _despawnType;
        private SerializedProperty _despawnTimeout;
        private SerializedProperty _despawnTimeoutMinimum;
        private SerializedProperty _despawnTimeoutMaximum;
        private SerializedProperty _spawnScale;
        private SerializedProperty _spawnType;

        private void OnEnable()
        {
            _despawnType = serializedObject.FindProperty("DespawnType");
            _despawnTimeout = serializedObject.FindProperty("DespawnTimeout");
            _despawnTimeoutMinimum = serializedObject.FindProperty("DespawnTimeoutMinimum");
            _despawnTimeoutMaximum = serializedObject.FindProperty("DespawnTimeoutMaximum");
            _spawnScale = serializedObject.FindProperty("SpawnScale");
            _spawnType = serializedObject.FindProperty("SpawnType");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            _despawnType.DrawLayout();

            var despawnTypeValue = _despawnType.EnumValue<DespawnType>();

            switch (despawnTypeValue)
            {
                case DespawnType.Timeout:
                    _despawnTimeout.DrawLayout();
                    break;
                case DespawnType.TimeoutRange:
                    _despawnTimeoutMinimum.DrawLayout();
                    _despawnTimeoutMaximum.DrawLayout();
                    break;
            }

            _spawnScale.DrawLayout();
            _despawnType.DrawLayout();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}