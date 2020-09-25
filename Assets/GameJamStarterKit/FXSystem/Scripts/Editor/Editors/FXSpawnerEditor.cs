using GameJamStarterKit.Editor;
using UnityEditor;
using UnityEngine;

namespace GameJamStarterKit.FXSystem.Editor
{
    [CustomEditor(typeof(FXSpawner), true)]
    public class FXSpawnerEditor : UnityEditor.Editor
    {
        [SerializeField]
        private bool ShowDebugMenu;

        [SerializeField]
        private bool Retain;

        [SerializeField]
        private string SelectedKey;
        
        [SerializeField]
        private AnimationParameter AnimationParameter;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            var fxSpawner = serializedObject.targetObject as FXSpawner;
            if (fxSpawner == null)
            {
                Debug.LogWarning("serializedObject.targetObject as FXSpawner == null. Something went very wrong.");
                return;
            }
            
            var str = ShowDebugMenu ? "Hide Spawn Menu" : "Show Spawn Menu";
            
            KitGUILayout.BeginCleanFoldout("Spawn Menu", ref ShowDebugMenu, size: 12);

            if (!ShowDebugMenu)
            {
                KitGUILayout.EndCleanFoldout();
                return;
            }
            
            if (!EditorApplication.isPlaying)
            {
                EditorGUILayout.HelpBox("FX Can only be spawned by the inspector at runtime.", MessageType.Info);
            }
            
            SelectedKey = EditorGUILayout.TextField("Key: ", SelectedKey);
            Retain = EditorGUILayout.Toggle("Retain? ", Retain);
            
            GUI.enabled = EditorApplication.isPlaying;
            EditorGUILayout.BeginHorizontal();
            if (Retain)
            {
                if (GUILayout.Button("Spawn and Retain"))
                {
                    fxSpawner.SpawnAndRetain(SelectedKey);
                }
                if (GUILayout.Button("Despawn"))
                {
                    fxSpawner.DespawnAndRelease(SelectedKey);
                }
            }
            else
            {
                if (GUILayout.Button("Spawn and Forget"))
                {
                    fxSpawner.SpawnAndForget(SelectedKey);
                }
            }
            EditorGUILayout.EndHorizontal();
            GUI.enabled = true;
            
            EditorGUILayout.LabelField("Interaction", EditorStyles.boldLabel);
            
            if (AnimationParameter == null)
                AnimationParameter = new AnimationParameter();
            
            AnimationParameter.Key = EditorGUILayout.TextField("Parameter Key: ", AnimationParameter.Key);

            AnimationParameter.ParameterType = (AnimatorControllerParameterType) 
                EditorGUILayout.EnumPopup("Parameter Tyoe: ", AnimationParameter.ParameterType);

            switch (AnimationParameter.ParameterType)
            {
                case AnimatorControllerParameterType.Float:
                    AnimationParameter.FloatValue = EditorGUILayout.FloatField("Float Value: ", AnimationParameter.FloatValue);
                    break;
                case AnimatorControllerParameterType.Int:
                    AnimationParameter.IntValue = EditorGUILayout.IntField("Int Value: ", AnimationParameter.IntValue);
                    break;
                case AnimatorControllerParameterType.Bool:
                    AnimationParameter.BoolValue = EditorGUILayout.Toggle("Bool Value: ", AnimationParameter.BoolValue);
                    break;
                default:
                case AnimatorControllerParameterType.Trigger:
                    break;
            }
            GUI.enabled = EditorApplication.isPlaying;
            if (GUILayout.Button("Set Parameter on retained FXUnit"))
            {
                var unit = fxSpawner.GetRetainedUnit(SelectedKey);
                if (unit == null)
                    return;
                
                var animator = unit.GetComponent<Animator>();
                if (animator == null)
                    return;
                
                AnimationParameter.Set(animator);
            }
            GUI.enabled = true;
            KitGUILayout.EndCleanFoldout();
        }
    }
}