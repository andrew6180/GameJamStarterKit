# Health System Module
Health system is a simple and easy way to implement health for game objects. 

[Info: Modules/Health System](https://aseward.gitlab.io/gamejamstarterkit/modules/Health_System.html) 
# How to use Damage Types
* Create a new class that inherits from `DamageType` using this template
```c#
[CreateAssetMenu(menuName = "DamageTypes/Create MyDamageType")]
public class MyDamageType : DamageType
{
    public override void ModifyDamageValue(ref int damage, HealthComponent healthComponent)
    {
    }

    public override void ModifyHealingValue(ref int healing, HealthComponent healthComponent)
    {
    }
}
```
* Create a new ScriptableObject in the assets folder using whatever you put for `CreateAssetMenu(menuName = "")`
* On your `HealthComponent` under `WeakAgainst` or `StrongAgainst` add the ScriptableObject you just created.
* When calling `HealthComponent.Damage()` or `HealthComponent.Heal()` you may instead do `HealthComponent.Damage<MyDamageType>()` or `HealthComponent.Heal<MyDamageType>();`
* `ModifyDamageValue` or `ModifyHealingValue` would be a good place to apply / remove buffs & debuffs if needed! 
