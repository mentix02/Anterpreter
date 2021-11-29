namespace Anterpreter.Commands
{
    internal class Help : ICommand
    {

        public static void Run()
        {
            Console.WriteLine("Commands -");
            foreach (var kvCommand in Anterpreter.CommandStore)
                Console.WriteLine($"- {kvCommand.Key}: {kvCommand.Value.Info()}");
        }

        void ICommand.Run()
        {
            Help.Run();
        }

        public string Info()
        {
            return "Lists all commands";
        }

    }
}
