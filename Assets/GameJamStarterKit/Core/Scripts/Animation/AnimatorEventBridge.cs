using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace GameJamStarterKit
{
    /// <summary>
    /// Allows for Animations to call UnityEvents using a list of <see cref="AnimatorEventData"/>
    /// </summary>
    public class AnimatorEventBridge : MonoBehaviour
    {
        /// <summary>
        /// Events the attached animator can call
        /// </summary>
        [Tooltip("Events the attached animator can call")]
        public List<AnimatorEventData> EventMap = null;

        /// <summary>
        /// Calls the event mapped to the key given. Does nothing if the key is not found.
        /// </summary>
        /// <param name="key">event key</param>
        public void CallEvent(string key)
        {
            var unityEvent = EventMap.FirstOrDefault(data => data.EventKey == key);
            unityEvent?.Event.Invoke();
        }

        /// <summary>
        /// Calls the event mapped to the key given. Logs a warning if the event is not found.
        /// </summary>
        /// <param name="key">event key</param>
        public void CallEventWithLogWarning(string key)
        {
            var unityEvent = EventMap.FirstOrDefault(data => data.EventKey == key);
            if (unityEvent == null)
            {
                Debug.LogWarning("[" + gameObject.name + ":AnimatorEventBridge] Tried to call event " + key +
                                 " but the event was not found.");
                return;
            }

            unityEvent.Event.Invoke();
        }

        /// <summary>
        /// Calls the event mapped to the key given. logs a debug message when called. 
        /// </summary>
        /// <param name="key">event key</param>
        public void CallEventWithDebugLog(string key)
        {
            var unityEvent = EventMap.FirstOrDefault(data => data.EventKey == key);

            if (unityEvent == null)
            {
                Debug.Log("[" + gameObject.name + ":AnimatorEventBridge] Could not find key " + key);
                return;
            }

            Debug.Log("[" + gameObject.name + ":AnimatorEventBridge] Called event for " + key);
            unityEvent.Event.Invoke();
        }
    }
    
    /// <summary>
    /// Maps a UnityEvent to a key.
    /// </summary>
    [Serializable]
    public class AnimatorEventData
    {
        public string EventKey;
        public UnityEvent Event;
    }
}