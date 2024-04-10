using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;

namespace JosephusProblem
{
    /// <summary>
    /// Providing functionality for the Josephus Flavius problem.
    /// <see>You can find more details on this problem in Wikipedia - https://en.wikipedia.org/wiki/Josephus_problem</see>.
    /// </summary>
    public static class JosephusFlavius
    {
        /// <summary>
        /// Returns the iterator that generates a list of persons that are crossed out.
        /// </summary>
        /// <param name="count">Count of the persons in circle.</param>
        /// <param name="crossedOut">The number of the person to be crossed out.</param>
        /// <returns>Returns the iterator that generates a list of persons that are crossed out.</returns>
        /// <exception cref="ArgumentException"><paramref name="count"/>is less than 1.</exception>
        /// <exception cref="ArgumentException"><paramref name="crossedOut"/> is less than 1.</exception>
        public static IEnumerable<int> GetCrossedOutPersons(int count, int crossedOut)
        {
            HandleException(count, crossedOut);

            return GetCrossedOutPersonsInternal(count, crossedOut);

            static IEnumerable<int> GetCrossedOutPersonsInternal(int count, int crossedOut)
            {
                List<int> persons = new List<int>(Enumerable.Range(1, count));

                int currentIndex = 0;
                while (persons.Count > 1)
                {
                    currentIndex = (currentIndex + crossedOut - 1) % persons.Count;
                    yield return persons[currentIndex];
                    persons.RemoveAt(currentIndex);
                }
            }
        }

        /// <summary>
        /// Returns order number of survivor.
        /// </summary>
        /// <param name="count">Count of the persons in circle.</param>
        /// <param name="crossedOut">The number of the person to be crossed out.</param>
        /// <returns>The order number of the last survivor.</returns>
        /// <exception cref="ArgumentException"><paramref name="count"/>is less than 1.</exception>
        /// <exception cref="ArgumentException"><paramref name="crossedOut"/> is less than 1.</exception>
        public static int GetSurvivor(int count, int crossedOut)
        {
            HandleException(count, crossedOut);
            int survivor = 0;
            for (int i = 2; i <= count; i++)
            {
                survivor = (survivor + crossedOut) % i;
            }

            return survivor + 1;
        }

        private static void HandleException(int count, int crossedOut)
        {
            if (count < 1)
            {
                throw new ArgumentException("Count must be greater than or equal to 1.", nameof(count));
            }

            if (crossedOut < 1)
            {
                throw new ArgumentException("CrossedOut must be greater than or equal to 1.", nameof(crossedOut));
            }
        }
    }
}
