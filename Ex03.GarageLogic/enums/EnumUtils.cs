using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public static class EnumUtils
    {
        /* search for an enum value in a given enum type, 
           returns null if the value isn't found and prints all possibilities. (case insensitive) */
        public static Enum ConvertStringToEnumValue(Type i_TypeOfEnum, string i_LookForStringValue)
        {
            bool isValidInput = false;
            Enum enumValueFound = null;
            StringBuilder optionsListStr = new StringBuilder();

            // check if i_LookForStringValue exist in enum and build enum possibilities string
            foreach (Enum valueInEnum in Enum.GetValues(i_TypeOfEnum))
            {
                optionsListStr.AppendFormat(
@"{0}
", valueInEnum.ToString());
                if (i_LookForStringValue.ToString().ToLower().Equals(valueInEnum.ToString().ToLower()))
                {
                    enumValueFound = valueInEnum;
                    isValidInput = true;
                    break;
                }
            }

            // if i_TypeOfEnum was not found
            if (!isValidInput)
            {
                string exceptionMessage = String.Format(
@"{0} is not a valid option. 
Valid options:
{1}",
i_LookForStringValue,
optionsListStr.ToString());

                throw new ArgumentException(exceptionMessage);
            }

            return enumValueFound;
        }
    }
}
