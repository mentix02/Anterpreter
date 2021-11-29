namespace Anterpreter.Commands
{
    internal class ListExercises : ICommand
    {

        public static void Run()
        {
            Console.WriteLine("Exercises -");
            foreach (var kvExercise in Anterpreter.ExerciseStore)
            {
                Console.Write($"{kvExercise.Key}.");
                if (kvExercise.Key > 9)
                    Console.WriteLine($" {kvExercise.Value.GetType().Name.ToLower()}");
                else
                    Console.WriteLine($"  {kvExercise.Value.GetType().Name.ToLower()}");
            }
        }

        void ICommand.Run()
        {
            ListExercises.Run();
        }

        public string Info()
        {
            return "Lists all exercises";
        }
    }
}
