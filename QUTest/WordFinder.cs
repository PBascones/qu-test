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
            if (matrix == null || !matrix.Any())
            {
                return;
                //throw new ArgumentNullException(nameof(matrix));
            }

            // Initialize values
            rows = matrix.Count();
            cols = matrix.First().Length;
            mainMatrix = new char[rows, cols];
            charPositions = new Dictionary<char, List<(int, int)>>();

            // Fill mainMatrix
            var i = 0;
            foreach (var row in matrix)
            {
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
            // HashSet to skip duplicates from wordStream
            var words = new HashSet<string>(wordStream);

            // Dictionary to count the matches
            var wordCount = new Dictionary<string, int>();

            foreach (var word in words)
            {
                // Search for the word in the matrix
                var count = CountOccurrences(word);
                if (count > 0)
                    wordCount[word] = count;
            }

            // Get top 10
            return wordCount.OrderByDescending(x => x.Value)
                             .Take(10)
                             .Select(x => $"'{x.Key}' appeared {x.Value} time/s");
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
            return SearchHorizontally(row, col, word) || SearchVertically(row, col, word);
        }

        private bool SearchHorizontally(int row, int col, string word)
        {
            // Validate remaining lenght to avoid unnecessary searches
            if (col + word.Length > cols)
                return false;

            for (var i = 0; i < word.Length; i++)
            {
                if (mainMatrix[row, col + i] != word[i])
                    return false;
            }

            return true;
        }

        private bool SearchVertically(int row, int col, string word)
        {
            // Validate remaining lenght to avoid unnecessary searches
            if (row + word.Length > rows)
                return false;

            for (var i = 0; i < word.Length; i++)
            {
                if (mainMatrix[row + i, col] != word[i])
                    return false;
            }

            return true;
        }
    }
}

