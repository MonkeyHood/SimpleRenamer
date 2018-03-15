using System;
using System.IO;
using System.Linq;

namespace SimpleRenamer
{
    public static class NameUtils
    {
        private static Random random = new Random();

        /// <summary>
        /// Get a filename that does not exist in the directory.
        /// </summary>
        /// <param name="template">The template must have 3 slots.</param>
        /// <returns></returns>
        public static string GenerateUnusedName(string template, string directory, string extension)
        {
            const int RANDOM_LENGTH = 10;
            string filename;
            do
            {
                filename = string.Format(template, directory, GetRandomString(RANDOM_LENGTH), extension);
            }
            while (File.Exists(filename));

            return filename;
        }

        private static string GetRandomString(int length)
        {
            const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(CHARS, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
