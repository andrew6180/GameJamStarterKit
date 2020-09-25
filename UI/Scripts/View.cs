using UnityEngine;

namespace GameJamStarterKit.UI
{
    /// <summary>
    /// The singleton root of a canvas view. Controls showing and hiding various child elements.
    /// </summary>
    public class View : SingletonBehaviour<View>
    {
        /// <summary>
        /// The modal to show for <see cref="ShowDialogPopup"/>
        /// </summary>
        [KitOnlyPrefabAsset]
        [Tooltip("The modal to use for ShowDialogPopup")]
        public GameObject DialogModal;

        /// <summary>
        /// Blocks input for anything behind this object
        /// </summary>
        [KitOnlyPrefabAsset]
        [Tooltip("Blocks input for anything behind this. Controlled by the view.")]
        public GameObject InputBlocker;
        private GameObject _activeDialog;
        private GameObject _activeInputBlocker;

        /// <summary>
        /// Opens the <see cref="DialogModal"/> with the message provided. Returns <see cref="PopupModal"/> to listen for results.
        /// </summary>
        /// <param name="message">the message to show in the dialog</param>
        /// <param name="blockInputBehind">should input behind this popup be blocked</param>
        /// <param name="closeOnSelection">automatically closes the modal when a selection is made.</param>
        /// <returns>returns the int return code from the dialog window.</returns>
        public PopupModal ShowDialogPopup(string message, bool blockInputBehind = true, bool closeOnSelection = true)
        {
            if (DialogModal != null)
            {
                var modal = Instantiate(DialogModal, transform);
                var popupModal = modal.GetComponent<PopupModal>();
                
                if (popupModal == null)
                {
                    Debug.LogError("DialogModal does not have a PopupModal component. Unable to handle DialogPopup.");
                    Destroy(modal);
                    return null;
                }
                
                var callback = modal.GetComponent<PopupModal>();
                if (callback == null)
                {
                    Debug.LogError($"No ModalCallback found on {modal.name}. Unable to handle DialogPopup.");
                    Destroy(modal);
                    return null;
                }
                
                if (blockInputBehind)
                {
                    ShowInputBlockerBehind();
                }
                
                modal.transform.SetAsLastSibling();
                
                popupModal.SetMessage(message);
                _activeDialog = modal;
                if (closeOnSelection)
                {
                    callback.OnResult.AddListener(CloseDialogPopup);
                }
                return callback;
            }
            Debug.LogError($"DialogModal is unset on {name}.");
            return null;
        }

        /// <summary>
        /// Closes the active dialog popup if any.
        /// </summary>
        public void CloseDialogPopup(int _)
        {
            if (_activeDialog != null)
                Destroy(_activeDialog);
            
            HideInputBlocker();
        }

        /// <summary>
        /// Hides the input blocker if it's active.
        /// </summary>
        public void HideInputBlocker()
        {
            if (_activeInputBlocker != null && _activeInputBlocker.activeInHierarchy)
            {
                _activeInputBlocker.SetActive(false);
            }
        }

        /// <summary>
        /// Moves the input blocker to be behind the passed transform
        /// </summary>
        /// <param name="tr">the transform to move behind. If null, the input blocker will be the top layer (blocking everything)</param>
        public void ShowInputBlockerBehind(Transform tr = null)
        {
            if (_activeInputBlocker == null)
            {
                _activeInputBlocker = Instantiate(InputBlocker, transform);
            }

            if (tr == null)
            {
                _activeInputBlocker.transform.SetParent(transform, true);
                _activeInputBlocker.transform.SetAsLastSibling();
                _activeInputBlocker.SetActive(true);
            }
            else
            {
                _activeInputBlocker.transform.SetParent(tr.parent, true);
                _activeInputBlocker.transform.SetSiblingIndex(tr.GetSiblingIndex());
                _activeInputBlocker.SetActive(true);
            }
            
        }
    }
}