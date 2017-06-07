/*
 * B17_Ex03: UserInterface.cs
 * 
 * Abstract class that defines all functions that any user interface must implement.
 * hold a dictionary of all available functions for the application.
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
 */

using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal abstract class UserInterface
    {
        protected static readonly Dictionary<ushort, KeyValuePair<string, string>> sr_AvailableActionsForUser =
            new Dictionary<ushort, KeyValuePair<string, string>>();
        // (according to stylecop we need a blank line here ?!)
        protected Garage m_Garage;

        // initialize Dictionary of <id, <function description, function name>>
        static UserInterface()
        {
            sr_AvailableActionsForUser.Add(0, new KeyValuePair<string, string>("Exit program", "ExitProgram"));
            sr_AvailableActionsForUser.Add(1, new KeyValuePair<string, string>("Add a vehicle to the garage", "AddVehicleToGarage"));
            sr_AvailableActionsForUser.Add(2, new KeyValuePair<string, string>("Display license plates", "PrintLicensePlatesInGarage"));
            sr_AvailableActionsForUser.Add(3, new KeyValuePair<string, string>("Change vehicle status", "ChangeVehicleStatus"));
            sr_AvailableActionsForUser.Add(4, new KeyValuePair<string, string>("Fill air in wheels", "FillAirInWheels"));
            sr_AvailableActionsForUser.Add(5, new KeyValuePair<string, string>("Fill fuel in vehicle", "FillFuelInVehicle"));
            sr_AvailableActionsForUser.Add(6, new KeyValuePair<string, string>("Charge battery in vehicle", "ChargeBatteryInVehicle"));
            sr_AvailableActionsForUser.Add(7, new KeyValuePair<string, string>("Print vehicle info", "PrintVehicleInfo"));
        }

        protected UserInterface(Garage i_Garage)
        {
            m_Garage = i_Garage;
        }

        internal abstract void Run();

        protected abstract void AddVehicleToGarage();

        protected abstract void PrintLicensePlatesInGarage();

        protected abstract void ChangeVehicleStatus();

        protected abstract void FillAirInWheels();

        protected abstract void FillFuelInVehicle();

        protected abstract void ChargeBatteryInVehicle();

        protected abstract void PrintVehicleInfo();

        protected abstract void ExitProgram();
    }
}
