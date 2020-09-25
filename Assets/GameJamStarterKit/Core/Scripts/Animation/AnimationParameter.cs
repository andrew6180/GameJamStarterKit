using System;
using UnityEngine;

namespace GameJamStarterKit
{
    /// <summary>
    /// A serialized Animation Parameter
    /// </summary>
    [Serializable]
    public class AnimationParameter
    {
        /// <summary>
        /// Parameter Key
        /// </summary>
        [Tooltip("Parameter Key")]
        public string Key;
        
        /// <summary>
        /// Type of the parameter
        /// </summary>
        [Tooltip("Type of the parameter")]
        public AnimatorControllerParameterType ParameterType = AnimatorControllerParameterType.Bool;
        
        public bool BoolValue;
        public float FloatValue;
        public int IntValue;

        /// <summary>
        /// Sets the animation parameter on the given animator. Does nothing if the animator does not have the parameter.
        /// </summary>
        /// <param name="animator">Animator to set the parameter of</param>
        public void Set(Animator animator)
        {
            // silently fail
            if (!animator.HasParameterWithType(Key, ParameterType))
                return;

            switch (ParameterType)
            {
                case AnimatorControllerParameterType.Float:
                    animator.SetFloat(Key, FloatValue);
                    break;
                case AnimatorControllerParameterType.Int:
                    animator.SetInteger(Key, IntValue);
                    break;
                default:
                case AnimatorControllerParameterType.Bool:
                    animator.SetBool(Key, BoolValue);
                    break;
                case AnimatorControllerParameterType.Trigger:
                    break;
            }
        }
    }
}