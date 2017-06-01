using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ex03.GarageLogic
{
    public class CarFactory
    {
        private List<Car> m_CarList = new List<Car>();
        private MotorCar c1 = new MotorCar("123", "abc", eColor.White, 4, "ab", 33f);

        
        public CarFactory()
        {
            Type typeofC1 = c1.GetType();

            foreach (PropertyInfo memberInfo in typeofC1.GetProperties())
            {
                Console.WriteLine(memberInfo.Name);
            }
        }

    }
}
