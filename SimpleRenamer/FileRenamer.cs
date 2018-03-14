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

            //todo reset counter for different image extensions
            GetAndRenameFiles(directoryPath, string.Format(NAME_TEMPLATE, Constants.JPG_EXTENSION), Constants.JPG_EXTENSION);
            GetAndRenameFiles(directoryPath, string.Format(NAME_TEMPLATE, Constants.JPEG_EXTENSION), Constants.JPEG_EXTENSION);
            GetAndRenameFiles(directoryPath, string.Format(NAME_TEMPLATE, Constants.PNG_EXTENSION), Constants.PNG_EXTENSION);
            GetAndRenameFiles(directoryPath, string.Format(NAME_TEMPLATE, Constants.BMP_EXTENSION), Constants.BMP_EXTENSION);
        }

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
            TryRenamePsdFile(originalName, desiredName);
        }

        private void RenameFile(string originalName, string desiredName, string msg = null)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                Logger.Log(msg);
            }

            File.Move(originalName, desiredName);
        }

        private void TryRenamePsdFile(string originalName, string desiredName)
        {
            string psdOriginal = string.Format(@"{0}\{1}.{2}",
                currentDirectory, Path.GetFileNameWithoutExtension(originalName), Constants.PSD_EXTENSION);

            if (File.Exists(psdOriginal))
            {
                string psdDesired = string.Format(@"{0}\{1}.{2}",
                    currentDirectory, Path.GetFileNameWithoutExtension(desiredName), Constants.PSD_EXTENSION);
                RenameFile(psdOriginal, psdDesired);
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
