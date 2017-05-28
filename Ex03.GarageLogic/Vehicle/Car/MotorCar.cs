using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class MotorCar : Car, IMotorizedVehicle
    {
        private const eFuelType m_FuelType = eFuelType.Octan98;

        // assumption, input parameters are validated before calling the ctor  
        public MotorCar(string i_LicensePlate, string i_ModelName, eColor i_CarColor, 
            byte i_NumDoors, string i_WheelManufacturer,float m_CurrentAirPressure)
            : base(i_LicensePlate, i_ModelName, i_CarColor, i_NumDoors, 
                  i_WheelManufacturer, m_CurrentAirPressure)
        {
            m_EnergyRemaining = 0;
            m_MaxEnergy = 42;
        }

        public void Refuel(eFuelType i_FuelType, float i_AmountOfFuelToFill)
        {
            if(i_FuelType == m_FuelType)
            {
                try
                {
                    fillEnergy(i_AmountOfFuelToFill);
                }
                catch(ValueOutOfRangeException amountOfFuelToFillIsOutOfRange)
                {
                    throw amountOfFuelToFillIsOutOfRange;
                }
            }
            else
            {
                throw new ArgumentException("Inappropiate type of fuel");
            }
        }
    }
}
