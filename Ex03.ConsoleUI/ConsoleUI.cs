using System;
using Ex03.GarageLogic;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Ex03.ConsoleUI
{
    // TODO change all getline of single digits to "Console.ReadKey().KeyChar"
    internal class ConsoleUI : UserInterface
    {
        private bool m_EndOfProgram = false;

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
                Console.WriteLine("(press any key to continue)");
                Console.ReadKey();
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
                Console.WriteLine(
"{0}. {1}",
action.Key,
action.Value.Key);
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

        // TODO name
        private byte getNumberInputFromUser(byte i_MinValidSelection, byte i_MaxValidSelection)
        {
            bool isValidInput = false;
            byte userSelection = 0; // TODO change to nullable?

            while (!isValidInput)
            {
                while (!Byte.TryParse(Console.ReadLine(), out userSelection))
                {
                    Console.WriteLine(
@"Input format error please input a number");
                }

                if (userSelection >= i_MinValidSelection && userSelection <= i_MaxValidSelection)
                {
                    isValidInput = true;
                }
                else
                {
                    // TODO use exception
                    Console.WriteLine(
@"Please input a number between {0} and {1}",
i_MinValidSelection,
i_MaxValidSelection);
                }
            }

            return userSelection;
        }

        protected override void PrintLicensePlatesInGarage()
        {
            eVehicleStatus? filter = null;

            Console.WriteLine(
@"Would you like to add a filter to the list of license plates? (Y/N):");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                Console.WriteLine(
@"Select filter for list of license plates:");
                filter = (eVehicleStatus)getEnumSelectionFromUser(typeof(eVehicleStatus));
            }
            else
            {
                filter = null;
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
                Console.WriteLine("All matching license plates:");
                foreach (string licensePlate in licensePlateArr)
                {
                    Console.WriteLine(licensePlate);
                }
            }
        }

        protected override void ChangeVehicleStatus()
        {
            string licensePlate;

            Console.WriteLine(
@"Change status of vehicle in garage.
");
            licensePlate = getLicensePlateFromUser();
            if (m_Garage.LicensePlateExists(licensePlate))
            {
                eVehicleStatus newStatus;
                eVehicleStatus prevStatus = m_Garage.GetVehicleStatus(licensePlate);

                Console.WriteLine("Please select the new status:");
                newStatus = (eVehicleStatus)getEnumSelectionFromUser(typeof(eVehicleStatus));
                m_Garage.SetVehicleInGarageStatus(licensePlate, newStatus);
                Console.WriteLine(
@"The status of {0} was changed from {1} to {2}.",
licensePlate,
prevStatus.ToString(),
newStatus.ToString());
            }
            else
            {
                Console.WriteLine(
@"The license plate {0}, is not in the garage.",
licensePlate);
            }
        }

        protected override void FillAirInWheels()
        {
            string licensePlate;
            Console.WriteLine(
@"Filling air in all wheels to max.");

            licensePlate = getLicensePlateFromUser();
            try
            {
                m_Garage.FillAirInWheels(licensePlate);
                Console.WriteLine("All wheels in {0} were filled to max", licensePlate);
            }
            catch (ArgumentException keyEx)
            {
                Console.WriteLine("The given licence plate is not in the garage.");
            }
            catch (ValueOutOfRangeException valueRangeEx)
            {
                Console.WriteLine(valueRangeEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - {0}", ex.Message);
            }
        }

        // TODO merge 2X next
        // TODO get parameters (string i_LicensePlate, eFuelType i_FuelType, float i_AmountToAdd) from user and call the relevant function
        protected override void FillFuelInVehicle()
        {
            // also check format validation to: licensePlate, fuelType, amountEnergyToAdd
            string licensePlate = getLicensePlateFromUser();
            eFuelType fuelType = (eFuelType)getEnumSelectionFromUser(typeof(eFuelType));
            float amountEnergyToAdd = getAmountEnergyToAddFromUser();

            try
            {
                m_Garage.FillEnergyInVehicle(licensePlate, fuelType, amountEnergyToAdd);
            }
            catch (NotImplementedException notImplementedException)
            {
                Console.WriteLine(notImplementedException.Message);
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
            }
            catch (ValueOutOfRangeException valueOutOfRangeException)
            {
                Console.WriteLine(valueOutOfRangeException.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        // TODO get parameters (string i_LicensePlate, float i_AmountToAdd) from user and call the relevant function
        protected override void ChargeBatteryInVehicle()
        {
            //// also check format validation to: licensePlate, fuelType, amountEnergyToAdd
            //string licensePlate = getLicensePlateFromUser();
            //float amountEnergyToAdd = getAmountEnergyToAddFromUser();

            //try
            //{
            //    m_Garage.FillEnergyInVehicle(licensePlate, amountEnergyToAdd);
            //}
            //catch (NotImplementedException notImplementedException)
            //{
            //    Console.WriteLine(notImplementedException.Message);
            //}
            //catch (ArgumentException argumentException)
            //{
            //    Console.WriteLine(argumentException.Message);
            //}
            //catch (ValueOutOfRangeException valueOutOfRangeException)
            //{
            //    Console.WriteLine(valueOutOfRangeException.Message);
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception.Message);
            //}
        }

        protected override void PrintVehicleInfo()
        {
            string licensePlate = getLicensePlateFromUser();

            try
            {
                Console.WriteLine(m_Garage.GetVehicleInformation(licensePlate));
            }
            catch (KeyNotFoundException keyEx)
            {
                Console.WriteLine(
@"The given licence plate is not in the garage.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
@"Error - {0}", 
ex.Message);
            }
        }

        protected override void ExitProgram()
        {
            Console.WriteLine(
@"End of program.
Have a nice day.");
            m_EndOfProgram = true;
        }

        private string getLicensePlateFromUser()
        {
            string userInput = string.Empty;

            Console.Write(
@"License plate: ");

            do
            {
                userInput = Console.ReadLine();
            } while (userInput == string.Empty);

            return userInput;
        }

        public Enum getEnumSelectionFromUser(Type i_EnumType)
        {
            Dictionary<byte, Enum> enumDictinary = new Dictionary<byte, Enum>();
            byte enumCounter = 1;
            byte userSelection;

            foreach (Enum item in Enum.GetValues(i_EnumType))
            {
                enumDictinary.Add(enumCounter, item);
                enumCounter++;
            }

            foreach (KeyValuePair<byte, Enum> item in enumDictinary)
            {
                Console.WriteLine("{0}. {1}", item.Key, item.Value);
            }

            userSelection = getNumberInputFromUser(1, (byte)(enumCounter - 1));

            return enumDictinary[userSelection];
        }

        //public void test()
        //{
        //    double d;
        //    bool valid = false;

        //    do
        //    {
        //        Console.WriteLine("Please input");
        //        valid = Double.TryParse("input", out d)
        //    } while (!valid);


        //}

        private float getFuelTypeFromUser()
        {
            string userInput;
            float amountEnergyToAdd;

            Console.Write(
@"Please enter amount energy to add: ");
            userInput = Console.ReadLine();
            while (float.TryParse(userInput, out amountEnergyToAdd))
            {
                Console.WriteLine(
@"Input format error please input a number");
                userInput = Console.ReadLine();
            }

            return amountEnergyToAdd;
        }

        private float getAmountEnergyToAddFromUser()
        {
            string userInput;
            float amountEnergyToAdd;

            Console.Write(
@"Please enter amount energy to add: ");
            userInput = Console.ReadLine();
            while (!float.TryParse(userInput, out amountEnergyToAdd))
            {
                Console.WriteLine(
@"Input format error please input a number");
                userInput = Console.ReadLine();
            }

            return amountEnergyToAdd;
        }

        private void pressAnyKeyToContinue()
        {
            Console.WriteLine("(press any key to continue)");
            Console.ReadKey();
        }
    }
}
