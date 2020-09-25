# Modifiers
`GameJamStarterKit.Modifiers` is a system to create modifiable fields, and a buff / debuff system.  

# How it works
Declare a new modifier
```c#
public ModifiableValue<float> MyFloat = 10f;
```
Create a modifier for it 
```c#
var modifier = ValueModifier<float>(initialValue => { return initialValue + 10; });
```
add the modifier to `MyFloat`
```c#
MyFloat.AddModifier(modifier);
```
Now `MyFloat = 20f` so long as `modifier` is added.

Remove `modifier`
```c#
MyFloat.RemoveModifier(modifier);
```
# Seeing `ModifiableValue<T>` in the inspector
### 2020.1 or higher
`ModifiableValue<T>` should display properly since `2020.1` adds support for generics in the inspector.
### 2019.3 or less
There are many ways to work around it without creating extra bloat. The easiest solution imo is to declare an initial value and use it to initialize your `ModifiableValue<T>` on awake.

Example:
```c#
public ModifiableValue<float> MyFloat;
[SerializeField]
private float _myFloatInitialValue;
private void Awake()
{
    MyFloat = _myFloatInitialValue;
}
```
which thanks to implicit operators, essentially calls `new ModifiableValue<float>(_myFloatInitialValue);`. Just ensure no modifiers are added before the initial value is setup
or put the script at the top of your execution order.

# Examples

## Creating a buff that increases speed for a duration. (Sprint)
```c#
public class MyCharacter : MonoBehaviour
{
    public ModifiableValue<float> Speed;
    
    // to get BaseSpeed in the inspector this is necessary unless on 2020.1 or higher
    [SerializeField] private float BaseSpeed = 8f;
    private ValueModifier<float> _sprintModifier;
    private void Awake()
    {
        // increase speed by 2x for 2 seconds.
        _speedModifier = new ValueModifier<float>(speed => speed * 2f, 2f);
        Speed = BaseSpeed; 

    }
    
    public void Sprint()
    {
        Speed.AddModifier(_speedModifier);
    }
    
    public void StopSprint()
    {
        Speed.RemoveModifier(_speedModifier);
    }

}
```

### But wait, what if the player finds a way to call that more than once?
Replace 
```c#
Speed.AddModifier(_speedModifier);
// and 
Speed.RemoveModifier(_speedModifier);
```
with
```c#
Speed.AddKeyedModifier(_speedModifier, "sprint");
// and
Speed.RemoveKeyedModifier("sprint");
```
which will only allow 1 modifier for the key given.



