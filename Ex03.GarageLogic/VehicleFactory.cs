using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        // TODO should this be static readonly?
        //private static readonly TypeList sr_VehicleTypes = new TypeList();
        //private static readonly TypeList sr_EngineTypes = new TypeList();
        private static readonly List<KeyValuePair<string, Type>> sr_VehicleTypes = new List<KeyValuePair<string, Type>>();
        private static readonly List<KeyValuePair<string, Type>> sr_EngineTypes = new List<KeyValuePair<string, Type>>();

        static VehicleFactory()
        {
            sr_VehicleTypes.Add(new KeyValuePair<string, Type>("Car", typeof(Car)));
            sr_EngineTypes.Add(new KeyValuePair<string, Type>("Electric engine", typeof(ElectricEngine)));
            sr_EngineTypes.Add(new KeyValuePair<string, Type>("Fuel Engine", typeof(MotorEngine)));
        }

        //public static TypeList VehicleTypes
        //{
        //    get { return sr_VehicleTypes; }
        //}

        //public static TypeList EngineTypes
        //{
        //    get { return sr_EngineTypes; }
        //}

        //public static IEnumerable GetVehicleTypeEnumerator(TypeList i_TypeList)
        //{
        //    foreach(KeyValuePair<string, Type> pair in i_TypeList)
        //    {
        //        yield return pair;
        //    }
        //}

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
    }
}