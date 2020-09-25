using UnityEngine;

namespace GameJamStarterKit
{
    /// <summary>
    /// Helpful math functions
    /// </summary>
    public static class KitMath
    {
        /// <summary>
        /// Normalizes a value from once range to another.
        /// </summary>
        /// <param name="currentValue">the current value</param>
        /// <param name="currentMin">the current minimum value</param>
        /// <param name="currentMax">the current maximum value</param>
        /// <param name="targetMin">the target minimum value</param>
        /// <param name="targetMax">the target maximum value</param>
        /// <returns>the value normalized to the new range</returns>
        public static float NormalizeToRange(float currentValue,
            float currentMin,
            float currentMax,
            float targetMin,
            float targetMax)
        {
            if (currentValue == currentMin && currentValue == currentMax)
                return targetMin;
            
            var currentRange = currentMax - currentMin;
            var targetRange = targetMax - targetMin;
            var currentValueDistance = currentValue - currentMin;

            return currentValueDistance * targetRange / currentRange + targetMin;
        }

        /// <summary>
        /// Check if a number is positive
        /// </summary>
        /// <param name="value">the number to check</param>
        /// <returns>returns true if the number is positive, otherwise false</returns>
        public static bool IsPositive(float value)
        {
            return Mathf.Sign(value) > 0;
        }
    }
}