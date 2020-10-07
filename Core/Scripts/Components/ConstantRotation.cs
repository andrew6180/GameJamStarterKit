using GameJamStarterKit.Attributes;
using UnityEngine;

namespace GameJamStarterKit.Components
{
    /// <summary>
    /// Rotates an object at a constant rate. <see cref="UseRandomRange"/> will roll a random rotation value on Start.
    /// </summary>
    public class ConstantRotation : MonoBehaviour
    {
        /// <summary>
        /// Tells this component to use <see cref="RotationRangeMin"/> and <see cref="RotationRangeMax"/>
        /// </summary>
        public bool UseRandomRange;

        /// <summary>
        /// The constant rotation applied to this GameObject
        /// </summary>
        public Vector3 Rotation;
    
        /// <summary>
        /// The minimum constant rotation applied to this GameObject when using <see cref="UseRandomRange"/>
        /// </summary>
        public Vector3 RotationRangeMin;
    
        /// <summary>
        /// The maximum constant rotation applied to this GameObject when using <see cref="UseRandomRange"/>
        /// </summary>
        public Vector3 RotationRangeMax;

        private Vector3 _randomForce;

        private void Start()
        {
            RandomizeConstantRotation();
        }

        /// <summary>
        /// Randomizes the constant rotation of this component.
        /// <para><see cref="UseRandomRange"/> must be true for this to have any effect</para>
        /// </summary>
        [KitButton("Randomize Constant Rotation")]
        public void RandomizeConstantRotation()
        {
            _randomForce = new Vector3();
            _randomForce.x = Random.Range(RotationRangeMin.x, RotationRangeMax.x);
            _randomForce.y = Random.Range(RotationRangeMin.y, RotationRangeMax.y);
            _randomForce.z = Random.Range(RotationRangeMin.z, RotationRangeMax.z);
        }

        private void Update()
        {
            var force = Rotation;
            if (UseRandomRange)
            {
                force = _randomForce;
            }

            force *= Time.deltaTime;
            force += transform.rotation.eulerAngles;

            transform.rotation = Quaternion.Euler(force);
        }
    }
}
