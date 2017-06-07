using System;
using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    internal static class ConsoleUtils
    {
        internal static string GetNonEmptyStrFromUser(string i_Prompt)
        {
            string inputStr = string.Empty;
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.Write(i_Prompt);
                inputStr = Console.ReadLine();
                if (inputStr.Equals(string.Empty))
                {
                    Console.WriteLine(
@"empty string is invalid");
                }
                else
                {
                    isValidInput = true;
                }
            }

            return inputStr;
        }
       
        // get a number selection from the user, the number must be between the given min and max values
        internal static ushort GetNumberInputFromUserInRange(ushort i_MinValidSelection, ushort i_MaxValidSelection)
        {
            bool isValidInput = false;
            ushort userSelection = 0; // TODO change to nullable?

            while (!isValidInput)
            {
                while (!ushort.TryParse(Console.ReadLine(), out userSelection))
                {
                    Console.WriteLine(
@"Input format error please input a number");
                }

                if (userSelection >= i_MinValidSelection && userSelection <= i_MaxValidSelection)
                {
                    isValidInput = true;
                }
                else
                {
                    // TODO use exception
                    Console.WriteLine(
@"Please input a number between {0} and {1}",
i_MinValidSelection,
i_MaxValidSelection);
                }
            }

            return userSelection;
        }

        internal static char GetYesOrNoFromUser(string i_Prompt)
        {
            string inputFromUser;
            char? selection = null;

            Console.WriteLine(i_Prompt);
            while (selection == null)
            {
                inputFromUser = Console.ReadLine();
                if (inputFromUser.ToUpper() == "Y")
                {
                    selection = 'Y';
                }
                else if (inputFromUser.ToUpper() == "N")
                {
                    selection = 'N';
                }
                else
                {
                    Console.Write(
@"invalid answer, please enter (Y/N): ");
                }
            }

            return (char)selection;
        }

        internal static Enum GetEnumSelectionFromUser(Type i_EnumType, string i_Prompt)
        {
            Dictionary<ushort, Enum> enumDictinary = new Dictionary<ushort, Enum>();
            ushort enumCounter = 1;
            ushort userSelection;

            Console.WriteLine(i_Prompt);
            foreach (Enum item in Enum.GetValues(i_EnumType))
            {
                enumDictinary.Add(enumCounter, item);
                enumCounter++;
            }

            foreach (KeyValuePair<ushort, Enum> item in enumDictinary)
            {
                Console.WriteLine("{0}. {1}", item.Key, item.Value);
            }

            userSelection = GetNumberInputFromUserInRange(1, (ushort)(enumCounter - 1));

            return enumDictinary[userSelection];
        }

        internal static void PressAnyKeyToContinue()
        {
            Console.WriteLine("(press any key to continue)");
            Console.ReadKey();
        }
    }
}
