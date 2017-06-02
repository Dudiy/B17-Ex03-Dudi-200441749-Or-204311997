using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GarageLogic
    {
        private Dictionary<string, Vehicle> m_VehicleInGarage = new Dictionary<string, Vehicle>();

        public void AddVehicleToGarage()
        {

        }

        public bool LicensePlateExists(string i_LicensePlate)
        {
            return m_VehicleInGarage.ContainsKey(i_LicensePlate);
        }

    }
}
