namespace QUTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Test
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

            var wordstream = new List<string> { "cold", "wind", "snow", "chill", "wind" };

            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);

            Console.WriteLine(string.Join(", ", result));
        }
    }
}
