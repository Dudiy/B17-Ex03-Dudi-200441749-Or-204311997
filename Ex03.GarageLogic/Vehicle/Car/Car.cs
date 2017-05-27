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

        public Car(string i_LicensePlate, string i_ModelName, float i_MaxEnergy, byte i_RequiredNumWheels,
            float i_MaxAirPress, eColor i_CarColor, byte i_NumDoors, List<Wheel> i_Wheels)
            : base(i_LicensePlate, i_ModelName, i_MaxEnergy, i_RequiredNumWheels, i_MaxAirPress, i_Wheels)
        {
            m_CarColor = i_CarColor;
            m_NumDoors = i_NumDoors;
        }

        private addWheelInPossition(ePossitionOfCarWheel)
        {
            // create 
        }
    }
}
