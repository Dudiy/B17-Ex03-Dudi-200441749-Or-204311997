using System;
using Ex03.GarageLogic;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Ex03.ConsoleUI
{
    internal class ConsoleUI : UserInterface
    {
        bool m_EndOfProgram = false;

        internal ConsoleUI(Garage i_Garage) : base(i_Garage) { }

        internal override void run()
        {
            byte userSelection;

            do
            {
                Console.Clear();
                Console.WriteLine(
@"Hello and welcome to the new and improved garage managing application :)");
                userSelection = getActionRequestFromUser();
                //string str = sr_AvailableActionsForUser[userSelection].Value;
                Console.Clear();
                string methodStr = sr_AvailableActionsForUser[userSelection].Value;
                MethodInfo requiredMethod = base.GetType().GetMethod(methodStr, BindingFlags.NonPublic | BindingFlags.Instance);//, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Default);
                requiredMethod.Invoke(this, new object[] { });
            } while (!m_EndOfProgram);
        }

        private byte getActionRequestFromUser()
        {
            byte numOptions = (byte)sr_AvailableActionsForUser.Count;
            byte userSelection;

            Console.WriteLine(
@"What would you like to do next?
");
            foreach (var action in sr_AvailableActionsForUser)
            {
                Console.WriteLine("{0}. {1}", action.Key, action.Value.Key);
            }

            userSelection = getNumberInputFromUser(0, numOptions);

            return userSelection;
        }

        protected override void AddNewVehicleToGarage()
        {
            string licensePlate = string.Empty;
            Type engineType = null;
            string modelName = string.Empty;
            string wheelManufacturer = string.Empty;

            Console.WriteLine(
@"Please input vehicle License Plate");
            licensePlate = Console.ReadLine();

            // TODO check validation 
            if (m_Garage.LicensePlateExists(licensePlate))
            {
                Console.WriteLine(
@"The given license plate already exists");
                m_Garage.SetVehicleInGarageStatus(licensePlate, eVehicleStatus.InProgress);
            }
            else
            {
                Type vehicleType;
                Vehicle vehicleToAdd;

                Console.WriteLine(
@"The given license plate does not exist, please add it to the garage");
                vehicleType = selectVehicleType();
                getCommonPropertiesForAllVehicle(ref modelName, ref wheelManufacturer, ref engineType);
                vehicleToAdd = createNewVehicle(vehicleType, licensePlate, modelName, wheelManufacturer, engineType);
                m_Garage.AddVehicleToGarage("Customer", "123", vehicleToAdd);
            }

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

        private void getCommonPropertiesForAllVehicle(ref string io_ModelName, ref string io_WheelManufacturer, ref Type io_EngineType)
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
            Vehicle vehicleToAdd = Garage.GetNewVehicleFromFactory(i_VehicleType, i_LicensePlate, i_ModelName, i_WheelManufacturer, i_EngineType);
            MethodInfo userPropertiesMethod = vehicleToAdd.GetType().GetMethod("GetUserInputPropertiesForNewVehicle");
            Dictionary<string, PropertyInfo> uninitializeProperties =
                (Dictionary<string, PropertyInfo>)userPropertiesMethod.Invoke(vehicleToAdd, new object[] { });
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

        private byte getNumberInputFromUser(byte i_MinValidSelection, byte i_MaxValidSelection)
        {
            bool isValidInput = false;
            byte userSelection = 0; // TODO change to nullable?

            while (!isValidInput)
            {
                while (!Byte.TryParse(Console.ReadLine(), out userSelection))
                {
                    Console.WriteLine("Input format error please input a number");
                }

                if (userSelection >= i_MinValidSelection && userSelection <= i_MaxValidSelection)
                {
                    isValidInput = true;
                }
                else
                {
                    // TODO use exception
                    Console.WriteLine("Please input a number between {0} and {1}", i_MinValidSelection, i_MaxValidSelection);
                }
            }

            return userSelection;
        }

        protected override void PrintLicensePlatesInGarage()
        {
            eVehicleStatus? filter = null;
            byte i = 1;
            string userSelection = "";
            bool isValidSelection = false;

            Console.WriteLine("Select filter for list of license plates:");
            foreach (eVehicleStatus status in Enum.GetValues(typeof(eVehicleStatus)))
            {
                Console.WriteLine("- {1}", i, status.ToString());
                i++;
            }
            Console.WriteLine("- No filter", i);

            while (!isValidSelection)
            {
                userSelection = Console.ReadLine();

                if (userSelection.ToUpper() == "NO FILTER")
                {
                    filter = null;
                    isValidSelection = true;
                }
                else
                {
                    try
                    {
                        filter = (eVehicleStatus)Enum.Parse(typeof(eVehicleStatus), userSelection); // TODO not sure if this works
                        isValidSelection = true;
                    }
                    catch
                    {
                        filter = null;
                        Console.WriteLine("invalid filter entered, try again (case sensitive)");
                    }
                }
            }

            printLicensePlatesInGarageWithParameter(filter);
        }

        private void printLicensePlatesInGarageWithParameter(eVehicleStatus? i_Status)
        {
            List<string> licensePlateArr = m_Garage.GetLicensePlatesByStatusFilter(i_Status);

            if (licensePlateArr.Count == 0)
            {
                Console.WriteLine("No matching license plates found.");
            }
            else
            {
                foreach (string licensePlate in licensePlateArr)
                {
                    Console.WriteLine(licensePlate);
                }
            }
            Console.Write(
@"
(Press any key to continue)");
            Console.ReadKey();
        }

        protected override void ChangeVehicleStatus()
        {
            // TODO get parameters (string licensePlate, eVehicleStatus status) from user and call the relevant function
            throw new NotImplementedException();
        }

        protected override void FillAirInWheels()
        {
            // TODO get parameters (string licensePlate) from user and call the relevant function
            throw new NotImplementedException();
        }

        protected override void FillFuelInVehicle()
        {
            // TODO get parameters (string i_LicensePlate, eFuelType i_FuelType, float i_AmountToAdd) from user and call the relevant function
            throw new NotImplementedException();
        }

        protected override void ChargeBatteryInVehicle()
        {
            // TODO get parameters (string i_LicensePlate, float i_AmountToAdd) from user and call the relevant function
            throw new NotImplementedException();
        }

        protected override void PrintVehicleInfo()
        {
            // TODO get parameters (string i_LicensePlate, float i_AmountToAdd) from user and call the relevant function          

            //Console.WriteLine(m_Garage.GetVehicleInformation(i_LicensePlate));
            throw new NotImplementedException();
        }

        protected override void ExitProgram()
        {
            Console.WriteLine(
@"End of program.
Have a nice day.
(Press any key to exit)");
            Console.ReadKey();
            m_EndOfProgram = true;
        }
    }
}
