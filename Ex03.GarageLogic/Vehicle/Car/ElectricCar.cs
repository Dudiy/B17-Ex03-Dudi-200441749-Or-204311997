using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car, IElectricVehicle
    {
        public ElectricCar(string i_LicensePlate, string i_ModelName, eColor i_CarColor, 
            byte i_NumDoors, string i_WheelManufacturer)
            : base(i_LicensePlate, i_ModelName, i_CarColor, i_NumDoors, i_WheelManufacturer)
        {
            m_EnergyRemaining = 0;
            m_MaxEnergy = 2.5f;
        }

        public void Charge(float i_AmountPowerInHourToAdd)
        {
            fillEnergy(i_AmountPowerInHourToAdd);
            //TODO exception
        }
    }
}
