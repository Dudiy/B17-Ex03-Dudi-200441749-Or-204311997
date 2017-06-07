using System;
using System.Collections.Generic;
using System.Reflection;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    // TODO change all getline of single digits to "Console.ReadKey().KeyChar"
    internal class ConsoleUI : UserInterface
    {
        private bool m_EndOfProgram = false;

        internal ConsoleUI(Garage i_Garage) : base(i_Garage) { }

        // main function to start running the program using this user interface (console)
        internal override void Run()
        {
            string selectedMethodStr;
            MethodInfo selectedMethod;

            // loop to get and invoke the requested action from the user
            do
            {
                Console.Clear();
                Console.WriteLine(
@"Hello and welcome to the new and improved garage managing application :)");
                selectedMethodStr = getActionRequestFromUser();
                Console.Clear();
                // according to the user's selection, get and invoke the matching function
                selectedMethod = GetType().GetMethod(selectedMethodStr, BindingFlags.NonPublic | BindingFlags.Instance);
                selectedMethod.Invoke(this, new object[] { });
                ConsoleUtils.PressAnyKeyToContinue();
            } while (!m_EndOfProgram);
        }

        //display all available actions to the user and return the user's selection
        private string getActionRequestFromUser()
        {
            ushort userSelection;
            ushort numOptions = (ushort)sr_AvailableActionsForUser.Count;

            Console.WriteLine(
@"What would you like to do next?
");
            // display all options to the user
            foreach (KeyValuePair<ushort, KeyValuePair<string, string>> action in sr_AvailableActionsForUser)
            {
                Console.WriteLine(
"{0}. {1}",
action.Key,
action.Value.Key);
            }

            // get the user's selection
            userSelection = ConsoleUtils.GetNumberInputFromUserInRange(0, numOptions);

            //return the name of the method that matches the user's selection
            return sr_AvailableActionsForUser[userSelection].Value;
        }

        // ==================================================== "AddNewVehicleToGarage" Functions ====================================================
        /* add a vehicle to the garage, if the license plate already exists in the garage 
           its status will be set to "in progress" otherwise a new vehicle will be added */
        protected override void AddVehicleToGarage()
        {
            string licensePlate = getLicensePlateFromUser();

            // if the license plate exists set status to InProgress
            if (isInGarage(licensePlate))
            {
                Console.WriteLine(
@"The given license plate already exists");
                m_Garage.SetVehicleInGarageStatus(licensePlate, eVehicleStatus.InProgress);
            }
            else
            {
                Console.WriteLine(
@"Please add the requested vehicle it to the garage
");
                addNewVehicleToGarage(licensePlate);
            }
        }

        // add a vehicle to the garage, assumption there is no vehicle in the garage with the given license plate
        private void addNewVehicleToGarage(string i_LicensePlate)
        {
            Type vehicleType, engineType;
            Vehicle vehicleToAdd;
            string customerName, customerNumber, modelName, wheelManufacturer;

            //vehicleType = getVehicleTypeFromUser();
            vehicleType = ConsoleUtils.SelectTypeFromListOfDescriptionAndTypePair(VehicleFactory.VehicleTypes, "vehicle");
            getCommonPropertiesForAllVehicles(out engineType, out modelName, out wheelManufacturer);
            vehicleToAdd = createNewVehicle(vehicleType, i_LicensePlate, engineType, modelName, wheelManufacturer);
            getVehicleOwnerInformation(out customerName, out customerNumber);
            m_Garage.AddVehicleToGarage(customerName, customerNumber, vehicleToAdd);
            Console.WriteLine(
@"
Vehicle with license plate {0}, was successfully added to the garage!
", i_LicensePlate);
        }

        // get all properties that are relevant to all vehicle types
        private void getCommonPropertiesForAllVehicles(out Type o_EngineType, out string o_ModelName, out string o_WheelManufacturer)
        {
            o_EngineType = ConsoleUtils.SelectTypeFromListOfDescriptionAndTypePair(VehicleFactory.EngineTypes, "engine");
            o_ModelName = ConsoleUtils.GetNonEmptyStrFromUser("Model name: ");
            o_WheelManufacturer = ConsoleUtils.GetNonEmptyStrFromUser("Wheel manufacturer: ");
        }

        /* generate a new vehicle in the garage. Sets all available parameters, and then,
           using the type (known in runtime) get all the additional unique parameters from the user */
        private Vehicle createNewVehicle(Type i_VehicleType, string i_LicensePlate, Type i_EngineType, string i_ModelName, string i_WheelManufacturer)
        {
            bool setSucceded;
            string input;
            MethodInfo currentSetMethod;
            // creates a new vehicle with available parameters using the "VehicleFactory" via "Garage"
            Vehicle vehicleToAdd = Garage.GetNewVehicleFromFactory(i_VehicleType, i_LicensePlate, i_ModelName, i_WheelManufacturer, i_EngineType);

            // after the type is known (in runtime) get the remaining parameters needed
            // setValueMethod is a pair of <description, function name>
            foreach (KeyValuePair<string, string> setValueMethod in vehicleToAdd.SetFunctionsForAddedParams)
            {
                setSucceded = false;
                while (!setSucceded)
                {
                    try
                    {
                        Console.Write(
@"Enter {0}: ",
setValueMethod.Key);
                        input = Console.ReadLine();
                        currentSetMethod = vehicleToAdd.GetType().GetMethod(setValueMethod.Value);
                        currentSetMethod.Invoke(vehicleToAdd, new object[] { input });
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

        // ==================================================== "PrintLicensePlatesInGarage" Functions ====================================================
        protected override void PrintLicensePlatesInGarage()
        {
            eVehicleStatus? filter = null;
            char userSelection = ConsoleUtils.GetYesOrNoFromUser("Would you like to add a filter to the list of license plates? (Y/N): ");

            // the above fuction is guaranteed to return Y or N and no other char
            if (userSelection == 'Y')
            {
                filter = (eVehicleStatus)ConsoleUtils.GetEnumSelectionFromUser(typeof(eVehicleStatus), "Select filter for list of license plates: ");

            }
            else if (userSelection == 'N')
            {
                filter = null;
            }

            printLicensePlatesInGarageWithParameter(filter);
        }

        // prints all license plates in the garage according to the given filter value, if null prints all license plates
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

        // ==================================================== "ChangeVehicleStatus" Functions ====================================================
        protected override void ChangeVehicleStatus()
        {
            string licensePlate;

            Console.WriteLine(
@"Change status of vehicle in garage.
");
            licensePlate = getLicensePlateFromUser();
            if (isInGarage(licensePlate))
            {
                eVehicleStatus newStatus;
                eVehicleStatus prevStatus = m_Garage.GetVehicleStatus(licensePlate);

                newStatus = (eVehicleStatus)ConsoleUtils.GetEnumSelectionFromUser(typeof(eVehicleStatus), "Please select the new status:");
                m_Garage.SetVehicleInGarageStatus(licensePlate, newStatus);
                Console.WriteLine(
@"The status of {0} was changed from {1} to {2}.
",
licensePlate,
prevStatus.ToString(),
newStatus.ToString());
            }
        }

        // ==================================================== "FillAirInWheels" Functions ====================================================
        protected override void FillAirInWheels()
        {
            string licensePlate;

            Console.WriteLine(
@"Choose vehicle too fill air in all wheels to max.");
            licensePlate = getLicensePlateFromUser();
            if (isInGarage(licensePlate))
            {
                try
                {
                    m_Garage.FillAirInWheels(licensePlate);
                    Console.WriteLine(
@"All wheels in {0} were filled to max
"
, licensePlate);
                }
                catch (ArgumentException argumentEx)
                {
                    Console.WriteLine(
@"{0}
", argumentEx.Message);
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
        }

        // ==================================================== "FillFuelInVehicle" Functions ====================================================
        protected override void FillFuelInVehicle()
        {
            string licensePlate;
            bool fuelingSuccessfull = false;

            Console.WriteLine(
@"Choose vehicle to fuel");
            licensePlate = getLicensePlateFromUser();
            if (isInGarage(licensePlate))
            {
                if (m_Garage.GetEngineType(licensePlate) == typeof(FuelEngine))
                {
                    while (!fuelingSuccessfull)
                    {
                        fuelingSuccessfull = tryFillFuelInVehicle(licensePlate);
                    }
                }
                else
                {
                    Console.WriteLine(
@"
Can't fuel a non fuelable vehicle.
");
                }
            }
        }

        // assumption - the given license plate is in the garage
        private bool tryFillFuelInVehicle(string i_LicensePlate)
        {
            bool fuelingSuccessfull = false;
            float amountEnergyToAdd;
            eFuelType fuelType = (eFuelType)ConsoleUtils.GetEnumSelectionFromUser(typeof(eFuelType), "Please select fuel type");

            try
            {
                amountEnergyToAdd = getAmountEnergyToAddFromUser("Please enter amount of fuel to add in liters: ");
                m_Garage.FuelVehicle(i_LicensePlate, amountEnergyToAdd, fuelType);
                Console.WriteLine(
@"Fueled vehicle {0} with {1} liters of fuel.
The tank is currently {2} full.
",
i_LicensePlate,
amountEnergyToAdd,
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
@"Fuel amount is invalid, valid range is: {0}-{1}
"
, valueOutOfRangeException.MinValue,
valueOutOfRangeException.MaxValue);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return fuelingSuccessfull;
        }

        // ==================================================== "ChargeBatteryInVehicle" Functions ====================================================
        protected override void ChargeBatteryInVehicle()
        {
            string licensePlate;
            bool chargingSuccessfull = false;

            Console.WriteLine(
@"Choose vehicle to charge");
            licensePlate = getLicensePlateFromUser();
            if (isInGarage(licensePlate))
            {
                if (m_Garage.GetEngineType(licensePlate) == typeof(ElectricEngine))
                {
                    while (!chargingSuccessfull)
                    {

                        chargingSuccessfull = tryChargeBatteryInVehicle(licensePlate);
                    }
                }
                else
                {
                    Console.WriteLine(
    @"Can't charge a non electric vehicle.
");
                }
            }
        }

        // assumption - the given license plate is in the garage
        private bool tryChargeBatteryInVehicle(string i_LicensePlate)
        {
            bool chargingSuccessfull = false;
            float amountEnergyToAdd;

            try
            {
                amountEnergyToAdd = getAmountEnergyToAddFromUser("Please enter amount to charge in hours: ");
                m_Garage.ChargeVehicle(i_LicensePlate, amountEnergyToAdd);
                Console.WriteLine(
@"Added {0} hours to battery of {1}.
The battery is currently {2} full.
",
amountEnergyToAdd,
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
@"Fuel amount is invalid, valid range is: {0}-{1}
"
, valueOutOfRangeException.MinValue,
valueOutOfRangeException.MaxValue);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return chargingSuccessfull;
        }

        // ==================================================== Print Vehicle Info Function ====================================================
        protected override void PrintVehicleInfo()
        {
            string licensePlate = getLicensePlateFromUser();

            if (isInGarage(licensePlate))
            {
                try
                {
                    Console.WriteLine(m_Garage.GetVehicleInformation(licensePlate));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
 @"Error - {0}",
ex.Message);
                }
            }
        }

        // ==================================================== Exit Program Function ====================================================
        protected override void ExitProgram()
        {
            Console.WriteLine(
@"Thank you for using the amazing garage manager app.
Have a nice day.");
            m_EndOfProgram = true;
        }

        // ==================================================== Other Functions ====================================================
        private string getLicensePlateFromUser()
        {
            return ConsoleUtils.GetNonEmptyStrFromUser("License plate: ");
        }

        private float getAmountEnergyToAddFromUser(string i_Prompt)
        {
            string userInput;
            float amountEnergyToAdd;

            Console.Write(i_Prompt);
            userInput = Console.ReadLine();
            while (!float.TryParse(userInput, out amountEnergyToAdd))
            {
                Console.WriteLine(
@"Input format error please input a number");
                userInput = Console.ReadLine();
            }

            return amountEnergyToAdd;
        }

        // check if a given license plate is in the garage, output message to console if not
        private bool isInGarage(string i_LicensePlate)
        {
            bool found = m_Garage.LicensePlateExists(i_LicensePlate);

            if (!found)
            {
                Console.WriteLine(
@"The license plate {0}, is not in the garage.
",
i_LicensePlate);
            }

            return found;
        }
    }
}
