using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string m_LicensePlate;
        private readonly string m_ModelName;
        protected float m_EnergyRemaining;
        protected float k_MaxWheelAirPress;        
        protected List<Wheel> m_Wheels = new List<Wheel>();
        protected Engine m_Engine;

        // assumption, input parameters are validated before calling the ctor        
        public Vehicle(string i_LicensePlate, string i_ModelName)
        {
            m_LicensePlate = i_LicensePlate;
            m_ModelName = i_ModelName;
        }

        public Type EngineType
        {
            get { return m_Engine.GetType(); }
        }

        public string WheelManufacturer
        {
            get { return m_Wheels[0].Manufacturer; }
            set
            {
                foreach(Wheel wheel in m_Wheels)
                {
                    wheel.Manufacturer = value;
                }
            }
        }

        public override bool Equals(object obj)
        {
            bool equals = false;
            Vehicle compareTo = obj as Vehicle;

            if (compareTo != null)
            {
                equals = m_LicensePlate.GetHashCode() == compareTo.GetHashCode();
            }

            return equals;
        }

        // TODO implement "==" and "!=" operators

        public override int GetHashCode()
        {
            return m_LicensePlate.GetHashCode();
        }

        // ========================================= Setters and Getters ====================================
        public string ModelName
        {
            get { return m_ModelName; }
        } 
         
        // ========================================= Methods ================================================
        
        public void InitAllWheels(Wheel i_Wheel, byte i_NumWheels)
        {
            for (byte i = 0; i < i_NumWheels; i++)
            {
                m_Wheels.Add(i_Wheel.Clone());
            }
        }

        // TODO FillAirToMax
        public void FillAllWheelsToMaxAirPress(float i_AirToFill)
        {
            foreach (Wheel wheel in m_Wheels)
            {
                float airToFill = wheel.MaxAirPressure - wheel.CurrentAirPressure;

                wheel.FillAir(airToFill);
            }
        }

        // public abstract Vehicle CreateNewFromModel(string i_LicensePlate, params object[] i_params);

        public abstract List<KeyValuePair<string, PropertyInfo>> GetUserInputPropertiesForNewVehicle();
    }
}