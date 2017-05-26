using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        public ElectricCar(string i_LicensePlate, string i_ModelName, eColor i_CarColor, byte i_NumDoors)
            : base(i_LicensePlate, i_ModelName, i_CarColor, i_NumDoors)
        {
            MaxEnergy = 2.5f;
            EnergyRemaining = MaxEnergy;
        }
        // TODO interface add ?
    }
}
