using ProjectMethylamine.Source.Utility.Commands;
using ProjectMethylamine.Source.Utility.Commands.Testing;

namespace ProjectMethylamine.Source.Utility
{
    public interface ICommand
    {
        void Execute(ConsoleLogger logger, string input);

        void ShowHelp(ConsoleLogger logger);
    }

    public static class CommandHandler
    {
        private static readonly Dictionary<string, ICommand> commands = new()
        {
            ["clear"] = new ClearCommand(),
            ["mappr"] = new MapprCommand(),
            ["pakr"] = new PakrCommand()
        };

        public static void InvokeCommand(ConsoleLogger logger, string input)
        {
            var baseCommand = input.Split(' ')[0]; // e.g., "pakr -p" => "pakr"
            if (commands.TryGetValue(baseCommand, out var command))
                command.Execute(logger, input.Trim());
            else
                new UnknownCommand().Execute(logger, input);
        }

        public static void Input(ConsoleLogger logger, bool repeats = true)
        {
            while (repeats)
            {
                logger.Log("TERMINAL", "E:\\Core> ", isLine: false, isSilent: false, isWritten: false);
                var input = Console.ReadLine()!.Trim();
                logger.Log("TERMINAL", $"E:\\Core> {input}", isLine: true, isSilent: true, isWritten: true);

                InvokeCommand(logger, input);
            }
        }
    }
}