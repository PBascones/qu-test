using System.Collections.Concurrent;

namespace QUTest
{
    public class WordFinder
    {
        private readonly char[,] mainMatrix;
        private readonly int rows;
        private readonly int cols;
        private readonly Dictionary<char, List<(int, int)>> charPositions;

        public WordFinder(IEnumerable<string> matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix), Constants.ValidationErrors.MATRIX_NULL);
            }

            // Initialize values
            rows = matrix.Count();
            if (this.rows == 0)
            {
                throw new ArgumentException(Constants.ValidationErrors.MATRIX_EMPTY, nameof(matrix));
            }

            cols = matrix.First().Length;
            if (this.cols == 0)
            {
                throw new ArgumentException(Constants.ValidationErrors.MATRIX_EMPTY_ROWS, nameof(matrix));
            }

            mainMatrix = new char[rows, cols];
            charPositions = new Dictionary<char, List<(int, int)>>();

            var i = 0;
            // Fill mainMatrix
            foreach (var row in matrix)
            {
                if (row.Length != cols)
                {
                    throw new ArgumentException(Constants.ValidationErrors.MATRIX_ROWS_SAME_LENGHT, nameof(matrix));
                }

                for (var j = 0; j < cols; j++)
                {
                    // Add variable to improve readability
                    var letter = row[j];
                    mainMatrix[i, j] = letter;

                    // We use this "charPositions" to know where that letter is located, to improve searchs
                    if (!charPositions.ContainsKey(letter))
                        charPositions[letter] = new List<(int, int)>();

                    charPositions[letter].Add((i, j));
                }
                i++;
            }
        }

        public IEnumerable<string> Find(IEnumerable<string> wordStream)
        {
            if (wordStream == null)
            {
                throw new ArgumentNullException(nameof(wordStream), Constants.ValidationErrors.WORD_STREAM_NULL);
            }

            // HashSet to skip duplicates from wordStream
            var words = new HashSet<string>(wordStream);

            // Dictionary to count the matches
            var wordCount = new ConcurrentDictionary<string, int>();

            Parallel.ForEach(words, word =>
            {
                // Search for the word in the matrix
                var count = CountOccurrences(word);
                if (count > 0)
                    wordCount[word] = count;
            });

            // The following return harms the unit tests:
            // Get top 10
            return wordCount.OrderByDescending(x => x.Value)
                             .Take(10)
                             .Select(x => string.Format(Constants.RESULT_MESSAGE, x.Key, x.Value));
        }

        private int CountOccurrences(string word)
        {
            var count = 0;

            // If the first letter isn't in the mainMatrix (charPositions), then skip it
            if (!charPositions.ContainsKey(word[0]))
                return count;

            // Search just from the stored positions (avoiding brute force algoritm)
            foreach (var (row, col) in charPositions[word[0]])
            {
                if (SearchFromPosition(row, col, word))
                {
                    // If found, sum 1 to the counter
                    count++;
                }
            }

            return count;
        }

        private bool SearchFromPosition(int row, int col, string word)
        {
            return Search(row, col, word, true) || Search(row, col, word, false);
        }

        private bool Search(int row, int col, string word, bool isHorizontal)
        {
            var maxIndex = (isHorizontal ? col : row) + word.Length;
            var limit = isHorizontal ? cols : rows;

            // Validate remaining length to avoid unnecessary searches
            if (maxIndex > limit)
                return false;

            for (var i = 0; i < word.Length; i++)
            {
                var matrixChar = isHorizontal ? mainMatrix[row, col + i] : mainMatrix[row + i, col];
                if (matrixChar != word[i])
                    return false;
            }

            return true;
        }
    }
}

