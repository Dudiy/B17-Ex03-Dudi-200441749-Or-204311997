using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eColor m_CarColor;
        private byte m_NumDoors;
        private static readonly byte[] sr_PossibleNumDoors = { 2, 3, 4, 5 };

        // assumption, input parameters are validated before calling the ctor  
        public Car(string i_LicensePlate, string i_ModelName, eColor i_CarColor, byte i_NumDoors,
            string i_WheelManufacturer, Type i_EngineType)
            : base(i_LicensePlate, i_ModelName)
        {
            
            // TODO maintainability issue - what if there is a new engine someday?
            if (i_EngineType.Equals(typeof(ElectricEngine)))
            {
                m_Engine = new ElectricEngine(2.5f);
            }
            else if (i_EngineType.Equals(typeof(MotorEngine)))
            {
                m_Engine = new MotorEngine(42f, eFuelType.Octan98);
            }
            else
            {
                throw new Exception("Illegal engine type entered");
            }

            m_EnergyRemaining = m_Engine.MaxEnergy;
            k_MaxWheelAirPress = 30;
            InitAllWheels(new Wheel(i_WheelManufacturer, k_MaxWheelAirPress), 4);
            m_CarColor = i_CarColor;
            m_NumDoors = i_NumDoors;
        }

        // creates a new instance of a model, returnes null if the model given is not a car
        // order and type of input params for i_Params: eColor color, byte numDoors 
        public override Vehicle CreateNewFromModel(string i_LicensePlate, params object[] i_params)
        {
            eColor color = (eColor)i_params[0];
            byte numDoors = (byte)(i_params[1]);
            return new Car(i_LicensePlate, ModelName, m_CarColor, numDoors, "Default Wheel Manufacturer", EngineType);
        }
    }
}
