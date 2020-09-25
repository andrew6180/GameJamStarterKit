using UnityEngine;

namespace GameJamStarterKit
{
    /// <summary>
    /// Useful Collider extensions
    /// </summary>
    public static class ColliderExtensions
    {
        /// <summary>
        /// <inheritdoc cref="GameObjectExtensions.HasComponent{T}"/>
        /// </summary>
        /// <param name="collider">this</param>
        /// <typeparam name="T">component type</typeparam>
        /// <returns>true if this has the component</returns>
        public static bool HasComponent<T>(this Collider collider)
        {
            return collider.gameObject.HasComponent<T>();
        }
    }

    /// <summary>
    /// Useful Collider2D Extensions
    /// </summary>
    public static class Collider2DExtnesions
    {
        /// <summary>
        /// <inheritdoc cref="GameObjectExtensions.HasComponent{T}"/>
        /// </summary>
        /// <param name="collider">this</param>
        /// <typeparam name="T">Component type</typeparam>
        /// <returns>true if this has the component</returns>
        public static bool HasComponent<T>(this Collider2D collider)
        {
            return collider.gameObject.HasComponent<T>();
        }

        /// <summary>
        /// Sets the size of a Collider2D to the Vector2 given.
        /// <remarks>CircleCollider2D will change the radius to Max of size.x and size.y.</remarks>
        /// <remarks>If the Collider2D is a Polygon or an Edge collider, a warning will be logged unless disabled.</remarks>
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="size">new size of the collider</param>
        /// <param name="disableWarning">disable warning message when called on Polygon or Edge colliders</param>
        public static void SetSize(this Collider2D collider, Vector2 size, bool disableWarning = false)
        {
            var box = collider as BoxCollider2D;
            if (box != null)
            {
                box.size = size;
                return;
            }

            var capsule = collider as CapsuleCollider2D;
            if (capsule != null)
            {
                capsule.size = size;
                return;
            }

            var circle = collider as CircleCollider2D;
            if (circle != null)
            {
                circle.radius = Mathf.Max(size.x, size.y);
                return;
            }

            if (!disableWarning)
                Debug.LogWarning("Tried to call SetSize on a Polygon or Edge Collider2D.");
        }

        /// <summary>
        /// Sets the height of the Collider2D to the value given.
        /// <remarks>CircleCollider2D will change the radius to the height given.</remarks>
        /// <remarks>If the Collider2D is a Polygon or an Edge collider, a warning will be logged unless disabled.</remarks>
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="height">new height of the collider</param>
        /// <param name="disableWarning">disable warning message when called on Polygon or Edge colliders</param>
        public static void SetHeight(this Collider2D collider, float height, bool disableWarning = false)
        {
            var box = collider as BoxCollider2D;
            if (box != null)
            {
                box.size = box.size.WithY(height);
                return;
            }

            var capsule = collider as CapsuleCollider2D;
            if (capsule != null)
            {
                capsule.size = capsule.size.WithY(height);
                return;
            }

            var circle = collider as CircleCollider2D;
            if (circle != null)
            {
                circle.radius = height;
                return;
            }

            if (!disableWarning)
                Debug.LogWarning("Tried to call SetHeight on a Polygon or Edge Collider2D.");
        }

        /// <summary>
        /// Sets the width of the Collider2D to the value given.
        /// <remarks>CircleCollider2D will change the radius to the width given.</remarks>
        /// <remarks>If the Collider2D is a Polygon or an Edge collider, a warning will be logged unless disabled.</remarks>
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="width">new width of the collider</param>
        /// <param name="disableWarning">disable warning message when called on Polygon or Edge colliders</param>
        public static void SetWidth(this Collider2D collider, float width, bool disableWarning = false)
        {
            var box = collider as BoxCollider2D;
            if (box != null)
            {
                box.size = box.size.WithX(width);
                return;
            }

            var capsule = collider as CapsuleCollider2D;
            if (capsule != null)
            {
                capsule.size = capsule.size.WithX(width);
                return;
            }

            var circle = collider as CircleCollider2D;
            if (circle != null)
            {
                circle.radius = width;
                return;
            }

            if (!disableWarning)
                Debug.LogWarning("Tried to call SetWidth on a Polygon or Edge collider.");
        }


        /// <summary>
        /// returns the size of this collider.
        /// <remarks>if the Collider2D has no size or radius, this returns the bounds of the collider.</remarks>
        /// </summary>
        /// <param name="collider"></param>
        /// <returns>returns the size or bounds of this collider</returns>
        public static Vector2 GetSize(this Collider2D collider)
        {
            var box = collider as BoxCollider2D;
            if (box != null)
            {
                return box.size;
            }

            var capsule = collider as CapsuleCollider2D;
            if (capsule != null)
            {
                return capsule.size;
            }

            var circle = collider as CircleCollider2D;
            if (circle != null)
            {
                return Vector2.one * circle.radius;
            }

            return collider.bounds.extents * 2f;
        }
    }
}