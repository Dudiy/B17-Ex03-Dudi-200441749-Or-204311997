using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eColor m_CarColor;
        private byte m_NumDoors;
        protected const float k_MaxAirPress = 30f;

        public Car(string i_LicensePlate, string i_ModelName, eColor i_CarColor, byte i_NumDoors) 
            : base(i_LicensePlate, i_ModelName)
        {
            m_NumWheels = 4;
            for (int i = 0; i < m_NumWheels; i++)
            {
                Wheels.Add(new Wheel("Wheel Company", k_MaxAirPress));
            }

            m_CarColor = i_CarColor;
            m_NumDoors = i_NumDoors;
        }
    }
}
