using System;
using System.Globalization;
using GenericMethods.Interfaces;

namespace IntegerPredicate
{
    /// <summary>
    /// Predicate class for the integer type.
    /// </summary>
    public class ContainsDigitPredicate : IPredicate<int>
    {
        private int digit;

        /// <summary>
        /// Gets or sets digit in the integer number.
        /// </summary>
        public int Digit
        {
            get => this.digit;

            set => this.digit = value < 0 || value > 9 ?
                throw new ArgumentOutOfRangeException(nameof(value), "Digit must be between 0 and 9.")
                : value;
        }

        /// <summary>
        /// Determines if a number contains a given digit.
        /// </summary>
        /// <param name="obj">The integer value.</param>
        /// <returns>true if integer value contains given digit; otherwise, false.</returns>
        public bool IsMatch(int obj)
        {
            string numberString = obj.ToString(CultureInfo.InvariantCulture);

            char digitChar = this.Digit.ToString(CultureInfo.InvariantCulture)[0];
            return numberString.Contains(digitChar, StringComparison.InvariantCulture);
        }
    }
}
