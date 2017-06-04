using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<int, VehicleInGarage> m_VehiclesInGarage = new Dictionary<int, VehicleInGarage>();
        private static readonly VehicleFactory sr_VehicleFactory = new VehicleFactory();

        public void AddVehicleToGarage(string i_OwnerName, string i_OwnerPhone, Vehicle i_Vehicle)
        {
            VehicleInGarage newVehicle = new VehicleInGarage(i_OwnerName, i_OwnerPhone, i_Vehicle);
            m_VehiclesInGarage.Add(i_Vehicle.GetHashCode(), newVehicle);
        }

        public bool LicensePlateExists(string i_LicensePlate)
        {
            return m_VehiclesInGarage.ContainsKey(i_LicensePlate.GetHashCode());
        }

        public void SetVehicleInGarageStatus(string i_LicensePlate, eVehicleStatus i_VehicleStatus)
        {
            m_VehiclesInGarage[i_LicensePlate.GetHashCode()].Status = i_VehicleStatus;
        }

        //public List<string> GetLicensePlates()
        //{
        //    return GetLicensePlatesByStatusFilter(null);
        //}
        // TODO
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

        public string GetVehicleInformation(string i_LicensePlate)
        {
            string vehicleInfo;
            int key = i_LicensePlate.GetHashCode();

            if (m_VehiclesInGarage.ContainsKey(key))
            {
                vehicleInfo = m_VehiclesInGarage[key].ToString();
            }
            else
            {
                // TODO
                vehicleInfo = "Vehicle not found in the garage.";
            }

            return vehicleInfo;
        }

        public void FillEnergyInVehicle(string i_LicensePlate, eFuelType i_FuelType, float i_AmountEnergyToFill)
        {
            m_VehiclesInGarage[i_LicensePlate.GetHashCode()].FillEnergy(i_FuelType, i_AmountEnergyToFill);
        }
    }
}
