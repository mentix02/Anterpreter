namespace Anterpreter.Exercises
{
    internal class EqualityDiff : IExercise
    {

        public uint Number()
        {
            return 2;
        }

        public void Run()
        {
            Console.WriteLine("\"==\" is equality operator.");
            Console.WriteLine("For user defined reference types it checks for reference equality -\ni.e. both object point to the same address in memory.");

            ShowExample(EqRefOperatorExample);

            Console.WriteLine("For most 'native' types or objects that overload the == operator, it acts as a typical equality operator, typically checking for equality of VALUE.");

            ShowExample(EqNativeOperatorExample);

            Console.WriteLine("Object.Equals is a method used for determining equality between objects - typically in some unique fashion.");

            ShowExample(ObjectEqualsExample);

            Console.WriteLine("Finally, Object.ReferenceEquals is a method that has the same functionality as a non-overloaded == operator on user defined reference types.");
            Console.WriteLine("In that it only checks whether the objects provided to it point to the same address in memory.");

            ShowExample(ObjectReferenceEqualsExample);
        }

        private static void ShowExample(Action example)
        {
            Console.WriteLine("Example - \n");
            example();
            Console.WriteLine();
        }

        private class Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public override bool Equals(object obj)
            {
                if ((obj == null) || !GetType().Equals(obj.GetType()))
                {
                    return false;
                }
                else
                {
                    Point p = (Point) obj;
                    return (X == p.X) && (Y == p.Y);
                }
            }

            public override int GetHashCode()
            {
                throw new NotImplementedException();
            }
        }

        private static void ObjectReferenceEqualsExample()
        {
            object o1 = new();
            object o2 = null;
            object o3 = null;

            Console.WriteLine("object o1 = new();");
            Console.WriteLine("object o2 = null;");
            Console.WriteLine("object o3 = null;\n");

            Console.WriteLine($"Object.ReferenceEquals(o1, o2) // {Object.ReferenceEquals(o1, o2)}");
            Console.WriteLine($"Object.ReferenceEquals(o1, o3) // {Object.ReferenceEquals(o1, o3)}");
            Console.Write($"Object.ReferenceEquals(o2, o3) // {Object.ReferenceEquals(o2, o3)}");
        }

        private static void EqRefOperatorExample()
        {
            object o1 = new();
            object o2 = new();
            object o3 = o1;

            Console.WriteLine("object o1 = new();");
            Console.WriteLine("object o2 = new();");
            Console.WriteLine("object o3 = o1;\n");

            Console.WriteLine($"o1 == o2 // {o1 == o2}");
            Console.WriteLine($"o2 == o3 // {o2 == o3}");
            Console.WriteLine($"o1 == o3 // {o1 == o3}");
        }

        private static void EqNativeOperatorExample()
        {
            int i1 = 1;
            int i2 = 2;
            int i3 = 1;

            Console.WriteLine("int i1 = 1;");
            Console.WriteLine("int i2 = 2;");
            Console.WriteLine("int i3 = 1;");

            Console.WriteLine($"i1 == i2 // {i1 == i2}");
            Console.WriteLine($"i2 == i3 // {i2 == i3}");
            Console.WriteLine($"i1 == i3 // {i1 == i3}");

        }

        private static void ObjectEqualsExample()
        {
            string classDef = @"class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public override bool Equals(object obj)
    {
        Point p = (Point) obj; // Usually a check for types precdes this cast
        return (X == p.X) && (Y == p.Y);
    }
}

You'd use this class this way - 

Point p1 = new() { X = 1, Y = 2 };
Point p2 = new() { X = 3, Y = 5 };
Point p3 = new() { X = 1, Y = 2 };
";

            Console.WriteLine(classDef);

            Point p1 = new() { X = 1, Y = 2 };
            Point p2 = new() { X = 3, Y = 5 };
            Point p3 = new() { X = 1, Y = 2 };

            Console.WriteLine(@$"p1.Equals(p2) // {p1.Equals(p2)}
p2.Equals(p3) // {p2.Equals(p3)}
p1.Equals(p3) // {p1.Equals(p3)}
p1.Equals(p1) // {p1.Equals(p1)}");
        }
    }
}
