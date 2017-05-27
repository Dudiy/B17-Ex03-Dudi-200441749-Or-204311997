using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class MotorCar : Car
    {
        private const eFuelType m_FuelType = eFuelType.Octan98;   

        public MotorCar(string i_LicensePlate, string i_ModelName, byte i_RequiredNumWheels,
            eColor i_CarColor, byte i_NumDoors, string i_WheelManufacturer)
            : base(i_LicensePlate, i_ModelName, i_CarColor, i_NumDoors, i_WheelManufacturer)
        {
            m_EnergyRemaining = 42;
            m_MaxEnergy = 42;
        }

        // TODO interface
        //public void AddFuel(float i_AmountFuelToAdd)
        //{
        //    if(m_CurrentFuelAmount + i_AmountFuelToAdd <= m_MaxFuelCapacity)
        //    {
        //        m_CurrentFuelAmount = m_CurrentFuelAmount + i_AmountFuelToAdd;
        //    }
        //    else
        //    {
        //        // TODO exception
        //    }
        //}
    }
}
