using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        private static List<Type> m_VehicleTypes = new List<Type>();
        //private Dictionary<string, Vehicle> m_VehicleList = new Dictionary<string, Vehicle>();

        public VehicleFactory()
        {
            //m_VehicleList.Add("Electric Mazda", new Car("Electric Mazda Template", "Electric Mazda", eColor.White, 5, "Default Wheel Company", typeof(ElectricEngine)));
            //m_VehicleList.Add("Motorized Mazda", new Car("Motorized Mazda Template", "Motorized Mazda", eColor.White, 5, "Default Wheel Company", typeof(MotorEngine)));
            m_VehicleTypes.Add(typeof(Car));
            m_VehicleTypes.Add(typeof(Bike));
        }

        public static Type GetVehicleTypeAtI(int i)
        {
            return m_VehicleTypes[i];
        }

        public static int NumOfVehicleTypes
        {
            get { return m_VehicleTypes.Count; }
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

        public Vehicle NewVehicle(Type i_VehicleType, Type i_EngineType, string i_LicensePlate)
        {
            Vehicle newVehicle = null;

            if (i_VehicleType.GetType().Equals(typeof(Car)))
            {
                //newVehicle = new Car()
            }

            return newVehicle;
        }
    }
}
