using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

// TODO look for change public to internal
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
        internal Vehicle(string i_LicensePlate, string i_ModelName)
        {
            m_LicensePlate = i_LicensePlate;
            m_ModelName = i_ModelName;
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
        public string LicensePlate
        {
            get { return m_LicensePlate; }
        }

        public string ModelName
        {
            get { return m_ModelName; }
        }

        public string WheelManufacturer
        {
            get { return m_Wheels[0].Manufacturer; }
            set
            {
                foreach (Wheel wheel in m_Wheels)
                {
                    wheel.Manufacturer = value;
                }
            }
        }

        public Type EngineType
        {
            get { return m_Engine.GetType(); }
        }

        // ========================================= Methods ================================================

        protected void InitAllWheels(Wheel i_Wheel, byte i_NumWheels)
        {
            for (byte i = 0; i < i_NumWheels; i++)
            {
                m_Wheels.Add(i_Wheel.Clone());
            }
        }

        public void FillAllWheelsToMaxAirPress(float i_AirToFill)
        {
            foreach (Wheel wheel in m_Wheels)
            {
                float airToFill = wheel.MaxAirPressure - wheel.CurrentAirPressure;

                wheel.FillAir(airToFill);
            }
        }

        public static void f()
        {

            GetUserInputPropertiesForNewVehicle();
        }

        public abstract Dictionary<string, PropertyInfo> GetUserInputPropertiesForNewVehicle();

        public override string ToString()
        {
            return String.Format(
@"  License plate: {0}
    Model Name: {1}
    Vehicle type: {2}
    Wheels Information:
        Manufacturer: {3}
        Max air pressure: {4}
    Engine Information:
    {5}
",
LicensePlate,
ModelName,
this.GetType().Name,
m_Wheels[0].Manufacturer,
m_Wheels[0].MaxAirPressure,
m_Engine.ToString()
);
        }
    }
}
