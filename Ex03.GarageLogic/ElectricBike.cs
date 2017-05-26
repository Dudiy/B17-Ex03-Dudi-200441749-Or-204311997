using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricBike : Bike
    {
        private const short k_MaxAirPress = 33;

        public ElectricBike(string i_LicensePlate, eLicenseType i_LicenseType, int i_EngineCapacity)
            :base(i_LicensePlate, i_LicenseType, i_EngineCapacity)
        {
            Wheels.Add(new Wheel("A Wheels", k_MaxAirPress));
            Wheels.Add(new Wheel("A Wheels", k_MaxAirPress));
            MaxEnergy = 2.7f;
            EnergyRemaining = MaxEnergy;
        }
    }
}
