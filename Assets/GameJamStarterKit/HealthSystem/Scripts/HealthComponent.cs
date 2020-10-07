using System;
using System.Collections.Generic;
using System.Linq;
using GameJamStarterKit.Events;
using UnityEngine;
using UnityEngine.Events;

namespace GameJamStarterKit.HealthSystem
{
    /// <summary>
    /// A Quick solution to adding health to various types of GameObjects. Has unity events for health changes and death.
    /// </summary>
    public class HealthComponent : MonoBehaviour
    {
        /// <summary>
        /// Damage types this health component will take more damage and less healing from.
        /// </summary>
        [Tooltip("Damage types this health component will take more damage and less healing from.")]
        public List<DamageType> WeakAgainst;
        
        /// <summary>
        /// Damage types this health component will take less damage and more healing from.
        /// </summary>
        [Tooltip("Damage types this health component will take less damage and more healing from.")]
        public List<DamageType> StrongAgainst;
        
        /// <summary>
        /// the current health on this component
        /// </summary>
        public int Health => CurrentHealth;

        /// <summary>
        /// the maximum health for this component
        /// </summary>
        public int MaximumHealth;

        /// <summary>
        /// returns the current health of this component
        /// </summary>
        [SerializeField]
        private int CurrentHealth;

        /// <summary>
        /// Clamps incoming damage or healing so it can never be a negative value or exceed maximum health
        /// </summary>
        [Tooltip("Clamp the incoming damage or healing so it can never be a negative value or exceed maximum health?")]
        public bool ClampDamageAndHealing = true;

        /// <summary>
        /// Allows the current health to exceed the maximum health
        /// </summary>
        [Tooltip("Allow the current health to exceed the maximum health?")]
        public bool CanExceedMaximumHealth = false;
        /// <summary>
        /// returns the current health as a 0 to 1 range
        /// </summary>
        public float NormalizedCurrentHealth => CurrentHealth / (float) MaximumHealth;

        /// <summary>
        /// Called when this HealthComponent loses health
        /// <para>Passes the new current health</para>
        /// </summary>
        public UnityIntEvent OnHealthLost;

        /// <summary>
        /// Called when this HealthComponent's percentage changes.
        /// <para>passes the current health percent.</para>
        /// </summary>
        public UnityFloatEvent OnHealthPercentChanged;

        /// <summary>
        /// Called when this HealthComponent no longer has any health
        /// </summary>
        public UnityEvent OnHealthEmpty;

        /// <summary>
        /// Called when this HealthComponent gains health
        /// <para>Passes the new current health</para>
        /// </summary>
        public UnityIntEvent OnHealthGained;

        /// <summary>
        /// Modifies the health by the amount given.
        /// </summary>
        /// <param name="amount">amount to modify health by</param>
        internal void ModifyHealth(int amount)
        {
            if (amount == 0 || CurrentHealth <= 0)
                return;
            
            if (CanExceedMaximumHealth)
            {
                CurrentHealth += amount;
            }
            else
            {
                CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaximumHealth);
            }
            
            OnHealthPercentChanged?.Invoke(NormalizedCurrentHealth);
            
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                OnHealthEmpty.Invoke();
                return;
            }

            if (amount > 0)
            {
                OnHealthGained?.Invoke(CurrentHealth);
            }
            else
            {
                OnHealthLost?.Invoke(CurrentHealth);
            }
        }

        /// <summary>
        /// applies the amount of damage to this HealthComponent
        /// </summary>
        /// <param name="amount">damage to apply</param>
        /// <param name="damageType">damage type to use. null will use <see cref="NoneDamageType"/></param>
        public virtual void Damage(int amount, DamageType damageType = null)
        {
            if (damageType == null)
                damageType = DamageType.Get<NoneDamageType>();

            damageType.ModifyDamageValue(ref amount, this);

            if (ClampDamageAndHealing)
            {
                amount = Mathf.Clamp(amount, 0, Mathf.Abs(amount));
            }
            ModifyHealth(-amount);
        }

        /// <summary>
        /// applies the amount of damage to this HealthComponent
        /// </summary>
        /// <param name="amount">damage to apply</param>
        /// <typeparam name="T">damage type to use.</typeparam>
        public virtual void Damage<T>(int amount) where T : DamageType
        {
            Damage(amount, DamageType.Get<T>());
        }

        /// <summary>
        /// <see cref="Damage"/> which can be invoked from a UnityEvent
        /// </summary>
        /// <param name="amount"></param>
        public void Event_Damage(int amount) => Damage(amount);

        /// <summary>
        /// applies the amount of healing to this HealthComponent
        /// </summary>
        /// <param name="amount">heal to apply</param>
        /// <param name="damageType">damage type to use. null will use <see cref="NoneDamageType"/></param>
        public virtual void Heal(int amount, DamageType damageType = null)
        {
            if (damageType == null)
                damageType = DamageType.Get<NoneDamageType>();

            damageType.ModifyHealingValue(ref amount, this);

            if (ClampDamageAndHealing)
            {
                amount = Mathf.Clamp(amount, 0, Mathf.Abs(amount));
            }
            ModifyHealth(amount);
        }

        /// <summary>
        /// <see cref="Heal"/> which can be invoked from a UnityEvent
        /// </summary>
        /// <param name="amount">amount to heal for</param>
        public void Event_Heal(int amount) => Heal(amount);

        /// <summary>
        /// applies the amount of healing to this HealthComponent
        /// </summary>
        /// <param name="amount">heal to apply</param>
        /// <typeparam name="T">damage type to use.</typeparam>
        public virtual void Heal<T>(int amount) where T : DamageType
        {
            Heal(amount, DamageType.Get<T>());
        }

        /// <summary>
        /// Check if this health component is weak against a given type.
        /// </summary>
        /// <param name="type">type to check</param>
        /// <returns>returns true if this is weak to the type</returns>
        public bool IsWeakAgainst(Type type)
        {
            if (type != typeof(DamageType))
                return false;

            return WeakAgainst.Any(weak => weak.GetType() == type);
        }

        /// <summary>
        /// Check if this health component is strong against a given type.
        /// </summary>
        /// <param name="type">type to check</param>
        /// <returns>returns true if this is strong against the type</returns>
        public bool IsStrongAgainst(Type type)
        {
            if (type != typeof(DamageType))
                return false;
            
            return StrongAgainst.Any(strong => strong.GetType() == type);
        }

        /// <summary>
        /// Check if this health component is weak against a given type.
        /// </summary>
        /// <typeparam name="T">type to check</typeparam>
        /// <returns>returns true if this is weak to the type</returns>
        public bool IsWeakAgainst<T>() where T : DamageType
        {
           return IsWeakAgainst(typeof(T));
        }

        /// <summary>
        /// Check if this health component is strong against a given type.
        /// </summary>
        /// <typeparam name="T">type to check</typeparam>
        /// <returns>returns true if this is strong against the type</returns>
        public bool IsStrongAgainst<T>() where T : DamageType
        {
            return IsStrongAgainst(typeof(T));
        }

        protected virtual void Start()
        {
            CurrentHealth = MaximumHealth;
        }
    }
}