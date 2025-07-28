using ProjectMethylamine.Source.Utility;

namespace ProjectMethylamine
{
    /// <summary>
    /// Main application entry point with automatic mod loading via CompilationUtility.
    /// </summary>
    internal static class Program
    {
        private const int versionID = 0;
        private const int buildID = 1;
        private static readonly String[] information = ["v0.0.1", "DEBUG"];

        private static readonly ConsoleLogger logger = new();

        /// <summary>
        /// Application entry point.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            Display_InitializeProgram(args);
#pragma warning restore CA1062 // Validate arguments of public methods
            CommandHandler.Input(logger);
        }

        private static void Display_InitializeProgram(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0] == "-v" && args[2] == "-b")
                {
                    information[versionID] = args[1];
                    information[buildID] = args[3].ToUpper(System.Globalization.CultureInfo.CurrentCulture);
                    logger.Log("INFO", $"Running Project Methylamine {information[versionID]}...");
                }
            }
            else
            {
                if (File.Exists("version.txt"))
                {
                    information[versionID] =
                       File.ReadAllText("version.txt").ToString()!.Trim();

                information[buildID] = "DEBUG";
                logger.Log("INFO", $"Running Project Methylamine {information[versionID]}...");
                }
            }
        }
    }
}