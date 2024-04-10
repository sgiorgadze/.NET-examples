namespace IsbnValidator
{
    public static class Validator
    {
        /// <summary>
        /// Returns true if the specified <paramref name="isbn"/> is valid; returns false otherwise.
        /// </summary>
        /// <param name="isbn">The string representation of 10-digit ISBN.</param>
        /// <returns>true if the specified <paramref name="isbn"/> is valid; false otherwise.</returns>
        /// <exception cref="ArgumentException"><paramref name="isbn"/> is empty or has only white-space characters.</exception>
        public static bool IsIsbnValid(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                throw new ArgumentException("ISBN cannot be empty or contain only white-space characters.");
            }

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
    }
}
