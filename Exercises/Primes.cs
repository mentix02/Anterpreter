namespace Anterpreter.Exercises
{
    internal class Primes : IExercise
    {

        private uint lower, upper;

        public uint Number()
        {
            return 3;
        }

        public void Run()
        {
            string input;
            bool firstNumValid, secondNumValid;

            Console.WriteLine("Please enter two numbers -");
            Console.WriteLine("1. a number between 2 and 999");
            Console.WriteLine("2. a number greater than the first number and less than or equal to 1000\n");
            Console.WriteLine("*note* numbers cannot be equal\n");
            do
            {
                Console.Write("Enter the first number: ");
                input = Console.ReadLine();
                firstNumValid = uint.TryParse(input, out lower);
                if (!firstNumValid || lower <= 1 || lower > 999)
                    Console.WriteLine("Please enter a valid first number according to given constraints!");
            } while (!firstNumValid || lower <= 1 || lower > 999);

            Console.WriteLine();

            do
            {
                Console.Write("Enter the second number: ");
                input = Console.ReadLine();
                secondNumValid = uint.TryParse(input,out upper);
                if (!secondNumValid || upper <= lower || upper > 1000)
                    Console.WriteLine("Please enter a valid second number according to given constrains!");
            } while (!secondNumValid || upper <= lower || upper > 1000);

            PrintPrimes();
        }

        private static bool IsPrime(uint number)
        {
            if (number == 2) return true;
            if ((number & 1) != 1) return false;

            for (uint i = 3; i <= (uint) Math.Floor(Math.Sqrt(number)); i += 2)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }

        private void PrintPrimes()
        {
            Console.Write($"Primes between {lower} and {upper}: [ ");
            for (uint i = lower; i < upper; i++)
                if (IsPrime(i))
                    Console.Write($"{i} ");
            Console.WriteLine("]");
        }
    }
}
