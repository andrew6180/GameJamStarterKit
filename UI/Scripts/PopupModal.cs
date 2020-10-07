using GameJamStarterKit.Events;
using TMPro;
using UnityEngine;

namespace GameJamStarterKit.UI
{
    /// <summary>
    /// Popup modal component with a result callback.
    /// </summary>
    public class PopupModal : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The dialog TMP_Text")]
        private TMP_Text DialogText = null;
        
        /// <summary>
        /// Called by <see cref="SendResult"/>
        /// </summary>
        public readonly UnityIntEvent OnResult = new UnityIntEvent();

        /// <summary>
        /// Invokes <see cref="OnResult"/>, passing an integer to listeners
        /// </summary>
        /// <param name="value">the value to send</param>
        public void SendResult(int value)
        {
            OnResult?.Invoke(value);
        }

        /// <summary>
        /// Sets the text message of the dialog popup
        /// </summary>
        /// <param name="message">the message to display</param>
        public void SetMessage(string message)
        {
            DialogText.SetText(message);   
        }
    }
}