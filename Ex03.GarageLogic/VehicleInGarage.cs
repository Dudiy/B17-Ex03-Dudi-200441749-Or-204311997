using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleInGarage
    {
        private string m_OwnerName = string.Empty;
        private string m_OwnerPhone = string.Empty;
        private Vehicle m_Vehicle;
        private eVehicleStatus m_Status = eVehicleStatus.InProgress;

        //assumption: input vehicle is not in the garage yet (must be checked by the callee)
        internal VehicleInGarage(string i_OwnerName, string i_OwnerPhone, Vehicle i_Vehicle)
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

        internal void FillAirToMax()
        {
            m_Vehicle.FillAllWheelsToMaxAirPress();
        }

        public string LicensePlate
        {
            get { return m_Vehicle.LicensePlate; }
        }

        public override int GetHashCode()
        {
            return m_Vehicle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            VehicleInGarage compareTo = obj as VehicleInGarage;

            return m_Vehicle.Equals(compareTo.m_Vehicle);
        }

        public static bool operator ==(VehicleInGarage i_Vehicle1, VehicleInGarage i_Vehicle2)
        {
            bool equals = false;

            if (i_Vehicle1 == null || i_Vehicle2 == null)
            {
                equals = false;
            }
            else if (i_Vehicle1.m_Vehicle.Equals(i_Vehicle2.m_Vehicle))
            {
                equals = true;
            }

            return equals;
        }

        public static bool operator !=(VehicleInGarage i_Vehicle1, VehicleInGarage i_Vehicle2)
        {
            return !(i_Vehicle1 == i_Vehicle2);
        }

        public override string ToString()
        {
            return String.Format(
@"Owner information:
    Name: {0}
    Phone number: {1} 
    Vehicle status: {2}

Vehicle information:
{3}",
m_OwnerName,
m_OwnerPhone,
m_Status,
m_Vehicle.ToString());
        }

        public void FillEnergy(float i_AmountEnergyToFill)
        {
            m_Vehicle.FillEnergy(i_AmountEnergyToFill);
        }

        public void FillEnergy(eFuelType i_FuelType, float i_AmountEnergyToFill)
        {
            m_Vehicle.FillEnergy(i_FuelType, i_AmountEnergyToFill);
        }
    }
}
