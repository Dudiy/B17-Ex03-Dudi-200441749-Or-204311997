using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eColor m_CarColor;
        private byte m_NumDoors;
        private static readonly byte[] sr_PossibleNumDoors = { 2, 3, 4, 5 };


        //// assumption, input parameters are validated before calling the ctor  
        // ctor to create a new car with no user parameters
        internal Car(string i_LicensePlate, string i_ModelName, string i_WheelManufacturer, Type i_EngineType)
            : base(i_LicensePlate, i_ModelName)
        {
            // TODO maintainability issue - what if there is a new engine someday?
            if (i_EngineType.Equals(typeof(ElectricEngine)))
            {
                m_Engine = new ElectricEngine(2.5f);
            }
            else // if not electric then is must be fueled (i_EngineType.Equals(typeof(MotorEngine)))
            {
                m_Engine = new MotorEngine(42f, eFuelType.Octan98);
            }

            k_MaxWheelAirPress = 30;
            // TODO delete
            //InitAllWheels(new Wheel(i_WheelManufacturer, k_MaxWheelAirPress), 4);
            InitAllWheels(i_WheelManufacturer, k_MaxWheelAirPress, 4);
        }

        // ======================================== Properties ========================================
        // string property for 
        // TODO delete
        //public string CarColor
        //{
        //    get { return m_CarColor.ToString(); }
        //}

        //public string NumDoors
        //{
        //    get { return m_NumDoors.ToString(); }
        //}

        // TODO delete
        //public override Dictionary<string, PropertyInfo> GetUserInputPropertiesForNewVehicle()
        //{
        //    Dictionary<string, PropertyInfo> userInputProperties = new Dictionary<string, PropertyInfo>();

        //    userInputProperties.Add("Car Color", this.GetType().GetProperty("CarColor"));
        //    userInputProperties.Add("Number of doors", this.GetType().GetProperty("NumDoors"));

        //    return userInputProperties;
        //}

        public override string ToString()
        {
            return String.Format(
@"{0}

Car Color: {1}
Number of doors: {2}
",
base.ToString(),
m_CarColor,
m_NumDoors);
        }


        // ============================================================================================================================================================
        protected override void initUserInputFunctions()
        {
            // TODO try to improve
            if (s_SetFunctionsForAddedParams == null)
            {
                s_SetFunctionsForAddedParams = new Dictionary<string, string>();
                s_SetFunctionsForAddedParams.Add("number of doors", "SetNumDoors");
                s_SetFunctionsForAddedParams.Add("color of car", "SetColor");
            }
        }

        public void SetNumDoors(string i_NumDoors)
        {
            bool isValidOption = false;
            byte byteValue = Byte.Parse(i_NumDoors);

            foreach (byte numDoorsOption in sr_PossibleNumDoors)
            {
                if (numDoorsOption == byteValue)
                {
                    m_NumDoors = byteValue;
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

        public void SetColor(string i_Color)
        {
            m_CarColor = (eColor)EnumUtils.ConvertStringToEnumValue(typeof(eColor), i_Color);
        }
        
    }
}


//public eColor CarColor
//{                           
//    get { return m_CarColor; }
//    set
//    {
//        m_CarColor = value;
//    }
//}
// creates a new instance of a model, returnes null if the model given is not a car
// order and type of input params for i_Params: eColor color, byte numDoors 
//public override Vehicle CreateNewFromModel(string i_LicensePlate, params object[] i_params)
//{
//    // TODO instead of params we can use default values and update them after creating the new car
//    eColor color = (eColor)i_params[0];
//    byte numDoors = (byte)(i_params[1]);

//    return new Car(i_LicensePlate, ModelName, m_CarColor, numDoors, "Default Wheel Manufacturer", EngineType);
//}



/*
private static readonly Dictionary<string, PropertyInfo> userInputProperties = 
    new Dictionary<string, PropertyInfo>();

static Car()
{
    Car car = new Car("123", "Model1", eColor.Blue, 3, "WheelManufaucturer", typeof(MotorEngine));
    typeof(Car).GetProperties();
    userInputProperties.Add("Car Color", car.GetType().GetProperty("CarColor"));
    userInputProperties.Add("Number of doors", car.GetType().GetProperty("NumDoors"));
    userInputProperties.Add("Wheel Manufacturer", car.GetType().GetProperty("WheelManufacturer"));
}
public static Dictionary<string, PropertyInfo> GetParamertersList()
{
    Dictionary<string, PropertyInfo> l = new Dictionary<string, PropertyInfo>();

    l.Add("Car Color", typeof(Car).GetProperty("CarColor"));

    return l;
}
*/
