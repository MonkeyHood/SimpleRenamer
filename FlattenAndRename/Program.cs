using SimpleRenamer;

namespace FlattenAndRename
{
    class Program
    {
        static void Main(string[] args)
        {
            FileMover fileMover = new FileMover();
            FileRenamer fileRenamer = new FileRenamer();

            fileMover.FlattenFolder();
            fileRenamer.CompressFileNumbers();
        }
    }
}
