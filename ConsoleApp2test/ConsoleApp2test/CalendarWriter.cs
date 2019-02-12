using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp2test
{
    class CalendarWriter : ICalendarWriter
    {
        private const String Title = "sun mon tue wed thu fri sat\n";
        private int _spaces = 4;

        public void WriteCalendar(DateTime dateValue)
        {
            var workingDays = 0;
            CreateCalendar(dateValue, ref workingDays);
            Console.WriteLine("Working days count: {0}", workingDays);
        }

        private void CreateCalendar(DateTime dateValue, ref int workingDays)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Title);
            Console.ResetColor();

            var date = new DateTime(dateValue.Year, dateValue.Month, 1);
            WriteAlignmentBeforeFirstDate(date);

            for (; date.Month == dateValue.Month; date = date.AddDays(1))
            {
                var color = ConsoleColor.Red;
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    color = ConsoleColor.Blue;
                    ++workingDays;
                }
                WriteDateWithAlignment(date, color);
            }
            Console.Write("\n");
        }

        private void WriteAlignmentBeforeFirstDate(DateTime date)
        {
            int dayOfWeek = (int)date.DayOfWeek;
            Console.Write(String.Join("", Enumerable.Repeat(" ", dayOfWeek * _spaces)));
        }

        private void WriteDateWithAlignment(DateTime date, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(date.Day);
            Console.ResetColor();
            Console.Write(date.DayOfWeek == DayOfWeek.Saturday
                ? "\n"
            : String.Join("", Enumerable.Repeat(" ", _spaces - (int) Math.Floor(Math.Log10(date.Day) + 1))));
        }
    }
}
