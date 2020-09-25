using System;
using System.Collections.Generic;
using System.Linq;

namespace GameJamStarterKit.Modifiers
{
    /// <summary>
    /// Base class for all modifiable objects
    /// </summary>
    /// <typeparam name="T">the type of modifiers expected</typeparam>
    [Serializable]
    public abstract class Modifiable<T> : IModifiable where T : Modifier, IModifier
    {
        /// <summary>
        /// A List of modifiers attached to this
        /// </summary>
        private readonly List<T> _modifiers = new List<T>();

        /// <summary>
        /// A dictionary of modifiers with a unique key.
        /// </summary>
        private readonly Dictionary<string, T> _keyedModifiers = new Dictionary<string, T>();

        /// <summary>
        /// Returns the modifiers combine with the keyed modifiers, sorted by their order.
        /// </summary>
        public IEnumerable<T> Modifiers => _modifiers.Concat(_keyedModifiers.Values).OrderBy(mod => mod.Order);

        /// <summary>
        /// Check if this has the <see cref="Modifier"/> given
        /// </summary>
        /// <param name="modifier">the modifier to check for</param>
        /// <returns>Returns true if this has the modifier</returns>
        public bool HasModifier(T modifier)
        {
            return _modifiers.Contains(modifier);
        }

        /// <inheritdoc />
        public void AddModifier(IModifier modifier)
        {
            _modifiers.Add((T)modifier);
            modifier.OnAttach(this);
        }

        /// <inheritdoc />
        public void RemoveModifier(IModifier modifier)
        {
            _modifiers.Remove((T) modifier);
            modifier.OnDetach(this);
        }

        /// <inheritdoc />
        public bool AddKeyedModifier(IModifier modifier, string key)
        {
            if (_keyedModifiers.ContainsKey(key))
                return false;
            
            _keyedModifiers.Add(key, (T)modifier);
            return true;
        }

        /// <inheritdoc />
        public bool RemoveKeyedModifier(string key)
        {
            return _keyedModifiers.Remove(key);
        }
    }
}