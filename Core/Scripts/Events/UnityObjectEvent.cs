using System;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace GameJamStarterKit.Events
{
    /// <summary>
    /// UnityEvent with a UnityEngine.Object parameter
    /// </summary>
    [Serializable]
    public class UnityObjectEvent : UnityEvent<Object> { }
}