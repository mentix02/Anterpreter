namespace Anterpreter.Exercises
{

    internal class ProductMarkDefectiveEventArgs : EventArgs
    {
        public bool IsDefective { get; set; }
    }

    internal class Product : IEquatable<Product>
    {

        private bool _isDefective;

        public uint Id { get; set; }
        public decimal Price { get; set; }
        public bool IsDefective { 
            get {
                return _isDefective;
            }
            set {
                _isDefective = value;
                if (value)
                {
                    OnProductMarkedDefective(new ProductMarkDefectiveEventArgs() { IsDefective = value });
                }
            }
        }

        protected virtual void OnProductMarkedDefective(ProductMarkDefectiveEventArgs e)
        {
            EventHandler<ProductMarkDefectiveEventArgs> handler = ProductMarkedDefective;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<ProductMarkDefectiveEventArgs> ProductMarkedDefective;

        public bool Equals(Product other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return (int) Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Product otherProduct)
                return false;
            else
                return base.Equals(otherProduct);
        }
    }

    internal static class Inventory
    {
        public static readonly Dictionary<Product, uint> Store = new();

        public static decimal Value {
            get {
                decimal res = 0;
                foreach (var item in Store)
                    res += item.Key.Price * item.Value;
                return res;
            }
        }

        public static void Add(Product product)
        {
            product.ProductMarkedDefective += (sender, args) =>
            {
                if (args.IsDefective)
                {
                    Store.Remove((Product) sender);
                }
            };
                if (!product.IsDefective)
                Store.Add(product, 1);
        }

        public static void Remove(Product product)
        {
            Store.Remove(product);
        }
    }

    internal class ProductInventory : IExercise
    {
        
        public uint Number()
        {
            return 14;
        }

        public void Run() 
        {

            Console.Write("Creating sample products... ");

            var product1 = new Product() { Id = 1, Price = 21.8m, IsDefective = false };
            var product2 = new Product() { Id = 2, Price = 18.9m, IsDefective = false };
            var product3 = new Product() { Id = 3, Price = 23.23m, IsDefective = false };
            var product4 = new Product() { Id = 4, Price = 32.1m, IsDefective = false };

            Console.WriteLine("done.");

            Console.Write("Adding products to inventory... ");

            Inventory.Add(product1);
            Inventory.Add(product2);
            Inventory.Add(product3);
            Inventory.Add(product4);

            Console.WriteLine("done.");

            Console.WriteLine($"Inventory value: {Inventory.Value}");

            Console.Write("Marking product 3 as defective... ");

            product3.IsDefective = true;

            Console.WriteLine("done.");

            Console.WriteLine($"Inventory value: {Inventory.Value}");

            Console.Write("Products in inventory- ");

            foreach (var product in Inventory.Store.Keys)
                Console.Write($"Id: {product.Id} ");
            
            Console.WriteLine();

        }

    }
}
