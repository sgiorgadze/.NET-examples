using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace UniqueWords
{
    public static class TextProcessor
    {
        /// <summary>
        /// Returns the number of words that are duplicated in the <see cref="text"/>.
        /// </summary>
        /// <param name="text">Source text.</param>
        /// <returns>The number of words that are duplicated in the <see cref="text"/>.</returns>
        public static int CountDuplicateWords(string text)
        {
            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<string> wordList = new List<string>();
            List<char> charList = new List<char>();
            List<int> counters = new List<int>();

            foreach (string word in words)
            {
                int index = wordList.IndexOf(word);

                if (index == -1)
                {
                    wordList.Add(word);
                    charList.Add(word[0]);
                    counters.Add(1);
                }
                else
                {
                    counters[index]++;
                }
            }

            int duplicateCount = 0;
            for (int i = 0; i < counters.Count; i++)
            {
                if (counters[i] > 1)
                {
                    duplicateCount++;
                }
            }

            return duplicateCount;
        }

        /// <summary>
        /// Returns the total number of all words that are duplicated in the <see cref="lines"/> list.
        /// </summary>
        /// <param name="lines">Source lines of text.</param>
        /// <returns>The total number of all words that are duplicated in the <see cref="lines"/> list.</returns>
        public static int GetTotalDuplicateWordsNumber(IList<string> lines)
        {
            StringBuilder builder = new StringBuilder();
            Dictionary<string, int> counters = new Dictionary<string, int>();
            foreach (string line in lines)
            {
                builder.Clear();

                foreach (char c in line)
                {
                    if (char.IsLetterOrDigit(c))
                    {
                        builder.Append(c);
                    }
                    else if (builder.Length > 0)
                    {
                        string word = builder.ToString();
                        if (counters.ContainsKey(word))
                        {
                            counters[word]++;
                        }
                        else
                        {
                            counters[word] = 1;
                        }

                        builder.Clear();
                    }
                }

                if (builder.Length > 0)
                {
                    string word = builder.ToString();
                    if (counters.ContainsKey(word))
                    {
                        counters[word]++;
                    }
                    else
                    {
                        counters[word] = 1;
                    }
                }
            }

            int totalDuplicates = 0;
            foreach (int count in counters.Values)
            {
                if (count > 1)
                {
                    totalDuplicates += count;
                }
            }

            return totalDuplicates;
        }

        /// <summary>
        /// Returns the list of words that are duplicated in the <see cref="lines"/> collection.
        /// </summary>
        /// <param name="lines">Source lines of text.</param>
        /// <returns>The list of words that are duplicated in the <see cref="lines"/> collection.</returns>
        /// <exception cref="ArgumentNullException"><see cref="lines"/> is null.</exception>
        public static IList<string> GetDuplicateWords(ICollection<string>? lines)
        {
            HashSet<string> words = new HashSet<string>();
            HashSet<string> duplicates = new HashSet<string>();

            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            foreach (string line in lines)
            {
                string[] lineWords = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in lineWords)
                {
                    if (!words.Add(word))
                    {
                        duplicates.Add(word);
                    }
                }
            }

            return new List<string>(duplicates);
        }

        /// <summary>
        /// Returns the collection of unique words from the <see cref="lines"/> enumerable object.
        /// </summary>
        /// <param name="lines">Source lines of text.</param>
        /// <param name="ignoreCase">If true, indicates that the case of strings should be ignored.</param>
        /// <returns>The collection of unique words from the <see cref="lines"/> enumerable object.</returns>
        /// <exception cref="ArgumentNullException"><see cref="lines"/> is null.</exception>
        public static ICollection<string> GetUniqueWords(IEnumerable<string>? lines, bool ignoreCase)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            StringComparer comparer = ignoreCase ? StringComparer.InvariantCultureIgnoreCase : StringComparer.InvariantCulture;
            HashSet<string> uniqueWords = new HashSet<string>(comparer);
            if (ignoreCase)
            {
                return uniqueWords.OrderBy(word => word, comparer).ToList();
            }

            foreach (string line in lines)
            {
                string[] words = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(word => word.Trim())
                                     .Distinct()
                                     .ToArray();

                foreach (string word in words)
                {
                    uniqueWords.Add(word);
                }
            }

            return uniqueWords.OrderBy(word => word, comparer).ToList();
        }

        /// <summary>
        /// Searches a string for duplicates and removes all next occurrences of duplicate words. The first occurrence of a duplicate word should say on it's place.
        /// </summary>
        /// <param name="stringBuilder">The <see cref="StringBuilder"/> that contains a source text.</param>
        /// <exception cref="ArgumentNullException"><see cref="StringBuilder"/> is null.</exception>
        public static void RemoveDuplicateWordsInStringBuilder(StringBuilder? stringBuilder)
        {
            if (stringBuilder == null)
            {
                throw new ArgumentNullException(nameof(stringBuilder));
            }

            string[] words = stringBuilder.ToString().Split(' ');
            HashSet<string> wordSet = new HashSet<string>();

            for (int i = 0; i < words.Length; i++)
            {
                if (wordSet.Contains(words[i]))
                {
                    words[i] = " ";
                }
                else
                {
                    wordSet.Add(words[i]);
                }
            }

            stringBuilder.Clear();

            foreach (string word in words)
            {
                stringBuilder.Append(word);
            }
        }

        /// <summary>
        /// Searches a string for duplicates and removes all next occurrences of duplicate words. The first occurrence of a duplicate word should say on it's place.
        /// </summary>
        /// <param name="text">The source text.</param>
        /// <param name="ignoreCase">If true, indicates that the case of strings should be ignored.</param>
        /// <exception cref="ArgumentNullException"><see cref="text"/> is null.</exception>
        public static void RemoveDuplicateWordsInString(ref string? text, bool ignoreCase)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            string[] words = text.Split(' ');

            HashSet<string> wordSet = new HashSet<string>();

            for (int i = 0; i < words.Length; i++)
            {
                string word = ignoreCase ? words[i].ToLowerInvariant() : words[i];

                if (wordSet.Contains(word))
                {
                    words[i] = string.Empty;
                }
                else
                {
                    wordSet.Add(word);
                }
            }

            text = string.Join(" ", words);
        }
    }
}
