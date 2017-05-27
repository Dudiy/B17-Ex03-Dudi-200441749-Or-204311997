using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string m_LicensePlate;
        private readonly string m_ModelName;
        protected float m_CurrentEnergyRemaining;
        protected float m_EnergyRemainingInPercent;
        protected readonly float m_MaxEnergy;
        protected readonly byte m_RequiredNumWheels;
        protected readonly float k_MaxAirPress;
        private List<Wheel> m_Wheels = new List<Wheel>();

        // assumption, input parameters are validated before calling the ctor        
        public Vehicle(string i_LicensePlate, string i_ModelName, float i_MaxEnergy, 
            byte i_RequiredNumWheels, float i_MaxAirPress, List<Wheel> i_Wheels)
        {
            m_LicensePlate = i_LicensePlate;
            m_ModelName = i_ModelName;
            // by default: energy is full
            m_CurrentEnergyRemaining = i_MaxEnergy;
            m_MaxEnergy = i_MaxEnergy;
            updatePercentOfEnergyRemaining();
            m_RequiredNumWheels = i_RequiredNumWheels;
            k_MaxAirPress = i_MaxAirPress;
            m_Wheels = i_Wheels;
        }

        public override bool Equals(object obj)
        {
            bool equals = false;
            Vehicle compareTo = obj as Vehicle;

            if (compareTo != null)
            {
                equals = m_LicensePlate == compareTo.m_LicensePlate;
            }

            return equals;
        }

        public override int GetHashCode()
        {
            return m_LicensePlate.GetHashCode();
        }
        
        // ========================================= Methods =========================================
        private void updatePercentOfEnergyRemaining()
        {
            m_EnergyRemainingInPercent = m_CurrentEnergyRemaining / m_MaxEnergy;
        }

        public void AddAir(float i_AirToAdd)
        {
            List<Wheel> unfilledWheels = null;          // TOOO do we really nead to save unfillef wheels?

            foreach (Wheel wheel in m_Wheels)
            {
                try
                {
                    wheel.AddAir(i_AirToAdd);
                }
                catch (ValueOutOfRangeException valueRangeEx)
                {
                    unfilledWheels.Add(wheel);
                }
            }
            // TODO 
            if (unfilledWheels != null)
            {
                throw new Exception("Not all wheels filled");
            }

        }

        //// assumption, input parameters are validated before calling the ctor        
        //public Vehicle(string i_LicensePlate, string i_ModelName)
        //{
        //    m_LicensePlate = i_LicensePlate;
        //    m_ModelName = i_ModelName;
        //}
        // ========================================= Setters and Getters ====================================
        //public string LicensePlate
        //{
        //    get { return m_LicensePlate; }
        //}

        //public string ModelName
        //{
        //    get { return m_ModelName; }
        //    //set { m_ModelName = value; }
        //}

        //public float EnergyRemaining
        //{
        //    get { return m_EnergyRemaining; }
        //    set
        //    {
        //        if (value <= m_MaxEnergy)
        //        {
        //            m_EnergyRemaining = value;
        //        }
        //        else
        //        {
        //            throw new ValueOutOfRangeException(0, m_MaxEnergy);   // TODO update after creating the class
        //        }
        //    }
        //}

        //public float MaxEnergy
        //{
        //    get { return m_MaxEnergy; }
        //    set
        //    {
        //        if (value >= m_EnergyRemaining)
        //        {
        //            m_MaxEnergy = value;
        //        }
        //        else
        //        {
        //            throw new ValueOutOfRangeException(0, m_EnergyRemaining);   // TODO update after creating the class
        //        }
        //    }
        //}

        //public List<Wheel> Wheels
        //{
        //    get { return m_Wheels; }
        //    set { m_Wheels = value; }
        //}

    }
}
