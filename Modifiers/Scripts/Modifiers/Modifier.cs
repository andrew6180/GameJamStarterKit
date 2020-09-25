using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GameJamStarterKit.Modifiers
{
    /// <summary>
    /// Base class for any type of modifier applied to a <see cref="Modifiable{T}"/>
    /// </summary>
    [Serializable]
    public abstract class Modifier : IModifier
    {
        /// <summary>
        /// The sorting order for this modifier. Lower values are processed first.
        /// </summary>
        public int Order = 0;
        
        /// <summary>
        /// The parent of this modifier
        /// </summary>
        public IModifiable Parent { get; private set; }

        /// <summary>
        /// The lifetime of this modifier
        /// </summary>
        public float Duration = 0f;

        /// <summary>
        /// Creates a new instance of this with a given duration
        /// </summary>
        /// <param name="duration">the duration/lifetime of this modifier</param>
        /// <param name="order">the sorting order for this modifier. Lower values are processed first</param>
        public Modifier(float duration = 0f, int order = 0)
        {
            Duration = duration;
            Order = order;
        }
        
        /// <summary>
        /// Sets the parent then calls <see cref="OnAttach"/>
        /// </summary>
        /// <param name="modifiable">the modifiable attached to</param>
        internal void OnAttach_Internal(IModifiable modifiable)
        {
            Debug.Log($"{Duration}");
            Parent = modifiable;
            
            if (!(Duration > 0))
                return;

            Task.Delay(Mathf.RoundToInt(Duration * 1000)).ContinueWith(t => Detach());
        }
        
        /// <summary>
        /// Sets the parent to null then calls <see cref="OnDetach"/>
        /// </summary>
        /// <param name="modifiable">the modifiable detached from</param>
        internal void OnDetach_Internal(IModifiable modifiable)
        {
            Parent = null;
        }
        
        /// <summary>
        /// Detaches this modifier from its parent.
        /// </summary>
        public void Detach()
        {
            Parent?.RemoveModifier(this);
        }

        /// <inheritdoc />
        public virtual void OnAttach(IModifiable modifiable)
        {
            OnAttach_Internal(modifiable);
        }
        
        /// <inheritdoc />
        public virtual void OnDetach(IModifiable modifiable)
        {
            OnDetach_Internal(modifiable);
        }
    }
}