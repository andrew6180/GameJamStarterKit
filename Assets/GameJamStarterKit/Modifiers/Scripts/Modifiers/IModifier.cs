namespace GameJamStarterKit.Modifiers
{
    /// <summary>
    /// Base interface for <see cref="Modifier"/>
    /// </summary>
    public interface IModifier
    {
        /// <summary>
        /// Called when this Modifier is attached to a <see cref="IModifiable"/>
        /// </summary>
        /// <param name="modifiable">the modifiable attached to</param>
        void OnAttach(IModifiable modifiable);
        
        /// <summary>
        /// Called when this Modifier is detached from a <see cref="IModifiable"/>
        /// </summary>
        /// <param name="modifiable">the modifiable detached from</param>
        void OnDetach(IModifiable modifiable);

    }
}