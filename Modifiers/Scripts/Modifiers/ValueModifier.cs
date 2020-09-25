using System;
using UnityEngine;

namespace GameJamStarterKit.Modifiers
{
    /// <summary>
    /// A modifier for value types
    /// </summary>
    /// <typeparam name="T">the type of value to modify</typeparam>
    [Serializable]
    public class ValueModifier<T> : Modifier
    {
        /// <summary>
        /// The type of values this modifies
        /// </summary>
        public readonly Type TargetType = typeof(T);

        public Func<T, T> ModifyValue { get; }

        /// <inheritdoc />
        public override void OnAttach(IModifiable modifiable)
        {
            base.OnAttach(modifiable);
            if (modifiable is ModifiableValue<T>)
                return;
            
            Debug.LogError($"Trying to attach {this} with type {TargetType} to {modifiable} which is incompatible. Expected ModifiableValue<{TargetType}>");
            Detach();
        }

        /// <summary>
        /// Creates a new value modifier
        /// </summary>
        /// <param name="applyModifier">the function to perform on the base value being modified</param>
        /// <param name="duration">the lifetime of this modifier</param>
        public ValueModifier(Func<T, T> applyModifier, float duration = 0f, int order = 0) : base(duration, order)
        {
            ModifyValue = applyModifier;
        }
    }
}