namespace Anterpreter.Exercises
{
    internal class Conversions : IExercise
    {
        public uint Number()
        {
            return 1;
        }

        private static bool ConversionsForInt(string input)
        {

            // Using int.TryParse
            bool convertable = int.TryParse(input, out int intFromTryParse);
            
            if (!convertable) return false;

            // Using int.parse
            int intUsingIntParse = int.Parse(input);
            Console.WriteLine($"Int from int.Parse: {intUsingIntParse}");

            // Using Convert.ToInt32
            int intUsingConvertToInt = Convert.ToInt32(input);
            Console.WriteLine($"Int from Convert.ToInt32: {intUsingConvertToInt}");

            Console.WriteLine($"Int from int.TryParse: {intFromTryParse}");

            return true;
        }

        private static bool ConversionsForFloat(string input)
        {
            // Using float.TryParse
            bool convertable = float.TryParse(input, out float floatFromTryParse);

            if (!convertable) return false;

            // Using float.parse
            float floatUsingFloatParse = float.Parse(input);
            Console.WriteLine($"Float from float.Parse: {floatUsingFloatParse}");

            Console.WriteLine($"Float from float.TryParse: {floatFromTryParse}");

            return true;
        }

        private static bool ConversionsForBool(string input)
        {
            // Using bool.TryParse
            bool convertable = bool.TryParse(input.Trim(), out bool boolFromTryParse);

            if (!convertable) return false;

            // Using boolean.parse
            bool boolUsingBoolParse = bool.Parse(input);
            Console.WriteLine($"Bool from bool.Parse: {boolUsingBoolParse}");

            // Using Convert.ToBoolean
            bool boolUsingConvertToBoolean = Convert.ToBoolean(input.Trim());
            Console.WriteLine($"Bool from Convert.ToBoolean: {boolUsingConvertToBoolean}");

            return true;
        }

        public void Run()
        {
            string input;
            bool isInt, isFloat, isBool;

            do
            {
                Console.Write("Enter an int: ");
                input = Console.ReadLine();
                isInt = ConversionsForInt(input);

                if (!isInt)
                    Console.WriteLine("Please enter a valid integer.");
            } while (!isInt);

            do
            {
                Console.Write("Enter a float: ");
                input = Console.ReadLine();
                isFloat = ConversionsForFloat(input);

                if (!isFloat)
                    Console.WriteLine("Please enter a valid float.");
            } while (!isFloat);

            do
            {
                Console.Write("Enter a bool: ");
                input = Console.ReadLine();
                isBool = ConversionsForBool(input);

                if (!isBool)
                    Console.WriteLine("Please enter a valid boolean.");
            } while (!isBool);

        }
    }
}
