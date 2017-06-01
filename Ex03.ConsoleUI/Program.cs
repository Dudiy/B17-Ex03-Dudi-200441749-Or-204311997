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

            v[0] = new Car("123", "Model1", eColor.Blue, 3, "WheelManufaucturer", typeof(MotorEngine));
            v[1] = new Car("44", "Model2", eColor.Blue, 4, "WheelManufaucturer", typeof(ElectricEngine));
            v[2] = new Car("55", "Model3", eColor.Blue, 5, "WheelManufaucturer", typeof(ElectricEngine));
            // energy test 

            VehicleFactory vehicleFactory = new VehicleFactory();
            Vehicle testVehicle = vehicleFactory.NewVehicleFromModel("Electric Mazda", "1234", eColor.White, (byte)4);
            Vehicle testVehicle2 = vehicleFactory.NewVehicleFromModel("Motorized Mazda", "5678", eColor.Black, (byte)3);

            foreach (Vehicle v1 in v)
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

            try
            {
                ((IElectricVehicle)v[0]).Charge(2);
            }
            catch (ValueOutOfRangeException e0)
            {
                Console.WriteLine("v[0]:\n{0}", e0);
            }

            try
            {
                ((IMotorizedVehicle)v[1]).Refuel(eFuelType.Octan98, 70);
            }
            catch (ValueOutOfRangeException e1)
            {
                Console.WriteLine("v[1]:\n{0}", e1);
            }

            try
            {
                ((IElectricVehicle)v[2]).Charge(0.1f);
            }
            catch (ValueOutOfRangeException e2)
            {
                Console.WriteLine("v[2]:\n{0}", e2);
            }


            // fillAir test: need to change to public some variable
            // there isn't require to fill specipic wheel in specipic amount
            //foreach (Vehicle v1 in v)
            //{
            //    v1.m_Wheels[Car.ePossitionOfCarWheel.BR].FillAir(10);
            //}

            //try
            //{
            //    v[0].m_Wheels[Car.ePossitionOfCarWheel.BR].FillAir(50);
            //}
            //catch (ValueOutOfRangeException e0)
            //{
            //    Console.WriteLine("v[0]:\n{0}", e0);
            //}
        }
    }
}
