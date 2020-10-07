using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameJamStarterKit.Events
{
    /// <summary>
    /// UnityEvent with a GameObject parameter
    /// </summary>
    [Serializable]
    public class UnityGameObjectEvent : UnityEvent<GameObject> { }
}