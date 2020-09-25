namespace GameJamStarterKit.Currency
{
    /// <summary>
    /// interface for creating a sellable class
    /// </summary>
    public interface ISellable
    {
        /// <summary>
        /// Check if this can be sold
        /// </summary>
        /// <returns>returns true if this can be sold</returns>
        bool CanSell();
        /// <summary>
        /// Sells this
        /// </summary>
        /// <returns>returns the currency this is sold for</returns>
        Currency Sell();
    }
}