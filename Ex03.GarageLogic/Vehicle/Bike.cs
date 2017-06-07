/*
 * B17_Ex03: Bike.cs
 * 
 * A type of vehicle, can have an electric or fueled engine
 * has two additional fields: engine capacity and license type.
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
 */
using System;

namespace Ex03.GarageLogic
{
    public class Bike : Vehicle
    {
        private const byte k_NumWheels = 2;
        private const byte k_MaxWheelAirPressForBike = 33;
        private const float k_MaxEnergyForElectricBike = 2.7f;
        private const float k_MaxEnergyForFueledBike = 5.5f;
        private const eFuelType k_FuelTypeForBike = eFuelType.Octan95;
        private eLicenseType m_LicenseType = eLicenseType.A;
        private int m_EngineCapacity = 0;

        public Bike(string i_LicensePlate, string i_ModelName, string i_WheelManufacturer)
            : base(i_LicensePlate, i_ModelName)
        {
            // setEngine(i_EngineType);
            m_MaxWheelAirPress = k_MaxWheelAirPressForBike;
            InitAllWheels(i_WheelManufacturer, k_MaxWheelAirPressForBike, k_NumWheels);
        }

        // ======================================== Additional Properties ========================================        
        protected override void InitValuesInSetFunctionsForAddedProperties()
        {
            if (r_SetFunctionsForAddedProperties.Count == 0)
            {
                r_SetFunctionsForAddedProperties.Add("engine type (\"Electric Engine\" or \"Fueled Engine\")", "SetEngineType");
                r_SetFunctionsForAddedProperties.Add("license type", "SetLicenseType");
                r_SetFunctionsForAddedProperties.Add("engine capacity", "SetEngineCapacity");
            }
        }

        public void SetEngineType(string i_EngineType)
        {
            string engineType = i_EngineType.ToUpper().Trim();

            if (engineType.Equals("FUELED ENGINE") || engineType.Equals("FUELED"))
            {
                m_Engine = new FuelEngine(k_MaxEnergyForFueledBike, k_FuelTypeForBike);
            }
            else if (engineType.Equals("ELECTRIC ENGINE") || engineType.Equals("ELECTRIC"))
            {
                m_Engine = new ElectricEngine(k_MaxEnergyForElectricBike);
            }
            else
            {
                throw new FormatException("Please enter \"Electric Engine\" or \"Fueled Engine\"");
            }
        }

        public void SetLicenseType(string i_LicenseType)
        {
            m_LicenseType = (eLicenseType)EnumUtils.ConvertStringToEnumValue(typeof(eLicenseType), i_LicenseType);
        }

        public void SetEngineCapacity(string i_EngineCapacity)
        {
            int engineCapacity;

            if (int.TryParse(i_EngineCapacity, out engineCapacity))
            {
                if (engineCapacity > 0)
                {
                    m_EngineCapacity = engineCapacity;
                }
                else
                {
                    // not a ValueOutOfRangeException because the is no max value
                    throw new ArgumentException("Input value must be a positive integer");
                }
            }
            else
            {
                throw new FormatException("Input value must be of type int");
            }
        }

        // ======================================== Override ========================================        
        public override string ToString()
        {
            string output = string.Format(
@"{0}

License type: {1}
Engine capacity: {2}
",
base.ToString(),
m_LicenseType,
m_EngineCapacity);

            return output;
        }
    }
}
