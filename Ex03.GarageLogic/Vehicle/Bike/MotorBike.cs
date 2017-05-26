using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class MotorBike : Bike
    {
        private const eFuelType m_FuelType = eFuelType.Octan95;    // TODO const?    
        
        public MotorBike(string i_LicensePlate, string i_ModelName, eLicenseType i_LicenseType, int i_EngineCapacity)
            : base(i_LicensePlate, i_ModelName, i_LicenseType, i_EngineCapacity)
        {
            MaxEnergy = 5.5f;
            EnergyRemaining = MaxEnergy;
        }

        public eFuelType FuelType
        {
            get { return m_FuelType; }
        }
    }
}
