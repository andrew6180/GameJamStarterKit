using UnityEngine;

namespace GameJamStarterKit.Components
{
    /// <summary>
    /// Randomizes a GameObjects transform component on start.
    /// </summary>
    public class RandomizeTransform : MonoBehaviour
    {
        /// <summary>
        /// Should the position be randomized?
        /// </summary>
        [Tooltip("Should the position be randomized?")]
        public bool Position = true;
        
        /// <summary>
        /// Minimum offset for the position
        /// </summary>
        [Tooltip("Minimum offset for the position")]
        public Vector3 PositionMinOffset;
        
        /// <summary>
        /// Maximum offset for the position
        /// </summary>
        [Tooltip("Maximum offset for the position")]
        public Vector3 PositionMaxOffset;

        /// <summary>
        /// Should the rotation be randomized?
        /// </summary>
        [Tooltip("Should the rotation be randomized?")]
        public bool Rotation = true;
        
        /// <summary>
        /// Minimum rotation offset
        /// </summary>
        [Tooltip("Minimum rotation offset")]
        public Vector3 RotationMinOffset;
        
        /// <summary>
        /// Maximum rotation offset
        /// </summary>
        [Tooltip("Maximum rotation offset")]
        public Vector3 RotationMaxOffset;

        /// <summary>
        /// Should the scale be randomized?
        /// </summary>
        [Tooltip("Should the scale be randomized?")]
        public bool Scale = true;
        
        /// <summary>
        /// Minimum scale offset
        /// </summary>
        [Tooltip("Minimum scale offset")]
        public Vector3 ScaleMinOffset;
        
        /// <summary>
        /// Maximum scale offset
        /// </summary>
        [Tooltip("Maximum scale offset")]
        public Vector3 ScaleMaxOffset;

        private void Start()
        {
            if (Position)
            {
                DoPosition();
            }

            if (Rotation)
            {
                DoRotation();
            }

            if (Scale)
            {
                DoScale();
            }
        }

        private void DoPosition()
        {
            transform.localPosition += KitRandom.Vector3(PositionMinOffset, PositionMaxOffset);
        }

        private void DoRotation()
        {
            transform.rotation *= KitRandom.Rotation(RotationMinOffset, RotationMaxOffset);
        }

        private void DoScale()
        {
            transform.localScale += KitRandom.Vector3(ScaleMinOffset, ScaleMaxOffset);
        }
    }
}