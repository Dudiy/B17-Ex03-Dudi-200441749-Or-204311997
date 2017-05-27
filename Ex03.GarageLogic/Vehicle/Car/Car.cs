using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        private enum ePossitionOfCarWheel
        {
            FR,     // Front Right
            FL,     // Front Left
            BR,     // Back Right
            BL      // Back Left
        }

        private eColor m_CarColor;
        private byte m_NumDoors;

        public Car(string i_LicensePlate, string i_ModelName, eColor i_CarColor, 
            byte i_NumDoors, string i_WheelManufacturer)
            : base(i_LicensePlate, i_ModelName)
        {
            m_RequiredNumWheels = 4;
            k_MaxAirPress = 30;
            m_CarColor = i_CarColor;
            m_NumDoors = i_NumDoors;
            //i_WheelManufacturer
            //m_RequiredNumWheels
        }

        //private addWheelInPossition(ePossitionOfCarWheel)
        //{
        //    // create 
        //}
    }
}
