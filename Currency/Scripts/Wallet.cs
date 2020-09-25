using System.Collections.Generic;
using UnityEngine;

namespace GameJamStarterKit.Currency
{
    /// <summary>
    /// A Component representing a game objects wallet of currencies.
    /// </summary>
    public class Wallet : MonoBehaviour
    {
        /// <summary>
        /// Invoked when a currency is added to. Passes the currency and the amount added
        /// </summary>
        [Tooltip("Invoked when a currency is added to. Passes the currency and the amount added")]
        public CurrencyDoubleUnityEvent OnAdd;

        /// <summary>
        /// Invoked when a currency is subtracted from. Passes the currency and the amount subtracted
        /// </summary>
        [Tooltip("Invoked when a currency is subtracted from. Passes the currency and the amount subtracted")]
        public CurrencyDoubleUnityEvent OnSubtract;

        /// <summary>
        /// Invoked when a currency is removed from this wallet. Passes the currency and its amount when removed.
        /// </summary>
        [Tooltip("Invoked when a currency is removed from this wallet. Passes the currency and its amount when removed.")]
        public CurrencyDoubleUnityEvent OnRemoved;
        
        [SerializeField]
        [Tooltip("The currencies in this wallet when Awake() is called.")]
        private List<Currency> InitialCurrencies = new List<Currency>();

        private readonly Dictionary<string, Currency> _currenciesDict = new Dictionary<string, Currency>();

        private void Awake()
        {
            foreach (var currency in InitialCurrencies)
            {
                if (_currenciesDict.TryGetValue(currency.GetName, out var existingCurrency))
                {
                    existingCurrency.Add(currency.GetAmount);   
                }
                else
                {
                    _currenciesDict.Add(currency.GetName, currency);
                }
            }
        }

        /// <summary>
        /// Adds the amount of currency to this wallet.
        /// </summary>
        /// <param name="currencyName">currency name to add</param>
        /// <param name="amount">amount to add</param>
        public void Add(string currencyName, double amount)
        {
            if (_currenciesDict.TryGetValue(currencyName, out var currency))
            {
                currency.Add(amount);
            }
            else
            {
                currency = new Currency(currencyName, amount);
                _currenciesDict.Add(currencyName, currency);
            }
            
            OnAdd?.Invoke(currency, amount);
        }

        /// <summary>
        /// Adds the amount of currency to this wallet
        /// </summary>
        /// <param name="currency">the currency to add</param>
        public void Add(Currency currency)
        {
            Add(currency.GetName, currency.GetAmount);
        }
        
        /// <summary>
        /// Subtracts the amount of currency from this wallet
        /// </summary>
        /// <param name="currencyName">currency name to subtract</param>
        /// <param name="amount">amount to subtract</param>
        public void Subtract(string currencyName, double amount)
        {
            if (_currenciesDict.TryGetValue(currencyName, out var currency))
            {
                currency.Subtract(amount);
                OnSubtract?.Invoke(currency, amount);
            }
        }

        /// <summary>
        /// Subtracts the amount of currency from this wallet
        /// </summary>
        /// <param name="currency">the currency to subtract</param>
        public void Subtract(Currency currency)
        {
            Subtract(currency.GetName, currency.GetAmount);
        }

        /// <summary>
        /// Check if this wallet can subtract the amount of currency
        /// </summary>
        /// <param name="currencyName">currency name to subtract</param>
        /// <param name="amount">amount to subtract</param>
        /// <returns>returns true if this wallet has the currency and enough amount</returns>
        public bool CanSubtract(string currencyName, double amount)
        {
            return _currenciesDict.TryGetValue(currencyName, out var currency) && currency.CanSubtract(amount);
        }

        /// <summary>
        /// Check if this wallet can subtract the currency
        /// </summary>
        /// <param name="currency">the currency to subtract</param>
        /// <returns>returns true if this wallet has the currency and enough amount</returns>
        public bool CanSubtract(Currency currency)
        {
            return CanSubtract(currency.GetName, currency.GetAmount);
        }

        /// <summary>
        /// Sets the amount of currency for this wallet
        /// </summary>
        /// <param name="currencyName">currency name to set</param>
        /// <param name="amount">currency amount</param>
        public void Set(string currencyName, double amount)
        {
            if (_currenciesDict.TryGetValue(currencyName, out var currency))
            {
                currency.Set(amount);
            }
            else
            {
                _currenciesDict.Add(currencyName, new Currency(currencyName, amount));
            }
        }

        /// <summary>
        /// Sets the amount of currency for this wallet
        /// </summary>
        /// <param name="currency">the currency to set</param>
        public void Set(Currency currency)
        {
            Set(currency.GetName, currency.GetAmount);
        }

        /// <summary>
        /// Removes a currency from this wallet entirely. Returns the amount removed
        /// </summary>
        /// <param name="currencyName">currency to remove</param>
        /// <returns>returns the amount of currency removed. Returns 0 if the wallet didn't have the currency</returns>
        public double Remove(string currencyName)
        {
            if (_currenciesDict.TryGetValue(currencyName, out var currency))
            {
                var retValue = currency.GetAmount;
                OnRemoved?.Invoke(currency, currency.GetAmount);
                _currenciesDict.Remove(currencyName);
                return retValue;
            }

            return 0;
        }

        /// <summary>
        /// Check if this wallet has a currency by name
        /// </summary>
        /// <param name="currencyName">currency name</param>
        /// <returns>returns true if the wallet has the currency</returns>
        public bool HasCurrency(string currencyName)
        {
            return _currenciesDict.ContainsKey(currencyName);
        }

        /// <summary>
        /// Get the <see cref="Currency"/> stored in this wallet
        /// </summary>
        /// <returns>returns an enumerable of the <see cref="Currency"/> in this wallet</returns>
        public IEnumerable<Currency> GetContents()
        {
            return _currenciesDict.Values;
        }
    }
}