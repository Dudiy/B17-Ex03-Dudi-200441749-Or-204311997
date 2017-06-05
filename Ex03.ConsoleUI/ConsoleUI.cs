using System;
using Ex03.GarageLogic;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;

namespace Ex03.ConsoleUI
{
    // TODO change all getline of single digits to "Console.ReadKey().KeyChar"
    internal class ConsoleUI : UserInterface
    {
        private bool m_EndOfProgram = false;

        internal ConsoleUI(Garage i_Garage) : base(i_Garage) { }

        internal override void run()
        {
            ushort userSelection;

            do
            {
                Console.Clear();
                Console.WriteLine(
@"Hello and welcome to the new and improved garage managing application :)");
                userSelection = getActionRequestFromUser();
                //string str = sr_AvailableActionsForUser[userSelection].Value;
                Console.Clear();
                string methodStr = sr_AvailableActionsForUser[userSelection].Value;
                MethodInfo requiredMethod = GetType().GetMethod(methodStr, BindingFlags.NonPublic | BindingFlags.Instance);//, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Default);
                requiredMethod.Invoke(this, new object[] { });
                Console.WriteLine("(press any key to continue)");
                Console.ReadKey();
            } while (!m_EndOfProgram);
        }

        private ushort getActionRequestFromUser()
        {
            ushort numOptions = (ushort)sr_AvailableActionsForUser.Count;
            ushort userSelection;

            Console.WriteLine(
@"What would you like to do next?
");
            foreach (KeyValuePair<ushort, KeyValuePair<string, string>> action in sr_AvailableActionsForUser)
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

            licensePlate = getLicensePlateFromUser();

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
                string customerName, customerNumber;

                Console.WriteLine(
@"The given license plate does not exist, please add it to the garage
");
                vehicleType = selectVehicleType();
                getCommonPropertiesForAllVehicle(out modelName, out wheelManufacturer, out engineType);
                vehicleToAdd = createNewVehicle(vehicleType, licensePlate, modelName, wheelManufacturer, engineType);
                getCustomerInfo(out customerName, out customerNumber);
                m_Garage.AddVehicleToGarage(customerName, customerNumber, vehicleToAdd);
                Console.WriteLine(
@"
Successfully added the vehicle to the garage!
");
            }

        }

        private Type selectVehicleType()
        {
            ushort input = 0;

            Console.WriteLine(
@"Please select the vehicle type:");
            // print all vehicle types available in VehicleFactory
            for (int i = 0; i < VehicleFactory.NumOfVehicleTypes; i++)
            {
                Console.WriteLine(
@"{0}. {1}",
i + 1,
VehicleFactory.GetVehicleTypeAtI(i).Key);
            }

            input = getNumberInputFromUser(1, (ushort)VehicleFactory.NumOfVehicleTypes);

            return VehicleFactory.GetVehicleTypeAtI(input - 1).Value;
        }

        private Type selectEngineType()
        {
            ushort input = 0;

            Console.WriteLine(
@"Please select the engine type:");
            // print all vehicle types available in VehicleFactory
            for (int i = 0; i < VehicleFactory.NumOfEngineTypes; i++)
            {
                Console.WriteLine(
@"{0}. {1}",
i + 1,
VehicleFactory.GetEngineTypeAtI(i).Key);
            }

            input = getNumberInputFromUser(1, (ushort)VehicleFactory.NumOfEngineTypes);

            return VehicleFactory.GetEngineTypeAtI(input - 1).Value;

        }

        // TODO change to out
        private void getCommonPropertiesForAllVehicle(out string o_ModelName, out string o_WheelManufacturer, out Type o_EngineType)
        {
            o_EngineType = selectEngineType();
            Console.Write(
@"Please enter the model name: ");
            o_ModelName = Console.ReadLine();
            Console.Write(
@"Please enter the wheel manufacturer: ");
            o_WheelManufacturer = Console.ReadLine();
        }

        private Vehicle createNewVehicle(Type i_VehicleType, string i_LicensePlate, string i_ModelName, string i_WheelManufacturer, Type i_EngineType)
        {
            Vehicle vehicleToAdd = Garage.GetNewVehicleFromFactory(i_VehicleType, i_LicensePlate, i_ModelName, i_WheelManufacturer, i_EngineType);

            // ==========================================================================================
            foreach (KeyValuePair<string, string> setValueMethod in vehicleToAdd.UserInputFunctionsList)
            {
                bool setSucceded = false;
                while (!setSucceded)
                {
                    try
                    {
                        Console.Write("Enter {0}: ", setValueMethod.Key);
                        string input = Console.ReadLine();
                        vehicleToAdd.GetType().GetMethod(setValueMethod.Value).Invoke(vehicleToAdd, new object[] { input });
                        setSucceded = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.InnerException.Message);
                    }

                }
            }


            // ==========================================================================================
            //            MethodInfo userPropertiesMethod = vehicleToAdd.GetType().GetMethod("GetUserInputPropertiesForNewVehicle");
            //            Dictionary<string, PropertyInfo> uninitializedProperties =
            //                (Dictionary<string, PropertyInfo>)userPropertiesMethod.Invoke(vehicleToAdd, new object[] { });
            //            string inputFromUser;

            //            foreach (KeyValuePair<string, PropertyInfo> propertyAndDescriptionPair in uninitializedProperties)
            //            {
            //                bool validPropertyInput = false;

            //                while (!validPropertyInput)
            //                {
            //                    Console.WriteLine(
            //@"please input {0}:",
            //propertyAndDescriptionPair.Key);
            //                    inputFromUser = Console.ReadLine();
            //                    try
            //                    {
            //                        propertyAndDescriptionPair.Value.GetSetMethod().Invoke(vehicleToAdd, new object[] { inputFromUser });
            //                        validPropertyInput = true;
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        Console.WriteLine(
            //@"error - {0}. please try again",
            //ex.InnerException.Message);
            //                    }
            //                }
            //            }

            return vehicleToAdd;
        }

        // TODO name
        private ushort getNumberInputFromUser(ushort i_MinValidSelection, ushort i_MaxValidSelection)
        {
            bool isValidInput = false;
            ushort userSelection = 0; // TODO change to nullable?

            while (!isValidInput)
            {
                while (!ushort.TryParse(Console.ReadLine(), out userSelection))
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
                MethodInfo fillEnergyInVehicleMethod = typeof(Garage).GetMethod(
                    "FillEnergyInVehicle", new Type[] { typeof(string), typeof(eFuelType), typeof(float) });
                fillEnergyInVehicleMethod.Invoke(m_Garage, new object[] {
                    licensePlate, fuelType, amountEnergyToAdd });
                //m_Garage.FillEnergyInVehicle(licensePlate, fuelType, amountEnergyToAdd);
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
            // also check format validation to: licensePlate, amountEnergyToAdd
            string licensePlate = getLicensePlateFromUser();
            float amountEnergyToAdd = getAmountEnergyToAddFromUser();

            try
            {
                MethodInfo fillEnergyInVehicleMethod = typeof(Garage).GetMethod(
                    "FillEnergyInVehicle", new Type[] { typeof(string), typeof(float) });
                fillEnergyInVehicleMethod.Invoke(m_Garage, new object[] {
                    licensePlate, amountEnergyToAdd });
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


            do
            {
                Console.Write(
@"License plate: ");
                userInput = Console.ReadLine();
                if (userInput.Equals(string.Empty))
                {
                    Console.WriteLine(
@"License plate cannot be empty.
");
                }
            } while (userInput.Equals(string.Empty));

            return userInput;
        }

        public Enum getEnumSelectionFromUser(Type i_EnumType)
        {
            Dictionary<ushort, Enum> enumDictinary = new Dictionary<ushort, Enum>();
            ushort enumCounter = 1;
            ushort userSelection;

            foreach (Enum item in Enum.GetValues(i_EnumType))
            {
                enumDictinary.Add(enumCounter, item);
                enumCounter++;
            }

            foreach (KeyValuePair<ushort, Enum> item in enumDictinary)
            {
                Console.WriteLine("{0}. {1}", item.Key, item.Value);
            }

            userSelection = getNumberInputFromUser(1, (ushort)(enumCounter - 1));

            return enumDictinary[userSelection];
        }

        //        private float getFuelTypeFromUser()
        //        {
        //            string userInput;
        //            float amountEnergyToAdd;

        //            Console.Write(
        //@"Please enter amount of fuel to add: ");
        //            userInput = Console.ReadLine();
        //            while (float.TryParse(userInput, out amountEnergyToAdd))
        //            {
        //                Console.WriteLine(
        //@"Input format error please input a number");
        //                userInput = Console.ReadLine();
        //            }

        //            return amountEnergyToAdd;
        //        }

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

        private void getCustomerInfo(out string o_Name, out string o_Number)
        {
            Console.Write(
@"Customer name: ");
            o_Name = Console.ReadLine();
            Console.Write(
@"Phone number: ");
            o_Number = Console.ReadLine();
        }
    }
}

//        private Type selectFromTypeList(TypeList i_TypeList, string i_TypeOfList)
//        {
//            ushort input = 0;
//            ushort counter = 1;
//            IEnumerable enumerator = VehicleFactory.GetVehicleTypeEnumerator(i_TypeList);

//            Console.WriteLine(
//@"Please select the {0}:",
//i_TypeOfList);
//            foreach (KeyValuePair<string, Type> type in enumerator)
//            {
//                Console.WriteLine(
//@"{0}. {1}",
//counter + 1,
//i_TypeList[counter-1].Key);
//                counter++;
//            }

//            input = getNumberInputFromUser(1, (ushort)i_TypeList.Count);

//            return i_TypeList[input - 1].Value;
//        }