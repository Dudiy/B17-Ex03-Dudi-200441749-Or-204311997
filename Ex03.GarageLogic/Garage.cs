/*
 * assumption for all functions in this class:
 *  - all calls to functions with a specific license plate assume that the license plate is in the garage,
 *    if a function is called with a license plate that is not in the garage it will do nothing and return null/0 value
 * 
 */

using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, VehicleInGarage> m_VehiclesInGarage = new Dictionary<string, VehicleInGarage>();
        private static readonly VehicleFactory sr_VehicleFactory = new VehicleFactory();

        // add a new initialized vehicle to the garage, if the license plate exists the vehicle isn't added
        // assumption: the given key is not in the garage yet, but it is checked to avoid bugs
        public void AddVehicleToGarage(string i_OwnerName, string i_OwnerPhone, Vehicle i_Vehicle)
        {
            string key = i_Vehicle.LicensePlate;

            if (!LicensePlateExists(key))
            {
                VehicleInGarage newVehicle = new VehicleInGarage(i_OwnerName, i_OwnerPhone, i_Vehicle);
                m_VehiclesInGarage.Add(key, newVehicle);
            }
        }

        // returns true if a vehicle with the given license plate exists in the garage
        public bool LicensePlateExists(string i_LicensePlate)
        {
            return m_VehiclesInGarage.ContainsKey(i_LicensePlate);
        }

        // changes the status of a vehicle in the garage, if an invalid license plate is entered nothing changes
        public void SetVehicleInGarageStatus(string i_LicensePlate, eVehicleStatus i_VehicleStatus)
        {
            if (LicensePlateExists(i_LicensePlate))
            {
                m_VehiclesInGarage[i_LicensePlate].Status = i_VehicleStatus;
            }
        }

        // returns a list of all license plates in the garage according to a given status filter (all if null)
        public List<string> GetLicensePlatesByStatusFilter(eVehicleStatus? i_StatusFilter)
        {
            List<string> licensePlatesList = new List<string>();

            foreach (KeyValuePair<string, VehicleInGarage> vehicle in m_VehiclesInGarage)
            {
                if (vehicle.Value.Status == i_StatusFilter || i_StatusFilter == null)
                {
                    licensePlatesList.Add(vehicle.Value.LicensePlate);
                }
            }

            return licensePlatesList;
        }

        // calls the ctor of vehicle in vehicle factory and returns the new object created
        public static Vehicle GetNewVehicleFromFactory(Type i_VehicleType, string i_LicensePlate, string i_ModelName, string i_WheelManufacturer, Type i_EngineType)
        {
            return sr_VehicleFactory.NewVehicle(i_VehicleType, i_LicensePlate, i_ModelName, i_WheelManufacturer, i_EngineType);
        }

        // return the engine type of a given license plate
        public Type GetEngineType(string i_LicensePlate)
        {
            Type engineType = null;

            if (LicensePlateExists(i_LicensePlate))
            {
                engineType = m_VehiclesInGarage[i_LicensePlate].EngineType;
            }

            return engineType;
        }

        // return the status of a given license plate
        public eVehicleStatus GetVehicleStatus(string i_LicensePlate)
        {
            eVehicleStatus status = 0;

            if (LicensePlateExists(i_LicensePlate))
            {
                status = m_VehiclesInGarage[i_LicensePlate].Status;
            }

            return status;
        }

        // fill air in all wheels to max for a given license plate
        public void FillAirInWheels(string i_LicensePlate)
        {
            if (LicensePlateExists(i_LicensePlate))
            {
                m_VehiclesInGarage[i_LicensePlate].FillAirToMax();
            }
        }

        // get vehicle information of a given license plate
        public string GetVehicleInformation(string i_LicensePlate)
        {
            string infoStr = String.Empty;

            if (LicensePlateExists(i_LicensePlate))
            {
                infoStr = m_VehiclesInGarage[i_LicensePlate].ToString();
            }

            return infoStr;
        }


        public void ChargeVehicle(string i_LicensePlate, float i_AmountEnergyToFill)
        {
            if (LicensePlateExists(i_LicensePlate))
            {
                m_VehiclesInGarage[i_LicensePlate].Charge(i_AmountEnergyToFill);
            }
        }

        public void FuelVehicle(string i_LicensePlate, float i_AmountEnergyToFill, eFuelType i_FuelType)
        {
            if (LicensePlateExists(i_LicensePlate))
            {
                m_VehiclesInGarage[i_LicensePlate].FillFuel(i_AmountEnergyToFill, i_FuelType);
            }
        }

        public float GetPercentOfEnergyRemaining(string i_LicensePlate)
        {
            float percentRemaining = 0f;

            if (LicensePlateExists(i_LicensePlate))
            {
                percentRemaining = m_VehiclesInGarage[i_LicensePlate].PercentOfEnergyRemaining;
            }

            return percentRemaining;
        }
    }
}
