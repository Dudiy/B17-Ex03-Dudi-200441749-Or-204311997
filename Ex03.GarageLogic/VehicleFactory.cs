/*
 * B17_Ex03: VehicleFactory.cs
 * 
 * The only class that can generate new vehicles, according to the input a matching type of Vehicle is created and returned.
 * 
 * Instructions for adding a new class that was inherited from Vehicle:
 *  1. add the decription and type to sr_VehicleTypes
 *  2. add "else if" to "NewVehicle" method
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
 */
using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        private static readonly List<KeyValuePair<string, Type>> sr_VehicleTypes = new List<KeyValuePair<string, Type>>();

        static VehicleFactory()
        {
            // initialize list of all vehicle types
            sr_VehicleTypes.Add(new KeyValuePair<string, Type>("Car", typeof(Car)));
            sr_VehicleTypes.Add(new KeyValuePair<string, Type>("Bike", typeof(Bike)));
            sr_VehicleTypes.Add(new KeyValuePair<string, Type>("Truck", typeof(Truck)));
            // #############  Add new vehicle type to list here  #############
        }

        // ==================================================== Properties ====================================================
        public static List<KeyValuePair<string, Type>> VehicleTypes
        {
            get { return sr_VehicleTypes; }
        }

        // ==================================================== Methods ====================================================
        public Vehicle NewVehicle(Type i_VehicleType, string i_LicensePlate, string i_ModelName, string i_WheelManufacturer)
        {
            Vehicle newVehicle = null;

            // call the relevant ctor according to i_VehicleType
            if (i_VehicleType.Equals(typeof(Car)))
            {
                newVehicle = new Car(i_LicensePlate, i_ModelName, i_WheelManufacturer);
            }
            else if (i_VehicleType.Equals(typeof(Bike)))
            {
                newVehicle = new Bike(i_LicensePlate, i_ModelName, i_WheelManufacturer);
            }
            else if (i_VehicleType.Equals(typeof(Truck)))
            {
                newVehicle = new Truck(i_LicensePlate, i_ModelName, i_WheelManufacturer);
            }
            // #############  Add new vehicle ctor here  #############
            else
            {
                throw new NotImplementedException();
            }

            return newVehicle;
        }
    }
}