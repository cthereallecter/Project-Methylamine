using ProjectMethylamine.Source.Utility;
using ProjectMethylamine.Source.Utility.Netting;
using System;

namespace ProjectMethylamine
{
    internal static class Program
    {
        private static String[] information = ["v0.0.1", "DEBUG"];

        private static readonly ConsoleLogger logger = new();

        public static async Task Main(string[] args)
        {
            // Display_InitializeProgram(args); 
            
            var webServer = new WebServer(
                domain: "thehidout.cthereallecter.com",
                httpsPort: 443,
                staticFileRoot: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Web")
            );

            logger.Log("INFO", $"Starting web server...");
            await webServer.StartAsync();

            InitializeProgram(args);
        }

        private static void InitializeProgram(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0] == "-v" && args[2] == "-b")
                {
                    information[0] = args[1];
                    information[1] = args[3].ToUpper(System.Globalization.CultureInfo.CurrentCulture);
                    logger.Log("INFO", $"Running Project Methylamine {information[0]}...");
                }
            }
            else
            {
                if (File.Exists("version.txt"))
                {
                    information[0] =
                       File.ReadAllText("version.txt").ToString()!.Trim();

                    information[1] = "DEBUG";
                    logger.Log("INFO", $"Running Project Methylamine {information[0]}...");
                }
            }
        }
    }
}