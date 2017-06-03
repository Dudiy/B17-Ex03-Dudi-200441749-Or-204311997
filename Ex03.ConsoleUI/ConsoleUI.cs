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
            Type vehicleType, engineType;
            string modelName = string.Empty;
            string wheelManufacturer = string.Empty;
            Vehicle vehicleToAdd;

            Console.WriteLine(
@"Please input vehicle License Plate");
            licensePlate = Console.ReadLine();

            // TODO check validation (no empty string)
            if (garageLogic.LicensePlateExists(licensePlate))
            {
                Console.WriteLine(
@"The given license plate already exists");
                garageLogic.GetVehicleInGarage(licensePlate).Status = eVehicleStatus.InProgress;
            }
            else
            {
                Console.WriteLine(
@"The given license plate does not exist, please add it to the garage");
                Console.WriteLine(
@"Please select the vehicle type:");
                vehicleType = selectVehicleType();
                Console.WriteLine(
@"Please enter the model name:");
                modelName = Console.ReadLine();
                Console.WriteLine(
@"Please enter the wheel manufacturer:");
                wheelManufacturer = Console.ReadLine();
                Console.WriteLine(
@"Please select engine type:");
                engineType = typeof(MotorEngine);       // TODO change to be like "selectVehicleType()|

                Type[] ctorParamTypes = new Type[] {
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(Type)
                };

                ConstructorInfo m1 = vehicleType.GetConstructor(ctorParamTypes);  // TODO can force all inheritants of Vehicle to have this ctor?
                vehicleToAdd = (Vehicle)m1.Invoke(new object[] { licensePlate, modelName, wheelManufacturer, engineType });
                MethodInfo userPropertiesMethod = vehicleToAdd.GetType().GetMethod("GetUserInputPropertiesForNewVehicle");
                string input;

                foreach (KeyValuePair<string, PropertyInfo> pair in 
                    (List<KeyValuePair<string, PropertyInfo>>)userPropertiesMethod.Invoke(vehicleToAdd, new object[] { }))
                {
                    bool success = false;

                    while (!success)
                    {
                        Console.WriteLine(
@"please input {0}:",
pair.Key);
                        input = Console.ReadLine();
                        try
                        {
                            pair.Value.GetSetMethod().Invoke(vehicleToAdd, new object[] { input });
                            success = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(
@"error - {0}. please try again",
ex.InnerException.Message);
                        }
                    }
                }

                garageLogic.AddVehicleToGarage("Customer", "123", vehicleToAdd);
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
