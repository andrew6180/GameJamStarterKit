using UnityEngine;

namespace GameJamStarterKit.HealthSystem.Samples
{
    //[CreateAssetMenu(menuName = "DemoDamage/Frost")]
    public class DemoFrostDamageType : DamageType
    {
        public override void ModifyDamageValue(ref int damage, HealthComponent healthComponent)
        {
            if (healthComponent.IsWeakAgainst(GetType()))
            {
                damage *= 2;
            }
            else if (healthComponent.IsStrongAgainst(GetType()))
            {
                damage = Mathf.RoundToInt(damage * 0.5f);
            }
        }

        public override void ModifyHealingValue(ref int healing, HealthComponent healthComponent)
        {
            if (healthComponent.IsWeakAgainst(GetType()))
            {
                healing = Mathf.RoundToInt(healing * 0.5f);
            }
            else if (healthComponent.IsStrongAgainst(GetType()))
            {
                healing *= 2;
            }
        }
    }
}