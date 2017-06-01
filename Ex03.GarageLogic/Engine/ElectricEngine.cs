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
            AddEnergy(i_AmountToCharge);
        }

        public override object Clone()
        {
            return new ElectricEngine(m_MaxEnergy);
        }
    }
}
