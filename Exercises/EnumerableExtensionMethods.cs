namespace Anterpreter.Exercises
{

    static class EnumerableExtensions
    {

        public static bool CustomAll<T>(this IEnumerable<T> source, Func<T, bool> cond)
        {
            foreach (var item in source)
            {
                if (!cond(item))
                    return false;
            }
            return true;
        }

        public static bool CustomAny<T>(this IEnumerable<T> source, Func<T, bool> cond)
        {
            foreach (var item in source)
            {
                if (cond(item))
                    return true;
            }
            return false;
        }

        public static TResult CustomMax<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {

            if (!source.Any())
                return default;

            TResult currMax = selector(source.First());
            Comparer<TResult> comparer = Comparer<TResult>.Default;

            foreach (var item in source)
            {
                TResult itemField = selector(item);
                if (itemField != null && comparer.Compare(itemField, currMax) > 0)
                    currMax = itemField;
            }

            return currMax;
        }

        public static TResult CustomMin<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {

            if (!source.Any())
                return default;

            TResult currMin = selector(source.First());
            Comparer<TResult> comparer = Comparer<TResult>.Default;

            foreach (var item in source)
            {
                TResult itemField = selector(item);
                if (itemField != null && comparer.Compare(itemField, currMin) < 0)
                    currMin = itemField;
            }

            return currMin;
        }

        public static IEnumerable<T> CustomWhere<T>(this IEnumerable<T> source, Func<T, bool> filter)
        {
            foreach (var item in source)
            {
                if (filter(item)) yield return item;
            }
        }

        public static IEnumerable<TResult>
            CustomSelect<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            foreach (var item in source)
                yield return selector(item);
        }
    }

    internal class EnumerableExtensionsMethods : IExercise
    {

        private class ExampleUser
        {
            public uint Age { get; set; }
            public string Name { get; set; }
            public ExampleUser(string Name, uint Age)
            {
                this.Age = Age;
                this.Name = Name;
            }
        }

        public uint Number()
        {
            return 13;
        }

        public void Run()
        {
            List<int> l = new() { 2, 4, 6, 8 };

            Console.WriteLine("CustomAll Example -\n");
            Console.WriteLine("List<int> l = new() { 2, 4, 6, 8 };");
            Console.Write("l.CustomAll(n => n.IsEven()) == l.All(n => n.IsEven()); // ");
            Console.WriteLine(l.CustomAll(n => n.IsEven()) == l.All(n => n.IsEven()));

            l = new() { 4, 4, 7, 8, 10 };
            Console.WriteLine("\nCustomAny Example -\n");
            Console.WriteLine("l = new() { 4, 4, 7, 8, 10 };");
            Console.Write("l.CustomAny(n => n.IsPrime()) == l.Any(n => n.IsPrime()); // ");
            Console.WriteLine(l.CustomAny(n => n.IsPrime()) == l.Any(n => n.IsPrime()));

            Console.WriteLine("\nCustomMax Example -\n");
            Console.Write("l.CustomMax(n => n) == l.Max(n => n) // ");

            var defaultMax = l.Max(n => n);
            var customMax = l.CustomMax(n => n);
            Console.WriteLine($"{defaultMax == customMax}");
            Console.WriteLine($"l.Max(n => n) == {defaultMax}");
            Console.WriteLine($"l.CustomMax(n => n) == {customMax}");

            Console.WriteLine("\nCustomMin Example -\n");
            Console.Write("l.CustomMin(n => n) == l.Min(n => n) // ");

            var defaultMin = l.Min(n => n);
            var customMin = l.CustomMin(n => n);

            Console.WriteLine($"{defaultMin == customMin}");
            Console.WriteLine($"l.Min(n => n) == {defaultMin}");
            Console.WriteLine($"l.CustomMin(n => n) == {customMin}");

            Console.WriteLine("\nCustomWhere Example -\n");
            Console.WriteLine("Print all numbers greater than 5 -");
            Console.WriteLine("foreach (var num in l.CustomWhere(n => n > 5)) Console.Write(num);");

            foreach (var num in l.CustomWhere(n => n > 5)) Console.Write($"{num} ");
            Console.WriteLine();

            Console.WriteLine("\nCustomSelect Example -\n");

            List<ExampleUser> users = new()
            {
                new ExampleUser("Manan", 19),
                new ExampleUser("Aryan", 20),
                new ExampleUser("John", 23)
            };

            Console.WriteLine("Get names of users -");
            foreach (string name in users.CustomSelect(user => user.Name))
                Console.Write($"\"{name}\" ");

            Console.WriteLine("Get ages of users -");
            foreach (uint age in users.CustomSelect(user => user.Age))
                Console.Write($"{age} ");

            Console.WriteLine();
        }

    }
}
