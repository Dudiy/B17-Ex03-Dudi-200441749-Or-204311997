﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        private static List<Type> m_VehicleTypes = new List<Type>();
        private static List<Type> m_EngineTypes = new List<Type>();

        static VehicleFactory()
        {
            m_VehicleTypes.Add(typeof(Car));
            m_EngineTypes.Add(typeof(ElectricEngine));
            m_EngineTypes.Add(typeof(MotorEngine));
        }

        public static Type GetEngineTypeAtI(int i)
        {
            return m_EngineTypes[i];
        }

        public static int NumOfEngineTypes
        {
            get { return m_EngineTypes.Count; }
        }

        public static Type GetVehicleTypeAtI(int i)
        {
            return m_VehicleTypes[i];
        }

        public static int NumOfVehicleTypes
        {
            get { return m_VehicleTypes.Count; }
        }

        public Vehicle NewVehicle(Type i_VehicleType, string i_LicensePlate, string i_ModelName, string i_WheelManufacturer, Type i_EngineType)
        {
            Vehicle newVehicle = null;

            if (i_VehicleType.Equals(typeof(Car)))
            {
                newVehicle = new Car(i_LicensePlate, i_ModelName, i_WheelManufacturer, i_EngineType);
            }

            return newVehicle;
        }
    }
}

        // TODO change to static?
        //public Vehicle NewVehicleFromModel(string i_Model, string i_LicensePlate, params object[] i_Params)
        //{
        //    Vehicle model = m_VehicleList[i_Model];
        //    Vehicle newVehicleFromModel = null;

        //    if (model == null)
        //    {
        //        throw new Exception("The model does not exist in the garage");
        //    }
        //    else if (model.GetType().Equals(typeof(Car)))
        //    {
        //        newVehicleFromModel = new Car(i_LicensePlate, (Car)model);

        //    }
        //    //else if (model.GetType().Equals(typeof(Bike)))
        //    //{

        //    //}
        //    else
        //    {
        //        throw new Exception("unknown vehicle type entered");
        //    }

        //    return newVehicleFromModel; // model.CreateNewFromModel(i_LicensePlate, i_Params);
        //}

