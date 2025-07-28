namespace ProjectMethylamine.Source.Utility
{
    /// <summary>
    /// Provides a lightweight logging interface for map operations.
    /// </summary>
    internal interface ILogger
    {
        String Log(string level, string message, bool isLine = true, bool isSilent = false, bool isWritten = true);

        void Log(string level, Exception ex, string message, bool isLine = true, bool isSilent = false, bool isWritten = true);
    }

    /// <summary>
    /// Simple console and file logger with timestamped entries, no archiving.
    /// </summary>
    internal class ConsoleLogger : ILogger
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
#pragma warning disable CA1305 // Specify IFormatProvider
            string time = DateTime.Now.ToString("HH:mm:ss");
#pragma warning restore CA1305 // Specify IFormatProvider
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
#pragma warning disable CA1305 // Specify IFormatProvider
            string time = DateTime.Now.ToString("HH:mm:ss");
#pragma warning restore CA1305 // Specify IFormatProvider
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