using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string m_LicensePlate;
        private readonly string m_ModelName;
        protected float m_EnergyRemaining;
        protected float m_MaxEnergy;
        protected float k_MaxWheelAirPress;
        protected List<Wheel> m_Wheels = new List<Wheel>();

        // assumption, input parameters are validated before calling the ctor        
        public Vehicle(string i_LicensePlate, string i_ModelName)
        {
            m_LicensePlate = i_LicensePlate;
            m_ModelName = i_ModelName;
        }

        public override bool Equals(object obj)
        {
            bool equals = false;
            Vehicle compareTo = obj as Vehicle;

            if (compareTo != null)
            {
                equals = m_LicensePlate.GetHashCode() == compareTo.GetHashCode();
            }

            return equals;
        }

        // TODO implement "==" and "!=" operators

        public override int GetHashCode()
        {
            return m_LicensePlate.GetHashCode();
        }

        // ========================================= Setters and Getters ====================================
        public float PercentOfEnergyRemaining
        {
            get { return m_EnergyRemaining / m_MaxEnergy; }
        }

        // ========================================= Methods ================================================
        protected void fillEnergy(float i_AmountEnergyToAdd)
        {
            if (m_EnergyRemaining + i_AmountEnergyToAdd <= m_MaxEnergy)
            {
                m_EnergyRemaining += i_AmountEnergyToAdd;
            }
            else
            {
                // (m_MaxEnergy - m_EnergyRemaining) is the max value that can fill
                throw new ValueOutOfRangeException(0, m_MaxEnergy - m_EnergyRemaining);
            }
        }
        
        public void InitAllWheels(Wheel i_Wheel, byte i_NumWheels)
        {
            for (byte i = 0; i < i_NumWheels; i++)
            {
                m_Wheels.Add(i_Wheel.Clone());
            }
        }

        // TODO FillAirToMax
        public void FillAllWheelsToMaxAirPress(float i_AirToFill)
        {
            foreach (Wheel wheel in m_Wheels)
            {
                float airToFill = wheel.MaxAirPressure - wheel.CurrentAirPressure;

                wheel.FillAir(airToFill);
            }
        }
    }
}

//public void FillAir(float i_FillToAdd)
//{
//    List<Wheel> unfilledWheels = null;          // TOOO do we really nead to save unfillef wheels?

//    foreach (KeyValuePair<Enum,Wheel> wheel in m_Wheels)
//    {
//        try
//        {
//            wheel.FillAir(i_FillToAdd);
//        }
//        catch (ValueOutOfRangeException valueRangeEx)
//        {
//            unfilledWheels.Add(wheel);
//        }
//    }
//    // TODO 
//    if (unfilledWheels != null)
//    {
//        throw new Exception("Not all wheels filled");
//    }
//}
