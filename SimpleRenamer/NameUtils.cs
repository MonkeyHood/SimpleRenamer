using System;
using System.IO;
using System.Linq;

namespace SimpleRenamer
{
    public static class NameUtils
    {
        private static Random random = new Random();

        public static string GetNextUnusedFilename(string path, string filename, string extension,
            ref int counter)
        {
            const string NAME_TEMPLATE = @"{0}\{1}\image {2}.{3}";
            string newFilename;

            do
            {
                newFilename = string.Format(NAME_TEMPLATE, path, filename, counter.ToString("D5"), extension);
                ++counter;
            }
            while (File.Exists(newFilename));

            return newFilename;
        }

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
