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
            Vehicle mb = new MotorBike("123", "Cool Bike", eLicenseType.A, 1200);
        }
    }
}
