namespace Anterpreter.Commands
{
    internal class Clear : ICommand
    {
        public static void Run()
        {
            Console.Clear();
        }

        void ICommand.Run()
        {
            Clear.Run();
        }

        public string Info()
        {
            return "Clears the screen";
        }
    }
}
