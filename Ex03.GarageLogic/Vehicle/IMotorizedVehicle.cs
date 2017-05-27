using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public interface IMotorizedVehicle
    {
        void Refuel(eFuelType i_FuelType, float i_AmountOfLitersToFill);
    }
}
