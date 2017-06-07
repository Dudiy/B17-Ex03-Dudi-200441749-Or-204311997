/*
 * B17_Ex03: VehicleInGarage.cs
 * 
 * A class that connects between a customer of the garage and the vehicle itself.
 * also includes the current status of the vehicle.
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
 */
using System;

namespace Ex03.GarageLogic
{
    internal class VehicleInGarage
    {
        private string m_OwnerName;
        private string m_OwnerPhone;
        private Vehicle m_Vehicle;
        private eVehicleStatus m_Status = eVehicleStatus.InProgress;

        //assumption: input vehicle is not in the garage yet (must be checked by the caller)
        internal VehicleInGarage(string i_OwnerName, string i_OwnerPhone, Vehicle i_Vehicle)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhone = i_OwnerPhone;
            m_Vehicle = i_Vehicle;
        }

        // ==================================================== Properties ====================================================
        internal eVehicleStatus Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        internal string LicensePlate
        {
            get { return m_Vehicle.LicensePlate; }
        }

        internal float PercentOfEnergyRemaining
        {
            get { return m_Vehicle.PercentOfEnergyRemaining; }
        }

        internal Type EngineType
        {
            get { return m_Vehicle != null ? m_Vehicle.EngineType : null; }
        }

        // ==================================================== overrides ====================================================
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

        // ==================================================== Methods ====================================================
        internal void FillAirToMax()
        {
            m_Vehicle.FillAllWheelsToMaxAirPress();
        }

        internal void Charge(float i_AmountEnergyToFill)
        {
            m_Vehicle.Charge(i_AmountEnergyToFill);
        }

        internal void FillFuel(float i_AmountEnergyToFill, eFuelType i_FuelType)
        {
            m_Vehicle.FillFuel(i_AmountEnergyToFill, i_FuelType);
        }
    }
}
