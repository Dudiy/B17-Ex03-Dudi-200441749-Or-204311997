/*
 * B17_Ex03: Program.cs
 * 
 * Initial class for running the program
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
*/
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class Program
    {
        public static void Main()
        {
            run();
        }

        private static void run()
        {
            Garage garage = new Garage();
            ConsoleUI userInterface = new ConsoleUI(garage);

            userInterface.Run();
        }
    }
}