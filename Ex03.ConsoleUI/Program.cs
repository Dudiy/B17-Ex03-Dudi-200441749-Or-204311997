using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class Program
    {
        public static void Main()
        {
            Garage garage = new Garage();
            ConsoleUI userInterface = new ConsoleUI(garage);

            userInterface.run();
        }
    }
}