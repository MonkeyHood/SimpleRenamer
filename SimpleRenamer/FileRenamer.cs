using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimpleRenamer
{
    public class FileRenamer
    {
        private const string NAME_TEMPLATE = "*.{0}";

        private string currentDirectory;

        public void CompressFileNumbers(string directoryPath)
        {
            currentDirectory = directoryPath;

            ChangeExtensionForFiles(directoryPath, string.Format(NAME_TEMPLATE, Constants.JPEG_EXTENSION), Constants.JPEG_EXTENSION);

            GetAndRenameFiles(directoryPath, string.Format(NAME_TEMPLATE, Constants.JPG_EXTENSION), Constants.JPG_EXTENSION);
            GetAndRenameFiles(directoryPath, string.Format(NAME_TEMPLATE, Constants.PNG_EXTENSION), Constants.PNG_EXTENSION);
            GetAndRenameFiles(directoryPath, string.Format(NAME_TEMPLATE, Constants.BMP_EXTENSION), Constants.BMP_EXTENSION);
        }

        private void ChangeExtensionForFiles(string directoryPath, string searchPattern, string extension)
        {
            const string FILENAME_TEMPLATE = @"{0}\{1}.{2}";
            string[] files = DirectoryUtils.GetAllFiles(directoryPath, searchPattern);
            foreach(string file in files)
            {
                string randomName = NameUtils.GenerateUnusedName(FILENAME_TEMPLATE, directoryPath, Constants.JPG_EXTENSION);
                RenameFile(file, randomName);
            }
        }

        /// <summary>
        /// Get all images that match the search pattern and rename them into a sequential list.
        /// </summary>
        private void GetAndRenameFiles(string directoryPath, string searchPattern, string extension)
        {
            string[] files = DirectoryUtils.GetAllFiles(directoryPath, searchPattern);
            Sort(ref files);
            RenameFiles(files, extension);
        }

        private void Sort(ref string[] files)
        {
            Array.Sort(files, new FilenameComparer());
        }

        private void Swap(ref string str, ref string other)
        {
            string temp = str;
            str = other;
            other = temp;
        }

        private void RenameFiles(string[] filenames, string extension)
        {
            RenameCollection renameCollection = new RenameCollection(filenames, extension, currentDirectory);
            List<RenameEntry> renameEntries = renameCollection.GetEntries();
            Logger.Log("Found {0} {1} images.", filenames.Length, extension);
            for(int i = 0; i < renameEntries.Count; ++i)
            {
                RenameEntry entry = renameEntries[i];
                if (entry.HasDifferentName)
                {
                    RenameFileAndPsd(entry.OriginalName, entry.NewName);
                }

                LogEntry(filenames.Length, i, entry);
            }
            Logger.Log("Done {0} images.", extension);
        }

        private void RenameFileAndPsd(string originalName, string desiredName, string msg = null)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                Logger.Log(msg);
            }

            RenameFile(originalName, desiredName);
            TryRenameAllPsdNameTypes(originalName, desiredName);
        }

        private void RenameFile(string originalName, string desiredName)
        {
            File.Move(originalName, desiredName);
        }

        private void TryRenameAllPsdNameTypes(string originalName, string desiredName)
        {
            const string PLAIN_NAME = @"{0}\{1}.{2}";
            const string BRACKET_NAME = @"{0}\{1} ({3}).{2}";
            const string EXTENSION_NAME = @"{0}\{1} {3}.{2}";

            string shortNameOriginal = Path.GetFileNameWithoutExtension(originalName);
            string extension = FileUtils.GetExtension(originalName);

            string shortNameDesired = Path.GetFileNameWithoutExtension(desiredName);
            string desiredPsd = string.Format(BRACKET_NAME, currentDirectory, shortNameDesired, Constants.PSD_EXTENSION, extension);

            TryRenameFile(desiredPsd, string.Format(PLAIN_NAME, currentDirectory, shortNameOriginal, Constants.PSD_EXTENSION));
            TryRenameFile(desiredPsd, string.Format(BRACKET_NAME, currentDirectory, shortNameOriginal, Constants.PSD_EXTENSION, extension));
            TryRenameFile(desiredPsd, string.Format(EXTENSION_NAME, currentDirectory, shortNameOriginal, Constants.PSD_EXTENSION, extension));
        }

        private void TryRenameFile(string desiredName, string originalName)
        {
            if (File.Exists(originalName))
            {
                RenameFile(originalName, desiredName);
            }
        }

        private static void LogEntry(int length, int i, RenameEntry entry)
        {
            string msg = string.Format("{0}/{1} {2}", i + 1, length,
                                entry.HasDifferentName ? string.Empty : "skipped");
            Logger.Log(msg);
        }
    }
}
