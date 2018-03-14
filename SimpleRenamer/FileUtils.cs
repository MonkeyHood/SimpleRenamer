using System.IO;

namespace SimpleRenamer
{
    public static class FileUtils
    {
        public static string GetExtension(string filename)
        {
            //Path.GetExtension() return a '.' as the first character but we don't want that
            const int EXTENSION_START_INDEX = 1;

            string extension = Path.GetExtension(filename);
            extension = extension.Substring(EXTENSION_START_INDEX, extension.Length - EXTENSION_START_INDEX);
            return extension;
        }
    }
}
