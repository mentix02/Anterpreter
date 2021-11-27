namespace Anterpreter.Commands
{
    internal class ListExercises : ICommand
    {

        public static void Run()
        {
            Console.WriteLine("Exercises -");
            foreach (var kvExercise in Interpreter.ExerciseStore)
                Console.WriteLine($"{kvExercise.Key}. {kvExercise.Value.GetType().Name.ToLower()}");
        }

        void ICommand.Run()
        {
            ListExercises.Run();
        }
    }
}
