using EventsHW.DirectoryHelper;
using EventsHW.Extensions;

namespace EventsHW
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Введите путь к каталогу для поиска файлов (или 'exit' для выхода): ");
                string path = Console.ReadLine();

                if (string.Equals(path, "exit", StringComparison.OrdinalIgnoreCase))
                    break;

                if (!Directory.Exists(path))
                {
                    Console.WriteLine("Каталог не найден! Попробуйте снова.");
                    continue;
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

                try
                {
                    walker.Walk(path);

                    var maxFile = foundFiles.GetMax(f => f.Length);

                    Console.WriteLine(maxFile != null
                        ? $"Файл с самым длинным именем: {maxFile} (длина: {maxFile.Length})"
                        : "Файлы не найдены.");
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"Ошибка доступа: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }

                Console.WriteLine("Поиск завершен. Хотите попробовать другой путь? (y/n)");
                var answer = Console.ReadLine();
                if (!string.Equals(answer, "y", StringComparison.OrdinalIgnoreCase))
                    break;
            }
        }
    }
}