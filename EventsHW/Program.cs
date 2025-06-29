using EventsHW.DirectoryHelper;
using EventsHW.Extensions;

namespace EventsHW
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите путь к каталогу для поиска файлов: ");
            string path = Console.ReadLine();

            if (!Directory.Exists(path))
            {
                Console.WriteLine("Каталог не найден!");
                return;
            }

            var walker = new DirectoryWalker();
            var foundFiles = new List<string>();

            walker.FileFound += (sender, eventArgs) =>
            {
                Console.WriteLine($"Найден файл: {eventArgs.FileName}");
                foundFiles.Add(eventArgs.FileName);

                if (eventArgs.FileName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Обнаружен .exe файл, поиск завершён.");
                    ((DirectoryWalker)sender).Cancel();
                }
            };

            walker.Walk(path);

            var maxFile = foundFiles.GetMax(f => f.Length);

            Console.WriteLine(maxFile != null
                ? $"Файл с самым длинным именем: {maxFile} (длина: {maxFile.Length})"
                : "Файлы не найдены.");
        }
    }
}