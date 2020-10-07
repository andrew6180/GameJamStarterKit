using System;
using UnityEngine.Events;

namespace GameJamStarterKit.Events
{
    /// <summary>
    /// UnityEvent with a float parameter
    /// </summary>
    [Serializable]
    public class UnityFloatEvent : UnityEvent<float> { }
}