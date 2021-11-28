namespace Anterpreter.Exercises
{

    internal class PriorityQueue3<T> where T : IEquatable<T>
    {

        private class PriorityNode
        {
            public T Item { get; set; }
            public int Priority { get; set; }
        }

        private IList<PriorityNode> elements;

        public PriorityQueue3()
        {
            elements = new List<PriorityNode>();
        }

        public PriorityQueue3(IDictionary<int, IList<T>> elements) : this()
        {
            foreach (var elementKv in elements)
                foreach (var item in elementKv.Value)
                    this.elements.Add(new PriorityNode() { Item = item, Priority = elementKv.Key });
        }

        public int Count()
        {
            return elements.Count;
        }

        public bool Contains(T item)
        {
            return elements.Any(node => node.Item.Equals(item));
        }

        public T Dequeue()
        {
            if (Count() == 0)
                return default(T);

            int idx;
            int maxPriority = GetHighestPriority();

            for (idx = 0; idx < elements.Count(); idx++)
            {
                if (elements[idx].Priority == maxPriority)
                    break;
            }

            var item = elements[idx].Item;
            elements.RemoveAt(idx);

            return item;
        }

        public void Enqueue(int priority, T item)
        {
            elements.Add(new PriorityNode() { Item = item, Priority = priority });
        }

        public T Peek()
        {
            if (Count() == 0)
                return default(T);

            int maxPriority = GetHighestPriority();
            var maxPriorityNode = elements.Where(node => node.Priority == maxPriority).First();
            return maxPriorityNode.Item;
        }

        private int GetHighestPriority()
        {
            return elements.Max(node => node.Priority);
        }

    }

    internal class PriorityQueueThree : IExercise
    {

        public uint Number()
        {
            return 10;
        }

        public void Run()
        {

            Console.Write("Creating PriorityQueue3... ");
            var pq3 = new PriorityQueue3<string>();
            Console.WriteLine("done.");

            Console.Write("Enqueueing items... ");
            pq3.Enqueue(1, "one");
            pq3.Enqueue(2, "two 1");
            pq3.Enqueue(2, "two 2");
            pq3.Enqueue(3, "three");
            pq3.Enqueue(4, "four");

            Console.WriteLine("done.");

            Console.WriteLine("Peeking... ");
            Console.WriteLine(pq3.Peek());

            Console.WriteLine("Dequeuing... ");

            var count = pq3.Count();

            for (int i = 0; i < count; i++)
                Console.WriteLine(pq3.Dequeue());

            Console.WriteLine("done.");
            Console.WriteLine($"Count: {pq3.Count()}");

        }

    }

}