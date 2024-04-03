using System.Diagnostics;
using System.Globalization;

namespace BookStoreCatalog
{
    /// <summary>
    /// Represents the an item in a book store.
    /// </summary>
    public class BookStoreItem
    {
        private BookPublication publication;
        private BookPrice price;
        private int amount;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookStoreItem"/> class with the specified <paramref name="authorName"/>, <paramref name="isniCode"/>, <paramref name="title"/>, <paramref name="publisher"/>, <paramref name="published"/>, <paramref name="bookBinding"/>, <paramref name="bookBinding"/>, <paramref name="isbn"/>, <paramref name="priceAmount"/>, <paramref name="priceCurrency"/> and <paramref name="amount"/>.
        /// </summary>
        public BookStoreItem(string authorName, string isniCode, string title, string publisher, DateTime published, BookBindingKind bookBinding, string isbn, decimal priceAmount, string priceCurrency, int amount)
        {
            this.publication = new BookPublication(new BookAuthor(authorName, isniCode), title, publisher, published, bookBinding, new BookNumber(isbn));
            this.price = new BookPrice(priceAmount, priceCurrency);
            this.amount = amount;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookStoreItem"/> class with the specified <paramref name="publication"/>, <paramref name="price"/> and <paramref name="amount"/>.
        /// </summary>
        public BookStoreItem(BookPublication publication, BookPrice price, int amount)
        {
            if (price == null)
            {
                throw new ArgumentNullException(nameof(price), "d");
            }

            if (publication == null)
            {
                throw new ArgumentNullException(nameof(publication), "d");
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "d");
            }

            this.Publication = publication;
            this.Price = price;
            this.Amount = amount;
        }

        /// <summary>
        /// Gets or sets a <see cref="BookPublication"/>.
        /// </summary>
        public BookPublication Publication
        {
            get => this.publication;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.publication = value;
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="BookPrice"/>.
        /// </summary>
        public BookPrice Price
        {
            get => this.price;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.price = value;
            }
        }

        /// <summary>
        /// Gets or sets an amount of books in the store's stock.
        /// </summary>
        public int Amount
        {
            get => this.amount;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Amount cannot be less than zero.");
                }

                if (this.amount < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(this.Amount), "Amount cannot be less than zero.");
                }

                this.amount = value;
            }
        }

        /// <summary>
        /// Returns the string that represents a current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var a = this.publication.Author.Isni;

            var currencyPart = this.price.Currency == "EUR"
                  ? $"{this.price}"
                  : $"\"{this.price}\"";

            if (a is not null)
            {
                return $"{this.publication.Title} by {this.publication.Author}, {this.price}, {this.Amount}";
            }
            else
            {
                return $"{this.publication.Title} by {this.publication.Author}, {currencyPart}, {this.Amount}";
            }
        }
    }
}
