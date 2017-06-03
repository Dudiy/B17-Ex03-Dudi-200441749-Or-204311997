using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleInGarage
    {
        private string m_OwnerName = string.Empty;
        private string m_OwnerPhone = string.Empty;
        private eVehicleStatus m_Status = eVehicleStatus.InProgress;
        private Vehicle m_Vehicle;

        //assumption: input vehicle is not in the garage yet (must be checked by the callee)
        public VehicleInGarage(string i_OwnerName, string i_OwnerPhone, Vehicle i_Vehicle)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhone = i_OwnerPhone;
            m_Vehicle = i_Vehicle;  
        }

        public eVehicleStatus Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        public Vehicle Vehicle
        {
            get { return m_Vehicle; }
        }

    }
}
