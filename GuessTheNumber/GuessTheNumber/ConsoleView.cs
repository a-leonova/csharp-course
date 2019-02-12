using System;
using System.Collections.Generic;
using System.Text;

namespace GuessTheNumber
{
    class ConsoleView : IView
    {
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string ReadMessage()
        {
            return Console.ReadLine();
        }
    }
}
