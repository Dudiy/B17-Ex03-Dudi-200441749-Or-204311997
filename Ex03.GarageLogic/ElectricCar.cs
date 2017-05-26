using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class ElectricCar : Car
    {
        private float m_RemainTimeOfEnergy;
        private static float s_MaxEnergyTime = (float)2.5;

        public ElectricCar(string i_LicensePlate, eColor i_CarColor, byte i_NumDoors,
            float i_RemainTimeOfEnergy) : base(i_LicensePlate, i_CarColor, i_NumDoors)
        {
            m_RemainTimeOfEnergy = i_RemainTimeOfEnergy;
        }
        // TODO interface add ?
    }
}
