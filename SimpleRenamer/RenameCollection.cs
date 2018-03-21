using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SimpleRenamer
{
    /// <summary>
    /// A collection of RenameEntries.
    /// </summary>
    class RenameCollection
    {
        private Dictionary<string, RenameEntry> _entries;

        public bool HasIncompleteEntries
        {
            get
            {
                bool hasIncompleteEntry = false;

                foreach(KeyValuePair<string, RenameEntry> kvp in _entries)
                {
                    if(!kvp.Value.HasOriginalFile)
                    {
                        hasIncompleteEntry = true;
                        break;
                    }

                }

                return hasIncompleteEntry;
            }
        }

        public RenameCollection(string[] originalFilenames, string extension, string currentDirectory)
        {
            RenameListGenerator generator = new RenameListGenerator(currentDirectory, extension);
            string[] desiredNames = generator.GetDesiredNamesList(originalFilenames.Length);
            SetupEntries(desiredNames);
            CreateMatchingValues(originalFilenames.ToList());
        }

        private void SetupEntries(string[] keys)
        {
            _entries = new Dictionary<string, RenameEntry>(keys.Length, new FilenameComparer());

            foreach (string desiredFilename in keys)
            {
                RenameEntry entry = new RenameEntry(desiredFilename);
                _entries.Add(entry.NewName, entry);
            }
        }

        private void CreateMatchingValues(List<string> originalFilenames)
        {
            for(int i = 0; i < originalFilenames.Count; ++i)
            {
                string original = originalFilenames[i];
                bool foundEntry = _entries.TryGetValue(original, out RenameEntry entry);
                if(foundEntry)
                {
                    entry.SetSameName();

                    originalFilenames.RemoveAt(i);
                    --i;
                }
            }

            List<RenameEntry> unmatchedEntries = 
                _entries.Where((kvp) => !kvp.Value.HasOriginalFile).Select((kvp) => kvp.Value)
                .ToList();

            Debug.Assert(unmatchedEntries.Count() == originalFilenames.Count,
                "Unmatched items.");

            for(int i = 0; i < unmatchedEntries.Count; ++i)
            {
                unmatchedEntries[i].SetOriginalName(originalFilenames[i]);
            }
        }

        public List<RenameEntry> GetEntries()
        {
            return _entries.Values.ToList();
        }

        private RenameEntry TryGetValue(string desiredName)
        {
            _entries.TryGetValue(desiredName, out RenameEntry entry);
            return entry;
        }
    }
}
