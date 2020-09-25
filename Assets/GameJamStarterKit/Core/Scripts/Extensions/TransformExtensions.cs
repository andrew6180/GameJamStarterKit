using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameJamStarterKit
{
    /// <summary>
    /// Useful Transform Extensions
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// returns the distance between this and target transform
        /// </summary>
        /// <param name="transform">this</param>
        /// <param name="target">target transform</param>
        /// <returns>The distance between this and the target</returns>
        public static float Distance(this Transform transform, Transform target)
        {
            return Distance(transform, target.position);
        }


        /// <summary>
        /// returns the distance between this and target vector3
        /// </summary>
        /// <param name="transform">this</param>
        /// <param name="target">target position</param>
        /// <returns>the distance between this and the target</returns>
        public static float Distance(this Transform transform, Vector3 target)
        {
            return Vector3.Distance(transform.position, target);
        }

        /// <summary>
        /// coroutine to move transform a localPosition to b using x units per second.
        /// </summary>
        /// <param name="transform">this</param>
        /// <param name="target">target position</param>
        /// <param name="unitsPerSecond">units per second</param>
        public static IEnumerator LinearMoveLocalTo(this Transform transform, Vector3 target, float unitsPerSecond)
        {
            while (transform.localPosition != target)
            {
                transform.localPosition =
                    Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime * unitsPerSecond);
                yield return null;
            }
        }

        /// <summary>
        /// coroutine to move transform a position to b using x units per second.
        /// </summary>
        /// <param name="transform">this</param>
        /// <param name="target">target position</param>
        /// <param name="unitsPerSecond">units per second</param>
        public static IEnumerator LinearMoveTo(this Transform transform, Vector3 target, float unitsPerSecond)
        {
            while (transform.position != target)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * unitsPerSecond);
                yield return null;
            }
        }
        
        /// <summary>
        /// Gets the component anywhere in this objects hierarchy, searching from the root parent first.
        /// </summary>
        /// <param name="transform">this transform</param>
        /// <typeparam name="T">component type to look for</typeparam>
        /// <returns>the component in this objects hierarchy</returns>
        public static T GetComponentInEntireObject<T>(this Transform transform)
        {
            var root = transform.root;
            return root.GetComponentInChildren<T>();
        }

        /// <summary>
        /// Gets the component anywhere in this objects hierarchy, searching from the root parent first.
        /// </summary>
        /// <param name="transform">this transform</param>
        /// <param name="type">component type to look for</param>
        /// <returns>the component in this objects hierarchy</returns>
        public static Component GetComponentInEntireObject(this Transform transform, Type type)
        {
            var root = transform.root;
            return root.GetComponentInChildren(type);
        }

        /// <summary>
        /// sets this transforms x position
        /// </summary>
        /// <param name="transform">this</param>
        /// <param name="x">x value</param>
        public static void SetX(this Transform transform, float x)
        {
            transform.position = transform.position.WithX(x);
        }

        /// <summary>
        /// sets this transforms y position
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="y"></param>
        public static void SetY(this Transform transform, float y)
        {
            transform.position = transform.position.WithY(y);
        }

        /// <summary>
        /// sets this transforms z position
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="z"></param>
        public static void SetZ(this Transform transform, float z)
        {
            transform.position = transform.position.WithZ(z);
        }

        /// <summary>
        /// Sets this transforms local x position
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        public static void SetLocalX(this Transform transform, float x)
        {
            transform.localPosition = transform.localPosition.WithX(x);
        }

        /// <summary>
        /// sets this transforms local y position 
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="y"></param>
        public static void SetLocalY(this Transform transform, float y)
        {
            transform.localPosition = transform.localPosition.WithY(y);
        }

        /// <summary>
        /// sets this transforms local z position
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="z"></param>
        public static void SetLocalZ(this Transform transform, float z)
        {
            transform.localPosition = transform.localPosition.WithZ(z);
        }

        /// <summary>
        /// adds v to this transforms local position
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="v">Vector to add</param>
        public static void AddLocalPosition(this Transform transform, Vector3 v)
        {
            var pos = transform.localPosition;
            pos += v;
            transform.localPosition = pos;
        }

        /// <summary>
        /// adds v to this transforms position
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="v">Vector to add</param>
        public static void AddPosition(this Transform transform, Vector3 v)
        {
            var pos = transform.position;
            pos += v;
            transform.position = pos;
        }

        /// <summary>
        /// adds x to this transforms position.x
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x">value to add</param>
        public static void AddX(this Transform transform, float x)
        {
            var pos = transform.localPosition;
            pos.x += x;
            transform.localPosition = pos;
        }

        /// <summary>
        /// adds y to this transforms position.y
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="y">value to add</param>
        public static void AddY(this Transform transform, float y)
        {
            var pos = transform.localPosition;
            pos.y += y;
            transform.localPosition = pos;
        }

        /// <summary>
        /// adds z to this transforms position.z
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="z">value to add</param>
        public static void AddZ(this Transform transform, float z)
        {
            var pos = transform.localPosition;
            pos.x += z;
            transform.localPosition = pos;
        }

        /// <summary>
        /// adds x to this transforms localPosition.x
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x">value to add</param>
        public static void AddLocalX(this Transform transform, float x)
        {
            var pos = transform.localPosition;
            pos.x += x;
            transform.localPosition = pos;
        }

        /// <summary>
        /// adds y to this transforms localPosition.y
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="y">value to add</param>
        public static void AddLocalY(this Transform transform, float y)
        {
            var pos = transform.localPosition;
            pos.y += y;
            transform.localPosition = pos;
        }

        /// <summary>
        /// adds z to this transforms localPosition.z
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="z">value to add</param>
        public static void AddLocalZ(this Transform transform, float z)
        {
            var pos = transform.localPosition;
            pos.x += z;
            transform.localPosition = pos;
        }

        /// <summary>
        /// adds to this transforms rotation with the given euler angle rotation
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="rotation">euler rotation to add</param>
        public static void AddRotation(this Transform transform, Vector3 rotation)
        {
            AddRotation(transform, rotation.ToQuaternion());
        }

        /// <summary>
        /// adds to this transforms rotation with the given quaternion
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="rotation">rotation to add</param>
        public static void AddRotation(this Transform transform, Quaternion rotation)
        {
            var rot = transform.localRotation;
            rot *= rotation;
            transform.localRotation = rot;
        }

        /// <summary>
        /// sets this transforms rotation to the given euler angle rotation
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="rotation"></param>
        public static void SetRotation(this Transform transform, Vector3 rotation)
        {
            transform.rotation = rotation.ToQuaternion();
        }

        /// <summary>
        /// Rotates the X axis of this transform by the amount given
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="amount">amount to rotate by</param>
        public static void RotateX(this Transform transform, float amount)
        {
            transform.rotation *= Quaternion.Euler(amount, 0, 0);
        }

        /// <summary>
        /// Rotates the Y axis of this transform by the amount given
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="amount">amount to rotate by</param>
        public static void RotateY(this Transform transform, float amount)
        {
            transform.rotation *= Quaternion.Euler(0, amount, 0);
        }

        /// <summary>
        /// Rotates the Z axis of this transform by the amount given
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="amount">amount to rotate by</param>
        public static void RotateZ(this Transform transform, float amount)
        {
            transform.rotation *= Quaternion.Euler(0, 0, amount);
        }

        /// <summary>
        /// returns a list of the given type inside the given radius around this transform
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="radius">radius to check for type in</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> FindTypeInRadius<T>(this Transform transform, float radius) where T : Object
        {
            var results = Physics.OverlapSphere(transform.position, radius, Physics.AllLayers,
                QueryTriggerInteraction.Ignore);

            return results
                .Where(collider => collider.HasComponent<T>())
                .Select(collider => collider.GetComponent<T>())
                .ToList();
        }

        /// <summary>
        /// returns a list of the given type inside the given radius around this transform without using Physics.
        /// <para><b>This uses FindObjectsOfType and is an expensive method. Avoid using in Update</b></para>
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="radius"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> FindTypeInRadiusNoCollider<T>(this Transform transform, float radius) where T : Component
        {
            var objects = Object.FindObjectsOfType<T>();

            return objects.Where(obj => obj.gameObject.Distance(transform) <= radius).ToList();
        }

        /// <summary>
        /// Returns the path to this transform's game object with an optional separator string
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="separator">string to separate the parent and child. Ex: Parent/Child/Child2</param>
        /// <returns></returns>
        public static string GetPath(this Transform transform, string separator = "/")
        {
            var path = transform.name;
            while (transform.parent != null)
            {
                transform = transform.parent;
                path = transform.name + separator + path;
            }

            return path;
        }

        /// <summary>
        /// Gets the direction to a transform
        /// </summary>
        /// <param name="transform">this</param>
        /// <param name="other">the transform to point towards</param>
        /// <returns>returns a normalized direction pointing towards <see cref="other"/></returns>
        public static Vector3 DirectionTo(this Transform transform, Transform other)
        {
            return transform.DirectionTo(other.position);
        }
        
        /// <summary>
        /// Gets the direction to a game object
        /// </summary>
        /// <param name="transform">this</param>
        /// <param name="other">the game object to point towards</param>
        /// <returns>returns a normalized direction pointing towards <see cref="other"/></returns>
        public static Vector3 DirectionTo(this Transform transform, GameObject other)
        {
            return transform.DirectionTo(other.transform.position);
        }

        /// <summary>
        /// Gets the direction to a position
        /// </summary>
        /// <param name="transform">this</param>
        /// <param name="other">the position to point towards</param>
        /// <returns>returns a normalized direction pointing towards <see cref="other"/></returns>
        public static Vector3 DirectionTo(this Transform transform, Vector3 other)
        {
            return transform.position.DirectionTo(other);
        }
    }
}