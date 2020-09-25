using UnityEngine;

namespace GameJamStarterKit.Currency
{
    /// <summary>
    /// Interface for creating a buyable class
    /// </summary>
    public interface IBuyable
    {
        /// <summary>
        /// Gets the cost of this Buyable
        /// </summary>
        /// <returns>returns a currency for the cost of this Buyable</returns>
        Currency GetCost();
        
        /// <summary>
        /// Check if the wallet can afford to purchase this Buyable
        /// </summary>
        /// <param name="wallet">the wallet to check</param>
        /// <returns>returns true if the wallet has enough of the cost currency</returns>
        bool CanBuy(Wallet wallet);
        
        /// <summary>
        /// Check if the currency can afford to purchase this Buyable
        /// </summary>
        /// <param name="currency">currency to check</param>
        /// <returns>returns true if the currency has enough and is the same name as the cost currency</returns>
        bool CanBuy(Currency currency);

        /// <summary>
        /// Purchases the item if possible using the given wallet
        /// </summary>
        /// <param name="wallet">the wallet to purchase with</param>
        /// <param name="boughtObject">the object bought</param>
        /// <returns>returns if this buyable was purchased or not.</returns>
        bool TryBuy(Wallet wallet, out Object boughtObject);

        /// <summary>
        /// Purchases the item if possible using the given currency
        /// </summary>
        /// <param name="currency">the currency to purchase with</param>
        /// <param name="boughtObject">the object bought</param>
        /// <returns>returns if this buyable was purchased or not.</returns>
        bool TryBuy(Currency currency, out Object boughtObject);
    }
}