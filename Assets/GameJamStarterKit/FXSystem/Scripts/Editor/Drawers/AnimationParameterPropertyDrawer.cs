using UnityEditor;
using UnityEngine;

namespace GameJamStarterKit.FXSystem.Editor
{
    [CustomPropertyDrawer(typeof(AnimationParameter))]
    public class AnimationParameterPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.serializedObject.isEditingMultipleObjects)
                return;

            label = EditorGUI.BeginProperty(position, label, property);

            var singleLineRect = new Rect(position);
            singleLineRect.height = EditorGUIUtility.singleLineHeight;

            property.isExpanded = EditorGUI.Foldout(singleLineRect, property.isExpanded, label);
            if (property.isExpanded)
            {
                var animParameter = fieldInfo.GetValue(property.serializedObject.targetObject) as AnimationParameter;

                singleLineRect.y += singleLineRect.height;
                DrawKeyField(singleLineRect, animParameter);

                // shift down by height
                singleLineRect.y += EditorGUIUtility.singleLineHeight;
                DrawTypeField(singleLineRect, animParameter);

                // shift down by height
                singleLineRect.y += EditorGUIUtility.singleLineHeight;
                DrawValueField(singleLineRect, animParameter);
            }

            EditorGUI.EndProperty();
        }

        private void DrawTypeField(Rect position, AnimationParameter animParameter)
        {
            var value = animParameter.ParameterType;

            var newValue = (AnimatorControllerParameterType) EditorGUI.EnumPopup(position, "Parameter Type ", value);

            if (newValue != value)
            {
                animParameter.ParameterType = newValue;
            }
        }

        private void DrawValueField(Rect position, AnimationParameter animParameter)
        {
            var type = animParameter.ParameterType;
            switch (type)
            {
                case AnimatorControllerParameterType.Float:
                    DrawFloatField(position, animParameter);
                    break;
                case AnimatorControllerParameterType.Int:
                    DrawIntField(position, animParameter);
                    break;
                default:
                case AnimatorControllerParameterType.Bool:
                    DrawBoolField(position, animParameter);
                    break;
                case AnimatorControllerParameterType.Trigger:
                    DrawTriggerField(position, animParameter);
                    break;
            }
        }

        private void DrawTriggerField(Rect position, AnimationParameter animParameter)
        {
            EditorGUI.LabelField(position, "Trigger");
        }

        private void DrawBoolField(Rect position, AnimationParameter animParameter)
        {
            var value = animParameter.BoolValue;

            var newValue = EditorGUI.Toggle(position, "Value ", value);

            if (newValue != value)
            {
                animParameter.BoolValue = newValue;
            }
        }

        private void DrawIntField(Rect position, AnimationParameter animParameter)
        {
            var value = animParameter.IntValue;

            var newValue = EditorGUI.IntField(position, "Value ", value);

            if (newValue != value)
            {
                animParameter.IntValue = newValue;
            }
        }

        private void DrawFloatField(Rect position, AnimationParameter animParameter)
        {
            var value = animParameter.FloatValue;

            var newValue = EditorGUI.FloatField(position, "Value ", value);

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (newValue != value)
            {
                animParameter.FloatValue = newValue;
            }
        }

        private void DrawKeyField(Rect position, AnimationParameter animParameter)
        {
            var key = animParameter.Key;

            var newKey = EditorGUI.TextField(position, "Key ", key);

            if (newKey != key)
            {
                animParameter.Key = newKey;
            }
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return property.isExpanded ? EditorGUIUtility.singleLineHeight * 4 : EditorGUIUtility.singleLineHeight;
        }
    }
}