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
            AddWheels(typeof(ePossitionOfCarWheel), new Wheel(i_WheelManufacturer, k_MaxAirPress));
            m_EnergyRemaining = (float)2.5;
            m_MaxEnergy = (float)2.5;
        }
        // TODO interface add ?
    }
}
