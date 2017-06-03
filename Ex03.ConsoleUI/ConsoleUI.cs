using System;
using Ex03.GarageLogic;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Ex03.ConsoleUI
{
    public class ConsoleUI
    {
        private GarageLogic.Garage garageLogic = new GarageLogic.Garage();

        public void AddNewVehicleToGarage()
        {
            string licensePlate = string.Empty;
            Type engineType = null;
            string modelName = string.Empty;
            string wheelManufacturer = string.Empty;

            Console.WriteLine(
@"Please input vehicle License Plate");
            licensePlate = Console.ReadLine();

            // TODO check validation 
            if (garageLogic.LicensePlateExists(licensePlate))
            {
                Console.WriteLine(
@"The given license plate already exists");
                garageLogic.SetVehicleInGarageStatus(licensePlate,eVehicleStatus.InProgress);

                
            }
            else
            {
                Type vehicleType;
                Vehicle vehicleToAdd; 

                Console.WriteLine(
@"The given license plate does not exist, please add it to the garage");
                vehicleType = selectVehicleType();
                getCommondPropertiesForAllVehicle(ref modelName, ref wheelManufacturer, ref engineType);
                vehicleToAdd = createNewVehicle(vehicleType, licensePlate, modelName, wheelManufacturer, engineType);
                garageLogic.AddVehicleToGarage("Customer", "123", vehicleToAdd);
            }

        }

        private void getCommondPropertiesForAllVehicle(ref string io_ModelName, ref string io_WheelManufacturer, ref Type io_EngineType)
        {            
            Console.WriteLine(
@"Please enter the model name:");
            io_ModelName = Console.ReadLine();
            Console.WriteLine(
@"Please enter the wheel manufacturer:");
            io_WheelManufacturer = Console.ReadLine();
            Console.WriteLine(
@"Please select engine type:");
            io_EngineType = typeof(MotorEngine);       // TODO change to be like "selectVehicleType()|
        }

        private Vehicle createNewVehicle(Type i_VehicleType, string i_LicensePlate, string i_ModelName, string i_WheelManufacturer, Type i_EngineType)
        {
            //Type[] ctorParamTypes = new Type[] {
            //        typeof(string),
            //        typeof(string),
            //        typeof(string),
            //        typeof(Type)
            //    };

            VehicleFactory vf = new VehicleFactory();
            Vehicle vehicleToAdd = vf.NewVehicle(i_VehicleType, i_LicensePlate, i_ModelName, i_WheelManufacturer, i_EngineType);

            // ConstructorInfo vehicleConstructorInfo = i_VehicleType.GetConstructor(ctorParamTypes);
            // Vehicle vehicleToAdd = (Vehicle)vehicleConstructorInfo.Invoke(i_CommondPropertiesOfAllVehicle);
            MethodInfo userPropertiesMethod = vehicleToAdd.GetType().GetMethod("GetUserInputPropertiesForNewVehicle");
            List<KeyValuePair<string, PropertyInfo>> uninitializeProperties =
                (List<KeyValuePair<string, PropertyInfo>>)userPropertiesMethod.Invoke(vehicleToAdd, new object[] { });
            string inputFromUser;

            foreach (KeyValuePair<string, PropertyInfo> propertyAndDescriptionPair in uninitializeProperties)
            {
                bool validPropertyInput = false;

                while (!validPropertyInput)
                {
                    Console.WriteLine(
@"please input {0}:",
propertyAndDescriptionPair.Key);
                    inputFromUser = Console.ReadLine();
                    try
                    {
                        propertyAndDescriptionPair.Value.GetSetMethod().Invoke(vehicleToAdd, new object[] { inputFromUser });
                        validPropertyInput = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(
@"error - {0}. please try again",
ex.InnerException.Message);
                    }
                }
            }

            return vehicleToAdd;
        }

        private Type selectVehicleType()
        {
            byte input = 0;
            bool isValidInput = false;

            Console.WriteLine(
@"Please select the vehicle type:");
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
