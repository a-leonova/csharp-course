using System;

namespace GuessTheNumber
{
    class Program
    {
        private static String greeting = "Hello! Let's play \"Guess the Number\"\n" +
                                         "To start write your name";
        private static String rules = $"Guess the number from - {Constants.From}, to - {Constants.To}";
        static void Main(string[] args)
        {
            var view = new ConsoleView();

            view.ShowMessage(greeting);
            String name = view.ReadMessage();
            view.ShowMessage(rules);
            var game = new GameLogic(view);
            game.Play(name);
            Console.ReadLine();
        }
    }
}
