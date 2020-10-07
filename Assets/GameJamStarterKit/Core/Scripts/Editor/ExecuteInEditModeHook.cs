using System.Collections.Generic;
using System.Reflection;
using GameJamStarterKit.Attributes;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameJamStarterKit.Editor
{
    internal static class ExecuteInEditModeHook
    {
        private static List<MethodInfoPair> _methodInfoPairs;
        static ExecuteInEditModeHook()
        {
            EditorSceneManager.sceneOpened += SceneChanged;
            RefreshMethods();
        }

        private static void SceneChanged(Scene scene, OpenSceneMode mode)
        {
            RefreshMethods();
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void RefreshMethods()
        {
            if (_methodInfoPairs != null)
            {
                foreach (var methodPair in _methodInfoPairs)
                {
                    EditorApplication.update -= methodPair.Invoke;
                    EditorApplication.hierarchyChanged -= methodPair.Invoke;
                }
                _methodInfoPairs.Clear();
            }
            
            _methodInfoPairs = new List<MethodInfoPair>();
            var count = EditorSceneManager.sceneCount;
            for (var i = 0; i < count; ++i)
            {
                var scene = EditorSceneManager.GetSceneAt(i);
                foreach (var rootGameObject in scene.GetRootGameObjects())
                {
                    var components = rootGameObject.GetComponentsInChildren<Component>();
                    foreach (var component in components)
                    {
                        if (component == null)
                            continue;
                        var methods = component.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                        foreach (var method in methods)
                        {
                            if (method.GetCustomAttribute<ExecuteOnEditorUpdateAttribute>() != null)
                            {
                                if (method.GetParameters().Length == 0)
                                {
                                    var pair = new MethodInfoPair(component, method);
                                    _methodInfoPairs.Add(pair);
                                    EditorApplication.update += pair.Invoke;
                                }
                            }

                            if (method.GetCustomAttribute<ExecuteOnEditorHierarchyChangeAttribute>() != null)
                            {
                                var pair = new MethodInfoPair(component, method);
                                _methodInfoPairs.Add(pair);
                                EditorApplication.hierarchyChanged += pair.Invoke;
                            }
                        }
                    }
                }
            }
        }
    }

    internal class MethodInfoPair
    {
        public object Obj;
        public MethodInfo Method;
        public MethodInfoPair(object obj, MethodInfo method)
        {
            Method = method;
            Obj = obj;
        }

        internal void Invoke()
        {
            Method.Invoke(Obj, new object[] {});
        }
    }
}