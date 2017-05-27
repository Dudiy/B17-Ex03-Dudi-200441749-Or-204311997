using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        public ElectricCar(string i_LicensePlate, string i_ModelName, float i_MaxEnergy, byte i_RequiredNumWheels,
            float i_MaxAirPress, eColor i_CarColor, byte i_NumDoors, List<Wheel> i_Wheels)
            : base(i_LicensePlate, i_ModelName, i_MaxEnergy, i_RequiredNumWheels, i_MaxAirPress,
                  i_CarColor, i_NumDoors, i_Wheels)
        {

        }
        // TODO interface add ?
    }
}
