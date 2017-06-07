using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        private static readonly List<KeyValuePair<string, Type>> sr_VehicleTypes = new List<KeyValuePair<string, Type>>();
        private static readonly List<KeyValuePair<string, Type>> sr_EngineTypes = new List<KeyValuePair<string, Type>>();

        static VehicleFactory()
        {
            sr_VehicleTypes.Add(new KeyValuePair<string, Type>("Car", typeof(Car)));
            sr_VehicleTypes.Add(new KeyValuePair<string, Type>("Bike", typeof(Car)));
            sr_EngineTypes.Add(new KeyValuePair<string, Type>("Electric engine", typeof(ElectricEngine)));
            sr_EngineTypes.Add(new KeyValuePair<string, Type>("Fuel Engine", typeof(MotorEngine)));
        }
        
        public Vehicle NewVehicle(Type i_VehicleType, string i_LicensePlate, string i_ModelName,
            string i_WheelManufacturer, Type i_EngineType)
        {
            Vehicle newVehicle = null;

            if (i_VehicleType.Equals(typeof(Car)))
            {
                newVehicle = new Car(i_LicensePlate, i_ModelName, i_WheelManufacturer, i_EngineType);
            }
            else
            {
                throw new NotImplementedException();
            }

            return newVehicle;
        }

        public static List<KeyValuePair<string, Type>> VehicleTypes
        {
            get { return sr_VehicleTypes; }
        }

        public static List<KeyValuePair<string, Type>> EngineTypes
        {
            get { return sr_EngineTypes; }
        }

        public static KeyValuePair<string, Type> GetEngineTypeAtI(int i)
        {
            return sr_EngineTypes[i];
        }

        public static int NumOfEngineTypes
        {
            get { return sr_EngineTypes.Count; }
        }

        public static KeyValuePair<string, Type> GetVehicleTypeAtI(int i)
        {
            return sr_VehicleTypes[i];
        }

        public static int NumOfVehicleTypes
        {
            get { return sr_VehicleTypes.Count; }
        }
    }
}