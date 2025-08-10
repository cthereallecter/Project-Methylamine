namespace ProjectMethylamine.Source.Utility
{
    internal interface ILogger
    {
        String Log(string level, string message, bool isLine = true, bool isSilent = false, bool isWritten = true);

        void Log(string level, Exception ex, string message, bool isLine = true, bool isSilent = false, bool isWritten = true);
    }

    public class ConsoleLogger : ILogger
    {
        private readonly string logPath = Path.Combine("Logs", "latest.log");
        private bool initialized;

        public ConsoleLogger()
        {
            InitializeLogFile();
        }

        private void InitializeLogFile()
        {
            if (initialized) return;

            Directory.CreateDirectory("Logs");

            // Optional: clear the file on startup or keep appending
            // File.WriteAllText(logPath, $"[INFO] Log started at {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n");

            initialized = true;
        }

        public String Log(string level, string message, bool isLine = true, bool isSilent = false, bool isWritten = true)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            string line = $"[{time}] [{level}] {message}";
            if (isSilent)
            {
                if (isWritten) File.AppendAllText(logPath, line + Environment.NewLine);
            }
            else
            {
                if (isLine)
                {
                    Console.WriteLine(line);
                }
                else
                {
                    Console.Write(line);
                }
                if (isWritten) File.AppendAllText(logPath, line + Environment.NewLine);
            }
            return line;
        }

        public void Log(string level, Exception ex, string message, bool isLine = true, bool isSilent = false, bool isWritten = true)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            string line = $"[{time}] [{level}] {message}: {ex}";
            if (isSilent)
            {
                if (isWritten) File.AppendAllText(logPath, line + Environment.NewLine);
            }
            else
            {
                if (isLine)
                {
                    Console.WriteLine(line);
                }
                else
                {
                    Console.Write(line);
                }
                if (isWritten) File.AppendAllText(logPath, line + Environment.NewLine);
            }
        }
    }
}