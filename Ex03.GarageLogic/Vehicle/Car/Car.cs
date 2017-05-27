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

        public Car(string i_LicensePlate, string i_ModelName, byte i_RequiredNumWheels,
            float i_MaxAirPress, eColor i_CarColor, byte i_NumDoors, string i_WheelManufacturer, 
            byte m_RequiredNumWheels)
            : base(i_LicensePlate, i_ModelName)
        {
            RequiredNumWheels = i_RequiredNumWheels;
            MaxAirPress = i_MaxAirPress;
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
