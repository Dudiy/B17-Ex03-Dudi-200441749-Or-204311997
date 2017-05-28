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
        private static readonly byte[] sr_PossibleNumDoors = { 2, 3, 4, 5 };

        public Car(string i_LicensePlate, string i_ModelName, string i_CarColor,
            byte i_NumDoors, string i_WheelManufacturer, float i_WheelsAirPress)
            : base(i_LicensePlate, i_ModelName)
        {
            k_MaxWheelAirPress = 30;
            // TODO add catch
            m_CarColor = (eColor)LogicUtilities.ConvertStringToEnumValue(typeof(eColor), i_CarColor);
            if(LogicUtilities.IsValueInArray(sr_PossibleNumDoors, i_NumDoors))
            {
                m_NumDoors = i_NumDoors;
            }

            Wheel wheelToAdd = new Wheel(i_WheelManufacturer, i_WheelsAirPress);

            AddAllWheels(wheelToAdd, typeof(ePossitionOfCarWheel));
        }
    }
}
