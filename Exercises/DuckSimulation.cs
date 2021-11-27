namespace Anterpreter.Exercises
{

    enum DuckType
    {
        Rubber,
        Mallard,
        Redhead,
    }

    interface IDuck
    {
        public string Fly();
        public string Quack();
        public DuckType Type { get; }

        public void ShowDetails()
        {
            Console.WriteLine($"Type: {Type}");
            Console.WriteLine($"Fly(): {Fly()}");
            Console.WriteLine($"Quack(): {Quack()}");
        }
    }

    class RubberDuck : IDuck
    {

        public string Fly()
        {
            return "Does not fly";
        }

        public string Quack()
        {
            return "Does not quack";
        }

        public DuckType Type { get; } = DuckType.Rubber;

    }

    class MallardDuck : IDuck
    {

        public string Fly()
        {
            return "Flies fast";
        }

        public string Quack()
        {
            return "Quacks loud";
        }

        public DuckType Type { get; } = DuckType.Rubber;

    }

    class RedHeadDuck : IDuck
    {

        public string Fly()
        {
            return "Flies slow";
        }

        public string Quack()
        {
            return "Quacks mild";
        }

        public DuckType Type { get; } = DuckType.Redhead;
    }

    internal class DuckSimulation : IExercise
    {

        public uint Number()
        {
            return 5;
        }

        public void Run()
        {
            IDuck duck = new MallardDuck();
            duck.ShowDetails();
        }

    }
}
