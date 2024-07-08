using BenchmarkDotNet.Attributes;

namespace QUTest
{
    public class WordFinderBenchmark
    {
        private WordFinder wordFinder;
        private List<string> matrix;
        private List<string> wordStream;

        [GlobalSetup]
        public void Setup()
        {
            matrix = new List<string>
            {
                "chill",
                "ooool",
                "linnd",
                "doldd",
                "chill"
            };

            wordStream = new List<string> { "chill", "cold", "wind", "cool" };
            wordFinder = new WordFinder(matrix);
        }

        [Benchmark]
        public void FindWordsBenchmark()
        {
            wordFinder.Find(wordStream);
        }

        [Benchmark]
        public void FindWordsWithSingleCharacterMatrixBenchmark()
        {
            var singleCharMatrix = new List<string>
            {
                "a",
                "a",
                "a",
                "a",
                "a"
            };
            var singleCharWordFinder = new WordFinder(singleCharMatrix);
            singleCharWordFinder.Find(new List<string> { "a" });
        }
    }
}
