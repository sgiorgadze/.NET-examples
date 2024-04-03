namespace BookStoreCatalog
{
    /// <summary>
    /// Represents an International Standard Book Number (ISBN).
    /// </summary>
    public class BookNumber
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookNumber"/> class with the specified 10-digit ISBN <paramref name="isbnCode"/>.
        /// </summary>
        /// <param name="isbnCode">A 10-digit ISBN code.</param>
        /// <exception cref="ArgumentNullException">a code argument is null.</exception>
        /// <exception cref="ArgumentException">a code argument is invalid or a code has wrong checksum.</exception>
        private readonly string code;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookNumber"/> class.
        /// Gets a 10-digit ISBN code.
        /// </summary>
        public BookNumber(string isbnCode)
        {
            if (isbnCode == null)
            {
                throw new ArgumentNullException(nameof(isbnCode), "Invalid ISBN ");
            }

            if (!ValidateCode(isbnCode))
            {
                throw new ArgumentException("Invalid ISBN format or checksuma.", nameof(isbnCode));
            }

            if (!ValidateChecksum(isbnCode))
            {
                throw new ArgumentException("Invalid ISBN format or checksums.", nameof(isbnCode));
            }

            this.code = isbnCode;
        }

        public string Code => this.code;

        /// <summary>
        /// Gets an <see cref="Uri"/> to the publication page on the isbnsearch.org website.
        /// </summary>
        /// <returns>an <see cref="Uri"/> to the publication page on the isbnsearch.org website.</returns>
        public Uri GetSearchUri()
        {
            if (string.IsNullOrWhiteSpace(this.code))
            {
                throw new InvalidOperationException("ISBN is not set.");
            }

            string isbnSearchUrl = $"https://isbnsearch.org/isbn/{this.code}";

            return new Uri(isbnSearchUrl);
        }

        public override string ToString()
        {
            return this.code;
        }

        /// <summary>
        /// Returns the string that represents a current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        private static bool ValidateCode(string isbnCode)
        {
            if (string.IsNullOrWhiteSpace(isbnCode))
            {
                return false;
            }

            string cleanedIsbn = isbnCode
                .Replace("-", string.Empty, StringComparison.InvariantCulture)
                .Replace(" ", string.Empty, StringComparison.InvariantCulture);

            if (cleanedIsbn.Length != 10)
            {
                return false;
            }

            return true;
        }

        private static bool ValidateChecksum(string isbnCode)
        {
            string cleanedIsbn = isbnCode
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
                char last = isbnCode[9];
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
    }
}
