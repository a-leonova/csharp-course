using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace LinesCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Bad number of arguments. Write one type of files you want to check.");
                return;
            }

            var type = args[0];
            var currentPath = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(currentPath, type, SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var totalCount = 0;
                var usefulCount = 0;
                var fileStream = new FileStream($"{file}", FileMode.Open, FileAccess.Read);
                var streamReader = new StreamReader(fileStream, Encoding.UTF8);
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    ++totalCount;
                    if (!line.StartsWith("//") && !string.IsNullOrWhiteSpace(line))
                    {
                        ++usefulCount;
                        Console.WriteLine(line);
                    }
                }
                Console.WriteLine($"For {file}:\ntotal lines - {totalCount}\nuseful lines - {usefulCount}");
            }
            Console.ReadLine();
        }
    }
}
