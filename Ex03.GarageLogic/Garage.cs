using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, VehicleInGarage> m_VehiclesInGarage = new Dictionary<string, VehicleInGarage>();
        private static readonly VehicleFactory sr_VehicleFactory = new VehicleFactory();

        public void AddVehicleToGarage(string i_OwnerName, string i_OwnerPhone, Vehicle i_Vehicle)
        {
            VehicleInGarage newVehicle = new VehicleInGarage(i_OwnerName, i_OwnerPhone, i_Vehicle);
            m_VehiclesInGarage.Add(i_Vehicle.LicensePlate, newVehicle);
        }

        public bool LicensePlateExists(string i_LicensePlate)
        {
            return m_VehiclesInGarage.ContainsKey(i_LicensePlate);
        }

        public void SetVehicleInGarageStatus(string i_LicensePlate, eVehicleStatus i_VehicleStatus)
        {
            m_VehiclesInGarage[i_LicensePlate].Status = i_VehicleStatus;
        }

        public List<string> GetLicensePlatesByStatusFilter(eVehicleStatus? i_StatusFilter)
        {
            List<string> licensePlatesList = new List<string>();

            foreach (var vehicle in m_VehiclesInGarage)
            {
                if (vehicle.Value.Status == i_StatusFilter || i_StatusFilter == null)
                {
                    licensePlatesList.Add(vehicle.Value.LicensePlate);
                }
            }

            return licensePlatesList;
        }

        public static Vehicle GetNewVehicleFromFactory(Type i_VehicleType, string i_LicensePlate, string i_ModelName, string i_WheelManufacturer, Type i_EngineType)
        {
            return sr_VehicleFactory.NewVehicle(i_VehicleType, i_LicensePlate, i_ModelName, i_WheelManufacturer, i_EngineType);
        }

        public Type GetEngineType(string i_LicensePlate)
        {
            return m_VehiclesInGarage[i_LicensePlate].EngineType;
        }

        public eVehicleStatus GetVehicleStatus(string i_LicensePlate)
        {
            try
            {
                return m_VehiclesInGarage[i_LicensePlate].Status;
            }
            catch
            {
                throw new ArgumentException("The given licence plate is not in the garage.");
            }
        }

        public void FillAirInWheels(string i_LicensePlate)
        {
            m_VehiclesInGarage[i_LicensePlate].FillAirToMax();
        }

        public string GetVehicleInformation(string i_LicensePlate)
        {
            return m_VehiclesInGarage[i_LicensePlate].ToString();
        }

        public void FillEnergyInVehicle(string i_LicensePlate, float i_AmountEnergyToFill)
        {
            m_VehiclesInGarage[i_LicensePlate].FillEnergy(i_AmountEnergyToFill);
        }

        public void FillEnergyInVehicle(string i_LicensePlate, float i_AmountEnergyToFill, eFuelType i_FuelType)
        {
            m_VehiclesInGarage[i_LicensePlate].FillEnergy(i_AmountEnergyToFill, i_FuelType);
        }

        public float GetPercentOfEnergyRemaining(string i_LicensePlate)
        {
            return m_VehiclesInGarage[i_LicensePlate].PercentOfEnergyRemaining ;
        }
    }
}
