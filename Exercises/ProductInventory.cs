namespace Anterpreter.Exercises
{

    internal class Product : IEquatable<Product>
    {
        public uint Id { get; set; }
        public decimal Price { get; set; }
        public bool IsDefective { get; set; }

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

    internal class Inventory
    {
        private readonly Dictionary<Product, uint> store = new();

        public void Add(Product product)
        {
            store.Add(product, 0);
        }

        public void Remove(Product product)
        {
            store.Remove(product);
        }
    }

    internal class ProductInventory
    {
        
    }
}

