using UnityEngine;

namespace GameJamStarterKit.UI
{
    /// <summary>
    /// Defines when a key should activate
    /// </summary>
    public enum KeyBindMode
    {
        /// <summary>
        /// Activate a button press the frame a key is pressed down
        /// </summary>
        [Tooltip("Activates the key press the frame the key is pressed down")]
        OnKeyDown,
        /// <summary>
        /// Activate a button press the frame a key is released
        /// </summary>
        [Tooltip("Activates the key press when the key is released")]
        OnKeyUp
    }
}