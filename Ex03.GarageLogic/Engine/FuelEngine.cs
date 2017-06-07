using System;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private eFuelType m_FuelType;

        public FuelEngine(float i_MaxEnergy, eFuelType i_FuelType)
            : base(i_MaxEnergy)
        {
            m_FuelType = i_FuelType;
        }

        public void FillFuel(float i_FuelToAdd, eFuelType i_FuelType)
        {
            if (i_FuelType.Equals(m_FuelType))
            {
                // if i_FuelToAdd is out of range an exception will be thrown from FillEnergy
                FillEnergy(i_FuelToAdd);
            }
            else
            {
                throw new ArgumentException(String.Format("Fuel type mismatch, valid fuel type is {0}", m_FuelType));
            }
        }

        public override string ToString()
        {
            string output = String.Format(
@"Engine type: Fuel running engine
Remaining fuel percent: {0}
Remaining fuel liters: {1}
Fuel tank size: {2}
Fuel type: {3}",
PercentOfEnergyRemaining.ToString("P"),
m_EnergyRemaining,
m_MaxEnergy,
m_FuelType
);

            return output;
        }
    }
}
