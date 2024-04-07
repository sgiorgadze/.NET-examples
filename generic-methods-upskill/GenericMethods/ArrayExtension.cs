using System.Runtime.CompilerServices;
using GenericMethods.Interfaces;

[assembly: InternalsVisibleTo("GenericMethods.Tests")]

namespace GenericMethods
{
    public static class ArrayExtension
    {
        /// <summary>
        /// Filters a source array based on a predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source array.</typeparam>
        /// <param name="source">The source array.</param>
        /// <param name="predicate">A <see cref="IPredicate{T}"/> to test each element for a condition.</param>
        /// <returns>An array of elements from the source array that satisfy the condition.</returns>
        /// <exception cref="ArgumentNullException">Thrown when source array or predicate is null.</exception>
        /// <exception cref="ArgumentException">Thrown when array is empty.</exception>
        public static TSource[] Filter<TSource>(this TSource[] source, IPredicate<TSource> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (source.Length == 0)
            {
                throw new ArgumentException("Array is empty.");
            }

            List<TSource> filteredList = source
                .Where(item => predicate.IsMatch(item))
                .ToList();

            return filteredList.ToArray();
        }

        /// <summary>
        /// Transforms each element of source array from one type to another type by some rule.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source array.</typeparam>
        /// <typeparam name="TResult">The type of the elements of result array.</typeparam>
        /// <param name="source">The source array.</param>
        /// <param name="transformer">A <see cref="ITransformer{TSource,TResult}"/> that defines the rule of transformation.</param>
        /// <returns>An array, each element of which is transformed.</returns>
        /// <exception cref="ArgumentNullException">Thrown when array or transformer is null.</exception>
        /// <exception cref="ArgumentException">Thrown when array is empty.</exception>
        public static TResult[] Transform<TSource, TResult>(this TSource[] source, ITransformer<TSource, TResult> transformer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (transformer == null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            int length = source.Length;
            if (length == 0)
            {
                throw new ArgumentException("Array is empty.", nameof(source));
            }

            TResult[] result = new TResult[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = transformer.Transform(source[i]);
            }

            return result;
        }

        /// <summary>
        /// Gets the elements of a sequence in ascending order by using a specified comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The source array.</param>
        /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
        /// <returns>An ordered by comparer array.</returns>
        /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
        /// <exception cref="ArgumentException">Thrown when array is empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when comparer is null, and one or more elements
        /// in array do not implement the <see cref="IComparable{T}"/>  interface.</exception>
        public static TSource[] SortBy<TSource>(this TSource[] source, IComparer<TSource> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Array cannot be null.");
            }

            if (source.Length == 0)
            {
                throw new ArgumentException("Array is empty.", nameof(source));
            }

            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer), "Comparer cannot be null.");
            }

            Array.Sort(source, comparer);

            return source;
        }

        /// <summary>
        /// Filters the elements of source array based on a specified type.
        /// </summary>
        /// <typeparam name="TResult">Type selector to return.</typeparam>
        /// <param name="source">The source array.</param>
        /// <returns>A array that contains the elements from source that have type TResult.</returns>
        /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
        /// <exception cref="ArgumentException">Thrown when array length equal to zero.</exception>
        public static TResult[] TypeOf<TResult>(this object[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Array cannot be null.");
            }

            if (source.Length == 0)
            {
                throw new ArgumentException("Array is empty.", nameof(source));
            }

            List<TResult> resultList = new List<TResult>();

            foreach (object item in source)
            {
                if (item is TResult resultItem)
                {
                    resultList.Add(resultItem);
                }
            }

            return resultList.ToArray();
        }

        /// <summary>
        /// Inverts the order of the elements in a array.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of array.</typeparam>
        /// <param name="source">A array of elements to reverse.</param>
        /// <returns>Revers array.</returns>
        /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
        /// <exception cref="ArgumentException">Thrown when array length equal to zero.</exception>
        public static TSource[] Reverse<TSource>(this TSource[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Array cannot be null.");
            }

            if (source.Length == 0)
            {
                throw new ArgumentException("Array is empty.", nameof(source));
            }

            TSource[] reversedArray = new TSource[source.Length];

            for (int i = 0, j = source.Length - 1; i < source.Length; i++, j--)
            {
                reversedArray[i] = source[j];
            }

            return reversedArray;
        }

        /// <summary>
        /// Swaps two objects.
        /// </summary>
        /// <typeparam name="T">The type of parameters.</typeparam>
        /// <param name="left">First object.</param>
        /// <param name="right">Second object.</param>
        internal static void Swap<T>(ref T left, ref T right)
        {
            T temp = left;
            left = right;
            right = temp;
        }
    }
}
