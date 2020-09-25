using UnityEngine;

namespace GameJamStarterKit.Currency
{
    /// <summary>
    /// A Generic implementation of IBuyable.
    /// </summary>
    public class BuyableComponent : MonoBehaviour, IBuyable
    {
        /// <summary>
        /// The cost of this Buyable
        /// </summary>
        [Tooltip("The cost of this buyable")]
        public Currency Cost;
        
        /// <summary>
        /// Called when this buyable is purchased. Passes the GameObject this is attached to.
        /// </summary>
        [Tooltip("Called when this buyable is purchased. Passes the Currency Cost and the Object this is attached to.")]
        public CurrencyObjectUnityEvent OnPurchased;
        
        /// <summary>
        /// <inheritdoc cref="IBuyable.GetCost"/>
        /// </summary>
        public Currency GetCost()
        {
            return Cost;
        }

        /// <summary>
        /// <inheritdoc cref="IBuyable.CanBuy(Wallet)"/>
        /// </summary>
        public bool CanBuy(Wallet wallet)
        {
            return wallet.CanSubtract(Cost.GetName, Cost.GetAmount);
        }
        /// <summary>
        /// <inheritdoc cref="IBuyable.CanBuy(Currency)"/>
        /// </summary>
        public bool CanBuy(Currency currency)
        {
            return currency.AreSameName(Cost) && currency.CanSubtract(Cost.GetAmount);
        }

        /// <summary>
        /// <inheritdoc cref="IBuyable.TryBuy(Wallet, out Object)"/>
        /// </summary>
        /// <param name="wallet"><inheritdoc cref="IBuyable.TryBuy(Wallet, out Object)"/></param>
        /// <param name="boughtObject">The object purchased. Null if unable to buy</param>
        public bool TryBuy(Wallet wallet, out Object boughtObject)
        {
            if (!CanBuy(wallet))
            {
                boughtObject = null;
                return false;
            }

            wallet.Subtract(Cost.GetName, Cost.GetAmount);
            OnPurchased?.Invoke(Cost, gameObject);
            boughtObject = gameObject;
            return true;
        }

        /// <summary>
        /// <inheritdoc cref="IBuyable.TryBuy(Currency, out Object)"/>
        /// </summary>
        /// <param name="currency"><inheritdoc cref="IBuyable.TryBuy(Currency, out Object)"/></param>
        /// <param name="boughtObject">The object purchased. Null if unable to buy</param>
        public bool TryBuy(Currency currency, out Object boughtObject)
        {
            if (!CanBuy(currency))
            {
                boughtObject = null;
                return false;
            }

            currency.Subtract(Cost.GetAmount);
            OnPurchased.Invoke(Cost, gameObject);
            boughtObject = gameObject;
            return true;
        }
    }
}