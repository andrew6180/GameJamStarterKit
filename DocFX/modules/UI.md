# UI
`GameJamStarterKit.UI` is a collection of useful prefabs and other various UI elements.

# Features
* Tabular menu component
* Canvas manager
* Interpolated Text for text mesh pro components. (display variables / method return values live in the ui)

# How to use Tabs
Attach `TabLayoutGroup` to a parent GameObject. Any UI GameObject attached to this group will have it's enabled status controlled by `TabLayoutGroup.SwitchTo`

# Canvas Management
In `UI/Prefabs` you'll find a `KitCanvasBase` which can be used as a base for your game's UI. This comes preconfigured with some common settings and has a `UI.View` component attached.

# UI.View
`View` is a singleton which is used to manage the canvas without knowing about everything on the canvas. Currently it has `ShowPopupDialog` to draw a quick dialog window with an int event callback for when the dialog is closed. 

# Editing the prefabs
 To modify the prefab instance, you can place them in your scene from the `packages/GameJamStarterKit/UI/Prefabs` folder (below your assets folder, you may need to scroll) in the content browser.
 
 To edit the prefab directly, you can 'copy' them to your project by dragging the prefab from the packages folder to your assets folder.
 
 # using the InterpolatedText component
 Checkout the example scene in `UI\Samples`