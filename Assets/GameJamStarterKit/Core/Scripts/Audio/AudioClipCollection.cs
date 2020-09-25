using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameJamStarterKit
{
    /// <summary>
    /// A collection of AudioClips that can be played in sequence or randomly.
    /// </summary>
    [Serializable]
    public class AudioClipCollection
    {
        /// <summary>
        /// Clips in this collection
        /// </summary>
        [SerializeField]
        [Tooltip("Clips in this collection")]
        private List<AudioClip> Clips = null;
        
        private AudioClip RandomClip => Clips.RandomItem();

        private int _currentIndex;
        
        /// <summary>
        /// If this collection should return a random clip or not.
        /// </summary>
        [Tooltip("If this collection should return a random clip or not.")]
        public bool RandomizedCollection;

        /// <summary>
        /// Creates a new audio clip collection with no clips.
        /// </summary>
        public AudioClipCollection()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="randomized"></param>
        /// <param name="clips"></param>
        public AudioClipCollection(bool randomized = false, params AudioClip[] clips)
        {
            Clips = clips.ToList();
            RandomizedCollection = randomized;
        }
        /// <summary>
        /// Returns the next clip in this collection. 
        /// </summary>
        private AudioClip NextClip()
        {
            if (_currentIndex >= Clips.Count)
                _currentIndex = 0;

            return Clips[_currentIndex++];
        }

        /// <summary>
        /// <inheritdoc cref="GetClip()"/>
        /// </summary>
        /// <param name="forceRandom">return a random clip regardless of if this collection is randomized or not</param>
        public AudioClip GetClip(bool forceRandom)
        {
            return forceRandom ? RandomClip : NextClip();
        }
        
        /// <summary>
        /// Returns the next clip in this collection. Returns a random clip if the collection is randomized.
        /// </summary>
        public AudioClip GetClip()
        {
            return GetClip(RandomizedCollection);
        }

        /// <summary>
        /// Adds a clip to this collection
        /// </summary>
        public void AddClip(AudioClip clip)
        {
            Clips.Add(clip);
        }

        /// <summary>
        /// Removes a clip from this collection.
        /// </summary>
        /// <param name="clip"></param>
        public void RemoveClip(AudioClip clip)
        {
            Clips.Remove(clip);
        }

        /// <summary>
        /// Clears all the clips from this collection.
        /// </summary>
        public void ClearCollection()
        {
            Clips.Clear();
        }

        public bool IsEmpty => Clips.Count == 0;
    }
}