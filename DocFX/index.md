# Game Jam Starter Kit
Game Jam Starter Kit is a collection of useful extensions, systems, and assets designed to help teams avoid writing boilerplate, or wasting time making throw away placeholder assets.

# What Game Jam Starter Kit isn't
This is not a collection of pre-made games, gameplay systems, or other 'game in a box' style tools. Jam Kit is intended to help you make a game from scratch. 

# How can I get it?
Ensure you have git [https://git-scm.com/](https://git-scm.com/)

Use the unity package manager to install from the git URL `https://github.com/ajseward/GameJamStarterKit.git#upm`

### My UPM doesn't support git urls! 
for `<=2018.2` package manager does not support git urls. Your best bet is either upgrading to `>=2018.3` or downloading a [release version](https://github.com/ajseward/GameJamStarterKit/releases). 
 
 
# how do I update git UPM packages!?
UPM locks to whatever the latest commit was when you added the project. *This may be different in future versions of unity. This was last tested with 2019.3*

##  \>=2019.3
if you already have `Packages\packages-lock.json`, delete the file and unity should update to the latest version for all your git packages.

if you do not have `packages-lock.json` continue

Add this to your `Packages\manifest.json` after `"dependencies": { ... }`

```
  "enableLockFile": true,
  "resolutionStrategy": "highest",
```
If there is a `"lock:" { ... }` section, delete it.

so your `Packages\manifest.json` looks like this 
```json5
{
  "dependencies": {
    "com.aseward.game-jam-starter-kit": "https://github.com/ajseward/GameJamStarterKit.git#upm",
    },
  "enableLockFile": true,
  "resolutionStrategy": "highest",
}
```

**What did I just do?**

This forces UPM to use a packages-lock.json file in the future.

essentially a file for that `"lock:" { ... }` section.

If you removed the `"lock": { ... }` section, or didn't have it, you should be safe to open unity and it will update to the latest version of the git package.

**In the future, delete `Packages\packages-lock.json` and it should update your git packages.**

# Links
[Github - Issue Tracker](https://github.com/ajseward/GameJamStarterKit/issues)