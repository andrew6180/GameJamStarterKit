using UnityEngine;

namespace GameJamStarterKit.UI
{
    /// <summary>
    /// This component will only allow 1 child <see cref="Tab"/> to be active at a time. 
    /// </summary>
    public class TabLayoutGroup : MonoBehaviour
    {
        private void Start()
        {
            SwitchTo(0);
        }

        /// <summary>
        /// Switches to a tab by index
        /// </summary>
        /// <param name="index">index of the tab</param>
        public void SwitchTo(int index)
        {
            for (var i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(i == index);
            }
        }

        /// <summary>
        /// Switches to a tab by GameObject name
        /// </summary>
        /// <param name="objectName">name of the gameobject to switch to</param>
        public void SwitchTo(string objectName)
        {
            foreach (Transform t in transform)
            {
                t.gameObject.SetActive(t.name == objectName);
            }
        }
    }
}