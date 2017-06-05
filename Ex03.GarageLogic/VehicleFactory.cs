using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        private static readonly List<Type> sr_VehicleTypes = new List<Type>();
        private static readonly List<Type> sr_EngineTypes = new List<Type>();

        static VehicleFactory()
        {
            sr_VehicleTypes.Add(typeof(Car));
            sr_EngineTypes.Add(typeof(ElectricEngine));
            sr_EngineTypes.Add(typeof(MotorEngine));            
        }

        public static Type GetEngineTypeAtI(int i)
        {
            return sr_EngineTypes[i];
        }

        public static int NumOfEngineTypes
        {
            get { return sr_EngineTypes.Count; }
        }

        public static Type GetVehicleTypeAtI(int i)
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