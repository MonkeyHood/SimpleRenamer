using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SimpleRenamer
{
    class FilenameComparer : IComparer<string>
    {
        public int Compare(string str, string other)
        {
            //https://stackoverflow.com/questions/4734116/find-and-extract-a-number-from-a-string
            int? strNumber = ExtractNumber(str);
            int? otherNumber = ExtractNumber(other);

            if (!strNumber.HasValue && !otherNumber.HasValue)
            {
                return 0;
            }
            else if(!strNumber.HasValue && otherNumber.HasValue)
            {
                return 1;
            }
            else if(strNumber.HasValue && !otherNumber.HasValue)
            {
                return -1;
            }
            else
            {
                return strNumber.Value - otherNumber.Value;
            }
        }

        private int? ExtractNumber(string str)
        {
            int? number = null;
            string potentialNumber = Regex.Match(str, @"\d+").Value;
            if(!string.IsNullOrEmpty(potentialNumber))
            {
                number = int.Parse(potentialNumber);
            }

            return number;
        }
    }
}
