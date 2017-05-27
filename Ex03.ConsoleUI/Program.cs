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
            Vehicle[] v = new Vehicle[3];
            v[0] = new ElectricCar("123", "Model", eColor.Blue, 3, "WheelManufaucturer");
            v[1] = new MotorCar("44", "Model", eColor.Blue, 4, "WheelManufaucturer");
            v[2] = new ElectricCar("55", "Model", eColor.Blue, 5, "WheelManufaucturer");

            foreach(Vehicle v1 in v)
            {
                if (v1 is IMotorizedVehicle)
                {
                    ((IMotorizedVehicle)v1).Refuel(eFuelType.Octan98, 10);
                }
                else if(v1 is IElectricVehicle)
                {
                    ((IElectricVehicle)v1).Charge(0.9f);
                }
            }
        }
    }
}
