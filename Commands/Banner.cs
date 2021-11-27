namespace Anterpreter.Commands
{
    internal class Banner : ICommand
    {

        public static void Run()
        {
            Console.WriteLine(@"
   ___        __                       __         
  / _ | ___  / /____ _______  _______ / /____ ____
 / __ |/ _ \/ __/ -_) __/ _ \/ __/ -_) __/ -_) __/
/_/ |_/_//_/\__/\__/_/ / .__/_/  \__/\__/\__/_/   
                      /_/                         
");
        }

        void ICommand.Run()
        {
            Banner.Run();
        }

    }
}
