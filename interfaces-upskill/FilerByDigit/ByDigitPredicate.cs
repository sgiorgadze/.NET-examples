using System;
using FilterByPredicate;

namespace FilterByDigit
{
    /// <summary>
    /// Predicate that determines the presence of some digit in integer.
    /// </summary>
    public class ByDigitPredicate : IPredicate
    {
        private int digit;

        /// <summary>
        /// Gets or sets a digit.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when Digit more than 9 or less than 0.</exception>
        public int Digit
        {
            get => this.digit;

            set
            {
                this.digit = value >= 0 && value <= 9 ? value : throw new ArgumentOutOfRangeException(nameof(value), "Digit value is out of range (0..9).");
            }
        }

        /// <inheritdoc/>
        public bool IsMatch(int number)
        {
            while (number != 0)
            {
                var lastDigit = Math.Abs(number % 10);
                if (lastDigit == this.Digit)
                {
                    return true;
                }

                number /= 10;
            }

            return false;
        }
    }
}
