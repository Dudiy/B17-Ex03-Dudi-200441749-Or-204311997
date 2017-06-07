using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public static class EnumUtils
    {
        public static Enum ConvertStringToEnumValue(Type i_TypeOfEnum, string i_LookForStringValue)
        {
            bool isValidInput = false;
            Enum findEnumValue = null;
            StringBuilder optionsListStr = new StringBuilder();

            // check if i_LookForStringValue exist in enum
            foreach (Enum valueInEnum in Enum.GetValues(i_TypeOfEnum))
            {
                optionsListStr.AppendFormat(
@"{0}
", valueInEnum.ToString());
                if (i_LookForStringValue.ToString().ToLower().Equals(valueInEnum.ToString().ToLower()))
                {
                    findEnumValue = valueInEnum;
                    isValidInput = true;
                    break;
                }
            }

            if (!isValidInput)
            {
                string exceptionMessage = String.Format(
@"{0} does not exist in the enum. 
Valid options:
{1}",
i_LookForStringValue,
optionsListStr.ToString());

                throw new ArgumentException(exceptionMessage);
            }

            return findEnumValue;
        }
    }
}
