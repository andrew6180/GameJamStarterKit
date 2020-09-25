using UnityEngine;
using UnityEngine.Events;

namespace GameJamStarterKit.FXSystem
{
    /// <summary>
    /// Represents an FX GameObject for use with an FX Spawner.
    /// </summary>
    public class FXUnit : MonoBehaviour
    {
        /// <summary>
        /// How this FX Unit leaves the world
        /// </summary>
        [Tooltip("How this FX Unit leaves the world")]
        public DespawnType DespawnType = DespawnType.Timeout;

        /// <summary>
        /// If using a timeout, how long until this FX Unit despawns in seconds
        /// </summary>
        [Tooltip("How long until this FX Unit despawns in seconds")]
        public float DespawnTimeout = 5f;
        
        /// <summary>
        /// if using a timeout range, minimum time it takes until this FX Unit despawns in seconds.
        /// </summary>
        [Tooltip("Minimum time it takes until this FX Unit despawns in seconds.")]
        public float DespawnTimeoutMinimum = 1f;
        
        /// <summary>
        /// if using a timeout range, maximum time it takes until this FX Unit despawns in seconds.
        /// </summary>
        [Tooltip("Maximum time it takes until this FX Unit despawns in seconds.")]
        public float DespawnTimeoutMaximum = 5f;
        
        /// <summary>
        /// How to scale this fx unit when spawning it
        /// </summary>
        [Tooltip("How to scale this FX Unit when spawning")]
        public SpawnScale SpawnScale;
        
        /// <summary>
        /// How this FX unit spawns in the world
        /// </summary>
        [Tooltip("How this FX Unit spawns in the world")]
        public SpawnType SpawnType;

        [HideInInspector]
        public UnityEvent OnDestroyed;

        private TimeSince _timeSinceSpawned;

        protected virtual void Start()
        {
            _timeSinceSpawned = 0f;

            if (DespawnType == DespawnType.TimeoutRange)
            {
                DespawnTimeout = Random.Range(DespawnTimeoutMinimum, DespawnTimeoutMaximum);
            }
        }

        protected virtual void Update()
        {
            if (DespawnType != DespawnType.Timeout && DespawnType != DespawnType.TimeoutRange)
                return;

            if (_timeSinceSpawned > DespawnTimeout)
            {
                Despawn();
            }
        }

        /// <summary>
        /// Despawns this FX Unit immediately
        /// </summary>
        public virtual void Despawn()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            OnDestroyed.Invoke();
        }

        /// <summary>
        /// Despawns this FX unit immediately
        /// </summary>
        public virtual void Animation_Despawn()
        {
            Despawn();
        }

        /// <summary>
        /// Detaches this FXUnit from its parent, keeping it's world position.
        /// </summary>
        public void Detach()
        {
            if (transform.parent != null)
            {
                transform.SetParent(null, true);
            }
        }
    }
}