// <copyright file="BookStoreItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Globalization;

[assembly: CLSCompliant(true)]

namespace BookStoreItem
{
    /// <summary>
    /// Represents an item in a book store.
    /// </summary>
    public class BookStoreItem
    {
        private readonly string authorName;
        private readonly string? isni;
        private readonly bool hasIsni;
        private string? currency;
        private decimal price;
        private int amount;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookStoreItem"/> class with the specified <paramref name="authorName"/>, <paramref name="title"/>, <paramref name="publisher"/> and <paramref name="isbn"/>.
        /// </summary>
        /// <param name="authorName">A book author's name.</param>
        /// <param name="title">A book title.</param>
        /// <param name="publisher">A book publisher.</param>
        /// <param name="isbn">A book ISBN.</param>
        public BookStoreItem(string authorName, string title, string publisher, string isbn)
        {
            if (string.IsNullOrWhiteSpace(authorName))
            {
                throw new ArgumentException("Author name cannot be empty or whitespace.", nameof(authorName));
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty or whitespace.", nameof(title));
            }

            if (string.IsNullOrWhiteSpace(publisher))
            {
                throw new ArgumentException("Publisher cannot be empty or whitespace.", nameof(publisher));
            }

            if (!authorName.Any(char.IsLetter) || !title.Any(char.IsLetter) || !publisher.Any(char.IsLetter))
            {
                throw new ArgumentException("Author name, title, and publisher must contain at least one letter character.");
            }

            if (string.IsNullOrWhiteSpace(isbn))
            {
                throw new ArgumentException("ISBN cannot be empty or whitespace.", nameof(isbn));
            }

            if (!ValidateIsbnFormat(isbn))
            {
                throw new ArgumentException("Invalid ISBN format or checksum.", nameof(isbn));
            }

            if (!ValidateIsbnChecksum(isbn))
            {
                throw new ArgumentException("Invalid ISBN format or checksum.", nameof(isbn));
            }

            this.authorName = authorName;
            this.Title = title;
            this.Publisher = publisher;
            this.Isbn = isbn;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookStoreItem"/> class with the specified <paramref name="authorName"/>, <paramref name="isni"/>, <paramref name="title"/>, <paramref name="publisher"/> and <paramref name="isbn"/>.
        /// </summary>
        /// <param name="authorName">A book author's name.</param>
        /// <param name="isni">A book author's ISNI.</param>
        /// <param name="title">A book title.</param>
        /// <param name="publisher">A book publisher.</param>
        /// <param name="isbn">A book ISBN.</param>
        public BookStoreItem(string authorName, string? isni, string title, string publisher, string isbn)
       : this(authorName, title, publisher, isbn)
        {
            if (isni is not null && !ValidateIsni(isni))
            {
                throw new ArgumentException("a", nameof(isni));
            }

            this.isni = isni;
            this.hasIsni = !string.IsNullOrWhiteSpace(isni);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookStoreItem"/> class with the specified <paramref name="authorName"/>, <paramref name="title"/>, <paramref name="publisher"/> and <paramref name="isbn"/>, <paramref name="published"/>, <paramref name="bookBinding"/>, <paramref name="price"/>, <paramref name="currency"/> and <paramref name="amount"/>.
        /// </summary>
        /// <param name="authorName">A book author's name.</param>
        /// <param name="title">A book title.</param>
        /// <param name="publisher">A book publisher.</param>
        /// <param name="isbn">A book ISBN.</param>
        /// <param name="published">A book publishing date.</param>
        /// <param name="bookBinding">A book binding type.</param>
        /// <param name="price">An amount of money that a book costs.</param>
        /// <param name="currency">A price currency.</param>
        /// <param name="amount">An amount of books in the store's stock.</param>
        public BookStoreItem(
       string authorName,
       string title,
       string publisher,
       string isbn,
       DateTime? published,
       string bookBinding,
       decimal price,
       string currency,
       int amount)
       : this(authorName, null, title, publisher, isbn, published, bookBinding, price, currency, amount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookStoreItem"/> class with the specified <paramref name="authorName"/>, <paramref name="isni"/>, <paramref name="title"/>, <paramref name="publisher"/> and <paramref name="isbn"/>, <paramref name="published"/>, <paramref name="bookBinding"/>, <paramref name="price"/>, <paramref name="currency"/> and <paramref name="amount"/>.
        /// </summary>
        /// <param name="authorName">A book author's name.</param>
        /// <param name="isni">A book author's ISNI.</param>
        /// <param name="title">A book title.</param>
        /// <param name="publisher">A book publisher.</param>
        /// <param name="isbn">A book ISBN.</param>
        /// <param name="published">A book publishing date.</param>
        /// <param name="bookBinding">A book binding type.</param>
        /// <param name="price">An amount of money that a book costs.</param>
        /// <param name="currency">A price currency.</param>
        /// <param name="amount">An amount of books in the store's stock.</param>
        public BookStoreItem(
       string authorName,
       string? isni,
       string title,
       string publisher,
       string isbn,
       DateTime? published,
       string bookBinding,
       decimal price,
       string currency,
       int amount)
            : this(authorName, isni, title, publisher, isbn)
        {
            ThrowExceptionIfCurrencyIsNotValid(currency);

            this.Published = published;
            this.BookBinding = bookBinding;
            this.Price = price;
            this.currency = currency;
            this.Amount = amount;
        }

        /// <summary>
        /// Gets a book author's name.
        /// </summary>
        public string AuthorName => this.authorName;

        /// <summary>
        /// Gets an International Standard Name Identifier (ISNI) that uniquely identifies a book author.
        /// </summary>
        public string? Isni => this.isni;

        /// <summary>
        /// Gets a value indicating whether an author has an International Standard Name Identifier (ISNI).
        /// </summary>
        public bool HasIsni => this.hasIsni;

        /// <summary>
        /// Gets a book title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets a book publisher.
        /// </summary>
        public string Publisher { get; private set; }

        /// <summary>
        /// Gets a book International Standard Book Number (ISBN).
        /// </summary>
        public string Isbn { get; private set; }

        /// <summary>
        /// Gets or sets a book publishing date.
        /// </summary>
        public DateTime? Published { get; set; }

        /// <summary>
        /// Gets or sets a book binding type.
        /// </summary>
        public string BookBinding { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets an amount of money that a book costs.
        /// </summary>
        public decimal Price
        {
            get => this.price;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(this.Price), "Price must be greater than or equal to zero.");
                }

                this.price = value;
            }
        }

        /// <summary>
        /// Gets or sets a price currency.
        /// </summary>
        public string Currency
        {
            get
            {
                return this.currency ?? "USD";
            }

            set
            {
                this.currency = value;
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
                    throw new ArgumentOutOfRangeException(nameof(this.Amount), "Amount must be greater than or equal to zero.");
                }

                this.amount = value;
            }
        }

        /// <summary>
        /// Gets a <see cref="Uri"/> to the contributor's page at the isni.org website.
        /// </summary>
        /// <returns>A <see cref="Uri"/> to the contributor's page at the isni.org website.</returns>
        public Uri GetIsniUri()
        {
            if (!this.HasIsni)
            {
                throw new InvalidOperationException("ISNI is not set.");
            }

            var isniCode = this.Isni!;

            var uriString = $"https://isni.org/isni/{isniCode}";

            return new Uri(uriString);
        }

        /// <summary>
        /// Gets an <see cref="Uri"/> to the publication page on the isbnsearch.org website.
        /// </summary>
        /// <returns>an <see cref="Uri"/> to the publication page on the isbnsearch.org website.</returns>
        public Uri GetIsbnSearchUri()
        {
            if (string.IsNullOrWhiteSpace(this.Isbn))
            {
                throw new InvalidOperationException("ISBN is not set.");
            }

            string isbnSearchUrl = $"https://isbnsearch.org/isbn/{this.Isbn}";

            return new Uri(isbnSearchUrl);
        }

        /// <summary>
        /// Returns the string that represents a current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var currencyPart = this.Currency == "EUR"
                ? $"\"{this.Price.ToString("N2", CultureInfo.InvariantCulture)} {this.Currency}\""
                : $"{this.Price.ToString("N2", CultureInfo.InvariantCulture)} {this.Currency}";

            return this.HasIsni
                ? $"{this.Title}, {this.AuthorName}, {this.Isni}, {currencyPart}, {this.Amount}"
                : $"{this.Title}, {this.AuthorName}, ISNI IS NOT SET, {currencyPart}, {this.Amount}";
        }

        private static bool ValidateIsni(string isni)
        {
            if (isni.Length != 16)
            {
                return false;
            }

            foreach (char c in isni)
            {
                if (!char.IsDigit(c) && c != 'X')
                {
                    return false;
                }
            }

            return true;
        }

        private static bool ValidateIsbnFormat(string isbn)
        {
            string cleanedIsbn = isbn
                .Replace("-", string.Empty, StringComparison.InvariantCulture)
                .Replace(" ", string.Empty, StringComparison.InvariantCulture);

            if (cleanedIsbn.Length != 10)
            {
                return false;
            }

            return true;
        }

        private static bool ValidateIsbnChecksum(string isbn)
        {
            string cleanedIsbn = isbn
               .Replace("-", string.Empty, StringComparison.InvariantCulture)
               .Replace(" ", string.Empty, StringComparison.InvariantCulture);

            if (cleanedIsbn.Length != 10)
            {
                return false;
            }

            int checksum = 0;
            for (int i = 0; i < 10; i++)
            {
                int digit;
                char last = isbn[9];
                if (last != 'X' && (last < '0'
                                 || last > '9'))
                {
                    return false;
                }

                if (i == 9 && cleanedIsbn[i] == 'X')
                {
                    digit = 10;
                }
                else if (!int.TryParse(cleanedIsbn[i].ToString(), out digit))
                {
                    return false;
                }

                checksum += digit * (10 - i);
            }

            var a = checksum % 11;
            if (a == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void ThrowExceptionIfCurrencyIsNotValid(string currency)
        {
            if (currency.Length != 3 || !currency.All(char.IsLetter))
            {
                throw new ArgumentException("Invalid currency.", nameof(currency));
            }
        }
    }
}
