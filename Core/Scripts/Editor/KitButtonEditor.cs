using System.Linq;
using System.Reflection;
using GameJamStarterKit.Attributes;
using UnityEditor;
using UnityEngine;

namespace GameJamStarterKit.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Object), true)]
    public class KitButtonEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            this.DrawKitButtons();
        }
    }

    public static class KitEditorExtensions
    {
        public static void DrawKitButtons(this UnityEditor.Editor editor)
        {
            const BindingFlags METHOD_FLAGS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                                              BindingFlags.NonPublic;

            var noParamMethods = editor.target.GetType().GetMethods(METHOD_FLAGS)
                .Where(m => m.GetParameters().Length == 0);

            var style = new GUIStyle("Button");

            foreach (var method in noParamMethods)
            {
                var attribute = method.GetCustomAttribute<KitButtonAttribute>();

                if (attribute == null)
                    continue;

                var text = string.IsNullOrWhiteSpace(attribute.Text) ? method.Name : attribute.Text;
                const int EXTRA_SMALL_HEIGHT = 12;
                const int SMALL_HEIGHT = 16;
                const int MEDIUM_HEIGHT = 30;
                const int LARGE_HEIGHT = 50;
                const int EXTRA_LARGE_HEIGHT = 70;

                var options = new GUILayoutOption[2];

                switch (attribute.ButtonSize)
                {
                    case StandardSize.ExtraSmall:
                        options[0] = GUILayout.Height(EXTRA_SMALL_HEIGHT);
                        options[1] = GUILayout.MaxWidth(150);
                        break;
                    
                    case StandardSize.Small:
                        options[0] = GUILayout.Height(SMALL_HEIGHT);
                        options[1] = GUILayout.MaxWidth(200);
                        break;

                    case StandardSize.Medium:
                        options[0] = GUILayout.Height(MEDIUM_HEIGHT);
                        options[1] = GUILayout.MaxWidth(250);
#if !UNITY_2019_3_OR_NEWER
                        style.fontSize *= 2;
#endif
                        break;

                    case StandardSize.Large:
                        options[0] = GUILayout.Height(LARGE_HEIGHT);
                        options[1] = GUILayout.MaxWidth(300);
#if !UNITY_2019_3_OR_NEWER
                        style.fontSize *= 3;
#endif
                        break;
                    case StandardSize.ExtraLarge:
                        options[0] = GUILayout.Height(EXTRA_LARGE_HEIGHT);
                        options[1] = GUILayout.MaxWidth(350);
#if !UNITY_2019_3_OR_NEWER
                        style.fontSize *= 4;
#endif
                        break;
                }

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button(text, style, options))
                {
                    foreach (var target in editor.targets)
                    {
                        method.Invoke(target, null);
                    }
                }

                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
        }
    }
}