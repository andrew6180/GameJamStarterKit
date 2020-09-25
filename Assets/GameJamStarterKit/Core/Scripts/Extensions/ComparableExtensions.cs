using System;

namespace GameJamStarterKit
{
    /// <summary>
    /// Useful extensions for IComparable
    /// </summary>
    public static class ComparableExtensions
    {
        /// <summary>
        /// Returns true if the value is between lower and upper, including if the value is equal to lower or upper.
        /// </summary>
        /// <param name="value">this value</param>
        /// <param name="lower">lower limit</param>
        /// <param name="upper">upper limit</param>
        /// <typeparam name="T">type of IComparable</typeparam>
        /// <returns>true if the value is between the lower and upper limit including equal to the limit values</returns>
        public static bool IsBetweenInclusive<T>(this T value, T lower, T upper) where T : IComparable<T>
        {
            return value.CompareTo(lower) >= 0 && value.CompareTo(upper) <= 0;
        }

        /// <summary>
        ///  Returns true if the value is between lower and upper, does not include values equal to lower or upper.
        /// </summary>
        /// <param name="value">this value</param>
        /// <param name="lower">lower limit</param>
        /// <param name="upper">upper limit</param>
        /// <typeparam name="T">type of IComparable</typeparam>
        /// <returns>true if the value is between the lower and upper limit not including equal to the limit values</returns>
        public static bool IsBetweenExclusive<T>(this T value, T lower, T upper) where T : IComparable<T>
        {
            return value.CompareTo(lower) > 0 && value.CompareTo(upper) < 0;
        }
    }
}