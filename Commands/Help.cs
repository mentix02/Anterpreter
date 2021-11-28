﻿namespace Anterpreter.Commands
{
    internal class Help : ICommand
    {

        public static void Run()
        {
            Console.WriteLine("Commands -");
            foreach (var kvCommand in Anterpreter.CommandStore)
                Console.WriteLine($"- {kvCommand.Key}");
        }

        void ICommand.Run()
        {
            Help.Run();
        }

    }
}