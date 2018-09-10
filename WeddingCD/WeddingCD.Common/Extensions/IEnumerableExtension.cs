using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.Extensions
{
    /// <summary>
    /// Extension class for the IEnumerable (generic version) class.
    /// </summary>
    public static class IEnumerableExtension
    {
        /// <summary>
        /// Queries the element from the source that are distinct, depending on a specific property (or multiple properties).
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <returns>The filtered enumerable</returns>
        /// <remarks>
        /// Usages:
        /// var query = people.DistinctBy(p => p.Id);
        /// var query = people.DistinctBy(p => new { p.Id, p.Name });
        /// </remarks>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Shuffles the specified IEnumerable source.
        /// </summary>
        /// <typeparam name="T">The type of the source</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="rng">The random number generator.</param>
        /// <returns>The shuffled enumeration</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            T[] elements = source.ToArray();
            for (int i = elements.Length - 1; i >= 0; i--)
            {
                // Swap element "i" with a random earlier element it (or itself)
                // ... except we don't really need to swap it fully, as we can
                // return it immediately, and afterwards it's irrelevant.
                int swapIndex = rng.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }
    }
}