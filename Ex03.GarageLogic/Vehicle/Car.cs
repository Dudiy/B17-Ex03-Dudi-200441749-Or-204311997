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
        internal Car(string i_LicensePlate, string i_ModelName, string i_WheelManufacturer, Type i_EngineType)
            : base(i_LicensePlate, i_ModelName)
        {
            setEngine(i_EngineType);
            m_MaxWheelAirPress = k_MaxWheelAirPressForCar;
            InitAllWheels(i_WheelManufacturer, m_MaxWheelAirPress, k_NumWheels);
        }

        // ======================================== Methods ========================================

        private void setEngine(Type i_EngineType)
        {
            if (i_EngineType.Equals(typeof(ElectricEngine)))
            {
                m_Engine = new ElectricEngine(k_MaxEnergyForElectricCar);
            }
            else // if not electric then is must be fueled
            {
                m_Engine = new FuelEngine(k_MaxEnergyForFueledCar, k_FuelTypeForCar);
            }
        }

        // ======================================== Additional Properties ========================================        
        protected override void InitValuesInSetFunctionsForAddedParams()
        {
            if (sr_SetFunctionsForAddedParams.Count == 0)
            {
                sr_SetFunctionsForAddedParams.Add("number of doors", "SetNumDoors");
                sr_SetFunctionsForAddedParams.Add("color of car", "SetColor");
            }
        }

        // get a string value and set m_NumDoors, throws an error if the string is invalid
        public void SetNumDoors(string i_NumDoors)
        {
            bool isValidOption = false;
            byte numDoors = Byte.Parse(i_NumDoors);

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