namespace ProjectMethylamine.Source.Utility.Commands
{
    internal class ClearCommand : ICommand
    {
        public void Execute(ConsoleLogger logger, string input) => Console.Clear();

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
}