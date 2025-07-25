namespace ProjectMethylamine.Source.Utility.Commands
{
    public class ClearCommand : ICommand
    {
        public void Execute(ConsoleLogger logger, string input) => Console.Clear();

        public void ShowHelp(ConsoleLogger logger)
        {
            throw new NotImplementedException();
        }
    }

    public class UnknownCommand : ICommand
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