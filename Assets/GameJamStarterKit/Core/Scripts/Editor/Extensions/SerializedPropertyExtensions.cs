using System;
using UnityEditor;
using UnityEngine;

namespace GameJamStarterKit.Editor
{
    /// <summary>
    /// Useful extensions for SerializedProperties
    /// </summary>
    public static class SerializedPropertyExtensions
    {
        /// <summary>
        /// Calls EditorGUILayout.PropertyField for this property.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="includeChildren">include propertyfield children</param>
        public static void DrawLayout(this SerializedProperty property, bool includeChildren = false)
        {
            EditorGUILayout.PropertyField(property, includeChildren);
        }

        /// <summary>
        /// Calls EditorGUI.PropertyField for this property.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="position">rect position of the property</param>
        /// <param name="includeChildren">include propertyfield children</param>
        public static void Draw(this SerializedProperty property, Rect position, bool includeChildren = false)
        {
            EditorGUI.PropertyField(position, property, includeChildren);
        }

        /// <summary>
        /// returns the enum value of this property
        /// </summary>
        /// <param name="property"></param>
        /// <typeparam name="T">expected return type</typeparam>
        /// <returns></returns>
        public static T EnumValue<T>(this SerializedProperty property) where T : struct, Enum
        {
            var enumNames = property.enumNames;
            var index = property.enumValueIndex;
            if (index < 0)
                index = 0;

            Enum.TryParse(enumNames[index], out T enumValue);
            return enumValue;
        }
    }
}