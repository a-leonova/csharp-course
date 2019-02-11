using System;
using System.Collections.Generic;

namespace ConsoleApp2test
{
    class Program
    {
        private Dictionary<int, int> f = new Dictionary<int, int>();

        static void Main(string[] args)
        {
            Console.WriteLine("Write date");
            var dateString = Console.ReadLine();

            if (!DateTime.TryParse(dateString, out var dateValue))
            {
                Console.WriteLine("Couldn't parse date");
                return;
            }
            var writer = new CalendarWriter();
            writer.Write(dateValue);
      //      Console.ReadLine();
        }
    
    }
}
