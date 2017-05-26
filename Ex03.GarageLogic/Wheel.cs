using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
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

        ////////// interface
        ////  public void FillAir(float i_AmontOfAirToFill)
        //public void FillAir(float i_AmontOfAirToAdd)
        //{
        //    if(m_CurrentAirPressure + i_AmontOfAirToAdd <= m_MaxAirPressure)
        //    {
        //        m_CurrentAirPressure = m_CurrentAirPressure + i_AmontOfAirToAdd;
        //    }
        //    else
        //    {
        //        // TODO why not add until max value reached?
        //        throw new ValueOutOfRangeException(0, m_MaxAirPressure);
        //    }
        //}
    }
}
