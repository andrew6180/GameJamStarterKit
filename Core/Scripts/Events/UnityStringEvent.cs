using System;
using UnityEngine.Events;

namespace GameJamStarterKit.Events
{
    /// <summary>
    /// UnityEvent with a string parameter
    /// </summary>
    [Serializable]
    public class UnityStringEvent : UnityEvent<string> { }
}