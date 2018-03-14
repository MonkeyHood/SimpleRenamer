using SimpleRenamer;
using System.IO;

namespace Renamer
{
    class Program
    {
        static void Main(string[] args)
        {
            FileRenamer fileRenamer = new FileRenamer();
            fileRenamer.CompressFileNumbers(Directory.GetCurrentDirectory());
        }
    }
}
