using System.Collections.Generic;
using System.IO;

namespace SimpleRenamer
{
    public static class DirectoryUtils
    {
        public static string[] GetAllFiles(string directory, string searchPattern,
            SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if(string.IsNullOrEmpty(directory))
            {
                directory = Directory.GetCurrentDirectory();
            }

            return Directory.GetFiles(directory, searchPattern, option);
        }

        public static string[] GetAllImages(string directory, SearchOption searchOption = SearchOption.TopDirectoryOnly,
            bool jpg = true, bool png = true, bool bmp = true)
        {
            List<string> images = new List<string>();

            images.AddRange(GetAllFiles(directory, "*.jpg", searchOption));
            images.AddRange(GetAllFiles(directory, "*.jpeg", searchOption));
            images.AddRange(GetAllFiles(directory, "*.png", searchOption));
            images.AddRange(GetAllFiles(directory, "*.bmp", searchOption));

            return images.ToArray();
        }

        public static string[] GetAllFolders(string directory, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return Directory.GetDirectories(directory, "*", searchOption);
        }

        public static void DeleteFolder(string folderPath, bool onlyIfEmpty = true)
        {
            string[] files = GetAllFiles(folderPath, "*");
            if(onlyIfEmpty && files.Length > 0)
            {
                return;
            }

            Directory.Delete(folderPath, false);
        }

        public static void CreateDirectoryIfNeeded(string directoryPath)
        {
            if(!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public static string GetDirectoryname(string file)
        {
            return new DirectoryInfo(file).Name;
        }
    }
}
