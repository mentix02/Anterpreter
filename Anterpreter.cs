using System.Reflection;
using Anterpreter.Commands;
using Anterpreter.Exercises;

namespace Anterpreter
{
    /* 
     * Interpreter fulfills some major requirements of the assignment - 
     *      1. Keeps track of a list of all IExercise implementors
     *      2. Keeps track of a list of all ICommand implementors
     *      3. Provides a shell for executing both commands and exercises
     */
    static class Anterpreter
    {

        public static readonly SortedList<uint, IExercise> ExerciseStore = new();
        public static readonly Dictionary<string, ICommand> CommandStore = new();

        private static readonly string CommandsNamespace = "Anterpreter.Commands";
        private static readonly string ExercisesNamespace = "Anterpreter.Exercises";

        /*
         * If the input provided is an unsigned integer, we look in the
         * ExerciseStore by key and return the result.
         * 
         * Otherwise we loop through the entire store and compare the
         * lowercased name of the IExercise instance to the input and
         * return if a match if found.
         */
        private static IExercise GetExercise(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            bool isUint = uint.TryParse(input, out uint option);

            if (isUint)
                return ExerciseStore.GetValueOrDefault(option);
            else
            {
                // User provided a string - could be an exercise name
                foreach (IExercise exercise in ExerciseStore.Values)
                {
                    if (exercise.GetType().Name.ToLower().Equals(input))
                        return exercise;
                }
                return null;
            }
        }

        private static ICommand GetCommand(string input)
        {

            if (string.IsNullOrWhiteSpace(input))
                return null;

            if (CommandStore.ContainsKey(input))
                return CommandStore[input];

            foreach (var commandKv in CommandStore)
                if (commandKv.Key.StartsWith(input))
                    return commandKv.Value;

            return null;
        }

        private static string GetInput(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine().ToLower().Trim();
            return input;
        }

        private static void RegisterExercise(IExercise exercise) { ExerciseStore.Add(exercise.Number(), exercise); }

        private static void RegisterCommand(ICommand command) { CommandStore.Add(command.GetType().Name.ToLower(), command); }

        /*
         * This method performs some meta black magic.
         * 
         * It gets a list of all types in the currently executing assembly
         * and then filters them based on their namespace - if the namespace
         * equals either the ExercisesNamespace or CommandsNamespace field, we
         * call CreateInstance on this Type and register it via an appropriate method
         * by casting the recently created instance into the apprpriate iterface.
         * 
         * As I said, black magic.
         */
        public static void Initialize()
        {
            object exercise, command;
            var types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(
                    t => string.Equals(t.Namespace, ExercisesNamespace) ||
                         string.Equals(t.Namespace, CommandsNamespace)
                );
            foreach (Type type in types)
            {
                if (typeof(IExercise).IsAssignableFrom(type))
                {
                    exercise = Activator.CreateInstance(type);
                    RegisterExercise((IExercise)exercise);
                }
                else if (typeof(ICommand).IsAssignableFrom(type))
                {
                    command = Activator.CreateInstance(type);
                    RegisterCommand((ICommand)command);
                }
            }   
        }

        public static void RunLoop(string prompt = "> ")
        {
            string input;
            ICommand command;
            IExercise exercise;

            Banner.Run();
            Help.Run();
            ListExercises.Run();

            while (true)
            {

                input = GetInput(prompt);

                command = GetCommand(input);

                if (command != null)
                {
                    command.Run();
                    continue;
                }

                exercise = GetExercise(input);

                if (exercise != null)
                    exercise.Run();
                else if (string.IsNullOrWhiteSpace(input))
                    continue;
                else
                    Console.WriteLine("Invalid input provided. Please try again.");

            }

        }

    }
}
