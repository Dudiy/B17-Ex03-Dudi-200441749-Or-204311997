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
        private Dictionary<string, Wheel> m_Wheels;

        // assumption, input parameters are validated before calling the ctor        
        public Vehicle(string i_LicensePlate)
        {
            m_LicensePlate = i_LicensePlate;
        }
        
        // assumption, input parameters are validated before calling the ctor        
        public Vehicle(string i_LicensePlate, string i_ModelName,
            float i_EnergyRemaining, float i_MaxEnergy, Dictionary<string, Wheel> i_Wheels)
        {
            m_LicensePlate = i_LicensePlate;
            m_ModelName = i_ModelName;
            m_EnergyRemaining = i_EnergyRemaining;
            m_MaxEnergy = i_MaxEnergy;
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

                }
            }
        }
        public float MaxEnergy;
        public Dictionary<string, Wheel> Wheels;
    }
}
