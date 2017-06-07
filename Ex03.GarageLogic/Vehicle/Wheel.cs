namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_Manufacturer;
        private float m_MaxAirPressure;
        private float m_CurrentAirPressure;

        public Wheel(string i_Manufacturer, float i_MaxAirPessure)
        {
            m_Manufacturer = i_Manufacturer;
            m_MaxAirPressure = i_MaxAirPessure;
            m_CurrentAirPressure = 0;
        }

        // ==================================================== Properties ====================================================
        public string Manufacturer
        {
            get { return m_Manufacturer; }
            internal set { m_Manufacturer = value; }
        }

        public float MaxAirPressure
        {
            get { return m_MaxAirPressure; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
        }

        // ==================================================== Methods ====================================================
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
    }
}