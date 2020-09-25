using UnityEngine;

namespace GameJamStarterKit.Currency
{
    /// <summary>
    /// A Generic implementation of ISellable.
    /// </summary>
    public class SellableComponent : MonoBehaviour, ISellable
    {
        
        /// <summary>
        /// If this GameObject can currently be sold or not.
        /// </summary>
        [Tooltip("If this can be sold or not")]
        public bool CanBeSold = true;
        /// <summary>
        /// The Currency to sell this for
        /// </summary>
        [Tooltip("The currency to sell this for")]
        public Currency SellValue;

        /// <summary>
        /// Invoked when this is sold
        /// </summary>
        [Tooltip("Invoked when this is sold. Passes the Currency sell value and this Object being sold")]
        public CurrencyObjectUnityEvent OnSold;

        /// <summary>
        /// <inheritdoc cref="ISellable.CanSell"/>
        /// returns <see cref="CanBeSold"/>
        /// </summary>
        public bool CanSell()
        {
            return CanBeSold;
        }
        
        /// <summary>
        /// <inheritdoc cref="ISellable.Sell"/>
        /// Calls OnSold then destroys the game object this is attached to.
        /// </summary>
        public Currency Sell()
        {
            OnSold?.Invoke(SellValue, gameObject);
            Destroy(gameObject);
            return SellValue;
        }
    }
}