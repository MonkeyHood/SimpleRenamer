namespace SimpleRenamer
{
    /// <summary>
    /// A container class to hold an original filename and it's new name.
    /// </summary>
    class RenameEntry
    {
        public string OriginalName { get; private set; }
        public string NewName { get; private set; }

        public string Extension { get; private set; }

        public bool HasDifferentName
        {
            get { return HasOriginalName && OriginalName != NewName; }
        }

        public bool HasOriginalName { get { return !string.IsNullOrEmpty(OriginalName); } }

        public RenameEntry(string name)
        {
            NewName = name;
            Extension = FileUtils.GetExtension(NewName);
        }

        public void SetOriginalName(string originalName)
        {
            OriginalName = originalName;
        }

        public void SetSameName()
        {
            OriginalName = NewName;
        }
    }
}
