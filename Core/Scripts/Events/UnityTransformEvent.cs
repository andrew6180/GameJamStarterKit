using System;
using UnityEngine.Events;

namespace GameJamStarterKit.Events
{
    /// <summary>
    /// UnityEvent with a transform parameter
    /// </summary>
    [Serializable]
    public class UnityTransformEvent : UnityEvent<UnityEngine.Transform> { }
}