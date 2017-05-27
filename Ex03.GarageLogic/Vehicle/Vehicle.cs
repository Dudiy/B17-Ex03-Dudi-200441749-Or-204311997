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
        protected byte m_RequiredNumWheels;
        protected float k_MaxAirPress;
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
                equals = m_LicensePlate == compareTo.m_LicensePlate;
            }

            return equals;
        }

        public override int GetHashCode()
        {
            return m_LicensePlate.GetHashCode();
        }

        // ========================================= Setters and Getters ====================================
        // TODO getter
        private void updatePercentOfEnergyRemaining()
        {
            //m_EnergyRemainingInPercent = m_CurrentEnergyRemaining / m_MaxEnergy;
        }


        // ========================================= Methods =========================================
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
    }
}
        
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
        //    get { return m_MaxEnergy; } // TODO not work public get
        //    protected set
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

        //public byte RequiredNumWheels
        //{
        //    get { return m_RequiredNumWheels; }
        //    protected set
        //    {
        //        m_RequiredNumWheels = value;
        //    }
        //}

        //public float MaxAirPress
        //{
        //    get { return k_MaxAirPress; }
        //    set
        //    {
        //        k_MaxAirPress = value;
        //    }
        //}
