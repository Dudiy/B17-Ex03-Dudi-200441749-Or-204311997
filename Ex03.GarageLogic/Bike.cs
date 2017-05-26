using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Bike : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;
        private const int k_MaxEngineCapacity = 1000000;    // TODO update to a relevant max value

        public Bike(string i_LicensePlate, eLicenseType i_LicenseType, int i_EngineCapacity)
            :base(i_LicensePlate)
        {
            m_LicenseType = i_LicenseType;
            m_EngineCapacity = i_EngineCapacity;
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }
        public int EngineCapacity
        {
            get { return m_EngineCapacity; }
            set
            {
                if (value > 0)
                {
                    m_EngineCapacity = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, k_MaxEngineCapacity);
                }
            }
        }
    }
}
