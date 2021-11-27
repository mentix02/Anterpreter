using System.Text;

namespace Anterpreter.Exercises
{

    internal abstract class Equipment
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public uint DistanceMoved { get; set; } = 0;
        public uint MaintenanceCost { get; set; } = 0;

        protected abstract void IncreaseMaintenanceCost();

        private void IncreaseDistanceMoved(uint distance)
        {
            DistanceMoved += distance;
        }

        public void MoveBy(uint distance)
        {
            IncreaseDistanceMoved(distance);
            IncreaseMaintenanceCost();
        }

        public virtual string GetDetails()
        {
            StringBuilder details = new("{\n");

            details.Append($"\tName: \"{Name}\",\n");
            details.Append($"\tDescription: \"{Description}\",\n");
            details.Append($"\tDistanceMoved: {DistanceMoved} Km,\n");
            details.Append($"\tMaintenanceCost: Rs. {MaintenanceCost},\n}}");

            return details.ToString();
        }

        public override string ToString()
        {
            return Name;
        }

    }

    internal class MobileEquipment : Equipment
    {

        public uint Wheels { get; set; }

        public override string GetDetails()
        {
            string baseDetails = base.GetDetails();
            StringBuilder details = new(baseDetails);

            details.Insert(baseDetails.Length-2, $"\n\tWheels = {Wheels}");

            return details.ToString();
        }

        protected override void IncreaseMaintenanceCost()
        {
            MaintenanceCost = Wheels * DistanceMoved;
        }
    }

    internal class ImmobileEquipment : Equipment
    {
        public uint Weight { get; set; }

        public override string GetDetails()
        {
            string baseDetails = base.GetDetails();
            StringBuilder details = new(baseDetails);

            details.Insert(baseDetails.Length - 2, $"\n\tWeight = {Weight} Kg");

            return details.ToString();
        }

        protected override void IncreaseMaintenanceCost()
        {
            MaintenanceCost = Weight * DistanceMoved;
        }
    }

    internal class EquipmentCompany : IExercise
    {
        public uint Number()
        {
            return 4;
        }

        public void Run()
        {

            MobileEquipment car = new()
            {
                Wheels = 4,
                Name = "Jeep Compass",
                Description = "A 5 seater SUV.",
            };

            ImmobileEquipment ladder = new()
            {
                Weight = 20,
                Name = "Ladder",
                Description = "A vertical or inclined set of rungs or steps.",
            };

            Console.WriteLine($"Creating... \"{car}\" & \"{ladder}\".");

            Console.WriteLine("Car details -");
            Console.WriteLine(car.GetDetails());

            Console.WriteLine($"Moving {car}...");

            car.MoveBy(100);

            Console.WriteLine("Car details after moving-");
            Console.WriteLine(car.GetDetails());

        }
    }
}
