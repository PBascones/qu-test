using Xunit;

namespace QUTest.Tests
{
    public class WordFinderTests
    {
        [Fact]
        public void Find_Words_Horizontally_And_Vertically()
        {
            // Arrange
            var matrix = new List<string>
            {
                "chill",
                "ooool",
                "linnd",
                "doldd",
                "chill"
            };

            var wordstream = new List<string> { "chill", "cold", "wind", "cool" };

            // Act
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);

            // Assert
            var expected = new List<string> { "chill", "cold" };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Find_Top_10_Words()
        {
            // Arrange
            var matrix = new List<string>
            {
                "abcdabcdabcdabcd",
                "abcdabcdabcdabcd",
                "abcdabcdabcdabcd",
                "abcdabcdabcdabcd",
                "abcdabcdabcdabcd",
                "abcdabcdabcdabcd",
                "abcdabcdabcdabcd",
                "abcdabcdabcdabcd",
                "abcdabcdabcdabcd",
                "abcdabcdabcdabcd"
            };

            var wordstream = new List<string> { "abcd", "efgh", "ijkl", "mnop", "qrst" };

            // Act
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);

            // Assert
            var expected = new List<string> { "abcd" };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Find_No_Words()
        {
            // Arrange
            var matrix = new List<string>
            {
                "xyzxyz",
                "xyzxyz",
                "xyzxyz",
                "xyzxyz",
                "xyzxyz"
            };

            var wordstream = new List<string> { "abc", "def", "ghi" };

            // Act
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Find_Word_With_Multiple_Occurrences()
        {
            // Arrange
            var matrix = new List<string>
            {
                "abcabc",
                "defdef",
                "ghighi",
                "jkljkl",
                "mnoman",
                "pqrpqr"
            };

            var wordstream = new List<string> { "abc", "def", "ghi", "jkl", "mno", "pqr" };

            // Act
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);

            // Assert
            var expected = new List<string> { "abc", "def", "ghi", "jkl", "pqr", "mno" };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Find_Words_With_Empty_Matrix()
        {
            // Arrange
            var matrix = new List<string> { };

            var wordstream = new List<string> { "abc", "def", "ghi" };

            // Act
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Find_Words_With_Empty_WordStream()
        {
            // Arrange
            var matrix = new List<string>
            {
                "abcabc",
                "defdef",
                "ghighi",
                "jkljkl",
                "mnoman",
                "pqrpqr"
            };

            var wordstream = new List<string> { };

            // Act
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Find_Words_With_Single_Character_Matrix()
        {
            // Arrange
            var matrix = new List<string>
            {
                "a",
                "a",
                "a",
                "a",
                "a"
            };

            var wordstream = new List<string> { "a" };

            // Act
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);

            // Assert
            var expected = new List<string> { "a" };
            Assert.Equal(expected, result);
        }
    }

}
