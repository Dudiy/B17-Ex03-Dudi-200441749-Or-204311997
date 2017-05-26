using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Car : Vehicle
    {
        private eColor m_Color;
        private byte m_NumDoors;

        public Car(string i_LicensePlate, eColor i_Color, byte i_NumDoors) : base(i_LicensePlate)
        {
            m_Color = i_Color;
            m_NumDoors = i_NumDoors;
        }
    }
}
