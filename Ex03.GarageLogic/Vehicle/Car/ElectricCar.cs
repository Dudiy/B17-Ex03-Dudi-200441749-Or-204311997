//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Ex03.GarageLogic
//{
//    public class ElectricCar : Car, IElectricVehicle
//    {
//        // assumption, input parameters are validated before calling the ctor  
//        public ElectricCar(string i_LicensePlate, string i_ModelName, eColor i_CarColor,
//            byte i_NumDoors,  string i_WheelsManufacturer, float m_CurrentAirPressure)
//            : base(i_LicensePlate, i_ModelName, i_CarColor, i_NumDoors, 
//                  i_WheelsManufacturer, m_CurrentAirPressure)
//        {
//            m_EnergyRemaining = 0;
//            m_MaxEnergy = 2.5f;
//        }

//        public void Charge(float i_AmountPowerInHourToAdd)
//        {
//            try
//            {
//                fillEnergy(i_AmountPowerInHourToAdd);
//            }
//            catch (ValueOutOfRangeException amountPowerInHourToAddIsOutOfRange)
//            {
//                throw amountPowerInHourToAddIsOutOfRange;
//            }
//        }
//    }
//}
