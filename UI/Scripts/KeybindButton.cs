using UnityEngine;
using UnityEngine.UI;

namespace GameJamStarterKit.UI
{
    /// <summary>
    /// Allows a key string to invoke the onClick of a UI.Button component
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class KeybindButton : MonoBehaviour
    {
        /// <summary>
        /// The key to bind
        /// </summary>
        public string Key;
        
        /// <summary>
        /// When the keypress should activate
        /// </summary>
        [Tooltip("When the keypress should activate")]
        public KeyBindMode BindingMode = KeyBindMode.OnKeyDown;
        
        private Button _button;
        
        private void Start()
        {
            _button = GetComponent<Button>();
            Key = Key.ToLowerInvariant();
        }

        private void Update()
        {
            bool active;
            switch (BindingMode)
            {
                default:
                case KeyBindMode.OnKeyDown:
                    active = Input.GetKeyDown(Key);
                    break;
                case KeyBindMode.OnKeyUp:
                    active = Input.GetKeyUp(Key);
                    break;
            }
            
            if (active)
            {
                _button.onClick.Invoke();
            }
        }
    }
}