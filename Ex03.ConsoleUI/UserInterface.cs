using System;
using Ex03.GarageLogic;
using System.Collections.Generic;
using System.Text;

namespace Ex03.ConsoleUI
{
    public abstract class UserInterface
    {
        protected Garage m_Garage;
        protected static readonly Dictionary<byte, KeyValuePair<string, string>> sr_AvailableActionsForUser = new Dictionary<byte, KeyValuePair<string, string>>();
        protected const byte k_ExitProgram = 0;

        // protected static readonly Dictionary<string, string> sr_DescriptionAndActions = new Dictionary<string, string>();

        static UserInterface()
        {
            sr_AvailableActionsForUser.Add(0, new KeyValuePair<string, string>("Exit program", "ExitProgram"));
            sr_AvailableActionsForUser.Add(1, new KeyValuePair<string, string>("Add a new vehicle", "AddNewVehicleToGarage"));
            sr_AvailableActionsForUser.Add(2, new KeyValuePair<string, string>("Display license plates", "PrintLicensePlatesInGarage"));
            sr_AvailableActionsForUser.Add(3, new KeyValuePair<string, string>("Change vehicle status", "ChangeVehicleStatus"));
            sr_AvailableActionsForUser.Add(4, new KeyValuePair<string, string>("Fill air in wheels", "FillAirInWheels"));
            sr_AvailableActionsForUser.Add(5, new KeyValuePair<string, string>("Fill fuel in vehicle", "FillFuelInVehicle"));
            sr_AvailableActionsForUser.Add(6, new KeyValuePair<string, string>("Charge battery in vehicle", "ChargeBatteryInVehicle"));
            sr_AvailableActionsForUser.Add(7, new KeyValuePair<string, string>("Print vehicle info", "PrintVehicleInfo"));

        }

        public UserInterface(Garage i_Garage)
        {
            m_Garage = i_Garage;
        }

        public abstract void run();

        public abstract void AddNewVehicleToGarage();

        public abstract void PrintLicensePlatesInGarage();

        public abstract void ChangeVehicleStatus();

        public abstract void FillAirInWheels();

        public abstract void FillFuelInVehicle();

        public abstract void ChargeBatteryInVehicle();

        public abstract void PrintVehicleInfo();

        public abstract void ExitProgram();
    }
}
