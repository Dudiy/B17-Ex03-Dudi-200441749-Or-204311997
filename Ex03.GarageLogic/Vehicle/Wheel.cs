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
            m_CurrentAirPressure = i_MaxAirPessure;
            m_MaxAirPressure = i_MaxAirPessure;
        }

        public void FillAir(float i_AmontOfAirToFill)
        {
            if (m_CurrentAirPressure + i_AmontOfAirToFill <= m_MaxAirPressure)
            {
                m_CurrentAirPressure = m_CurrentAirPressure + i_AmontOfAirToFill;
            }
            else
            {
                //throw new ValueOutOfRangeException(0, m_MaxAirPressure);
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
