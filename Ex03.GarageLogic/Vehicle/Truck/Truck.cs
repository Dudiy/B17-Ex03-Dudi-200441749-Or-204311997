using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const float k_MaxAirPressure = 32;
        private const eFuelType m_FuelType = eFuelType.Octan96;     // TODO const?    
        private bool m_CarriesHazardousMaterials;
        private float m_MaxCarryingWeight;

        public Truck(string i_LicensePlate, string i_ModelName, 
            bool i_CarriesHazardousMaterials, float i_MaxCarryingWeight)
            : base(i_LicensePlate, i_ModelName)
        {
            m_NumWheels = 12;
            for (int i = 0; i < m_NumWheels; i++)
            {
                Wheels.Add(new Wheel("Another Wheel Company", k_MaxAirPressure));
            }

            MaxEnergy = 135;
            EnergyRemaining = MaxEnergy;
            m_CarriesHazardousMaterials = i_CarriesHazardousMaterials;
            m_MaxCarryingWeight = i_MaxCarryingWeight;
        }
    }
}
