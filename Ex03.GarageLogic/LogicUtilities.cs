using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class LogicUtilities
    {
        public static Enum ConvertStringToEnumValue(Type i_TypeOfEnum, string i_LookForStringValue)
        {
            bool isValidInput = false;
            Enum findEnumValue = null;

            // check if syntax of i_LookForStringValue is valid
            foreach (char letterInLookForString in i_LookForStringValue)
            {
                if(!Char.IsLetter(letterInLookForString))
                {
                    throw new FormatException("FormatException: Name have to include only letter.");
                }
            }

            // check if i_LookForStringValue exist in database
            foreach (Enum valueInEnum in Enum.GetValues(i_TypeOfEnum))
            {
                if (i_LookForStringValue.ToString().ToLower().Equals(valueInEnum.ToString().ToLower()))
                {
                    findEnumValue = valueInEnum;
                    isValidInput = true;
                    break;
                }
            }

            if (!isValidInput)
            {
                throw new ArgumentException("ArgumentException: This value doesn't exist in database.");
            }

            return findEnumValue;
        }

        public static bool IsValueInArray(Array i_Array, object i_LookForValue)
        {
            bool isValidInput = false;

            foreach (object valueInArray in i_Array)
            {
                if (valueInArray.Equals(i_LookForValue))
                {
                    isValidInput = true;
                    break;
                }
            }

            if (!isValidInput)
            {
                throw new ArgumentException("ArgumentException: This value doesn't exist in database.");
            }

            return isValidInput;
        }
    }
}
