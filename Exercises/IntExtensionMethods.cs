namespace Anterpreter.Exercises
{

    static class IntExtensions
    {
        public static bool IsOdd(this int n)
        {
            return (n & 1) == 1;
        }

        public static bool IsEven(this int n)
        {
            return (n & 1) == 0;
        }

        public static bool IsPrime(this int n)
        {
            if (n < 2) return false;
            if (n == 2) return true;
            if (n.IsEven()) return false;

            for (int i = 3; i <= Math.Floor(Math.Sqrt(n)); i += 2)
                if (n.IsDivisibleBy(i))
                    return false;

            return true;
        }

        public static bool IsDivisibleBy(this int n, int divisor)
        {
            return n % divisor == 0;
        }

    }

    internal class IntExtensionMethods : IExercise
    {

        public uint Number()
        {
            return 11;
        }

        public void Run()
        {
            Console.WriteLine("int.IsEven -");
            Console.WriteLine($"2.IsEven(); // {2.IsEven()}");
            Console.WriteLine($"3.IsEven(); // {3.IsEven()}");

            Console.WriteLine("\nint.IsOdd -");
            Console.WriteLine($"2.IsOdd(); // {2.IsOdd()}");
            Console.WriteLine($"3.IsOdd(); // {3.IsOdd()}");

            Console.WriteLine("\nint.IsPrime -");
            Console.WriteLine($"2.IsPrime(); // {2.IsPrime()}");
            Console.WriteLine($"7.IsPrime(); // {7.IsPrime()}");
            Console.WriteLine($"15.IsPrime(); // {15.IsPrime()}");
        }

    }

}