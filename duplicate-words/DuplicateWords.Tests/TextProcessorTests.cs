using System.Text;
using NUnit.Framework;

namespace UniqueWords.Tests
{
    [TestFixture]
    public class TextProcessorTests
    {
        private static readonly object[][] GetTotalDuplicateWordsNumberData =
        {
            new object[]
            {
                Array.Empty<string>(),
                0,
            },
            new object[]
            {
                new string[] { string.Empty },
                0,
            },
            new object[]
            {
                new string[] { "aaa" },
                0,
            },
            new object[]
            {
                new string[] { "aaa aaa" },
                2,
            },
            new object[]
            {
                new string[] { "aaa aaa aaa" },
                3,
            },
            new object[]
            {
                new string[] { "aaa bbb aaa" },
                2,
            },
            new object[]
            {
                new string[] { "aaa bbb ccc" },
                0,
            },
            new object[]
            {
                new string[] { "aaa bbb bbb" },
                2,
            },
            new object[]
            {
                new string[] { "aaa bbb aaa bbb" },
                4,
            },
            new object[]
            {
                new string[] { "aaa bbb ccc aaa bbb ccc" },
                6,
            },
            new object[]
            {
                new string[] { "    aaa     aaa bbb    bbb     ccc    ccc    ccc aaa    bbb" },
                9,
            },
            new object[]
            {
                new string[] { "α β  β γ   γ γ δ α β β γ γ γ δ  " },
                14,
            },
            new object[]
            {
                new string[] { "alpha beta  beta gamma   gamma gamma delta alpha beta beta gamma gamma gamma delta  " },
                14,
            },
            new object[]
            {
                new string[] { "    alpha   beta       beta   gamma       gamma   gamma   delta       alpha  beta      beta  gamma    gamma  gamma   delta     " },
                14,
            },
            new object[]
            {
                new string[] { "aaa", "aaa", "aaa" },
                3,
            },
            new object[]
            {
                new string[] { "aaa", "bbb", "ccc" },
                0,
            },
            new object[]
            {
                new string[] { "aaa bbb ccc", "aaa bbb ccc", "aaa bbb ccc" },
                9,
            },
            new object[]
            {
                new string[] { "aaa bbb", "bbb ccc", "ccc ddd" },
                4,
            },
        };

        private static readonly object[][] GetDuplicateWordsData =
        {
            new object[]
            {
                Array.Empty<string>(),
                Array.Empty<string>(),
            },
            new object[]
            {
                new string[] { "aaa" },
                Array.Empty<string>(),
            },
            new object[]
            {
                new string[] { "aaa aaa" },
                new string[] { "aaa" },
            },
            new object[]
            {
                new string[] { "aaa aaa aaa" },
                new string[] { "aaa" },
            },
            new object[]
            {
                new string[] { "aaa", "aaa" },
                new string[] { "aaa" },
            },
            new object[]
            {
                new string[] { "aaa", "aaa", "aaa" },
                new string[] { "aaa" },
            },
            new object[]
            {
                new string[] { "aaa bbb ccc" },
                Array.Empty<string>(),
            },
            new object[]
            {
                new string[] { "aaa bbb ccc bbb ccc ddd" },
                new string[] { "bbb", "ccc" },
            },
            new object[]
            {
                new string[] { "aaa", "bbb", "aaa" },
                new string[] { "aaa" },
            },
            new object[]
            {
                new string[] { "aaa", "bbb", "bbb" },
                new string[] { "bbb" },
            },
            new object[]
            {
                new string[] { "aaa", "bbb", "bbb" },
                new string[] { "bbb" },
            },
            new object[]
            {
                new string[] { "aaa bbb", "bbb ccc", "aaa ddd" },
                new string[] { "aaa", "bbb" },
            },
        };

        private static readonly object[][] GetUniqueWordsData =
        {
            new object[]
            {
                Array.Empty<string>(),
                true,
                Array.Empty<string>(),
            },
            new object[]
            {
                Array.Empty<string>(),
                false,
                Array.Empty<string>(),
            },
            new object[]
            {
                new string[] { "aaa" },
                true,
                new string[] { "aaa" },
            },
            new object[]
            {
                new string[] { "aaa" },
                false,
                new string[] { "aaa" },
            },
            new object[]
            {
                new string[] { "aaa", "aaa" },
                true,
                Array.Empty<string>(),
            },
            new object[]
            {
                new string[] { "aaa", "aaa" },
                false,
                Array.Empty<string>(),
            },
            new object[]
            {
                new string[] { "aaa", "AAA" },
                true,
                Array.Empty<string>(),
            },
            new object[]
            {
                new string[] { "aaa", "AAA" },
                false,
                new string[] { "aaa", "AAA" },
            },
            new object[]
            {
                new string[] { "aaa", "AaA", "aAa", "AAA" },
                true,
                Array.Empty<string>(),
            },
            new object[]
            {
                new string[] { "aaa", "AaA", "aAa", "AAA" },
                false,
                new string[] { "aaa", "aAa", "AaA", "AAA" },
            },
            new object[]
            {
                new string[] { "AAA", "AaA", "aAa", "aaa" },
                false,
                new string[] { "aaa", "aAa", "AaA", "AAA" },
            },
            new object[]
            {
                new string[] { "AAA bbb CCC", "aaa BBB ccc" },
                true,
                Array.Empty<string>(),
            },
            new object[]
            {
                new string[] { "AAA bbb CCC", "aaa BBB ccc" },
                false,
                new string[] { "aaa", "AAA", "bbb", "BBB", "ccc", "CCC" },
            },
        };

        private static readonly object[][] RemoveDuplicateWordsInStringBuilderData =
        {
            new object[]
            {
                string.Empty,
                string.Empty,
            },
            new object[]
            {
                "aaa aaa",
                "aaa ",
            },
            new object[]
            {
                "aaa aaa aaa aaa",
                "aaa   ",
            },
            new object[]
            {
                "alpha beta  beta gamma   gamma gamma delta alpha beta beta gamma gamma gamma delta  ",
                "alpha beta   gamma     delta         ",
            },
            new object[]
            {
                " Broadcasting  by television and  radio in Britain is regulated  by the Minister of Posts and   Telecommunications.  101",
                " Broadcasting  by television and  radio in Britain is regulated   the Minister of Posts    Telecommunications.  101",
            },
            new object[]
            {
                "123   Both the BBC and the ITV services provide programmes   of music, and drama, and light entertainment, and films.",
                "123   Both the BBC and  ITV services provide programmes   of music,  drama,  light entertainment,  films.",
            },
            new object[]
            {
                "  In  the evening, I often watch TV with my family and discuss my plans  for the next  day.",
                "  In  the evening, I often watch TV with my family and discuss  plans  for  next  day.",
            },
            new object[]
            {
                "Test1   We are very  much alike: open-hearted, smart and merry.  That is why we have a lot of  friends. Test1 - Test1",
                "Test1   We are very  much alike: open-hearted, smart and merry.  That is why we have a lot of  friends.  - ",
            },
            new object[]
            {
                "Your own,personal.Jesus***Someone##to%hear<your>prayers--- ----Someone!@()^who cares,,,,,,,,,,,,Your own personal$$$$Jesus = Someone to   hear your prayers  Someone who's there",
                "Your own,personal.Jesus***Someone##to%hear<your>prayers--- ----!@()^who cares,,,,,,,,,,,,  $$$$ =          's there",
            },
        };

        private static readonly object[][] RemoveDuplicateWordsInStringData =
        {
            new object[]
            {
                string.Empty,
                false,
                string.Empty,
            },
            new object[]
            {
                "aaa aaa",
                false,
                "aaa ",
            },
            new object[]
            {
                "aaa aaa",
                true,
                "aaa ",
            },
            new object[]
            {
                "aaa AaA aAa AAA",
                false,
                "aaa AaA aAa AAA",
            },
            new object[]
            {
                "aaa AaA aAa AAA",
                true,
                "aaa   ",
            },
            new object[]
            {
                "aaa aaa",
                true,
                "aaa ",
            },
            new object[]
            {
                "aaa aaa aaa aaa",
                false,
                "aaa   ",
            },
            new object[]
            {
                "aaa aaa aaa aaa",
                true,
                "aaa   ",
            },
            new object[]
            {
                "alpha beta  beta gamma   gamma gamma delta alpha beta beta gamma gamma gamma delta  ",
                false,
                "alpha beta   gamma     delta         ",
            },
            new object[]
            {
                "alpha beta  beta gamma   gamma gamma delta alpha beta beta gamma gamma gamma delta  ",
                true,
                "alpha beta   gamma     delta         ",
            },
            new object[]
            {
                " Broadcasting  by television and  radio in Britain is regulated  by the Minister of Posts and   Telecommunications.  101",
                false,
                " Broadcasting  by television and  radio in Britain is regulated   the Minister of Posts    Telecommunications.  101",
            },
            new object[]
            {
                " Broadcasting  by television and  radio in Britain is regulated  by the Minister of Posts and   Telecommunications.  101",
                true,
                " Broadcasting  by television and  radio in Britain is regulated   the Minister of Posts    Telecommunications.  101",
            },
            new object[]
            {
                "123   Both the BBC and the ITV services provide programmes   of music, and drama, and light entertainment, and films.",
                false,
                "123   Both the BBC and  ITV services provide programmes   of music,  drama,  light entertainment,  films.",
            },
            new object[]
            {
                "123   Both the BBC and the ITV services provide programmes   of music, and drama, and light entertainment, and films.",
                true,
                "123   Both the BBC and  ITV services provide programmes   of music,  drama,  light entertainment,  films.",
            },
            new object[]
            {
                "  In  the evening, I often watch TV with my family and discuss my plans  for the next  day.",
                false,
                "  In  the evening, I often watch TV with my family and discuss  plans  for  next  day.",
            },
            new object[]
            {
                "  In  the evening, I often watch TV with my family and discuss my plans  for the next  day.",
                true,
                "  In  the evening, I often watch TV with my family and discuss  plans  for  next  day.",
            },
            new object[]
            {
                "Test1   We are very  much alike: open-hearted, smart and merry.  That is why we have a lot of  friends. Test1 - Test1",
                false,
                "Test1   We are very  much alike: open-hearted, smart and merry.  That is why we have a lot of  friends.  - ",
            },
            new object[]
            {
                "Test1   We are very  much alike: open-hearted, smart and merry.  That is why we have a lot of  friends. Test1 - Test1",
                true,
                "Test1   We are very  much alike: open-hearted, smart and merry.  That is why  have a lot of  friends.  - ",
            },
            new object[]
            {
                "Your own,personal.Jesus***Someone##to%hear<your>prayers--- ----Someone!@()^who cares,,,,,,,,,,,,Your own personal$$$$Jesus = Someone to   hear your prayers  Someone who's there",
                false,
                "Your own,personal.Jesus***Someone##to%hear<your>prayers--- ----!@()^who cares,,,,,,,,,,,,  $$$$ =          's there",
            },
            new object[]
            {
                "Your own,personal.Jesus***Someone##to%hear<your>prayers--- ----Someone!@()^who cares,,,,,,,,,,,,Your own personal$$$$Jesus = Someone to   hear your prayers  Someone who's there",
                true,
                "Your own,personal.Jesus***Someone##to%hear<>prayers--- ----!@()^who cares,,,,,,,,,,,,  $$$$ =          's there",
            },
        };

        [TestCase("", ExpectedResult = 0)]
        [TestCase("aaa", ExpectedResult = 0)]
        [TestCase("aaa aaa", ExpectedResult = 1)]
        [TestCase("aaa aaa aaa", ExpectedResult = 1)]
        [TestCase("aaa bbb aaa", ExpectedResult = 1)]
        [TestCase("aaa bbb ccc", ExpectedResult = 0)]
        [TestCase("aaa bbb bbb", ExpectedResult = 1)]
        [TestCase("aaa bbb aaa bbb", ExpectedResult = 2)]
        [TestCase("aaa bbb ccc aaa bbb ccc", ExpectedResult = 3)]
        [TestCase("    aaa     aaa bbb    bbb     ccc    ccc    ccc aaa    bbb", ExpectedResult = 3)]
        [TestCase("α β  β γ   γ γ δ α β β γ γ γ δ  ", ExpectedResult = 4)]
        [TestCase("alpha beta  beta gamma   gamma gamma delta alpha beta beta gamma gamma gamma delta  ", ExpectedResult = 4)]
        [TestCase("    alpha   beta       beta   gamma       gamma   gamma   delta       alpha  beta      beta  gamma    gamma  gamma   delta     ", ExpectedResult = 4)]
        public int CountDuplicateWords_ReturnsDuplicateWordsNumber(string text)
        {
            // Act
            return TextProcessor.CountDuplicateWords(text);
        }

        [TestCaseSource(nameof(GetTotalDuplicateWordsNumberData))]
        public void GetTotalDuplicateWordsNumber_ReturnsDuplicateWordsNumber(IList<string> lines, int expectedResult)
        {
            // Act
            int actualResult = TextProcessor.GetTotalDuplicateWordsNumber(lines);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void GetDuplicateWords_LinesIsNull_ThrowsArgumentNullException()
        {
            // Act
            Assert.Throws<ArgumentNullException>(() => TextProcessor.GetDuplicateWords(null), "The lines argument should be tested for null.");
        }

        [TestCaseSource(nameof(GetDuplicateWordsData))]
        public void GetDuplicateWords_ReturnsDuplicateWordList(ICollection<string> lines, IList<string> expectedResult)
        {
            // Act
            IList<string> actualResult = TextProcessor.GetDuplicateWords(lines);

            // Assert
            Assert.AreEqual(expectedResult.Count, actualResult.Count);
            Assert.That(actualResult, Is.EquivalentTo(expectedResult));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetUniqueWords_LinesIsNull_ThrowsArgumentNullException(bool ignoreCase)
        {
            // Act
            Assert.Throws<ArgumentNullException>(() => TextProcessor.GetUniqueWords(null, ignoreCase), "The lines argument should be tested for null.");
        }

        [TestCaseSource(nameof(GetUniqueWordsData))]
        public void GetUniqueWords_ReturnsUniqueWordList(IEnumerable<string>? lines, bool ignoreCase, ICollection<string> expectedResult)
        {
            // Act
            ICollection<string> actualResult = TextProcessor.GetUniqueWords(lines, ignoreCase);

            // Assert
            Assert.AreEqual(expectedResult.Count, actualResult.Count);

            var actualResultArray = actualResult.ToArray();
            var expectedResultArray = expectedResult.ToArray();
            Assert.AreEqual(expectedResultArray, actualResultArray);
        }

        [Test]
        public void RemoveDuplicateWordsInStringBuilder_StringBuilderIsNull_ThrowsArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => TextProcessor.RemoveDuplicateWordsInStringBuilder(null), "The stringBuilder argument should be tested for null.");
        }

        [TestCaseSource(nameof(RemoveDuplicateWordsInStringBuilderData))]
        public void RemoveDuplicateWordsFromStringBuilder_RemovesDuplicateWords(string text, string expectedResult)
        {
            // Arrange
            var stringBuilder = new StringBuilder(text);

            // Act
            TextProcessor.RemoveDuplicateWordsInStringBuilder(stringBuilder);

            // Assert
            Assert.AreEqual(expectedResult, stringBuilder.ToString());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void RemoveDuplicateWordsInString_TestIsNull_ThrowsArgumentNullException(bool ignoreCase)
        {
            // Arrange
            string? text = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() => TextProcessor.RemoveDuplicateWordsInString(ref text, ignoreCase), "The text argument should be tested for null.");
        }

        [TestCaseSource(nameof(RemoveDuplicateWordsInStringData))]
        public void RemoveDuplicateWordsInString_RemovesDuplicateWords(string? text, bool ignoreCase, string expectedResult)
        {
            // Act
            TextProcessor.RemoveDuplicateWordsInString(ref text, ignoreCase);

            // Assert
            Assert.NotNull(text);
            Assert.AreEqual(expectedResult, text);
        }
    }
}
