using System;
using System.Linq;

namespace GameJamStarterKit.Modifiers
{
    /// <summary>
    /// Represents a value that can have modifiers applied to it.
    /// </summary>
    [Serializable]
    public class ModifiableValue<T> : Modifiable<ValueModifier<T>>
    {
        /// <summary>
        /// Gets the type of value this represents
        /// </summary>
        public readonly Type ValueType = typeof(T);

        /// <summary>
        /// The value of this modifiable before modifiers are applied.
        /// </summary>
        public T BaseValue;

        /// <summary>
        /// Gets the modified value
        /// </summary>
        public T ModifiedValue => GetModifiedValue();
        
        /// <summary>
        /// Creates a new instance of this using the base value given
        /// </summary>
        /// <param name="value">the base value</param>
        public ModifiableValue(T value)
        {
            BaseValue = value;
        }

        /// <summary>
        /// Creates a new instance of this using default(T)
        /// </summary>
        public ModifiableValue()
        {
            BaseValue = default;
        }

        private T GetModifiedValue()
        {
            return Modifiers.Aggregate(BaseValue, (current, modifier) => modifier.ModifyValue(current));
        }
        
        public static implicit operator ModifiableValue<T>(T value)
        {
            return new ModifiableValue<T>(value);
        }
    }
}