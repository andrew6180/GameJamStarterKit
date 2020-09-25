# Installation
Ensure you have git [https://git-scm.com/](https://git-scm.com/)

If you're starting a brand new project, don't wanna mess with upm, and just want the entire project right away do 

`git checkout https://github.com/ajseward/GameJamStarterKit.git`

if you want to use UPM then

# \>= 2019.3
1) Open the package manager `Window > Package Manager` 
2) Click the `+` in the top left
3) Click `Add package from git URL...`
4) paste `https://github.com/ajseward/GameJamStarterKit.git#upm` and hit enter
5) done!

# \<=2019.2 
1) Locate `Packages\manifest.json` in your root project folder (the folder that has your assets folder in it).
2) Open with a text editor
3) under `"dependencies` add `"com.aseward.game-jam-starter-kit": "https://github.com/ajseward/GameJamStarterKit.git#upm"`
4) It should look like this 
```json5
{
  "dependencies": {
    "com.aseward.game-jam-starter-kit": "https://github.com/ajseward/GameJamStarterKit.git#upm",
    // a bunch of other packages
  }
}
```
Restart / launch unity, and that's it!

# What now?

[Join the Discord](https://discord.gg/zXs5MCb) for help, sharing your projects, feature requests, or anything else you'd use an asset's discord for really. 
# How do I edit prefabs?
To modify the prefab instance, you can place them in your scene from the `packages/GameJamStarterKit/` folder (below your assets folder, you may need to scroll) in the content browser.

To edit the prefab directly, you can 'copy' them to your project by dragging the prefab from the packages folder to your assets folder.

### My UPM doesn't support git urls! 
for `<=2018.2` package manager does not support git urls. Your best bet is either upgrading to `>=2018.3` or cloning the entire repo and copying the modules you want to use.
 
 
# how do I update git UPM packages!?
UPM locks to whatever the latest commit was when you added the project. 

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
    "com.aseward.game-jam-starter-kit": "https://gitlab.com/ASeward/gamejamstarterkit.git#upm",
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

## \<=2019.2

In `Packages\manifest.json` locate the section for `"lock": { ... }`.

It will look something like this 

```json5
  "lock": {
    "com.aseward.game-jam-starter-kit": {
      "hash": "b93a03f45e25e075987d50157ffe0c0960607aa6",
      "revision": "HEAD"
    }
```

remove the entire section, save, and start unity.