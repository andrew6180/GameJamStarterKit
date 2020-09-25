namespace GameJamStarterKit.Modifiers
{
    public interface IModifiable
    {
        /// <summary>
        /// Adds an <see cref="IModifier"/> to the modifier stack
        /// </summary>
        /// <param name="modifier">the modifier to add</param>
        void AddModifier(IModifier modifier);
        
        /// <summary>
        /// Removes an <see cref="IModifier"/> from the modifier stack
        /// </summary>
        /// <param name="modifier">the modifier to remove</param>
        void RemoveModifier(IModifier modifier);

        /// <summary>
        /// Adds a unique <see cref="IModifier"/> to the modifier stack using the key given
        /// </summary>
        /// <param name="modifier">the modifier to add</param>
        /// <param name="key">the key to store it at</param>
        /// <returns>returns true if the modifier was successfully added. False if the key is already in use.</returns>
        bool AddKeyedModifier(IModifier modifier, string key);

        /// <summary>
        /// Removes a modifier for the given key
        /// </summary>
        /// <param name="key">the key to remove</param>
        /// <returns>returns true if the key was found and removed.</returns>
        bool RemoveKeyedModifier(string key);
    }
}