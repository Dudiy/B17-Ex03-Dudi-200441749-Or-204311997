using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class MotorBike : Bike
    {
        private const short k_MaxAirPress = 33;
        private eFuelType m_FuelType = eFuelType.Octan95;        
        
        public MotorBike(string i_LicensePlate, eLicenseType i_LicenseType, int i_EngineCapacity)
            : base(i_LicensePlate, i_LicenseType, i_EngineCapacity)
        {
            for (int i = 0; i < m_NumWheels; i++)
            {
                Wheels.Add(new Wheel("A Wheels", k_MaxAirPress));
            }

            MaxEnergy = 5.5f;
            EnergyRemaining = MaxEnergy;
        }

        public eFuelType FuelType
        {
            get { return m_FuelType; }
            set { m_FuelType = value; }
        }
    }
}
