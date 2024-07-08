using BenchmarkDotNet.Running;

namespace QUTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to WordFinder, built for QU Beyond!");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Manual path");
            Console.WriteLine("2. Automatic path");
            Console.WriteLine("3. Run benchmarks");
            Console.WriteLine("Press '1' for Manual path, '2' for Automatic path (matrix and wordStream with values) or '3' to run some Benchmarks.");

            var key = Console.ReadKey().Key;
            Console.Clear();

            if (key == ConsoleKey.D1)
            {
                RunManualPath();
            }
            else if (key == ConsoleKey.D2)
            {
                RunAutomaticPath();
            }
            else if (key == ConsoleKey.D3)
            {
                RunBenchmarks();
            }
            else
            {
                Console.WriteLine("Invalid option. Exiting...");
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void RunManualPath()
        {
            Console.WriteLine("Enter the matrix (type 'done' when finished):");

            var matrix = new List<string>();
            var rowLength = -1;
            while (true)
            {
                var input = Console.ReadLine();
                if (input.ToLower() == "done")
                {
                    break;
                }

                // Validate the lenght of the given word
                if (rowLength == -1)
                {
                    rowLength = input.Length;
                }
                else if (input.Length != rowLength)
                {
                    Console.WriteLine(Constants.ValidationErrors.MATRIX_ROWS_SAME_LENGHT);
                    continue;
                }

                matrix.Add(input);
            }

            if (matrix.Count == 0)
            {
                Console.WriteLine(Constants.ValidationErrors.MATRIX_EMPTY);
                return;
            }

            var wordFinder = new WordFinder(matrix);

            Console.WriteLine("Enter words to search (separated by commas):");
            var wordStreamInput = Console.ReadLine();
            var wordStream = wordStreamInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var results = wordFinder.Find(wordStream);

            Console.WriteLine("Top 10 Words Found:");
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        private static void RunAutomaticPath()
        {
            var matrix = new List<string>
            {
                "aabcdcu",
                "afgwiou",
                "achillu",
                "apqnsdu",
                "auvdxyu",
                "awinddu",
                "awinddu",
            };

            var wordStream = new List<string> { "cold", "wind", "snow", "chill", "wind" };

            Console.WriteLine($"Matrix: \n{string.Join("\n", matrix)}");
            Console.WriteLine($"WordStream: {string.Join(", ", wordStream)}");

            var wordFinder = new WordFinder(matrix);
            var results = wordFinder.Find(wordStream);

            Console.WriteLine($"Top 10 Words Found (Automatic Path): \n{string.Join("\n", results)}");
        }

        private static void RunBenchmarks()
        {
            BenchmarkRunner.Run<WordFinderBenchmark>();
        }
    }
}
