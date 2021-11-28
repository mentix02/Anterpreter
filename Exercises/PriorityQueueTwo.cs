namespace Anterpreter.Exercises
{

    interface IPriority
    {
        int Priority { get; set; }
    }

    internal class PriorityQueue2<T> where T: IEquatable<T>, IPriority
    {

        private IDictionary<int, IList<T>> elements;

        public PriorityQueue2()
        {
            elements = new Dictionary<int, IList<T>>();
        }

        public PriorityQueue2(IEnumerable<T> elements) : this()
        {
            foreach (var element in elements)
                Enqueue(element);
        }

        public int Count()
        {
            int count = 0;
            foreach (var list in elements.Values)
            {
                count += list.Count;
            }
            return count;
        }

        public bool Contains(T item)
        {
            return elements.Values.Any(list => list.Contains(item));
        }

        public T Dequeue()
        {
            if (Count() == 0)
                return default(T);

            // Get highest priority and corresponding list
            var maxPriority = GetHighestPriority();
            var maxPriorityElList = elements[maxPriority];

            // Get first element from list and remove it
            var item = maxPriorityElList[0];
            maxPriorityElList.RemoveAt(0);

            // If list is empty, remove it entirely
            if (maxPriorityElList.Count == 0)
                elements.Remove(maxPriority);

            return item;
        }

        public void Enqueue(T item)
        {
            var priority = item.Priority;

            if (!elements.ContainsKey(priority))
                elements[priority] = new List<T>();

            elements[priority].Add(item);
        }
        
        public T Peek()
        {
            if (Count() == 0)
                return default(T);
            
            // Get highest priority and corresponding list
            var maxPriority = GetHighestPriority();
            var maxPriorityElList = elements[maxPriority];

            // Get first element from list
            return maxPriorityElList[0];
        }

        private int GetHighestPriority()
        {
            return elements.Keys.Max();
        }

    }

    internal class PriorityQueueTwo : IExercise
    {

        class PriorityType : IPriority, IEquatable<PriorityType>
        {
            public string Data { get; set; }
            public int Priority { get; set; }

            public PriorityType(int priority, string data)
            {
                Data = data;
                Priority = priority;
            }

            public override string ToString()
            {
                return Data;
            }

            public bool Equals(PriorityType other)
            {
                return Priority == other.Priority;
            }
        }

        public uint Number()
        {
            return 9;
        }

        public void Run()
        {

            Console.Write("Creating PriorityQueue2... ");
            var pq2 = new PriorityQueue2<PriorityType>();
            Console.WriteLine("done.");

            Console.Write("Enqueueing items... ");

            pq2.Enqueue(new PriorityType(1, "one"));
            pq2.Enqueue(new PriorityType(2, "two 1"));
            pq2.Enqueue(new PriorityType(2, "two 2"));
            pq2.Enqueue(new PriorityType(3, "three"));
            pq2.Enqueue(new PriorityType(4, "four"));

            Console.WriteLine("done.");

            Console.WriteLine("Peeking... ");
            Console.WriteLine(pq2.Peek());

            Console.WriteLine("Dequeuing... ");

            var count = pq2.Count();

            for (int i = 0; i < count; i++)
                Console.WriteLine(pq2.Dequeue());

            Console.WriteLine("done.");
            Console.WriteLine($"Count: {pq2.Count()}");

        }
    }

}