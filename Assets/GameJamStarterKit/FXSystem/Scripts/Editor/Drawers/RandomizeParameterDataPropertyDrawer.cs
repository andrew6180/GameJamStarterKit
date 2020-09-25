using GameJamStarterKit.Editor;
using UnityEditor;
using UnityEngine;

namespace GameJamStarterKit.FXSystem.Editor
{
    [CustomPropertyDrawer(typeof(RandomizeAnimatorParameters.RandomizeParameterData))]
    public class RandomizeParameterDataPropertyDrawer : PropertyDrawer
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
                var key = property.FindPropertyRelative("ParameterKey");
                var type = property.FindPropertyRelative("ParameterType");
                var overrideRange = property.FindPropertyRelative("OverrideRange");
                var minimumValue = property.FindPropertyRelative("MinimumValue");
                var maximumValue = property.FindPropertyRelative("MaximumValue");

                singleLineRect.y += singleLineRect.height;
                key.Draw(singleLineRect);

                singleLineRect.y += singleLineRect.height;
                type.Draw(singleLineRect);

                var typeValue = type.EnumValue<AnimatorControllerParameterType>();

                if (typeValue != AnimatorControllerParameterType.Float &&
                    typeValue != AnimatorControllerParameterType.Int)
                {
                    return;
                }

                singleLineRect.y += singleLineRect.height;
                overrideRange.Draw(singleLineRect);

                if (!overrideRange.boolValue)
                    return;

                singleLineRect.y += singleLineRect.height;
                minimumValue.Draw(singleLineRect);

                singleLineRect.y += singleLineRect.height;
                maximumValue.Draw(singleLineRect);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
                return EditorGUIUtility.singleLineHeight;

            var type = property.FindPropertyRelative("ParameterType");

            var typeValue = type.EnumValue<AnimatorControllerParameterType>();

            switch (typeValue)
            {
                case AnimatorControllerParameterType.Int:
                case AnimatorControllerParameterType.Float:
                    var overrideRange = property.FindPropertyRelative("OverrideRange");
                    if (overrideRange.boolValue)
                    {
                        return EditorGUIUtility.singleLineHeight * 6;
                    }
                    else
                    {
                        return EditorGUIUtility.singleLineHeight * 4;
                    }

                case AnimatorControllerParameterType.Bool:
                case AnimatorControllerParameterType.Trigger:
                    return EditorGUIUtility.singleLineHeight * 3;
                default:
                    return EditorGUIUtility.singleLineHeight;
            }
        }
    }
}