using System;
using System.Collections.Generic;
using System.Text;

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
     
        public override string ToString()
        {
            string exceptionMessage =
                string.Format("ValueOutOfRangeException: The value is out of range, " +
                "the required range is between {0} to {1}.",
                m_MinValue, m_MaxValue);

            return exceptionMessage;
        }
    }
}
