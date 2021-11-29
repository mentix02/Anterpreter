namespace Anterpreter.Exercises
{
    internal class FileOperations : IExercise
    {

        private uint NumTextfiles(string directoryPath)
        {
            return (uint) Directory.EnumerateFiles(directoryPath, "*.txt").Count();
        }

        private IDictionary<string, uint> GetExtensionCountGroups(string directoryPath)
        {
            string extension;
            Dictionary<string, uint> extensionCountGroups = new();
            foreach (string file in Directory.EnumerateFiles(directoryPath))
            {
                extension = Path.GetExtension(file);
                if (extensionCountGroups.ContainsKey(extension))
                    extensionCountGroups[extension]++;
                else
                    extensionCountGroups[extension] = 1;
            }
            return extensionCountGroups;
        }

        private IEnumerable<string> GetTop5LargestFiles(string directoryPath)
        {
            return Directory
                .EnumerateFiles(directoryPath)
                .OrderByDescending(file => new FileInfo(file).Length)
                .Take(5);
        }

        private string GetFileWithMaximumLength(string directoryPath)
        {
            return Directory
                .EnumerateFiles(directoryPath)
                .OrderByDescending(file => new FileInfo(file).Length)
                .First();
        }

        public uint Number()
        {
            return 16;
        }

        public void Run()
        {
            string directoryPath = "Exercises/FileOperations/";
            Console.WriteLine($"Number of text files: {NumTextfiles(directoryPath)}");

            Console.WriteLine($"\nExtension count groups -");

            GetExtensionCountGroups(directoryPath)
                .ToList()
                .ForEach(kvp => Console.WriteLine($"{kvp.Key}: {kvp.Value}"));

            Console.WriteLine($"\nTop 5 largest files -");
            GetTop5LargestFiles(directoryPath)
                .ToList()
                .ForEach(file => Console.WriteLine($"{file}: {new FileInfo(file).Length}"));

            Console.WriteLine($"\nFile with maximum length: {GetFileWithMaximumLength(directoryPath)}");
        }

    }
}
