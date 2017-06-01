using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel : ICloneable
    {
        private string m_Manufacturer;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        public Wheel(string i_Manufacturer, float i_MaxAirPessure)
        {
            m_Manufacturer = i_Manufacturer;
            m_MaxAirPressure = i_MaxAirPessure;
            m_CurrentAirPressure = m_MaxAirPressure;
        }

        public float MaxAirPressure
        {
            get { return m_MaxAirPressure; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
        }

        public void FillAir(float i_AmountOfAirToFill)
        {
            if (m_CurrentAirPressure + i_AmountOfAirToFill <= m_MaxAirPressure)
            {
                m_CurrentAirPressure = m_CurrentAirPressure + i_AmountOfAirToFill;
            }
            else
            {
                // (m_MaxAirPressure - m_CurrentAirPressure) is the max value that can fill
                throw new ValueOutOfRangeException(0, m_MaxAirPressure - m_CurrentAirPressure);
            }
        }

        public void FillAirToMax()
        {
            m_CurrentAirPressure = m_MaxAirPressure;
        }

        public Wheel Clone()
        {
            return MemberwiseClone() as Wheel;
        }

        // TODO according to the book Clone() should be implemented this way        
        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }
    }
}
