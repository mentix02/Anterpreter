namespace Anterpreter.Exercises {

    internal class PriorityQueue1<T> where T: IEquatable<T>
    {

        private IDictionary<int, IList<T>> elements;

        public PriorityQueue1()
        {
            elements = new Dictionary<int, IList<T>>();
        }

        public PriorityQueue1(IDictionary<int, IList<T>> elements) : this()
        {
            this.elements = elements;
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

        public void Enqueue(int priority, T item)
        {
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

    internal class PriorityQueueOne : IExercise
    {
        public uint Number()
        {
            return 8;
        }

        public void Run()
        {
            
            Console.Write("Creating PriorityQueue1... ");
            var pq1 = new PriorityQueue1<string>();
            Console.WriteLine("done.");

            Console.Write("Enqueueing items... ");
            pq1.Enqueue(1, "one");
            pq1.Enqueue(2, "two 1");
            pq1.Enqueue(2, "two 2");
            pq1.Enqueue(3, "three");
            pq1.Enqueue(4, "four");

            Console.WriteLine("done.");

            Console.WriteLine("Peeking... ");
            Console.WriteLine(pq1.Peek());

            Console.WriteLine("Dequeuing... ");

            var count = pq1.Count();

            for (int i = 0; i < count; i++)
                Console.WriteLine(pq1.Dequeue());

            Console.WriteLine("done.");
            Console.WriteLine($"Count: {pq1.Count()}");

        }
    }
}