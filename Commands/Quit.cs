namespace Anterpreter.Commands
{
    internal class Quit : ICommand
    {
        public static void Run()
        {
            Console.WriteLine("Bye");
            Environment.Exit(Environment.ExitCode);
        }

        void ICommand.Run()
        {
            Quit.Run();
        }
    }
}
