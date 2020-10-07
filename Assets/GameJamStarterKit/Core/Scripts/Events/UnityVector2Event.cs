using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameJamStarterKit.Events
{
    /// <summary>
    /// UnityEvent with a Vector2 parameter
    /// </summary>
    [Serializable]
    public class UnityVector2Event : UnityEvent<Vector2> { }
}