using Xunit;

namespace QUTest.Tests
{
    public class WordFinderTests
    {
        #region  Null Or Empty Tests

        [Fact]
        public void Constructor_NullMatrix_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new WordFinder(null));
        }

        [Fact]
        public void Constructor_EmptyMatrix_ThrowsArgumentException()
        {
            var emptyMatrix = new List<string>();
            Assert.Throws<ArgumentException>(() => new WordFinder(emptyMatrix));
        }

        [Fact]
        public void Constructor_MatrixWithEmptyRow_ThrowsArgumentException()
        {
            var matrixWithEmptyRow = new List<string> { "abc", "def", "" };
            Assert.Throws<ArgumentException>(() => new WordFinder(matrixWithEmptyRow));
        }

        [Fact]
        public void Constructor_MatrixWithInconsistentRowLengths_ThrowsArgumentException()
        {
            var inconsistentMatrix = new List<string> { "abc", "defg", "hij" };
            Assert.Throws<ArgumentException>(() => new WordFinder(inconsistentMatrix));
        }

        [Fact]
        public void Find_NullWordStream_ThrowsArgumentNullException()
        {
            var matrix = new List<string> { "abc", "def", "ghi" };
            var wordFinder = new WordFinder(matrix);
            Assert.Throws<ArgumentNullException>(() => wordFinder.Find(null));
        }

        #endregion

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
            var expected = new List<string>
            {
                string.Format(Constants.RESULT_MESSAGE, "chill", 2),
                string.Format(Constants.RESULT_MESSAGE, "cold", 1),
            };
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
            var expected = new List<string>
            {
                string.Format(Constants.RESULT_MESSAGE, "abcd", 40),
            };
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
            var expected = new List<string>
            {
                string.Format(Constants.RESULT_MESSAGE, "a", 5)
            };

            Assert.Equal(expected, result);
        }
    }

}
