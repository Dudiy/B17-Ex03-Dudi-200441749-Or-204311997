/*
 * Instructions for adding a new class that was inherited from Vehicle:
 *  1. add the decription and type to sr_VehicleTypes
 *  2. add "else if" to "NewVehicle" method
 * 
 */

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
            // initialize list of all vehicle types
            sr_VehicleTypes.Add(new KeyValuePair<string, Type>("Car", typeof(Car)));
            // initialize list of all engine types
            sr_EngineTypes.Add(new KeyValuePair<string, Type>("Electric engine", typeof(ElectricEngine)));
            sr_EngineTypes.Add(new KeyValuePair<string, Type>("Fuel Engine", typeof(FuelEngine)));
        }

        // ==================================================== Properties ====================================================
        public static List<KeyValuePair<string, Type>> VehicleTypes
        {
            get { return sr_VehicleTypes; }
        }

        public static List<KeyValuePair<string, Type>> EngineTypes
        {
            get { return sr_EngineTypes; }
        }

        // ==================================================== Methods ====================================================
        public Vehicle NewVehicle(Type i_VehicleType, string i_LicensePlate, string i_ModelName, string i_WheelManufacturer, Type i_EngineType)
        {
            Vehicle newVehicle = null;

            // call the relevant ctor according to i_VehicleType
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