using UnityEngine;

namespace GameJamStarterKit.HealthSystem
{
    /// <summary>
    /// Adds extension methods to GameObjects allowing easy access to <see cref="HealthComponent"/>
    /// </summary>
    public static class GameObjectHealthExtensions
    {
        /// <summary>
        /// applies to damage to this GameObject's HealthComponent.
        /// <para>Does nothing if this doesn't have a HealthComponent</para>
        /// </summary>
        /// <param name="gameObject">this</param>
        /// <param name="amount">damage to apply</param>
        /// <param name="damageType">damage type to use</param>
        public static void Damage(this GameObject gameObject, int amount, DamageType damageType = null)
        {
            gameObject.GetComponent<HealthComponent>()?.Damage(amount, damageType);
        }
        
        /// <summary>
        /// applies to damage to this GameObject's HealthComponent.
        /// <para>Does nothing if this doesn't have a HealthComponent</para>
        /// </summary>
        /// <typeparam name="T">damage type to use</typeparam>
        /// <param name="gameObject">this</param>
        /// <param name="amount">damage to apply</param>
        public static void Damage<T>(this GameObject gameObject, int amount) where T : DamageType
        {
            gameObject.GetComponent<HealthComponent>()?.Damage<T>(amount);
        }

        /// <summary>
        /// applies a heal to this GameObject's HealthComponent.
        /// <para>Does nothing if this doesn't have a HealthComponent</para>
        /// </summary>
        /// <param name="gameObject">this</param>
        /// <param name="amount">amount to heal for</param>
        /// <param name="damageType">damage type to use</param>
        public static void Heal(this GameObject gameObject, int amount, DamageType damageType = null)
        {
            gameObject.GetComponent<HealthComponent>()?.Heal(amount, damageType);
        }

        /// <summary>
        /// applies a heal to this GameObject's HealthComponent.
        /// <para>Does nothing if this doesn't have a HealthComponent</para>
        /// </summary>
        /// <typeparam name="T">damage type to use</typeparam>
        /// <param name="gameObject">this</param>
        /// <param name="amount">amount to heal for</param>
        public static void Heal<T>(this GameObject gameObject, int amount) where T : DamageType
        {
            gameObject.GetComponent<HealthComponent>()?.Heal<T>(amount);
        }

        /// <summary>
        /// returns the current health for this GameObject's HealthComponent. Returns -1 if it has no component.
        /// </summary>
        /// <param name="gameObject">this</param>
        /// <returns>returns the current health of the health component if any. Returns -1 if there is no component.</returns>
        public static int GetCurrentHealth(this GameObject gameObject)
        {
            var health = gameObject.GetComponent<HealthComponent>();
            return health != null ? health.Health : -1;
        }
    }
}