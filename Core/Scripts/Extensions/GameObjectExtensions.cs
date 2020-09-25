using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameJamStarterKit
{
    /// <summary>
    /// Useful extensions for GameObject
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// returns Object.Instantiate for a random item with the given parent.
        /// </summary>
        /// <param name="objects">this</param>
        /// <param name="parent">parent for the instantiated item</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T InstantiateRandomItem<T>(this IEnumerable<T> objects, Transform parent)
            where T : Object
        {
            return Object.Instantiate(objects.RandomItem(), parent);
        }

        /// <summary>
        /// returns Object.Instantiate for a random item with the given parent in world position or not.
        /// </summary>
        /// <param name="objects">this</param>
        /// <param name="parent">parent for the instantiated item</param>
        /// <param name="inWorldPosition">spawn in world position or not</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T InstantiateRandomItem<T>(this IEnumerable<T> objects, Transform parent, bool inWorldPosition)
            where T : Object
        {
            return Object.Instantiate(objects.RandomItem(), parent, inWorldPosition);
        }

        /// <summary>
        /// returns Object.Instantiate for a random item with the given position
        /// </summary>
        /// <param name="objects">this</param>
        /// <param name="position">position to instantiate the item at</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T InstantiateRandomItem<T>(this IEnumerable<T> objects, Vector3 position)
            where T : Object
        {
            var item = objects.RandomItem();
            var go = item as GameObject;
            var rotation = go != null ? go.transform.rotation : Quaternion.identity;
            return Object.Instantiate(item, position, rotation);
        }

        /// <summary>
        /// returns Object.Instantiate for a random item with the given position and rotation
        /// </summary>
        /// <param name="objects">this</param>
        /// <param name="position">position to instantiate the item at</param>
        /// <param name="rotation">rotation for the item</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T InstantiateRandomItem<T>(this IEnumerable<T> objects, Vector3 position, Quaternion rotation)
            where T : Object
        {
            return Object.Instantiate(objects.RandomItem(), position, rotation);
        }

        /// <summary>
        /// returns Object.Instantiate for a random item with the given position and rotation
        /// </summary>
        /// <param name="objects">this</param>
        /// <param name="position">position to instantiate the item at</param>
        /// <param name="rotation">rotation for the item</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T InstantiateRandomItem<T>(this IEnumerable<T> objects,
            Vector3 position,
            Transform parent,
            Quaternion rotation) where T : Object
        {
            return Object.Instantiate(objects.RandomItem(), position, rotation, parent);
        }

        /// <summary>
        /// <inheritdoc cref="TransformExtensions.FindTypeInRadius{T}(Transform, float)"/>
        /// </summary>
        /// <param name="gameObject">this</param>
        /// <param name="radius">radius to search in</param>
        /// <typeparam name="T">Type to look for</typeparam>
        /// <returns>true if the type was found in the radius</returns>
        public static List<T> FindTypeInRadius<T>(this GameObject gameObject, float radius) where T : Object
        {
            return gameObject.transform.FindTypeInRadius<T>(radius);
        }

        /// <summary>
        /// <inheritdoc cref="TransformExtensions.FindTypeInRadiusNoCollider{T}(Transform, float)"/>
        /// </summary>
        /// <param name="gameObject">this</param>
        /// <param name="radius">radius to search in</param>
        /// <typeparam name="T">Type to look for</typeparam>
        /// <returns>true if the type was found in the radius</returns>
        public static List<T> FindTypeInRadiusNoCollider<T>(this GameObject gameObject, float radius)
            where T : Component
        {
            return gameObject.transform.FindTypeInRadiusNoCollider<T>(radius);
        }

        /// <summary>
        /// returns true if this GameObject has a component with the given type
        /// </summary>
        /// <param name="gameObject">this</param>
        /// <typeparam name="T">type of component</typeparam>
        /// <returns>true if this has a component with the type</returns>
        public static bool HasComponent<T>(this GameObject gameObject)
        {
            return gameObject.GetComponent<T>() != null;
        }

        /// <summary>
        /// <inheritdoc cref="TransformExtensions.Distance(Transform, Transform)"/>
        /// </summary>
        /// <param name="gameObject">this</param>
        /// <param name="target">game object to get the distance to</param>
        /// <returns>the distance between this and the target</returns>
        public static float Distance(this GameObject gameObject, GameObject target)
        {
            return gameObject.transform.Distance(target.transform);
        }

        /// <summary>
        /// <inheritdoc cref="TransformExtensions.Distance(Transform, Transform)"/>
        /// </summary>
        /// <param name="gameObject">this</param>
        /// <param name="target">transform to get the distance to</param>
        /// <returns>the distance between this and the target</returns>
        public static float Distance(this GameObject gameObject, Transform target)
        {
            return gameObject.transform.Distance(target);
        }

        /// <summary>
        /// Gets or adds a component of the given type
        /// </summary>
        /// <param name="gameObject">this</param>
        /// <typeparam name="T">component type to get or add</typeparam>
        /// <returns>the component gotten or added</returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }

        /// <summary>
        /// Gets or adds a component of the given type
        /// </summary>
        /// <param name="gameObject">this game object</param>
        /// <param name="type">component type ot get or add</param>
        /// <returns>the component gotten or added</returns>
        public static Component GetOrAddComponent(this GameObject gameObject, Type type)
        {
            var component = gameObject.GetComponent(type);
            if (component == null)
            {
                component = gameObject.AddComponent(type);
            }

            return component;
        }

        /// <summary>
        /// Gets the component anywhere in this objects hierarchy, searching from the root parent first.
        /// </summary>
        /// <param name="gameObject">this game object</param>
        /// <typeparam name="T">component type to look for</typeparam>
        /// <returns>the component in this objects hierarchy</returns>
        public static T GetComponentInEntireObject<T>(this GameObject gameObject)
        {
            return gameObject.transform.GetComponentInChildren<T>();
        }

        /// <summary>
        /// Gets the component anywhere in this objects hierarchy, searching from the root parent first.
        /// </summary>
        /// <param name="gameObject">this game object</param>
        /// <param name="type">component type to look for</param>
        /// <returns>the component in this objects hierarchy</returns>
        public static Component GetComponentInEntireObject(this GameObject gameObject, Type type)
        {
            return gameObject.transform.GetComponentInChildren(type);
        }

        /// <summary>
        /// <inheritdoc cref="TransformExtensions.GetPath(Transform, string)"/>
        /// </summary>
        /// <param name="gameObject">this</param>
        /// <param name="separator">string to separate the parent and child. Ex: Parent/Child/Child2</param>
        /// <returns>the path to this game object</returns>
        public static string GetPath(this GameObject gameObject, string separator = "/")
        {
            return gameObject.transform.GetPath(separator);
        }

        /// <summary>
        /// Gets the direction to another game object.
        /// </summary>
        /// <param name="gameObject">this</param>
        /// <param name="other">the game object to point towards</param>
        /// <returns>returns a normalized direction pointing towards <see cref="other"/></returns>
        public static Vector3 DirectionTo(this GameObject gameObject, GameObject other)
        {
            return gameObject.DirectionTo(other.transform.position);
        }

        /// <summary>
        /// Gets the direction to another transform
        /// </summary>
        /// <param name="gameObject">this</param>
        /// <param name="other">the transform to point towards</param>
        /// <returns>returns a normalized direction pointing towards <see cref="other"/></returns>
        public static Vector3 DirectionTo(this GameObject gameObject, Transform other)
        {
            return gameObject.DirectionTo(other.position);
        }

        /// <summary>
        /// Gets the direction to a vector3
        /// </summary>
        /// <param name="gameObject">this</param>
        /// <param name="other">the position to point towards</param>
        /// <returns>returns a normalized direction pointing towards <see cref="other"/></returns>
        public static Vector3 DirectionTo(this GameObject gameObject, Vector3 other)
        {
            return gameObject.transform.position.DirectionTo(other);
        }

        /// <summary>
        ///  Check if this game object's position is visible to the camera given
        ///  <para>consider <seealso cref="RendererExtensions.IsVisibleTo"/>
        /// if this object has a RendererComponent to check against this objects bounds instead</para> 
        /// </summary>
        /// <param name="gameObject">the game object to check</param>
        /// <param name="cam">the camera to test</param>
        /// <returns>returns true if the object is visible to the camera</returns>
        public static bool IsVisibleTo(this GameObject gameObject, Camera cam)
        {
            var pos = cam.WorldToViewportPoint(gameObject.transform.position);
            return (pos.x.IsBetweenInclusive(0, 1) && pos.y.IsBetweenInclusive(0, 1) && pos.z > 0);
        }

        /// <summary>
        /// Moves the children of this GameObject to the target GameObject
        /// </summary>
        /// <param name="from">the GameObject to move from</param>
        /// <param name="to">the GameObject to move to</param>
        public static void MoveChildrenTo(this GameObject from, GameObject to)
        {
            var count = from.transform.childCount;
            var children = new Transform[count];
            for (var i = 0; i < count; ++i)
            {
                children[i] = from.transform.GetChild(i);
            }

            for (var i = 0; i < count; ++i)
            {
                children[i].SetParent(to.transform);
            }
        }
    }
}