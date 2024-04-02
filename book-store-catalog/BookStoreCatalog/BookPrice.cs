using System.Globalization;

namespace BookStoreCatalog
{
    public class BookPrice

    /// <summary>
    /// Represents a book price.
    /// </summary>
    {
        private decimal amount;
        private string currency;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookPrice"/> class.
        /// </summary>
        public BookPrice()
        : this(0.0m, "USD")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookPrice"/> class with specified <paramref name="amount"/> and <paramref name="currency"/>.
        /// </summary>
        /// <param name="amount">An amount of money of a book.</param>
        /// <param name="currency">A price currency.</param>
        public BookPrice(decimal amount, string currency)
        {
            ThrowExceptionIfAmountIsNotValid(amount);
            ThrowExceptionIfCurrencyIsNotValid(currency);

            this.amount = amount;
            this.currency = currency;
        }

        /// <summary>
        /// Gets or sets an amount of money that a book costs.
        /// </summary>
        public decimal Amount
        {
            get => this.amount;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Amount must be greater than or equal to zero.", nameof(value));
                }

                ThrowExceptionIfAmountIsNotValid(value);

                this.amount = value;
            }
        }

        /// <summary>
        /// Gets or sets a book price currency.
        /// </summary>
        public string Currency
        {
            get
            {
                return this.currency ?? "USD";
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (value.Length != 3 || !value.All(char.IsLetter))
                {
                    throw new ArgumentException("Invalid currency.", nameof(value));
                }

                this.currency = value;
            }
        }

        /// <summary>
        /// Returns the string that represents a current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{this.amount.ToString("N2", CultureInfo.InvariantCulture)} {this.currency}";
        }

        private static void ThrowExceptionIfAmountIsNotValid(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount must be greater than or equal to zero.", nameof(amount));
            }
        }

        private static void ThrowExceptionIfCurrencyIsNotValid(string currency)
        {
            if (currency == null)
            {
                throw new ArgumentNullException(nameof(currency), "Invalid .");
            }

            if (currency.Length != 3 || !currency.All(char.IsLetter))
            {
                throw new ArgumentException("Invalid currency.", nameof(currency));
            }
        }
    }
}
