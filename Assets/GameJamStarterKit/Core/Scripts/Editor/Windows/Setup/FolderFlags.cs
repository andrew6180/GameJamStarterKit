using System;

namespace GameJamStarterKit.Editor
{
    [Flags]
    [Serializable]
    internal enum FolderFlags
    {
        None = 0,
        Default = Scripts | Animations | Materials | Models | Effects | Prefabs | Audio | Textures | Scenes | Resources,
        Scripts = 1,
        Animations = 1 << 1,
        Materials = 1 << 2,
        Models = 1 << 3,
        Effects = 1 << 4,
        Prefabs = 1 << 5,
        Audio = 1 << 6,
        Textures = 1 << 7,
        Scenes = 1 << 8,
        Resources = 1 << 9,
        Editor = 1 << 10
    }
}