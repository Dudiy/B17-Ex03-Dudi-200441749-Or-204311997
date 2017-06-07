/*
 * B17_Ex03: ElectricEngine.cs
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
*/
using System;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxEnergy) : base(i_MaxEnergy)
        {
        }

        // charge the battery to i_AmountToCharge hours, or until battery is full
        public void Charge(float i_AmountToCharge)
        {
            float amountToCharge = i_AmountToCharge;

            if (i_AmountToCharge > m_MaxEnergy - m_EnergyRemaining)
            {
                amountToCharge = m_MaxEnergy - m_EnergyRemaining;
            }

            FillEnergy(amountToCharge);
        }

        public override string ToString()
        {
            return String.Format(
@"Engine type: Electric engine
Remaining battery percent: {0}
Remaining battery time (hours): {1}
Max battery capacity: {2}",
PercentOfEnergyRemaining.ToString("P"),
m_EnergyRemaining,
m_MaxEnergy
);
        }
    }
}
