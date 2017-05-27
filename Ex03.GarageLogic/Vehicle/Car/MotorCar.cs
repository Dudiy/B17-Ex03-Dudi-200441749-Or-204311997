using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class MotorCar : Car, IMotorizedVehicle
    {
        private const eFuelType m_FuelType = eFuelType.Octan98;   

        public MotorCar(string i_LicensePlate, string i_ModelName, eColor i_CarColor,
            byte i_NumDoors, string i_WheelManufacturer)
            : base(i_LicensePlate, i_ModelName, i_CarColor, i_NumDoors, i_WheelManufacturer)
        {
            m_EnergyRemaining = 0;
            m_MaxEnergy = 42;
        }

        public void Refuel(eFuelType i_FuelType, float i_AmountOfFuelToFill)
        {
            if(i_FuelType == m_FuelType)
            {
                fillEnergy(i_AmountOfFuelToFill);
            }
            else
            {
                // TODO exception
            }
        }
    }
}
