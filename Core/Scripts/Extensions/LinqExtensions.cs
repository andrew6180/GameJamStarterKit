using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

// ReSharper disable PossibleMultipleEnumeration

namespace GameJamStarterKit
{
    /// <summary>
    /// Useful extensions for Linq
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// performs action on each item
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="action">the action to perform</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable.IsNullOrEmpty())
                return enumerable;

            foreach (var obj in enumerable)
            {
                action(obj);
            }

            return enumerable;
        }

        /// <summary>
        /// performs action on each item, with a counter
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="action">action to perform</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            if (enumerable.IsNullOrEmpty())
                return enumerable;

            var i = 0;

            foreach (var obj in enumerable)
            {
                action(obj, i);
                ++i;
            }

            return enumerable;
        }

        /// <summary>
        /// returns true if the enumerable is null or has no items.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        /// <summary>
        /// returns a random item using UnityEngine.Random
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">thrown if the enumerable is null or empty</exception>
        public static T RandomItem<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable.IsNullOrEmpty())
                throw new ArgumentOutOfRangeException(nameof(enumerable),
                    $"{nameof(enumerable)} cannot be null or empty");
            return enumerable.ElementAt(Random.Range(0, enumerable.Count()));
        }

        /// <summary>
        /// shuffles this collection using UnityEngine.Random
        /// </summary>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentOutOfRangeException">thrown if the collection is null or empty</exception>
        public static void Shuffle<T>(this ICollection<T> collection)
        {
            if (collection.IsNullOrEmpty())
                throw new ArgumentOutOfRangeException(nameof(collection),
                    $"{nameof(collection)} cannot shuffle null or empty collection");
            var newList = new List<T>();

            while (collection.Count > 0)
            {
                var item = collection.ElementAt(Random.Range(0, collection.Count));
                collection.Remove(item);
                newList.Add(item);
            }

            newList.ForEach(collection.Add);
        }
    }
}