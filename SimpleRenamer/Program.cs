using System;
using System.IO;

namespace SimpleRenamer
{
    class Program
    {
        const string STARTUP_MESSAGE = "Simple Renamer\n" +
            "This program renames files so there is no gap between images numbers.\n" +
            "Only images with the '" + IMAGE_NAME_PREFIX + "' will be renamed.\n" +
            Constants.JPG_EXTENSION + " and " + Constants.PNG_EXTENSION + " will be sorted separately.\n";
        const string CONTINUE_MESSAGE = "Press Y to continue, N to cancel";
        const string IMAGE_NAME_PREFIX = "image";
        
        const string DECLINE = "N";
        const string APPROVE = "Y";

        const int NORMAL_EXIT = 0;
        const int EQUAL_STRING_VALUE = 0;

        private static FileRenamer renamer = new FileRenamer();
        private static FileMover fileMover = new FileMover();

        static void Main(string[] args)
        {
            Logger.Log(STARTUP_MESSAGE);
            Logger.Log("Current director: {0}", Directory.GetCurrentDirectory());
            //string input = PauseConsole(CONTINUE_MESSAGE);
            //if(string.Compare(input, DECLINE, true) == EQUAL_STRING_VALUE)
            //{
            //    Environment.Exit(NORMAL_EXIT);
            //}
            //else if(string.Compare(input, APPROVE, true) == EQUAL_STRING_VALUE)
            {
                fileMover.FlattenFolder();
                renamer.CompressFileNumbers(Directory.GetCurrentDirectory());
                Logger.Log("Rename complete!");
                PauseConsole("Press any key to exit");
                Environment.Exit(NORMAL_EXIT);
            }
        }

        private static string PauseConsole(string msg, params object[] args)
        {
            if(!string.IsNullOrEmpty(msg))
            {
                Logger.Log(msg, args);
            }

            return Console.ReadLine();
        }
    }
}
