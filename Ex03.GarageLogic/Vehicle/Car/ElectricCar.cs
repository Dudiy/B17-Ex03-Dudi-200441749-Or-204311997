using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        public ElectricCar(string i_LicensePlate, string i_ModelName, byte i_RequiredNumWheels, 
            eColor i_CarColor, byte i_NumDoors, string i_WheelManufacturer)
            : base(i_LicensePlate, i_ModelName, i_CarColor, i_NumDoors, i_WheelManufacturer)
        {
            m_EnergyRemaining = 2.5f;
            m_MaxEnergy = 2.5f;
        }
        // TODO interface add ?
    }
}
