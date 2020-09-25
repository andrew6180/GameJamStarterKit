using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameJamStarterKit.Editor;
using UnityEditor;
using UnityEngine;

namespace GameJamStarterKit.UI.Editor
{
    /// <summary>
    /// Custom editor for <see cref="InterpolateText"/>
    /// </summary>
    [CustomEditor(typeof(InterpolateText))]
    public class InterpolateTextEditor : UnityEditor.Editor
    {
        private SerializedProperty _dataMapProperty;
        private SerializedProperty _showDataMapProperty;

        private bool _showDataMap;

        private Dictionary<string, bool> _showPropertyMap;
        private void OnEnable()
        {
            _dataMapProperty = serializedObject.FindProperty("DataMap");
            _showDataMapProperty = serializedObject.FindProperty("ShowDataMap");
            _showPropertyMap = new Dictionary<string, bool>();
        }

        public override void OnInspectorGUI()
        {
            // start data map foldout
            _showDataMap = _showDataMapProperty.boolValue;
            KitGUILayout.BeginCleanFoldout(_dataMapProperty.displayName, ref _showDataMap, size: 12);
            _showDataMapProperty.boolValue = _showDataMap;
            
            // data map foldout is open
            if (_showDataMap)
            {
                // draw each data map 
                for (var i = 0; i < _dataMapProperty.arraySize; ++i)
                {
                    var property = _dataMapProperty.GetArrayElementAtIndex(i);
                    var key = property.FindPropertyRelative("Key");
                    
                    if (!_showPropertyMap.ContainsKey(property.name))
                    {
                        _showPropertyMap.Add(property.name, true);
                    }

                    var show = _showPropertyMap[property.name];
                    
                    // property foldout
                    var display = string.IsNullOrEmpty(key.stringValue) ? property.displayName : key.stringValue;
                    KitGUILayout.BeginCleanFoldout(display, ref show, size: 10);
                    _showPropertyMap[property.name] = show;
                    if (show)
                    {
                        DrawData(property, key);
                        // delete button
                        if (GUILayout.Button(EditorGUIUtility.IconContent("d_CollabDeleted Icon"),
                            GUILayout.MaxHeight(15)))
                        {
                            _dataMapProperty.DeleteArrayElementAtIndex(i);
                            serializedObject.ApplyModifiedProperties();
                            return;
                        }
                    }
                    KitGUILayout.EndCleanFoldout();
                }
            } // end of data map foldout
            
            // add new entry button
            if (GUILayout.Button(EditorGUIUtility.IconContent("d_CollabCreate Icon"),
                GUILayout.MaxHeight(30)))
            {
                _dataMapProperty.InsertArrayElementAtIndex(_dataMapProperty.arraySize);
                _showDataMapProperty.boolValue = true;
            }
            KitGUILayout.EndCleanFoldout();

            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Draws a <see cref="TextInterpolationData"/> member 
        /// </summary>
        /// <param name="rootProperty">the root <see cref="TextInterpolationData"/> property</param>
        /// <param name="keyProperty">the <see cref="TextInterpolationData.Key"/> property</param>
        private void DrawData(SerializedProperty rootProperty, SerializedProperty keyProperty)
        {
            var sourceProperty = rootProperty.FindPropertyRelative("Source");
            
            // draw key field
            EditorGUILayout.PropertyField(keyProperty);

            // draw Source field
            var go = (GameObject)EditorGUILayout.ObjectField(sourceProperty.displayName, sourceProperty.objectReferenceValue,
                typeof(GameObject), true);

            var sourceIndexProperty = rootProperty.FindPropertyRelative("SourceComponentIndex");
            
            // ensure it is not a prefab.
            if (go != sourceProperty.objectReferenceValue)
            {
                if (!AssetDatabase.Contains(go) && go.scene.name != null)
                {
                    // reset component index
                    sourceIndexProperty.intValue = 0;
                    sourceProperty.objectReferenceValue = go;
                }
            }
            
            if (go == null)
                return;
            
            // get components on the object
            var components = go.GetComponents<Component>();
            var componentIndex = sourceIndexProperty.intValue;
            
            // create a popup for the component names
            componentIndex =
                EditorGUILayout.Popup("Component", componentIndex, components
                    .Select(c => c.GetType().ToString())
                    .ToArray());

            // "select" the component 
            sourceIndexProperty.intValue = componentIndex;
            
            var component = components[componentIndex];
            var componentType = component.GetType();
            
            var componentProperty = rootProperty.FindPropertyRelative("SourceComponent");
            
            // update component property
            if (componentProperty.objectReferenceValue != component)
            {
                componentProperty.objectReferenceValue = component;
            }

            var memberTypeProperty = rootProperty.FindPropertyRelative("MemberType");
            var useDropdownProperty = rootProperty.FindPropertyRelative("UseDropdown");
            var nameProperty = rootProperty.FindPropertyRelative("Name");

            // draw use dropdown 
            EditorGUILayout.PropertyField(useDropdownProperty);
            
            var nameIndexProperty = rootProperty.FindPropertyRelative("NameIndex");
            var nameIndex = nameIndexProperty.intValue;

            // draw member type property 
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(memberTypeProperty);
            if (EditorGUI.EndChangeCheck())
            {
                nameIndex = 0;
            }
            
            // show dropdown for members
            if (useDropdownProperty.boolValue)
            {
                MemberInfo[] members;

                const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Public;
                switch(memberTypeProperty.EnumValue<DataMemberType>())
                {
                    case DataMemberType.Method:
                        members = componentType.GetMethods(FLAGS).Where(m => m.ReturnType != typeof(void)).Cast<MemberInfo>().ToArray();
                        break;
                    case DataMemberType.Property:
                        members = componentType.GetProperties(FLAGS).Where(p => p.CanRead).Cast<MemberInfo>().ToArray();
                        break;
                    default:
                    case DataMemberType.Field:
                        members = componentType.GetFields(FLAGS).Cast<MemberInfo>().ToArray();
                        break;
                }

                // create name popup
                nameIndex = EditorGUILayout.Popup("Member Name", nameIndex, members.Select(m => m.Name).ToArray());
                
                var newName = members[nameIndex].Name;
                nameIndexProperty.intValue = nameIndex;
                // set name property
                nameProperty.stringValue = newName;
            }
            else // have the Name field exposed to type member names.
            {
                // draw name property
                EditorGUILayout.PropertyField(nameProperty);
                var nameToCheck = nameProperty.stringValue;
                bool hasName;
                var returnsVoid = false;
                // check if name is acceptable
                if (!string.IsNullOrEmpty(nameToCheck))
                {
                    switch (memberTypeProperty.EnumValue<DataMemberType>())
                    {
                        case DataMemberType.Method:
                            var method = componentType.GetMethod(nameToCheck);
                            hasName = method != null;
                            if (hasName)
                            {
                                returnsVoid = method.ReturnType == typeof(void);
                            }
                            break;
                        case DataMemberType.Property:
                            hasName = componentType.GetProperty(nameToCheck) != null;
                            break;
                        default:
                        case DataMemberType.Field:
                            hasName = componentType.GetField(nameToCheck) != null;
                            break;
                    }
                }
                else
                {
                    hasName = true;
                }
                
                if (hasName && !returnsVoid)
                    return;
                
                if (returnsVoid)
                {
                    EditorGUILayout.HelpBox($"{nameToCheck} returns void and cannot be used as a source.", MessageType.Error);
                }
                else
                {
                    EditorGUILayout.HelpBox($"{nameToCheck} was not found {componentType}", MessageType.Error);
                }
            }
        }
    }
}