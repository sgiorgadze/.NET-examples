namespace BookStoreCatalog
{
    /// <summary>
    /// Represents a book author.
    /// </summary>
    public class BookAuthor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookAuthor"/> class with the specified author name and 16-digit ISNI code.
        /// </summary>
        /// <param name="authorName">A book author's name.</param>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        public BookAuthor(string authorName)
        {
            if (string.IsNullOrWhiteSpace(authorName))
            {
                throw new ArgumentNullException(nameof(authorName), "Author name cannot be empty or whitespace.");
            }

            this.AuthorName = authorName;
            this.HasIsni = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookAuthor"/> class with the specified author name and 16-digit ISNI code.
        /// </summary>
        /// <param name="authorName">A book author's name.</param>
        /// <param name="isniCode">A 16-digit ISNI code that uniquely identifies a book author.</param>
        /// <exception cref="ArgumentNullException">name is null or isniCode is null.</exception>
        /// <exception cref="ArgumentException">name is empty or consists of white-space only characters.</exception>
        public BookAuthor(string authorName, string isniCode)
            : this(authorName)
        {
            if (isniCode == null)
            {
                throw new ArgumentNullException(nameof(isniCode));
            }

            this.Isni = new NameIdentifier(isniCode);
            this.HasIsni = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookAuthor"/> class with the specified author name and 16-digit ISNI code.
        /// </summary>
        /// <param name="authorName">A book author name.</param>
        /// <param name="nameIdentifier">An International Standard Name Identifier that uniquely identifies a book author.</param>
        /// <exception cref="ArgumentNullException">name is null or nameIdentifier is null.</exception>
        /// <exception cref="ArgumentException">name is empty or consists of white-space only characters.</exception>
        public BookAuthor(string authorName, NameIdentifier nameIdentifier)
            : this(authorName)
        {
            if (nameIdentifier == null)
            {
                throw new ArgumentNullException(nameof(nameIdentifier));
            }

            this.Isni = nameIdentifier;
            this.HasIsni = true;
        }

        /// <summary>
        /// Gets a book author's name.
        /// </summary>
        public string AuthorName { get; private set; }

        /// <summary>
        /// Gets a value indicating whether an author has an International Standard Name Identifier (ISNI).
        /// </summary>
        public bool HasIsni { get; private set; }

        /// <summary>
        /// Gets an International Standard Name Identifier (ISNI) that uniquely identifies a book author.
        /// </summary>
        public NameIdentifier Isni { get; private set; }

        /// <summary>
        /// Returns the string that represents a current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            if (this.HasIsni)
            {
                return $"{this.AuthorName} (ISNI:{this.Isni.Code})";
            }

            return this.AuthorName;
        }
    }
}
