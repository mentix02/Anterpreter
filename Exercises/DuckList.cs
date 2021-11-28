using System.Collections;

namespace Anterpreter.Exercises
{

    internal class DuckPond : IEnumerable<IDuck>
    {

        private readonly List<IDuck> Ducks = new();

        public void Add(IDuck duck)
        {
            Ducks.Add(duck);
        }

        public void KillDuck(int idx)
        {
            Ducks.RemoveAt(idx);
        }

        public void KillAllDucks()
        {
            Ducks.Clear();
        }

        public IEnumerator<IDuck> GetEnumerator()
        {
            foreach (var duck in Ducks.OrderBy(d => d.Weight))
                yield return duck;
        }

        public IEnumerator<IDuck> GetWingOrderEnumerator()
        {
            foreach (var duck in Ducks.OrderBy(d => d.Wings))
                yield return duck;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

    internal class DuckList : IExercise
    {

        private readonly Random _random = new();

        private DuckType GetRandomDuckType()
        {
            var opt = _random.Next(2);
            return opt switch
            {
                0 => DuckType.Rubber,
                1 => DuckType.Mallard,
                _ => DuckType.Redhead,
            };
        }

        private IDuck GetRandomDuck()
        {

            uint weight = (uint)_random.Next(100);
            uint wings = (uint)_random.Next(1, 10);

            var duckType = GetRandomDuckType();

            return duckType switch
            {
                DuckType.Rubber => new RubberDuck { Weight = weight, Wings = wings },
                DuckType.Mallard => new MallardDuck { Weight = weight, Wings = wings },
                _ => new RedHeadDuck { Weight = weight, Wings = wings }
            };
        }

        public uint Number()
        {
            return 7;
        }

        public void Run()
        {
            uint idx = 0;
            DuckPond pond = new();

            Console.Write("Creating ducks... ");

            for (int i = 0; i < 10; i++) pond.Add(GetRandomDuck());

            Console.Write("done\n");

            Console.WriteLine("Printing according to weight -");
            foreach (var duck in pond)
                Console.WriteLine($"#{++idx} weight - {duck.Weight} g");

            Console.WriteLine("\nPrinting according to wings -");
            var wingOrderEnumerator = pond.GetWingOrderEnumerator();

            while (wingOrderEnumerator.MoveNext())
                Console.WriteLine($"#{++idx} wings - {wingOrderEnumerator.Current.Wings}");
        }
    }
}
