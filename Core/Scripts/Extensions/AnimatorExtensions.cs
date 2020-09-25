using System.Linq;
using UnityEngine;

namespace GameJamStarterKit
{
    /// <summary>
    /// Useful extensions for Animators
    /// </summary>
    public static class AnimatorExtensions
    {
        /// <summary>
        /// returns if the given animator has a parameter with the given name or not.
        /// </summary>
        /// <param name="animator">this</param>
        /// <param name="parameter">parameter to search for</param>
        /// <returns>true if this has a parameter with the given key</returns>
        public static bool HasParameter(this Animator animator, string parameter)
        {
            return animator.parameters.Any(p => p.name == parameter);
        }

        /// <summary>
        /// returns if the given animator has a parameter with the given type or not.
        /// </summary>
        /// <param name="animator">this</param>
        /// <param name="parameter">parameter to search for</param>
        /// <param name="type">type of the parameter</param>
        /// <returns>true if this has a parameter with the given key and type</returns>
        public static bool HasParameterWithType(this Animator animator,
            string parameter,
            AnimatorControllerParameterType type)
        {
            return animator.parameters.Any(p => p.name == parameter && p.type == type);
        }

        /// <summary>
        /// Sets the parameter using a serializable AnimationParameter.
        /// <remarks>calls parameter.Set(this)</remarks>
        /// </summary>
        /// <param name="animator">this</param>
        /// <param name="parameter">parameter to set.</param>
        public static void SetParameter(this Animator animator, AnimationParameter parameter)
        {
            parameter.Set(animator);
        }
    }
}