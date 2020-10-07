using UnityEngine;

namespace GameJamStarterKit.Components
{
    /// <summary>
    /// Forces this GameObject to look at a target transform.
    /// </summary>
    public class LookAtTarget : MonoBehaviour
    {
        /// <summary>
        /// Transform to look at
        /// </summary>
        [Tooltip("Transform to look at")]
        public Transform Target;

        private void LateUpdate()
        {
            transform.LookAt(Target);
        }
    }
}