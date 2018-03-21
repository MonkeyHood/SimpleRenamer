namespace SimpleRenamer
{
    public static class ArrayUtils
    {
        public static bool Contains(this string[] array, string str, bool ignoreCase)
        {
            bool hasStr = false;

            foreach(string item in array)
            {
                if(string.Compare(str, item, ignoreCase) == 0)
                {
                    hasStr = true;
                    break;
                }
            }

            return hasStr;
        }

        public static bool ContainsSubstring(this string[] array, string str, bool ignoreCase)
        {
            bool hasStr = false;

            foreach (string item in array)
            {
                string shortItem = shortItem = DirectoryUtils.GetDirectoryname(item);

                if (string.Compare(shortItem, str, ignoreCase) == 0)
                {
                    hasStr = true;
                    break;
                }
            }

            return hasStr;
        }
    }
}
