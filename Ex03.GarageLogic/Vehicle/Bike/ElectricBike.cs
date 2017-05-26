using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricBike : Bike
    {
        public ElectricBike(string i_LicensePlate, string i_ModelName, eLicenseType i_LicenseType, int i_EngineCapacity)
            : base(i_LicensePlate, i_ModelName, i_LicenseType, i_EngineCapacity)
        {
            MaxEnergy = 2.7f;
            EnergyRemaining = MaxEnergy;
        }
    }
}
