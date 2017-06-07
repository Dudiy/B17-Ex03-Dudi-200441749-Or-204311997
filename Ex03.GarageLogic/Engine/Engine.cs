/*
 * This abstract class is the base class of both "Electric" and "Fueled" engine types
 */

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected float m_EnergyRemaining;
        protected float m_MaxEnergy;

        // ==================================================== Properties ====================================================
        public Engine(float i_MaxEnergy)
        {
            m_MaxEnergy = i_MaxEnergy;
            m_EnergyRemaining = 0;
        }

        public float PercentOfEnergyRemaining
        {
            get { return m_EnergyRemaining / m_MaxEnergy; }
        }

        public float MaxEnergy
        {
            get { return m_MaxEnergy; }
        }

        // ==================================================== Methods ====================================================
        // all inheritants must implement a "ToString" method
        public abstract override string ToString();

        // add to the m_Energy remaining, each inheritant will call this function according to the relevant energy type
        protected void FillEnergy(float i_EnergyToFill)
        {
            if (m_EnergyRemaining + i_EnergyToFill <= m_MaxEnergy)
            {
                m_EnergyRemaining += i_EnergyToFill;
            }
            else
            {
                // (m_MaxEnergy - m_EnergyRemaining) is the max value that can be filled
                throw new ValueOutOfRangeException(0, m_MaxEnergy - m_EnergyRemaining);
            }
        }
    }
}
