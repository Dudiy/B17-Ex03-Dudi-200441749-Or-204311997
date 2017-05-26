using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        private string m_LicensePlate;
        private string m_ModelName;
        private float m_EnergyRemaining;
        private float m_MaxEnergy;
        private List<Wheel> m_Wheels = new List<Wheel>();
        protected byte m_NumWheels;

        // assumption, input parameters are validated before calling the ctor        
        public Vehicle(string i_LicensePlate, string i_ModelName)
        {
            m_LicensePlate = i_LicensePlate;
            m_ModelName = i_ModelName;
        }

        // assumption, input parameters are validated before calling the ctor        
        public Vehicle(
            string i_LicensePlate, string i_ModelName,
            float i_EnergyRemaining, float i_MaxEnergy)
        {
            m_LicensePlate = i_LicensePlate;
            m_ModelName = i_ModelName;
            m_EnergyRemaining = i_EnergyRemaining;
            m_MaxEnergy = i_MaxEnergy;
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

        // ========================================= Setters and Getters ====================================
        public string LicensePlate
        {
            get { return m_LicensePlate; }
        }

        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public float EnergyRemaining
        {
            get { return m_EnergyRemaining; }
            set
            {
                if (value <= m_MaxEnergy)
                {
                    m_EnergyRemaining = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, m_MaxEnergy);   // TODO update after creating the class
                }
            }
        }

        public float MaxEnergy
        {
            get { return m_MaxEnergy; }
            set
            {
                if (value >= m_EnergyRemaining)
                {
                    m_MaxEnergy = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, m_EnergyRemaining);   // TODO update after creating the class
                }
            }
        }

        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
        }

        // ========================================= Methods =========================================
        public void FillAir(float i_AirToFill)
        {
            List<Wheel> unfilledWheels = null;          // TOOO do we really nead to save unfillef wheels?

            foreach (Wheel wheel in m_Wheels)
            {
                try
                {
                    wheel.FillAir(i_AirToFill);
                }
                catch(ValueOutOfRangeException valueRangeEx)
                {
                    unfilledWheels.Add(wheel);
                }
            }

            if (unfilledWheels != null)
            {
                throw new Exception("Not all wheels filled");
            }

        }
    }
}
