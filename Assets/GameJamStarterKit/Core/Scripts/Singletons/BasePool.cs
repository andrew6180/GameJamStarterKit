using System.Collections.Generic;
using UnityEngine;

namespace GameJamStarterKit
{
    /// <summary>
    /// Starting point to creating a pool of objects. See GameJamStarterKit.Audio.AudioPool for an example usage.
    /// </summary>
    /// <typeparam name="T">type of this pool</typeparam>
    public abstract class BasePool<T> : MonoBehaviour where T : Component
    {
        /// <summary>
        /// How many GameObjects this pool has
        /// </summary>
        public int PoolSize = 32;

        private readonly Queue<T> _pool = new Queue<T>();

        /// <summary>
        /// Gets the next object in the pool.
        /// <remarks>Remember to call SetActive(true) on the gameObject.</remarks>
        /// </summary>
        /// <returns></returns>
        public T GetNext()
        {
            if (_pool.Count == 0)
            {
                RegeneratePool();
            }

            var value = _pool.Dequeue();
            _pool.Enqueue(value);
            return value;
        }

        /// <summary>
        /// Destroys every item in the pool and re-creates them.
        /// </summary>
        public void RegeneratePool()
        {
            while (_pool.Count > 0)
            {
                var item = _pool.Dequeue();
                Destroy(item.gameObject);
            }
            
            for (var i = 0; i < PoolSize; ++i)
            {
                AddPoolItem();
            }
        }
        
        private void AddPoolItem()
        {
            var o = new GameObject("[Pool] Pool Item (" + (_pool.Count + 1) + ")");
            o.transform.SetParent(transform, false);
            var component = (T) o.AddComponent(typeof(T));
            InitializeComponent(component);
            _pool.Enqueue(component);
            o.SetActive(false);
        }

        protected abstract void InitializeComponent(T component);
    }
}