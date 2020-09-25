using System;

namespace GameJamStarterKit
{
    /// <summary>
    /// X Y Z Axis flags
    /// </summary>
    [Flags]
    [Serializable]
    public enum KitAxisFlags
    {
        None = 0,
        X = 1,
        Y = 1 << 1,
        Z = 1 << 2
    }
}