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

        public uint Wings { get; set; }
        public uint Weight { get; set; }

        public void ShowDetails()
        {
            Console.WriteLine($"Type: {Type}");
            Console.WriteLine($"Fly(): {Fly()}");
            Console.WriteLine($"Wings: {Wings}");
            Console.WriteLine($"Weight: {Weight} g");
            Console.WriteLine($"Quack(): {Quack()}");
        }
    }

    class RubberDuck : IDuck
    {

        public uint Wings { get; set; }
        public uint Weight { get; set; }

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

        public uint Wings { get; set; }
        public uint Weight { get; set; }

        public string Fly()
        {
            return "Flies fast";
        }

        public string Quack()
        {
            return "Quacks loud";
        }

        public DuckType Type { get; } = DuckType.Mallard;

    }

    class RedHeadDuck : IDuck
    {

        public uint Wings { get; set; }
        public uint Weight { get; set; }

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
            Console.Write("Creating a mallard duck... ");
            IDuck duck = new MallardDuck();
            Console.WriteLine("done");
            duck.ShowDetails();
        }

    }
}
