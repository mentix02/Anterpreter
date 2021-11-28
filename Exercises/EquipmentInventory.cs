using System.Linq;
using System.Reflection;

namespace Anterpreter.Exercises
{

    [AttributeUsage(System.AttributeTargets.Method)]
    internal class InventoryCommandAttribute : Attribute
    {
        public InventoryCommandAttribute() { }
    }

    internal static class InventoryInterpreter
    {

        private static EquipmentInventoryStore InventoryStore;
        private static readonly Dictionary<string, Action> InventoryInterpreterCommands = new();

        private static string ReadString(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine().ToLower().Trim();
            return input;
        }

        private static uint ReadUint(string prompt)
        {
            bool isUint;
            string input;

            do
            {
                Console.Write(prompt);

                input = Console.ReadLine();

                isUint = uint.TryParse(input, out uint result);

                if (!isUint)
                    Console.WriteLine("Please enter a valid positive number.");
                else
                    return result;

            } while (!isUint);

            return 0;
        }

        private static uint ReadValidIdx(string prompt)
        {
            uint idx;
            do
            {
                idx = ReadUint(prompt);

                if (idx >= InventoryStore.GetEquipmentCount())
                    Console.WriteLine("Please enter a valid index.");

            } while (idx >= InventoryStore.GetEquipmentCount());
            return idx;
        }

        [InventoryCommand]
        private static void Add()
        {
            Equipment equipment;

            string name = ReadString("Enter equipment name: ");

            string description = ReadString("Enter equipment description: ");

            Console.WriteLine("Enter type of equipment -");
            Console.WriteLine("1. Mobile");
            Console.WriteLine("2. Immobile");

            uint equipmentTypeOption;

            do
            {
                equipmentTypeOption = ReadUint("Enter equipment type: ");

                if (equipmentTypeOption < 1 || equipmentTypeOption > 2)
                    Console.WriteLine("Please enter a valid equipment type.");
            } while (equipmentTypeOption < 1 || equipmentTypeOption > 2);

            if (equipmentTypeOption == 1)
                equipment = new MobileEquipment()
                {
                    Name = name,
                    Description = description,
                    Wheels = ReadUint("Enter number of wheels: "),
                };
            else
                equipment = new ImmobileEquipment()
                {
                    Name = name,
                    Description = description,
                    Weight = ReadUint("Enter weight of equipment: "),
                };

            InventoryStore.AddEquipment(equipment);
        }

        [InventoryCommand]
        private static void Count()
        {
            Console.WriteLine(InventoryStore.GetEquipmentCount());
        }

        [InventoryCommand]
        private static void Delete()
        {
            if (InventoryStore.IsEmpty())
            {
                Console.WriteLine("No equipment to remove in inventory.");
                return;
            }
            uint equipmentIdx = ReadValidIdx("Enter index of equipment to delete: ");
            InventoryStore.RemoveEquipment(equipmentIdx);
        }

        [InventoryCommand]
        private static void Move()
        {
            if (InventoryStore.IsEmpty())
            {
                Console.WriteLine("No equipment to move in inventory.");
                return;
            }
            uint equipmentIdx = ReadValidIdx("Enter index of equipment to move: ");
            uint distance = ReadUint("Enter distance to move: ");
            InventoryStore.MoveEquipment(equipmentIdx, distance);
        }

        [InventoryCommand]
        private static void Detail()
        {
            if (InventoryStore.IsEmpty())
            {
                Console.WriteLine("No equipment to view in inventory.");
                return;
            }
            uint equipmentIdx = ReadValidIdx("Enter index of equipment to view: ");
            Console.WriteLine(InventoryStore.GetEquipmentDetails(equipmentIdx));
        }

        [InventoryCommand]
        private static void ListAll()
        {
            uint idx = 0;
            foreach (var equipment in InventoryStore.GetEquipments())
                Console.WriteLine($"{idx++}. {equipment.Name}: {equipment.Description}");
        }

        [InventoryCommand]
        private static void ListMobile()
        {
            foreach (var mobileEquipment in InventoryStore.GetMobileEquipments())
                Console.WriteLine(mobileEquipment);
        }

        [InventoryCommand]
        private static void ListImmobile()
        {
            foreach (var immobileEquipment in InventoryStore.GetImmobileEquipments())
                Console.WriteLine(immobileEquipment);
        }

        [InventoryCommand]
        private static void ClearScreen()
        {
            Console.Clear();
        }

        [InventoryCommand]
        private static void DeleteAll()
        {
            Console.WriteLine($"Removed {InventoryStore.GetEquipmentCount()} items.");
            InventoryStore.RemoveAll();
        }

        [InventoryCommand]
        private static void DeleteMobile()
        {
            Console.WriteLine($"Deleted {InventoryStore.RemoveMobileEquipment()} items.");
        }
    
        [InventoryCommand]
        private static void DeleteImmobile()
        {
            Console.WriteLine($"Deleted {InventoryStore.RemoveImmobileEquipment()} items.");
        }

        [InventoryCommand]
        private static void Help()
        {
            Console.WriteLine("Inventory commands -");
            Console.WriteLine("* quit");
            foreach (var commandName in InventoryInterpreterCommands.Keys)
                Console.WriteLine($"* {commandName}");
        }

        private static Action GetCommand(string input)
        {
            if (InventoryInterpreterCommands.ContainsKey(input))
                return InventoryInterpreterCommands[input];
            foreach (var commandKv in InventoryInterpreterCommands)
                if (commandKv.Key.StartsWith(input))
                    return commandKv.Value;
            return null;
        }

        public static void Initialize(EquipmentInventoryStore InventoryStore)
        {

            Action action;
            InventoryInterpreter.InventoryStore = InventoryStore;
            var privateMethods = typeof(InventoryInterpreter).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);

            foreach (var method in privateMethods)
            {
                if (method.GetCustomAttributes(typeof(InventoryCommandAttribute), false).Any())
                {
                    action = (Action)method.CreateDelegate(typeof(Action));
                    InventoryInterpreterCommands.Add(method.Name.ToLower(), action);
                }
            }

        }

        public static void RunLoop(string prompt = "(inventory)> ")
        {

            string input;
            Action command;

            Help();

            do
            {
                input = ReadString(prompt).ToLower().Trim();
                command = GetCommand(input);

                if (command != null)
                    command();
                else if (input.StartsWith("q"))
                    break;
                else
                    Console.WriteLine("Invalid input provided. Please try again.");
            } while (!input.StartsWith("q"));
        }

    }

    internal class EquipmentInventoryStore
    {

        private List<Equipment> Equipments = new();

        public void AddEquipment(Equipment equipment)
        {
            Equipments.Add(equipment);
        }

        public void RemoveEquipment(uint idx)
        {
            Equipments.RemoveAt((int) idx);
        }

        public void RemoveAll()
        {
            Equipments.Clear();
        }

        public uint GetEquipmentCount()
        {
            return (uint) Equipments.Count;
        }

        public bool IsEmpty()
        {
            return GetEquipmentCount() == 0;
        }

        public uint RemoveMobileEquipment()
        {
            uint count = 0;
            Equipments = Equipments
                .Where(e =>
                {
                    if (e.Type != EquipmentType.MOBILE)
                        return true;
                    else
                        count++;
                    return false;
                })
                .ToList();
            return count;
        }

        public uint RemoveImmobileEquipment()
        {
            uint count = 0;
            Equipments = Equipments
                .Where(e =>
                {
                    if (e.Type != EquipmentType.IMMOBILE)
                        return true;
                    else
                        count++;
                    return false;
                })
                .ToList();
            return count;
        }

        public IEnumerable<Equipment> GetUnmovedEquipments()
        {
            return Equipments.Where(e => e.DistanceMoved == 0);
        }

        public IEnumerable<Equipment> GetMobileEquipments()
        {
            return Equipments.Where(e => e.Type == EquipmentType.MOBILE);
        }

        public IEnumerable<Equipment> GetImmobileEquipments()
        {
            return Equipments.Where(e => e.Type == EquipmentType.IMMOBILE);
        }

        public void MoveEquipment(uint idx, uint distance)
        {
            Equipments[(int) idx].MoveBy(distance);
        }

        public string GetEquipmentDetails(uint idx)
        {
            return Equipments[(int) idx].GetDetails();
        }

        public List<Equipment> GetEquipments()
        {
            return Equipments;
        }

    }

    internal class EquipmentInventory : IExercise
    {
        public uint Number()
        {
            return 6;
        }

        public void Run()
        {
            EquipmentInventoryStore store = new();
            InventoryInterpreter.Initialize(store);
            InventoryInterpreter.RunLoop();
        }
    }

}
