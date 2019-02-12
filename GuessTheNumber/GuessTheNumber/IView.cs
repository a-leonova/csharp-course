using System;
using System.Collections.Generic;
using System.Text;

namespace GuessTheNumber
{
    interface IView
    {
        void ShowMessage(String Message);
        String ReadMessage();
    } 
}
