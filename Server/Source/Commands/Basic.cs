using ProjectMethylamine.Source.Utility;

namespace ProjectMethylamine.Source.Commands
{
    internal class ClearCommand : ICommand
    {
        public void Execute(ConsoleLogger logger, string input) => Console.Clear();

        public void ShowHelp(ConsoleLogger logger)
        {
            throw new NotImplementedException();
        }
    }

    internal class HelpCommand : ICommand
    {
        public void Execute(ConsoleLogger logger, string input)
        {
            throw new NotImplementedException();
        }

        public void ShowHelp(ConsoleLogger logger)
        {
            throw new NotImplementedException();
        }
    }

    internal class UnknownCommand : ICommand
    {
        public void Execute(ConsoleLogger logger, string input)
        {
            logger.Log("HELP", $"{input} is not a valid command...");
        }

        public void ShowHelp(ConsoleLogger logger)
        {
            throw new NotImplementedException();
        }
    }

    internal class ServerCommand : ICommand
    {
        public void Execute(ConsoleLogger logger, string input)
        {
            logger.Log("SERVER", "This command is not implemented yet.");
        }
        public void ShowHelp(ConsoleLogger logger)
        {
            logger.Log("HELP", "Usage: server [options]");
            logger.Log("HELP", "Options: -start, -stop, -status");
        }
    }
}