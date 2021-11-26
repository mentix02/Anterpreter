namespace Anterpreter
{
    class Program
    {
        static void Main()
        {
            Interpreter.RegisterExercises();
            Interpreter.DisplayBanner();
            Interpreter.RunLoop();
        }
    }
}
