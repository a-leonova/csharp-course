using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GuessTheNumber
{
    static class Constants
    {
        public const int From = 0;
        public const int To = 50;
        public static readonly IReadOnlyList<string> Compliments = new []
        {
            "You are on right way!",
            "So smarty!",
            "Keep going!",
            "You are really cool!"
        };
    }
}
