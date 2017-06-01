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
        private static readonly byte[] sr_PossibleNumDoors = { 2, 3, 4, 5 };

        // assumption, input parameters are validated before calling the ctor  
        public Car(string i_LicensePlate, string i_ModelName, eColor i_CarColor, byte i_NumDoors, 
            string i_WheelManufacturer, float m_CurrentAirPressure)
            : base(i_LicensePlate, i_ModelName)
        {
            Wheel wheelToAdd = new Wheel(i_WheelManufacturer, m_CurrentAirPressure, k_MaxWheelAirPress);

            k_MaxWheelAirPress = 30;
            m_CarColor = i_CarColor;
            m_NumDoors = i_NumDoors;
            InitAllWheels(wheelToAdd, 4);
        }
    }
}
