using System;
using UnityEngine.Events;

namespace GameJamStarterKit.Events
{
    /// <summary>
    /// UnityEvent with a bool parameter
    /// </summary>
    [Serializable]
    public class UnityBoolEvent : UnityEvent<bool> { }
}