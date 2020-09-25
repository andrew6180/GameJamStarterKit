using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameJamStarterKit.FXSystem
{
    /// <summary>
    /// Spawns FX Units
    /// </summary>
    public class FXSpawner : MonoBehaviour
    {
        public List<FXSpawnerItem> Units = null;

        private Dictionary<string, FXUnit> _trackedUnits = new Dictionary<string, FXUnit>();

        /// <summary>
        /// Spawns the FXUnit for the given key. Does not retain the key, and will not check if it's retained already.
        /// </summary>
        /// <param name="key">key to spawn</param>
        public FXUnit SpawnAndForget(string key)
        {
            var item = GetFXSpawnerItem(key);
            if (item == null)
                return null;

            switch (item.Unit.SpawnType)
            {
                case SpawnType.AttachedToParent:
                    return InternalSpawnAttached(item);

                default:
                case SpawnType.WorldLocation:
                    return InternalSpawnPosition(item);
            }
        }

        /// <summary>
        /// Returns the spawner item for this key
        /// </summary>
        /// <param name="key">key to lookup</param>
        /// <returns></returns>
        private FXSpawnerItem GetFXSpawnerItem(string key)
        {
            return Units.FirstOrDefault(fx => fx.Key == key);
        }

        /// <summary>
        /// Spawns the FXUnit for the given key. Retains the key. Returns the retained key if already retained. 
        /// </summary>
        /// <param name="key">key to spawn and retain</param>
        public FXUnit SpawnAndRetain(string key)
        {
            if (IsRetained(key))
            {
                return GetRetainedUnit(key);
            }

            var item = GetFXSpawnerItem(key);
            if (item == null)
                return null;

            if (item.Unit == null)
            {
                Debug.LogWarning("[" + name + ":FXSpawner] Key: " + key + " has no associated FXUnit prefab.");
                return null;
            }

            FXUnit unit;
            switch (item.Unit.SpawnType)
            {
                case SpawnType.AttachedToParent:
                    unit = InternalSpawnAttached(item);
                    break;

                default:
                case SpawnType.WorldLocation:
                    unit = InternalSpawnPosition(item);
                    break;
            }

            Retain(key, unit);
            return unit;
        }

        /// <summary>
        /// Calls despawn on the FXUnit retained with the key and releases the retainer on the key.
        /// <para>does nothing if the key is not retained.</para>
        /// </summary>
        /// <param name="key">Key to check for</param>
        public void DespawnAndRelease(string key)
        {
            if (!IsRetained(key))
                return;

            var unit = GetRetainedUnit(key);

            unit.Despawn();
            Release(key);
        }

        /// <summary>
        /// Spawns the FXUnit for the given key. Retains the key. Returns the retained key if already retained. 
        /// <para>waits a single frame before spawning the FXUnit.</para>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerator Animation_SpawnAndRetain(string key)
        {
            yield return null;
            SpawnAndRetain(key);
        }


        /// <summary>
        /// Spawns the FXUnit for the given key. Does not retain the key, and will not check if it's retained already.
        /// <para>waits a single frame before spawning the FXUnit</para>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerator<FXUnit> Animation_SpawnAndForget(string key)
        {
            yield return null;
            SpawnAndForget(key);
        }

        /// <summary>
        /// Retains the given unit if not already retained.
        /// </summary>
        /// <param name="key">key to retain</param>
        /// <param name="unit">unit associated with the key</param>
        public void Retain(string key, FXUnit unit)
        {
            if (!IsRetained(key))
            {
                _trackedUnits[key] = unit;
                unit.OnDestroyed.AddListener(() => Release(key));
            }
        }

        /// <summary>
        /// Releases the retainer on the key given.
        /// </summary>
        /// <param name="key">key to release</param>
        public void Release(string key)
        {
            if (IsRetained(key))
                _trackedUnits.Remove(key);
        }

        /// <summary>
        /// returns if a key is actively retained or not.
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns></returns>
        public bool IsRetained(string key)
        {
            return _trackedUnits.ContainsKey(key);
        }

        /// <summary>
        /// Gets the retained FXUnit for the given key. returns null if the key isn't retained.
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns></returns>
        public FXUnit GetRetainedUnit(string key)
        {
            _trackedUnits.TryGetValue(key, out var unit);
            return unit;
        }

        private FXUnit InternalSpawnPosition(FXSpawnerItem item)
        {
            var position = item.Parent == null ? transform.position : item.Parent.position;
            var fxUnit = Instantiate(item.Unit, position, item.Unit.transform.localRotation);
            var fxUnitTransform = fxUnit.transform;

            switch (item.Unit.SpawnScale)
            {
                case SpawnScale.IgnoreRelative:
                    fxUnitTransform.localScale = Vector3.one;
                    break;
                default:
                case SpawnScale.KeepRelative:
                    break;
            }

            return fxUnit;
        }

        private FXUnit InternalSpawnAttached(FXSpawnerItem item)
        {
            var parent = item.Parent == null ? transform : item.Parent;
            var fxUnit = Instantiate(item.Unit, parent, false);
            var fxUnitTransform = fxUnit.transform;

            switch (item.Unit.SpawnScale)
            {
                case SpawnScale.IgnoreRelative:
                    fxUnitTransform.localScale = Vector3.one;
                    break;
                default:
                case SpawnScale.KeepRelative:
                    break;
            }

            return fxUnit;
        }
    }

    /// <summary>
    /// An Item entry for an FX Spawner
    /// </summary>
    [Serializable]
    public class FXSpawnerItem
    {
        /// <summary>
        /// Key of the FX Unit
        /// </summary>
        public string Key;
        
        /// <summary>
        /// The FX Unit
        /// </summary>
        public FXUnit Unit;
        
        /// <summary>
        /// The transform to attach this FX Unit to
        /// </summary>
        public Transform Parent;
    }
}