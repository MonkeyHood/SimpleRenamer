using System.Collections.Generic;
using System.IO;

namespace SimpleRenamer
{
    public class RenameListGenerator
    {
        private const string FULL_NAME_TEMPLATE = "{0}\\image {1}.{2}";
        private const int START_NUMBER = 1;

        private string currentDirectory;
        private string extension;

        public RenameListGenerator(string currentDirectory, string extension)
        {
            this.currentDirectory = currentDirectory;
            this.extension = extension;
        }

        public string[] GetDesiredNamesList(int count)
        {
            string[] desiredNames = new string[count];
            for(int i = 0; i < count; ++i)
            {
                desiredNames[i] = GenerateDesiredname(i + 1);
            }

            return desiredNames;
        }

        private string GenerateDesiredname(int count)
        {
            //https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-pad-a-number-with-leading-zeros
            return string.Format(FULL_NAME_TEMPLATE, currentDirectory, count.ToString("D5"), extension);
        }

        private bool EqualContents(string str, string other, bool ignoreCase = true)
        {
            return string.Compare(str, other, ignoreCase) == 0;
        }
    }
}
