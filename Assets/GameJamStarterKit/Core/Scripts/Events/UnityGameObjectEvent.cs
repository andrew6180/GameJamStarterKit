using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameJamStarterKit
{
    /// <summary>
    /// UnityEvent with a GameObject parameter
    /// </summary>
    [Serializable]
    public class UnityGameObjectEvent : UnityEvent<GameObject> { }
}