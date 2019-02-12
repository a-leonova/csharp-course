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
            StringBuilder calendar = new StringBuilder(Title);
            var workingDays = 0;
            CreateCalendar(dateValue, calendar, ref workingDays);
            Console.WriteLine(calendar);
            Console.WriteLine("Working days count: {0}", workingDays);
        }

        private void CreateCalendar(DateTime dateValue, StringBuilder calendar, ref int workingDays)
        {
            var date = new DateTime(dateValue.Year, dateValue.Month, 1);
            WriteAlignmentBeforeFirstDate(date, calendar);
            for (; date.Month == dateValue.Month; date = date.AddDays(1))
            {
                WriteDateWithAlignment(date, calendar);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    ++workingDays;
                }
            }
        }

        private void WriteAlignmentBeforeFirstDate(DateTime date, StringBuilder calendar)
        {
            int dayOfWeek = (int)date.DayOfWeek;
            calendar.Append(String.Join("", Enumerable.Repeat(" ", dayOfWeek * _spaces)));
        }

        private void WriteDateWithAlignment(DateTime date, StringBuilder calendar)
        {
            calendar.Append(date.Day);
            calendar.Append(date.DayOfWeek == DayOfWeek.Saturday
                ? "\n"
                : String.Join("", Enumerable.Repeat(" ", _spaces - (int) Math.Floor(Math.Log10(date.Day) + 1))));
        }
    }
}
