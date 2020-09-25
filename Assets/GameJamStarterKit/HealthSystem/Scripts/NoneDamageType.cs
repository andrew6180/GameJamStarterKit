namespace GameJamStarterKit.HealthSystem
{
    /// <summary>
    /// Provides a basic implementation of <see cref="DamageType"/> that applies no modifications.
    /// </summary>
    public class NoneDamageType : DamageType
    {
        public override void ModifyDamageValue(ref int damage, HealthComponent healthComponent)
        {
        }

        public override void ModifyHealingValue(ref int healing, HealthComponent healthComponent)
        {
        }
    }
}