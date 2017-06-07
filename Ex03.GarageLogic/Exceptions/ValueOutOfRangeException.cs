/*
 * B17_Ex03: ValueOutOfRangeException.cs
 * 
 * Exception that will be thrown when an input is out of range, 
 * returns the minimal and maximal valid values as properties.
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
 */
using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MinValue;
        private float m_MaxValue;        

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }

        public float MinValue
        {
            get { return m_MinValue; }
        }

        public float MaxValue
        {
            get { return m_MaxValue; }
        }
    }
}
