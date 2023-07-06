namespace DealeronTest.Common
{
    public static class Utilities
    {
        public static string[] InputStringToArgs(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            return input.Replace("\t", " ").Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
