using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamStarterKit.HealthSystem
{
    /// <summary>
    /// An abstract class for creating damage types that modify incoming damage or healing to a <see cref="HealthComponent" />
    /// </summary>
    public abstract class DamageType : ScriptableObject
    {
        protected static readonly Dictionary<Type, DamageType> DAMAGE_TYPES = new Dictionary<Type, DamageType>();
        
        protected DamageType()
        {
        }

        /// <summary>
        /// allows modification of incoming damage to a health component
        /// </summary>
        /// <param name="damage">damage to modify</param>
        /// <param name="healthComponent">the health component being damaged</param>
        /// <returns>modified health value</returns>
        public abstract void ModifyDamageValue(ref int damage, HealthComponent healthComponent);

        /// <summary>
        /// allows modification of incoming healing to a health component
        /// </summary>
        /// <param name="healing">healing to modify</param>
        /// <param name="healthComponent">the health component being healed</param>
        public abstract void ModifyHealingValue(ref int healing, HealthComponent healthComponent);

        /// <summary>
        /// Gets an instance of the damage type passed to T
        /// </summary>
        /// <typeparam name="T">the damage type to get</typeparam>
        /// <returns>an instance of the damage type T</returns>
        public static T Get<T>() where T : DamageType
        {
            if (DAMAGE_TYPES.ContainsKey(typeof(T)))
            {
                return DAMAGE_TYPES[typeof(T)] as T;
            }
            else
            {
                var damageType = CreateInstance<T>();
                DAMAGE_TYPES[typeof(T)] = damageType;
                return damageType;
            }
        }
    }
}