using System.Reflection;

namespace Anterpreter
{
    /* 
     * Interpreter fulfills two major requirements of the assignment - 
     *      1. Keeps track of a list of all Exercises 
     */
    static class Interpreter
    {

        private static readonly SortedList<uint, IExercise> ExerciseStore = new();
        private static readonly string ExercisesNamespace = "Anterpreter.Exercises";

        /*
         * Parse reads an input string and returns an Exercise.
         * 
         * If the input provided is an unsigned integer, we look in the
         * ExerciseStore by key and return the result.
         * 
         * Otherwise we loop through the entire store and compare the
         * lowercased name of the IExercise instance to the input and
         * return if a match if found.
         */
        private static IExercise Parse(string input)
        {
            if (input == null) return null;

            bool isUint = uint.TryParse(input, out uint option);

            if (isUint)
                return ExerciseStore.GetValueOrDefault(option);
            else
            {

                // uint.TryParse succeeded but exercise # does not exist.
                if (isUint)
                    return null;

                // User provided a string - could be an exercise name
                foreach (IExercise exercise in ExerciseStore.Values)
                {
                    if (exercise.GetType().Name.ToLower().Equals(input))
                        return exercise;
                }
                return null;
            }
        }

        private static string GetInput(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine().ToLower();
            return input;
        }

        private static void DisplayExercises()
        {
            Console.WriteLine("------------------");
            foreach (var exercise in ExerciseStore)
            {
                Console.WriteLine($"{exercise.Key}. {exercise.Value.GetType().Name.ToLower()}");
            }
            Console.WriteLine("------------------");
        }

        private static void RegisterExercise(IExercise exercise) { ExerciseStore.Add(exercise.Number(), exercise); }

        /*
         * This method performs some meta black magic.
         * 
         * It gets a list of all types in the currently executing assembly
         * and then filters them based on their namespace - if the namespace
         * equals the ExercisesNamespace field, we call CreateInstance on
         * this Type and register it via RegisterExercise by casting the
         * recently created instance into IExercise.
         * 
         * As I said, black magic.
         */
        public static void RegisterExercises()
        {
            object exercise;
            var types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => string.Equals(t.Namespace, ExercisesNamespace) && typeof(IExercise).IsAssignableFrom(t));
            foreach (Type type in types)
            {
                exercise = Activator.CreateInstance(type);
                RegisterExercise((IExercise) exercise);
            }
        }

        public static void RunLoop(string prompt = "> ")
        {
            string input;
            IExercise exercise;

            DisplayExercises();

            while (true)
            {

                input = GetInput(prompt);

                if (input.Equals("q") || input.Equals("quit"))
                    break;

                exercise = Parse(input);

                if (exercise == null)
                    Console.WriteLine("Invalid input provided. Please try again.");
                else
                    exercise.Run();
            }

            Console.WriteLine("Bye!");
        }

        public static void DisplayBanner()
        {
            Console.WriteLine(@"
   ___        __                       __         
  / _ | ___  / /____ _______  _______ / /____ ____
 / __ |/ _ \/ __/ -_) __/ _ \/ __/ -_) __/ -_) __/
/_/ |_/_//_/\__/\__/_/ / .__/_/  \__/\__/\__/_/   
                      /_/                         
");
        }

    }
}
