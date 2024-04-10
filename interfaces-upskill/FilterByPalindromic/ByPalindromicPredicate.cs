using System;
using System.Globalization;
using FilterByPredicate;

namespace FilterByPalindromic
{
    /// <summary>
    /// Palindrome predicate.
    /// </summary>
    public class ByPalindromicPredicate : IPredicate
    {
        /// <inheritdoc/>
        public bool IsMatch(int number)
        {
            string stringValue = number.ToString(CultureInfo.InvariantCulture);

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
