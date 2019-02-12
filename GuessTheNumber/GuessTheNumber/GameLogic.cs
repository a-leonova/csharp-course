using System;
using System.Collections.Generic;
using System.Text;

namespace GuessTheNumber
{
    class GameLogic
    {
        private IView _view;
        private int _attemptCount = 0;
        private TimeSpan _totalTime;
        private String _name;
        private StringBuilder _history = new StringBuilder();


        public GameLogic(IView view)
        {
            _view = view;
        }

        public void Play(String name)
        {
            _name = name;
            Random rand = new Random();
            var randomNumber = rand.Next(Constants.From, Constants.To + 1);
            int numberFromUser;

            _view.ShowMessage("Write number");
            var _startTime = DateTime.Now;
            do
            {
                ++_attemptCount;
                if (_attemptCount % 5 == 0)
                {
                    _view.ShowMessage(Constants.Compliments[rand.Next(Constants.Compliments.Count)]);
                }
                var stringNumber = _view.ReadMessage();
                if (stringNumber == "q")
                {
                    _view.ShowMessage("Okay, let's quit");
                    return;
                }

                if (!int.TryParse(stringNumber, out numberFromUser))
                {
                    _view.ShowMessage("Hey? It doesn't look like a number!!");
                    _history.Append($"{stringNumber}\n");
                    continue;
                }

                if (numberFromUser > randomNumber)
                {
                    _view.ShowMessage("This number more than guessed");
                    _history.Append($"{numberFromUser} > {randomNumber}\n");
                }
                else if (numberFromUser < randomNumber)
                {
                    _view.ShowMessage("This number less than guessed");
                    _history.Append($"{numberFromUser} < {randomNumber}\n");
                }
                else
                {
                    _history.Append($"{numberFromUser} == {randomNumber}\n");
                    _totalTime = DateTime.Now - _startTime;
                    FinishGame();
                }

            } while (numberFromUser != randomNumber);
        }

        private void FinishGame()
        {
            _view.ShowMessage($"{_name}, you guessed the number in {_attemptCount} steps");
            _view.ShowMessage("Your history:");
            _view.ShowMessage(_history.ToString());
            _view.ShowMessage($"You needed {_totalTime} time");
        }

    }
}
