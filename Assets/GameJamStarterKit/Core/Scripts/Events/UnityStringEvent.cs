using System;
using UnityEngine.Events;

namespace GameJamStarterKit
{
    /// <summary>
    /// UnityEvent with a string parameter
    /// </summary>
    [Serializable]
    public class UnityStringEvent : UnityEvent<string> { }
}