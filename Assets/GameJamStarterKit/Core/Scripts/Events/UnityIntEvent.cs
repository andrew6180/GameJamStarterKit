using System;
using UnityEngine.Events;

namespace GameJamStarterKit.Events
{
    /// <summary>
    /// UnityEvent with a int parameter
    /// </summary>
    [Serializable]
    public class UnityIntEvent : UnityEvent<int> { }
}