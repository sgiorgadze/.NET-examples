﻿using System;
using System.Collections.Generic;

namespace Comparators
{
    /// <summary>
    /// Comparator class for string type.
    /// </summary>
    public class StringByLengthComparer : IComparer<string>
    {
        /// <summary>
        /// Compares two strings by length of the string.
        /// </summary>
        /// <param name="x">The first string to compare.</param>
        /// <param name="y">The second string to compare.</param>
        /// <returns>Less than zero if length of the string x is less than length of the string y.
        /// Zero if length of the string x equals length of the string y.
        /// Greater than zero if length of the string x is greater than length of the string y.
        /// </returns>
        public int Compare(string? x, string? y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            int lengthX = x.Length;
            int lengthY = y.Length;

            return lengthX.CompareTo(lengthY);
        }
    }
}
