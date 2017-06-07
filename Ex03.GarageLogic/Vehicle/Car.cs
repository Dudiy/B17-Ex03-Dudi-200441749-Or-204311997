using System;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eColor m_CarColor = eColor.White;
        private byte m_NumDoors = 4;
        private static readonly byte[] sr_PossibleNumDoors = { 2, 3, 4, 5 };
        private const byte k_NumWheels = 4;
        private const byte k_MaxWheelAirPressForCar = 30;
        private const float k_MaxEnergyForElectricCar = 2.5f;
        private const float k_MaxEnergyForFueledCar = 42f;
        private const eFuelType k_FuelTypeForCar = eFuelType.Octan98;

        // assumption, input parameters are validated before calling the ctor  
        internal Car(string i_LicensePlate, string i_ModelName, string i_WheelManufacturer)
            : base(i_LicensePlate, i_ModelName)
        {
            //setEngine(i_EngineType);
            m_MaxWheelAirPress = k_MaxWheelAirPressForCar;
            InitAllWheels(i_WheelManufacturer, k_MaxWheelAirPressForCar, k_NumWheels);
        }

        // ======================================== Additional Properties ========================================        
        protected override void InitValuesInSetFunctionsForAddedProperties()
        {
            if (r_SetFunctionsForAddedProperties.Count == 0)
            {
                r_SetFunctionsForAddedProperties.Add("engine type (\"Electric Engine\" or \"Fueled Engine\")", "SetEngineType");
                r_SetFunctionsForAddedProperties.Add("number of doors", "SetNumDoors");
                r_SetFunctionsForAddedProperties.Add("color of car", "SetColor");
            }
        }

        public void SetEngineType(string i_EngineType)
        {
            string engineType = i_EngineType.ToUpper();

            if (engineType.Equals("FUELED ENGINE") || engineType.Equals("FUELED"))
            {
                m_Engine = new FuelEngine(k_MaxEnergyForFueledCar,k_FuelTypeForCar);
            }
            else if (engineType.Equals("ELECTRIC ENGINE") || engineType.Equals("ELECTRIC"))
            {
                m_Engine = new ElectricEngine(k_MaxEnergyForElectricCar);
            }
            else
            {
                throw new FormatException("Please enter \"Electric Engine\" or \"Fueled Engine\"");
            }
        }

        // get a string value and set m_NumDoors, throws an error if the string is invalid
        public void SetNumDoors(string i_NumDoors)
        {
            byte numDoors;

            if (Byte.TryParse(i_NumDoors, out numDoors))
            {
                bool isValidOption = false;

                foreach (byte numDoorsOption in sr_PossibleNumDoors)
                {
                    if (numDoorsOption == numDoors)
                    {
                        m_NumDoors = numDoors;
                        isValidOption = true;
                        break;
                    }
                }

                if (!isValidOption)
                {
                    // not ValueOutOfRangeException because num doors doesnt have to be a range
                    throw new ArgumentException("Invalid value for number of doors");
                }
            }
            else
            {
                throw new FormatException("Input value must be of type byte");
            }
        }

        // assumption, the input value is a valid eColor value
        public void SetColor(string i_Color)
        {
            m_CarColor = (eColor)EnumUtils.ConvertStringToEnumValue(typeof(eColor), i_Color);
        }

        // ======================================== Override ========================================        
        public override string ToString()
        {
            string output = String.Format(
@"{0}

Car Color: {1}
Number of doors: {2}
",
base.ToString(),
m_CarColor,
m_NumDoors);

            return output;
        }
    }
}