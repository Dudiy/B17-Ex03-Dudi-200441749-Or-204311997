using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class Program
    {
        public static void Main()
        {
            run();
        }

        private static void run()
        {
            Garage garage = new Garage();
            ConsoleUI userInterface = new ConsoleUI(garage);

            userInterface.run();
        }
    }
}