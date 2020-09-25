# Game Jam Starter Kit
## About
Game Jam Starter Kit is a collection of useful assets, tools, components, and extension methods to help your team work together more efficiently and save you time!

# Features
### Extension Methods
* Tons of extension methods to give you quick shortcuts and super useful functionality right out of the box!
* Example: Getting a random item from a collection
```c#
// Without starter kit
var myCollection = new List<string>();
var length = myCollection.Count;
var randomIndex = Random.Range(0, length);
var randomItem = myCollection[randomIndex];

// With starter kit
var myCollection = new List<string>();
var randomItem = myCollection.RandomItem();

```
* Example: Getting the direction to another game object

```c#
// Without starter kit
var myPos = transform.position;
var otherPos = other.gameObject.transform.position;
var dir = (otherPos - myPos).normalized;

// With starter kit
var dir = transform.DirectionTo(other.gameObject);

```

### Boilerplate Systems
* Useful pre-made boilerplate systems ready to drop right into your game and use immediately! 
* Example: [Health System](https://aseward.gitlab.io/gamejamstarterkit/modules/Health_System.html)
* Example: [Currency System](https://aseward.gitlab.io/gamejamstarterkit/modules/Currency.html)

### Components
* Useful components and serialized objects ready to improve your workflow and save you tons of time.
* Example: [Background Music Player With Crossfading](https://gitlab.com/ASeward/gamejamstarterkit/-/blob/master/Assets/GameJamStarterKit/Audio/Scripts/BackgroundMusic.cs)
* Example: [Tabular UI Layout Group](https://gitlab.com/ASeward/gamejamstarterkit/-/blob/master/Assets/GameJamStarterKit/UI/Scripts/TabLayoutGroup.cs)

### Teamwork Oriented Tools
* Tools, components and systems designed to help a team get started working collaboratively right off the bat.
* Example: [FX System](https://aseward.gitlab.io/gamejamstarterkit/modules/FX_System.html)

### Prototyping Assets
* Premade simple assets ready to use for placeholders and prototyping. Not ready for a full game but good enouigh to fill in the blanks for a programmer to work with.
* *this is still a very work in progress feature*

### So much more
These key features only scratch the surface of what game jam starter kit can help with. For more check out the [Modules](https://aseward.gitlab.io/gamejamstarterkit/modules/Core.html) in the documentation for a brief summary of everything starter kit has to offer!

# How can I get it?
Head over to the documentation and follow the [Getting Started](https://aseward.gitlab.io/gamejamstarterkit/guides/Getting_Started.html) guide!

# Links
[Documentation](https://aseward.gitlab.io/gamejamstarterkit)

[Modules](https://aseward.gitlab.io/gamejamstarterkit/modules/Core.html)

[Discord](https://discord.gg/zXs5MCb)


# Contributing
See [Wiki/Contributing](https://gitlab.com/ASeward/gamejamstarterkit/-/wikis/Contributing)
