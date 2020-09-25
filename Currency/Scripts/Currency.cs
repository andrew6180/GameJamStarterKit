using System;
using UnityEngine;

namespace GameJamStarterKit.Currency
{
    /// <summary>
    /// Serializable struct that represents a currency.
    /// </summary>
    [Serializable]
    public struct Currency
    {
        /// <summary>
        /// The type of currency
        /// </summary>
        [SerializeField]
        private string Name;
        
        /// <summary>
        /// Gets the name of the currency
        /// </summary>
        public string GetName => Name;
        
        /// <summary>
        /// The amount of currency
        /// </summary>
        [SerializeField]
        private double Amount;
        
        /// <summary>
        /// Gets the current amount of currency
        /// </summary>
        public double GetAmount => Amount;

        /// <summary>
        /// Creates a new currency with the given name and amount
        /// </summary>
        /// <param name="name">currency name</param>
        /// <param name="amount">currency amount</param>
        public Currency(string name, double amount = 0d)
        {
            Name = name;
            Amount = amount;
        }
        
        /// <summary>
        /// Compare the names of this currency and another
        /// </summary>
        /// <param name="currency">currency to compare to</param>
        /// <returns>returns true if this and the other currency have the same name</returns>
        public bool AreSameName(Currency currency)
        {
            return Name == currency.GetName;
        }

        
        /// <inheritdoc cref="System.Object.Equals(object)"/>
        public override bool Equals(object obj)
        {
            if (obj is Currency currency)
            {
                return Equals(currency);
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // ReSharper disable once NonReadonlyMemberInGetHashCode
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ Amount.GetHashCode();
            }
        }

        /// <summary>
        /// Compare two currency objects
        /// </summary>
        /// <param name="currency">the currency to compare with</param>
        /// <returns>Returns true if the name and currency amount is equal to another currency object</returns>
        public bool Equals(Currency currency)
        {
            return Name == currency.Name && Amount.Equals(currency.Amount);
        }
        
        /// <summary>
        /// Represents this currency object as Name xAmount.
        /// </summary>
        /// <example>USD x1000.50</example>
        public override string ToString()
        {
            return $"Currency: {Name} x{Amount}";
        }

        /// <summary>
        /// Adds the amount given to this currency 
        /// </summary>
        /// <param name="amount">the amount to add</param>
        public void Add(double amount)
        {
            // clamp to prevent overflow
            var newAmount = Amount + amount;
            if (double.IsInfinity(newAmount))
            {
                Amount = double.IsNegativeInfinity(newAmount) ? double.MinValue : double.MaxValue;
            }
            else
            {
                Amount = newAmount;
            }
            Amount = (double.IsInfinity(newAmount)) ? double.MaxValue : newAmount;
        }

        /// <summary>
        /// Subtracts the amount given from this currency
        /// </summary>
        /// <param name="amount">amount to subtract</param>
        public void Subtract(double amount)
        {
            Add(-amount);
        }

        /// <summary>
        /// Check if this currency has enough to subtract the amount
        /// </summary>
        /// <param name="amount">amount to subtract</param>
        /// <returns>returns true if this currency has enough</returns>
        public bool CanSubtract(double amount)
        {
            return Amount >= amount;
        }

        /// <summary>
        ///  Add another currency to this currency. Ensures the other currency has the same name and has enough.
        /// </summary>
        /// <param name="currency">currency to add</param>
        /// <param name="amount">amount to subtract</param>
        public void Add(Currency currency, double amount)
        {
            if (!AreSameName(currency) || !currency.CanSubtract(amount))
                return;
            
            Add(amount);
            currency.Subtract(amount);
        }

        /// <summary>
        /// Sets the amount for this currency.
        /// </summary>
        /// <param name="amount">amount of currency</param>
        public void Set(double amount)
        {
            Amount = amount;
        }
    }
}