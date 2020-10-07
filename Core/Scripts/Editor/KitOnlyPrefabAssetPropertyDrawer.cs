using GameJamStarterKit.Attributes;
using UnityEditor;
using UnityEngine;

namespace GameJamStarterKit.Editor
{
    [CustomPropertyDrawer(typeof(KitOnlyPrefabAsset))]
    public class KitOnlyPrefabAssetPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.ObjectReference:
                    var newValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(GameObject), false);
                    if (newValue != property.objectReferenceValue)
                    {
                        property.objectReferenceValue = newValue;
                        property.serializedObject.ApplyModifiedProperties();
                    }
                    return;
                default:
                    EditorGUI.PropertyField(position, property, label);
                    return;
            }
        }
    }
}