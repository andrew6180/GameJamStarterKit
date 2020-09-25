# Camera
`GameJamStarterKit.Camera` contains useful components and extensions relating to the game Camera. Both 2D and 3D games will probably find something useful inside. 

# Features
* SpringArmComponent similar to Unreal Engine 4's spring arm component.
* CameraShake System. Allows for saving shake profiles for use in animation, as well as one-off code based shakes. 

# Creating a Camera Shake
You can call `CameraShakeProfile.MakeProfile()` and pass it into `CameraShaker.StartShake(profile)`, or call it directly on a Camera object with `Camera.StartShake(profile)`

To create a persistent profile, right click in the asset browser, and select `GameJamStarterKit > Camera > Create CameraShake Profile`. Pass this profile into a `CameraShaker` component and call `CameraShaker.StartShake()` to use the attached profile.

