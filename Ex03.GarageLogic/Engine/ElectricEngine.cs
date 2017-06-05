using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxEnergy) : base(i_MaxEnergy) { }

        public void ChargeBattery(float i_AmountToCharge)
        {
            // TODO if there is more in amount to charge then max, should we charge to max or throw error?
            FillEnergy(i_AmountToCharge);
        }

        public void Charge(float i_FuelToAdd)
        {
            FillEnergy(i_FuelToAdd);
        }

        public override string ToString()
        {
            return String.Format(
@"      Engine type: Electric engine
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
