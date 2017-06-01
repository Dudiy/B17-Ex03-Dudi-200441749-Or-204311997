using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        private Dictionary<string, Vehicle> m_VehicleList = new Dictionary<string, Vehicle>();

        public VehicleFactory()
        {
            m_VehicleList.Add("Electric Mazda", new Car("Electric Mazda Template", "Electric Mazda", eColor.White, 5, "Default Wheel Company", typeof(ElectricEngine)));
            m_VehicleList.Add("Motorized Mazda", new Car("Motorized Mazda Template", "Motorized Mazda", eColor.White, 5, "Default Wheel Company", typeof(MotorEngine)));

        }

        public Vehicle NewVehicleFromModel(string i_Model, string i_LicensePlate, params object[] i_Params)
        {
            Vehicle model = m_VehicleList[i_Model];

            return model.CreateNewFromModel(i_LicensePlate, i_Params);
        }
    }
}
