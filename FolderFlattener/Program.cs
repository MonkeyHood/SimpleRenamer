using SimpleRenamer;

namespace FolderFlattener
{
    class Program
    {
        static void Main(string[] args)
        {
            FileMover fileMover = new FileMover();
            fileMover.FlattenFolder();
        }
    }
}
