﻿using System;
using System.Globalization;

namespace ArrayExtensions
{
    /// <summary>
    /// Class of the additional operations with array.
    /// </summary>
    public static class ArrayExtension
    {
        /// <summary>
        /// Returns new array of elements that contain expected digit from source array.
        /// </summary>
        /// <param name="source">Source array.</param>
        /// <param name="digit">Expected digit.</param>
        /// <returns>Array of elements that contain expected digit.</returns>
        /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
        /// <exception cref="ArgumentException">Thrown when array is empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when digit value is out of range (0..9).</exception>
        /// <example>
        /// {1, 2, 3, 4, 5, 6, 7, 68, 69, 70, 15, 17}  => { 7, 70, 17 } for digit = 7.
        /// </example>
        public static int[] FilterByDigit(this int[]? source, int digit)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source.Length == 0)
            {
                throw new ArgumentException("Array is empty.", nameof(source));
            }

            if (digit < 0 || digit > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(digit), "Digit value is out of range (0..9).");
            }

            List<int> result = new List<int>();

            foreach (var item in source)
            {
                if (IsMatch(item))
                {
                    result.Add(item);
                }
            }

            return result.ToArray();

            bool IsMatch(int value)
            {
                while (value > 0)
                {
                    int lastDigit = value % 10;
                    if (lastDigit == digit)
                    {
                        return true;
                    }

                    value /= 10;
                }

                return false;
            }
        }

        /// <summary>
        /// Returns new array that contains only palindromic numbers from source array.
        /// </summary>
        /// <param name="source">Source array.</param>
        /// <returns>Array of elements that are palindromic numbers.</returns>
        /// <exception cref="ArgumentNullException">Throw when array is null.</exception>
        /// <exception cref="ArgumentException">Throw when array is empty.</exception>
        /// <example>
        /// {12345, 1111111112, 987654, 56, 1111111, -1111, 1, 1233321, 70, 15, 123454321}  => { 1111111, 123321, 123454321 }
        /// {56, -1111111112, 987654, 56, 890, -1111, 543, 1233}  => {  }.
        /// </example>
        public static int[] FilterByPalindromic(this int[]? source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source.Length == 0)
            {
                throw new ArgumentException("Array is empty.", nameof(source));
            }

            List<int> result = new List<int>();
            foreach (var item in source)
            {
                if (IsMatch(item))
                {
                    result.Add(item);
                }
            }

            throw new NotImplementedException();

            static bool IsMatch(int value)
            {
                string stringValue = value.ToString(CultureInfo.InvariantCulture);

                int left = 0;
                int right = stringValue.Length - 1;
                while (left < right)
                {
                    if (stringValue[left] != stringValue[right])
                    {
                        return false;
                    }

                    left++;
                    right--;
                }

                return true;
            }
        }
    }
}
