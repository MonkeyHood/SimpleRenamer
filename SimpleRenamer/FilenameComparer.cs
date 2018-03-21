using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SimpleRenamer
{
    class FilenameComparer : IComparer<string>, IEqualityComparer<string>
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

        public bool Equals(string str, string other)
        {
            int comparison = string.Compare(str, other, true);
            return comparison == 0;
        }

        public int GetHashCode(string obj)
        {
            return obj.ToLower().GetHashCode();
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
