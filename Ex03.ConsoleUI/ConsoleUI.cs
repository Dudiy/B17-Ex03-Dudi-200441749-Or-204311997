using System;
using Ex03.GarageLogic;
using System.Collections.Generic;
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
            ushort userSelection;
            string selectedMethodStr;
            MethodInfo selectedMethod;

            // loop to get and invoke the requested action from the user
            do
            {
                Console.Clear();
                Console.WriteLine(
@"Hello and welcome to the new and improved garage managing application :)");
                userSelection = getActionRequestFromUser();
                Console.Clear();
                selectedMethodStr = sr_AvailableActionsForUser[userSelection].Value;
                selectedMethod = base.GetType().GetMethod(selectedMethodStr, BindingFlags.NonPublic | BindingFlags.Instance);// TODO delete , BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Default);
                selectedMethod.Invoke(this, new object[] { });
                ConsoleUtils.PressAnyKeyToContinue();
            } while (!m_EndOfProgram);
        }

        //display all available actions to the user and return he user's selection
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

            userSelection = ConsoleUtils.GetNumberInputFromUserInRange(0, numOptions);

            return userSelection;
        }

        // ===================================== Add New Vehicle To Garage Functions ====================================
        /* add a vehicle to the garage, if the license plate already exists in the garage 
           its status will be set to "in progress" otherwise a new vehicle will be added */
        protected override void AddNewVehicleToGarage()
        {
            string licensePlate;

            licensePlate = getLicensePlateFromUser();
            // if the licnense plate exists set status to InProgress
            if (m_Garage.LicensePlateExists(licensePlate))
            {
                Console.WriteLine(
@"The given license plate already exists");
                m_Garage.SetVehicleInGarageStatus(licensePlate, eVehicleStatus.InProgress);
            }
            else
            {
                AddVehicleToGarage(licensePlate);
            }
        }

        private void AddVehicleToGarage(string licensePlate)
        {
            Type vehicleType, engineType;
            Vehicle vehicleToAdd;
            string customerName, customerNumber, modelName, wheelManufacturer;

            Console.WriteLine(
@"The given license plate does not exist, please add it to the garage
");
            vehicleType = getVehicleTypeFromUser();
            getCommonPropertiesForAllVehicles(out modelName, out wheelManufacturer, out engineType);
            vehicleToAdd = createNewVehicle(vehicleType, licensePlate, modelName, wheelManufacturer, engineType);
            getVehicleOwnerInformation(out customerName, out customerNumber);
            m_Garage.AddVehicleToGarage(customerName, customerNumber, vehicleToAdd);
            Console.WriteLine(
@"
Successfully added the vehicle to the garage!
");
        }

        // display all vehicle types and get selection from user
        private Type getVehicleTypeFromUser()
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

            input = ConsoleUtils.GetNumberInputFromUserInRange(1, (ushort)VehicleFactory.NumOfVehicleTypes);

            return VehicleFactory.GetVehicleTypeAtI(input - 1).Value;
        }

        // get all properties that are relevant to all vehicle types
        private void getCommonPropertiesForAllVehicles(out string o_ModelName, out string o_WheelManufacturer, out Type o_EngineType)
        {
            o_EngineType = selectEngineType();
            o_ModelName = ConsoleUtils.GetNonEmptyStrFromUser("Model name: ");
            o_WheelManufacturer = ConsoleUtils.GetNonEmptyStrFromUser("wheel manufacturer: ");
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

            input = ConsoleUtils.GetNumberInputFromUserInRange(1, (ushort)VehicleFactory.NumOfEngineTypes);

            return VehicleFactory.GetEngineTypeAtI(input - 1).Value;
        }

        /* generate a new vehicle in the garag. Sets all available parameters, and then,
           using the type (know in runtime) get all the additional unique parameters from the user */
        private Vehicle createNewVehicle(Type i_VehicleType, string i_LicensePlate, string i_ModelName, string i_WheelManufacturer, Type i_EngineType)
        {
            // creates a new vehicle with available parameters using the "VehicleFactory" via "Garage"
            Vehicle vehicleToAdd = Garage.GetNewVehicleFromFactory(i_VehicleType, i_LicensePlate, i_ModelName, i_WheelManufacturer, i_EngineType);
            bool setSucceded;

            // after the type is know (in runtime) get the remaining parameters needed
            foreach (KeyValuePair<string, string> setValueMethod in vehicleToAdd.SetFunctionsForAddedParams)
            {
                setSucceded = false;
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

            return vehicleToAdd;
        }

        private void getVehicleOwnerInformation(out string o_Name, out string o_Number)
        {
            o_Name = ConsoleUtils.GetNonEmptyStrFromUser("Customer name: ");
            o_Number = ConsoleUtils.GetNonEmptyStrFromUser("Phone number: ");
        }

        // ===================================== Print License Plates In Garage Functions ===============================
        protected override void PrintLicensePlatesInGarage()
        {
            eVehicleStatus? filter = null;
            char userSelection;

            userSelection = ConsoleUtils.GetYesOrNoFromUser("Would you like to add a filter to the list of license plates? (Y/N): ");
            // the above fuction is guaranteed to return Y or N and no other char
            if (userSelection == 'Y')
            {
                Console.WriteLine(
@"Select filter for list of license plates:");
                filter = (eVehicleStatus)ConsoleUtils.GetEnumSelectionFromUser(typeof(eVehicleStatus));

            }
            else if (userSelection == 'N')
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
                Console.WriteLine(
@"No matching license plates found.");
            }
            else
            {
                Console.WriteLine(
@"All matching license plates:");
                foreach (string licensePlate in licensePlateArr)
                {
                    Console.WriteLine(licensePlate);
                }
            }
        }

        // ===================================== Change Vehicle Status Functions ========================================
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

                newStatus = (eVehicleStatus)ConsoleUtils.GetEnumSelectionFromUser(
                    typeof(eVehicleStatus), "Please select the new status:");
                m_Garage.SetVehicleInGarageStatus(licensePlate, newStatus);
                Console.WriteLine(
@"The status of {0} was changed from {1} to {2}.
",
licensePlate,
prevStatus.ToString(),
newStatus.ToString());
            }
            else
            {
                Console.WriteLine(
@"The license plate {0}, is not in the garage.
",
licensePlate);
            }
        }

        // ===================================== Fill Air In Wheels Functions ===========================================
        protected override void FillAirInWheels()
        {
            string licensePlate;

            Console.WriteLine(
@"Choose vehicle too fill air in all wheels to max");

            licensePlate = getLicensePlateFromUser();
            try
            {
                m_Garage.FillAirInWheels(licensePlate);
                Console.WriteLine(
@"All wheels in {0} were filled to max
"
, licensePlate);
            }
            catch (ArgumentException keyEx)
            {
                Console.WriteLine(
@"The given licence plate is not in the garage.
");
            }
            catch (ValueOutOfRangeException valueRangeEx)
            {
                Console.WriteLine(
@"{0}
", valueRangeEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
@"Error - {0}
", ex.Message);
            }
        }

        // ===================================== Fill Fuel In Vehicle Functions =========================================
        protected override void FillFuelInVehicle()
        {
            string licensePlate;
            bool fuelingSuccessfull = false;
            float amountEnergyToAdd;

            Console.WriteLine(
@"Choose vehicle to fuel");
            licensePlate = getLicensePlateFromUser();
            if (m_Garage.GetEngineType(licensePlate) == typeof(MotorEngine))
            {
                while (!fuelingSuccessfull)
                {
                    amountEnergyToAdd = getAmountEnergyToAddFromUser();
                    fuelingSuccessfull = tryFillFuelInVehicle(licensePlate, amountEnergyToAdd);
                }
            }
            else
            {
                Console.WriteLine(
@"Can't fuel a non fuelable vehicle.
");
            }
        }

        private bool tryFillFuelInVehicle(string i_LicensePlate, float i_AmountEnergyToAdd)
        {
            bool fuelingSuccessfull = false;
            eFuelType fuelType = (eFuelType)ConsoleUtils.GetEnumSelectionFromUser(
                        typeof(eFuelType), "Please select engine type");

            try
            {
                m_Garage.FillEnergyInVehicle(i_LicensePlate, i_AmountEnergyToAdd, fuelType);
                Console.WriteLine(
@"Fueled vehicle {0} with {1} liters of fuel.
the tank is currently {2} full.
", i_LicensePlate,
i_AmountEnergyToAdd,
m_Garage.GetPercentOfEnergyRemaining(i_LicensePlate).ToString("P"));
                // will only reach the next line if no exception was thrown
                fuelingSuccessfull = true;
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
                Console.WriteLine(
@"Fuel amount is invalid, valid range is: {0}-{1}"
, valueOutOfRangeException.MinValue,
valueOutOfRangeException.MaxValue);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return fuelingSuccessfull;
        }

        // ===================================== Charge Battery In Vehicle Functions ====================================
        protected override void ChargeBatteryInVehicle()
        {
            string licensePlate;
            float amountEnergyToAdd;
            bool chargingSuccessfull = false;

            Console.WriteLine(
@"Choose vehicle to charge");
            licensePlate = getLicensePlateFromUser();
            if (m_Garage.GetEngineType(licensePlate) == typeof(ElectricEngine))
            {
                while (!chargingSuccessfull)
                {
                    amountEnergyToAdd = getAmountEnergyToAddFromUser();
                    chargingSuccessfull = tryChargeBatteryInVehicle(licensePlate, amountEnergyToAdd);
                }
            }
            else
            {
                Console.WriteLine(
@"Can't charge a non electric vehicle.
");
            }
        }

        private bool tryChargeBatteryInVehicle(string i_LicensePlate, float i_AmountEnergyToAdd)
        {
            bool chargingSuccessfull = false;
            eFuelType fuelType = (eFuelType)ConsoleUtils.GetEnumSelectionFromUser(
                        typeof(eFuelType), "Please select engine type");

            try
            {
                m_Garage.FillEnergyInVehicle(i_LicensePlate, i_AmountEnergyToAdd);
                Console.WriteLine(
@"Added {0} hours to battery of {1}.
the tank is currently {2} full.
", i_AmountEnergyToAdd,
i_LicensePlate,
m_Garage.GetPercentOfEnergyRemaining(i_LicensePlate).ToString("P"));
                // will only reach the next line if no exception was thrown
                chargingSuccessfull = true;
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
                Console.WriteLine(
@"Fuel amount is invalid, valid range is: {0}-{1}"
, valueOutOfRangeException.MinValue,
valueOutOfRangeException.MaxValue);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return chargingSuccessfull;
        }

        // ===================================== Print Vehicle Info Function ============================================
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

        // ======================================== Exit Program Function ===============================================
        protected override void ExitProgram()
        {
            Console.WriteLine(
@"End of program.
Have a nice day.");
            m_EndOfProgram = true;
        }

        // ========================================= Other Functions ====================================================
        private string getLicensePlateFromUser()
        {
            return ConsoleUtils.GetNonEmptyStrFromUser("License plate: ");
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
    }
}
