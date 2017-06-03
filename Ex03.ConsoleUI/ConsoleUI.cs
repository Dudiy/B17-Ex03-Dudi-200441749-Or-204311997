using System;
using Ex03.GarageLogic;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Ex03.ConsoleUI
{
    public class ConsoleUI
    {
        private GarageLogic.GarageLogic garageLogic = new GarageLogic.GarageLogic();

        public void AddVehicle()
        {
            string licensePlate = string.Empty;
            Type vehicleType;

            Console.WriteLine(
@"Please input vehicle License Plate");
            licensePlate = Console.ReadLine();
            licensePlate.Trim();

            // TODO check validation 
            if (garageLogic.LicensePlateExists(licensePlate))
            {
                Console.WriteLine(
@"The given license plate already exists");
            }
            else
            {
                Console.WriteLine(
@"The given license plate does not exist, please add it to the garage");
                Console.WriteLine(
@"Please select the vehicle type:");
                vehicleType = selectVehicleType();
                // TODO i got stuck here, can't call the method for getting the list of properties because it
                // cant be a static method (cant have abstract static)


                //MethodInfo m = vehicleType.GetMethod("GetUserInputPropertiesForNewVehicle");
                //foreach (KeyValuePair<string, object> kp in m.Invoke(vehicleType, new object[] { }))
                //{

                //}
            }
        }

        private Type selectVehicleType()
        {
            byte input = 0;
            bool isValidInput = false;

            // print all vehicle types available in VehicleFactory
            for (int i = 0; i < VehicleFactory.NumOfVehicleTypes; i++)
            {
                Console.WriteLine(
@"{0}. {1}", 
i + 1, 
VehicleFactory.GetVehicleTypeAtI(i).Name);
            }

            while (!isValidInput)
            {
                while (!Byte.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine(
@"Format error - please input a number");
                }

                if (input > 0 && input <= VehicleFactory.NumOfVehicleTypes)
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine(
@"Logic error - given number is not in the list");
                }
            }

            return VehicleFactory.GetVehicleTypeAtI(input - 1);
        }
    }
}
