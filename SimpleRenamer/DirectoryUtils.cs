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
    }
}
