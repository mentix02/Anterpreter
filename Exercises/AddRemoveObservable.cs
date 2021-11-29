using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Anterpreter.Exercises
{

    class Handler
    {
        public ObservableCollection<string> Collection;

        public Handler()
        {
            Collection = new ObservableCollection<string>();
            Collection.CollectionChanged += HandleChange;
        }

        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Console.WriteLine($"Element `{e.NewItems[e.NewStartingIndex]}` added in collection.");
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                Console.WriteLine($"Element `{e.OldItems[e.OldStartingIndex]}` removed from collection.");
            }
        }

    }

    internal class AddRemoveObservable : IExercise
    {

        public uint Number()
        {
            return 15;
        }

        public void Run()
        {
            Handler handler = new();

            Console.WriteLine("Adding to collection - ");

            handler.Collection.Add("Manan");

            Console.WriteLine("Removing from collection - ");

            handler.Collection.RemoveAt(0);
        }
    }
}

