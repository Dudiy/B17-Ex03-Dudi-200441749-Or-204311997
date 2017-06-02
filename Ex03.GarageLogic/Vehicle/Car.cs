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
                throw new Exception("Invalid engine type entered");
            }

            m_EnergyRemaining = m_Engine.MaxEnergy;
            k_MaxWheelAirPress = 30;
            InitAllWheels(new Wheel(i_WheelManufacturer, k_MaxWheelAirPress), 4);
            m_CarColor = i_CarColor;
            m_NumDoors = i_NumDoors;
        }

        // ctor to create a new car with a new license plate based on a given car model
        public Car(string i_LicensePlate, Car i_Model)
            : this(i_LicensePlate, i_Model.ModelName, i_Model.m_CarColor, i_Model.m_NumDoors,
                  i_Model.m_Wheels[0].Manufacturer, i_Model.EngineType)
        { }

        // ======================================== Properties ========================================
        public eColor CarColor
        {
            get { return m_CarColor; }
            set { m_CarColor = value; }
        }

        public byte NumDoors
        {
            get { return m_NumDoors; }
            set
            {
                bool isValidOption = false;

                foreach (byte numDoorsOption in sr_PossibleNumDoors)
                {
                    if (numDoorsOption == value)
                    {
                        m_NumDoors = value;
                        isValidOption = true;
                        break;
                    }
                }

                if (!isValidOption) // TODO is it ok to write like this or do we need "isValidOption == true"
                {
                    throw new Exception("Invalid value for number of doors");   // not ValueOutOfRangeException because num doors doesnt have to be a range
                }
            }
        }

        // creates a new instance of a model, returnes null if the model given is not a car
        // order and type of input params for i_Params: eColor color, byte numDoors 
        //public override Vehicle CreateNewFromModel(string i_LicensePlate, params object[] i_params)
        //{
        //    // TODO instead of params we can use default values and update them after creating the new car
        //    eColor color = (eColor)i_params[0];
        //    byte numDoors = (byte)(i_params[1]);

        //    return new Car(i_LicensePlate, ModelName, m_CarColor, numDoors, "Default Wheel Manufacturer", EngineType);
        //}

        public override object[] GetUserInputParamsListForNewCar()
        {
            //return object[] userInputParamsList = { eColor }
            throw new NotImplementedException();
        }
    }
}
