using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class MotorCar : Car
    {
        private eFuelType m_FuelType;
        private float m_CurrentFuelAmount;
        private static float s_MaxFuelCapacity= 42;

        public MotorCar(string i_LicensePlate, eColor i_CarColor, byte i_NumDoors,
            eFuelType i_FuelType, float i_CurrentFuelAmount)
            : base(i_LicensePlate, i_CarColor, i_NumDoors)
        {
            m_FuelType = i_FuelType;
            m_CurrentFuelAmount = i_CurrentFuelAmount;
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
