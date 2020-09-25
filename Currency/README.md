# Currency Module
Currency is a set of components and utilities to create a basic currency system. 

[Info: Modules/Currency](https://aseward.gitlab.io/gamejamstarterkit/modules/Currency.html) 
# Why string comparison instead of enum?
Strings are extensible enums are not.

# Basic Wallet Use
```c#
// add starting currency in the inspector to the Wallet "MyWallet"
// buying an IBuyable

var buyable = gameObject.GetComponent<IBuyable>();
if (buyable.TryBuy(MyWallet, out var boughtItem))
{
    // do something with the purchased item
}
else
{
    // I couldn't afford the item
}

// selling an ISellable

var sellable = gameObject.GetComponent<ISellable>();
if (sellable.CanSell())
{
    var sellValue = sellable.Sell();
    MyWallet.Add(sellValue);
}
```