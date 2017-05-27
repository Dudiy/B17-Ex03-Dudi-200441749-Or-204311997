using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        protected enum ePossitionOfCarWheel
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
            k_MaxAirPress = 30;
            m_CarColor = i_CarColor;
            m_NumDoors = i_NumDoors;

            Wheel wheelToAdd = new Wheel(i_WheelManufacturer, k_MaxAirPress);
            
            AddAllWheels(wheelToAdd, typeof(ePossitionOfCarWheel));     
        }
    }
}
