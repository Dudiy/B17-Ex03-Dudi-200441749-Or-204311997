using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Vehicle
    {
        private string m_LicensePlate;
        private string m_ModelName;
        private float m_EnergyRemaining;
        private float m_MaxEnery;
        private Dictionary<string, Wheel> m_Wheels;
    }
}
