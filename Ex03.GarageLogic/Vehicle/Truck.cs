using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_CarriesHazardousMaterials = false;
        private float m_MaxCarryingWeight = 0;
        private const byte k_NumWheels = 12;
        private const byte k_MaxWheelAirPressForTruck = 32;
        private const float k_MaxEnergyForTruck = 135;
        private const eFuelType k_FuelTypeForTruck = eFuelType.Octan96;

        public Truck(string i_LicensePlate, string i_ModelName, string i_WheelManufacturer)
            : base(i_LicensePlate, i_ModelName)
        {
            m_Engine = new FuelEngine(k_MaxEnergyForTruck, k_FuelTypeForTruck);
            m_MaxWheelAirPress = k_MaxWheelAirPressForTruck;
            InitAllWheels(i_WheelManufacturer, k_MaxWheelAirPressForTruck, k_NumWheels);
        }

        // ======================================== Additional Properties ========================================        
        protected override void InitValuesInSetFunctionsForAddedProperties()
        {
            if (r_SetFunctionsForAddedProperties.Count == 0)
            {
                r_SetFunctionsForAddedProperties.Add("if carries hazardous materials (Y/N)", "SetCarriesHazardousMaterials");
                r_SetFunctionsForAddedProperties.Add("max carrying weight", "SetMaxCarryingWeight");
            }
        }

        public void SetCarriesHazardousMaterials(string i_CarriesHazardousMaterials)
        {
            string input = i_CarriesHazardousMaterials.ToUpper();
            if (input.Equals("Y") || input.Equals("YES"))
            {
                m_CarriesHazardousMaterials = true;
            }
            else if (input.Equals("N") || input.Equals("NO"))
            {
                m_CarriesHazardousMaterials = false;
            }
            else
            {
                throw new FormatException("Please enter Yes or No");
            }
        }

        public void SetMaxCarryingWeight(string i_MaxCarryingWeight)
        {
            float maxCarryingWeight;

            if (float.TryParse(i_MaxCarryingWeight, out maxCarryingWeight))
            {
                if (maxCarryingWeight > 0)
                {
                    m_MaxCarryingWeight = maxCarryingWeight;
                }
                else
                {
                    // not a ValueOutOfRangeException because the is no max value
                    throw new ArgumentException("Input value must be a positive number");
                }
            }
            else
            {
                throw new FormatException("Input value must be of type float");
            }
        }

        // ======================================== Override ========================================        
        public override string ToString()
        {
            string output = String.Format(
@"{0}

Carries hazardous materials: {1}
MAx carrying weight: {2}
",
base.ToString(),
m_CarriesHazardousMaterials ? "Yes" : "No",
m_MaxCarryingWeight);

            return output;
        }

    }
}
