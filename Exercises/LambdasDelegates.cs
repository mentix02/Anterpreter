namespace Anterpreter.Exercises
{

    internal class LambdasDelegates : IExercise
    {

        public uint Number()
        {
            return 12;
        }
        
        public void Run()
        {
            
            Console.Write("Generating random numbers... ");
            IList<int> numbers = GetRandomIntList(10);

            Console.Write("done.\nNumbers: ");
            PrintIEnumerable(numbers);

            Console.WriteLine("\nOdd numbers without curly braces: ");
            PrintIEnumerable(numbers.Where(n => n.IsOdd()));

            Console.WriteLine("\nEven numbers with curly braces: ");
            PrintIEnumerable(numbers.Where(n => {
                return n.IsEven();
            }));

            var isPrimeAnonymousMethod = delegate (int n) {
                return n.IsPrime();
            };
            Console.WriteLine("\nPrime numbers with anonymous method: ");
            PrintIEnumerable(numbers.Where(isPrimeAnonymousMethod));

            Console.WriteLine("\nPrime numbers with lambda expression: ");
            PrintIEnumerable(numbers.Where(n => n.IsPrime()));

            Console.WriteLine("\nLess than five with method group conversion: ");
            LessThanFiveDel d = IsLessThanFive;
            PrintIEnumerable(numbers.Where(n => d(n)));

            // TODO: 3k, 3k + 1, 3k + 2

            Console.WriteLine("\nFinding anything with anonymous delegate: ");
            PrintIEnumerable(numbers.Where(delegate (int n) {
                return true;
            }));

            FindAnythingDel findAnything = FindAnything;
            Console.WriteLine("\nFinding anything with method group conversion: ");
            PrintIEnumerable(numbers.Where(n => findAnything(n)));

        }

        private delegate bool LessThanFiveDel(int n);

        private delegate bool FindAnythingDel(int n);

        private bool IsLessThanFive(int number)
        {
            return number < 5;
        }

        private bool FindAnything(int n)
        {
            return true;
        }

        private void PrintIEnumerable(IEnumerable<int> numbers)
        {
            Console.Write("[ ");
            foreach (var number in numbers)
                Console.Write($"{number} ");
            Console.WriteLine("]");
        }

        private IList<int> GetRandomIntList(int size)
        {
            var random = new Random();
            var list = new List<int>();
            for (int i = 0; i < size; i++)
            {
                list.Add(random.Next(0, 100));
            }
            return list;
        }

    }

}