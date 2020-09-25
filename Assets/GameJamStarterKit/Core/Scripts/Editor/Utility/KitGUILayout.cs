using UnityEditor;
using UnityEngine;

namespace GameJamStarterKit.Editor
{
    /// <summary>
    /// Useful EditorGUILayout wrappers to provide nicer looking ui elements in the editor.
    /// </summary>
    public static class KitGUILayout
    {
        public static readonly Texture2D DROPDOWN_ICON = EditorGUIUtility.FindTexture("icon dropdown");
        public static readonly Texture2D EXPAND_ICON = EditorGUIUtility.FindTexture("forward");

        /// <summary>
        /// Starts a Responsive ui layout, switching between BeginHorizontal and BeginVertical depending on minWidth
        /// </summary>
        /// <param name="minWidth">switches to vertical layout below this value</param>
        /// <param name="options"></param>
        public static void BeginResponsive(int minWidth, params GUILayoutOption[] options)
        {
            if (Screen.width >= minWidth)
            {
                EditorGUILayout.BeginHorizontal(options);
            }
            else
            {
                EditorGUILayout.BeginVertical(options);
            }
        }

        /// <summary>
        /// Starts a Responsive ui layout, switching between BeginHorizontal and BeginVertical depending on minWidth
        /// </summary>
        /// <param name="minWidth">switches to vertical layout below this value</param>
        /// <param name="style"></param>
        /// <param name="options"></param>
        public static void BeginResponsive(int minWidth, GUIStyle style, params GUILayoutOption[] options)
        {
            if (Screen.width >= minWidth)
            {
                EditorGUILayout.BeginHorizontal(style, options);
            }
            else
            {
                EditorGUILayout.BeginVertical(style, options);
            }
        }


        /// <summary>
        /// Ends a responsive ui layout, switching between EndHorizontal and EndVertical depending on minWidth;
        /// </summary>
        /// <param name="minWidth">switches to vertical layout below this value</param>
        public static void EndResponsive(int minWidth)
        {
            if (Screen.width >= minWidth)
            {
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.EndVertical();
            }
        }

        /// <summary>
        /// Starts a new responsive foldout section.
        /// </summary>
        /// <param name="text">text to display for the foldout</param>
        /// <param name="responsiveMinWidth">width to switch to vertical from horizontal</param>
        /// <param name="show">should the foldout be enabled or disabled</param>
        /// <param name="bold">should the label text be bold</param>
        /// <param name="size">font size</param>
        public static void BeginResponsiveCleanFoldout(string text,
            int responsiveMinWidth,
            ref bool show,
            bool bold = true,
            int size = 18)
        {
            BeginResponsive(responsiveMinWidth, "Box");
            var icon = show ? DROPDOWN_ICON : EXPAND_ICON;
            var content = new GUIContent(text, icon);
            if (LabelButton(content, bold, size))
            {
                show = !show;
            }
        }

        /// <summary>
        /// Starts a new foldout section using BeginVertical
        /// </summary>
        /// <param name="text">text to display for the foldout</param>
        /// <param name="show">should the foldout be enabled or disabled</param>
        /// <param name="bold">should the label text be bold</param>
        /// <param name="size">font size</param>
        public static void BeginCleanFoldout(string text, ref bool show, bool bold = true, int size = 18)
        {
            EditorGUILayout.BeginVertical("Box");
            var icon = show ? DROPDOWN_ICON : EXPAND_ICON;
            var content = new GUIContent(text, icon);
            if (LabelButton(content, bold, size))
            {
                show = !show;
            }
        }


        /// <summary>
        /// Ends a responsive clean foldout
        /// </summary>
        /// <param name="responsiveMinWidth">width (should be same as the begin responsive foldout value)</param>
        public static void EndResponsiveCleanFoldout(int responsiveMinWidth)
        {
            EndResponsive(responsiveMinWidth);
        }

        /// <summary>
        /// Ends a vertical foldout
        /// </summary>
        public static void EndCleanFoldout()
        {
            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// A plain text button.
        /// </summary>
        /// <param name="content">content of the button</param>
        /// <param name="bold">if the text should be bold or not</param>
        /// <param name="size">font size</param>
        /// <returns>returns if the button was clicked this frame</returns>
        public static bool LabelButton(GUIContent content, bool bold = false, int size = 18)
        {
            var s = new GUIStyle();
            var b = s.border;
            b.left = 0;
            b.top = 0;
            b.right = 0;
            b.bottom = 0;
            s.border = b;

            s.fontSize = size;

            if (bold)
                s.fontStyle = FontStyle.Bold;
            return GUILayout.Button(content, s);
        }
    }
}