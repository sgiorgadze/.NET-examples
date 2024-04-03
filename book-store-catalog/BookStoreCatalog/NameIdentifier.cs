﻿namespace BookStoreCatalog
{
    /// <summary>
    /// Represents an International Standard Name Identifier (ISNI).
    /// </summary>
    public class NameIdentifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameIdentifier"/> class with the specified 16-digit ISNI <paramref name="isniCode"/>.
        /// </summary>
        /// <param name="isniCode">A 16-digit ISNI code.</param>
        /// <exception cref="ArgumentNullException">a code argument is null.</exception>
        /// <exception cref="ArgumentException">a code argument is invalid.</exception>
        public NameIdentifier(string isniCode)
        {
            if (isniCode == null)
            {
                throw new ArgumentNullException(nameof(isniCode));
            }

            if (!ValidateCode(isniCode))
            {
                throw new ArgumentException("Invalid ISNI code.", nameof(isniCode));
            }

            this.Code = isniCode;
        }

        /// <summary>
        /// Gets a 16-digit ISNI code.
        /// </summary>
        public string Code { get; init; }

        public override string ToString()
        {
            return this.Code;
        }

        /// <summary>
        /// Gets a <see cref="Uri"/> to the contributor's page at the isni.org website.
        /// </summary>
        /// <returns>A <see cref="Uri"/> to the contributor's page at the isni.org website.</returns>
        public Uri GetUri()
        {
            var uriString = $"http://www.isni.org/isni/{this.Code}";

            return new Uri(uriString);
        }

        /// <summary>
        /// Returns the string that represents a current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        private static bool ValidateCode(string isniCode)
        {
            if (isniCode.Length != 16)
            {
                return false;
            }

            foreach (char c in isniCode)
            {
                if (!char.IsDigit(c) && c != 'X')
                {
                    return false;
                }
            }

            return true;
        }
    }
}
