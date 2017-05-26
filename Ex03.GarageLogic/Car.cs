using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Car : Vehicle
    {
        private eColor m_CarColor;
        private byte m_NumDoors;

        public Car(string i_LicensePlate, eColor i_CarColor, byte i_NumDoors) : base(i_LicensePlate)
        {
            m_CarColor = i_CarColor;
            m_NumDoors = i_NumDoors;
        }
    }
}
