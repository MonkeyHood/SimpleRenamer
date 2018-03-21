using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimpleRenamer
{
    public class FileMover
    {
        private const int DEFAULT_LEVELS = 1;

        private const string FOLDER_EXAMPLE = " ( Image, 5 x 5 pixels)";

        private Random random = new Random();

        public void FlattenFolder(int levels = DEFAULT_LEVELS)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string[] allFolders = DirectoryUtils.GetAllFolders(currentDirectory);

            Logger.Log(LogLevel.Warning, "Looking for folder named with the style '{0}'", FOLDER_EXAMPLE);
            Logger.Log(LogLevel.Warning, "Found {0} potential subfolders.", allFolders.Length);

            foreach(string sourceFolderPath in allFolders)
            {
                Logger.Log(LogLevel.Info, "Processing folder {0}", sourceFolderPath);
                string folderName = Path.GetFileName(sourceFolderPath);
                if(FolderIsValid(folderName))
                {
                    //this only goes one level deep
                    MoveAllFilesToFolder(sourceFolderPath, currentDirectory);
                }
            }
        }

        /// <summary>
        /// Checks if the folder has a valid name.
        /// 
        /// We do this because sometimes when saving images,
        /// they get saved to a subfolder instead of the selected folder.
        /// All these subfolders appear to have '([Z] x [Y] pixels)'.
        /// </summary>
        /// <param name="foldername"></param>
        /// <returns></returns>
        private bool FolderIsValid(string foldername)
        {
            const string REGEX_PATTERN = @"(.+)\(.+Image, (\d+)(.+)(\d+) pixels\)";
            if(string.IsNullOrEmpty(foldername))
            {
                return false;
            }

            return Regex.Match(foldername, REGEX_PATTERN).Success;
        }

        private void MoveAllFilesToFolder(string sourceFolder, string destinationFolder)
        {
            string[] allFiles = DirectoryUtils.GetAllFiles(sourceFolder, "*");
            foreach(string file in allFiles)
            {
                string name = Path.GetFileNameWithoutExtension(file);
                string extension = FileUtils.GetExtension(file);
                string newFilePath = GetUniqueName(name, extension, destinationFolder);
                File.Move(file, newFilePath);
            }

            DirectoryUtils.DeleteFolder(sourceFolder);
        }

        private string GetUniqueName(string filename, string extension, string destinationFolder)
        {
            const int LENGTH = 5;
            string newFilename;
            do
            {
                newFilename = string.Format("{0}\\{1}.{2}",
                    destinationFolder, filename + GetRandomString(LENGTH), extension);
            }
            while (File.Exists(newFilename));
            return newFilename;
        }

        private string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
