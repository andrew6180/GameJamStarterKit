using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameJamStarterKit
{
    /// <summary>
    /// UnityEvent with a Vector3 parameter
    /// </summary>
    [Serializable]
    public class UnityVector3Event : UnityEvent<Vector3> { }
}