using ProjectMethylamine.Source.Utility;

namespace ProjectMethylamine
{
    /// <summary>
    /// Main application entry point with automatic mod loading via CompilationUtility.
    /// </summary>
    public static class Program
    {
        private static int versionID = 0;
        private static int buildID = 1;
        public static String[] information = new string[16];

        private static ConsoleLogger logger = new ConsoleLogger();

        /// <summary>
        /// Application entry point.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            Display_InitializeProgram(args);
            CommandHandler.Input(logger);
        }

        private static void Display_InitializeProgram(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0] == "-v" && args[2] == "-b")
                {
                    information[versionID] = args[1];
                    information[buildID] = args[3].ToUpper();
                }
                else if (args[0] == "-b" && args[2] == "-v")
                {
                    information[versionID] = args[3];
                    information[buildID] = args[1].ToUpper();
                }
                logger.Log("INFO", $"Running Project Methylamine {information[versionID]}...");
            }
            else
            {
                if (File.Exists("version.txt"))
                {
                    information[versionID] =
                       File.ReadAllText("version.txt").ToString()!.Trim();
                }
                information[buildID] = "DEBUG";
                logger.Log("INFO", $"Running Project Methylamine {information[versionID]}...");
            }
        }
    }
}