using System;
using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    internal static class ConsoleUtils
    {
        // get a non empty string from the user, displays "i_Prompt"
        internal static string GetNonEmptyStrFromUser(string i_Prompt)
        {
            string inputStr;

            Console.Write(i_Prompt);
            inputStr = Console.ReadLine();
            while (inputStr.Equals(string.Empty))
            {
                Console.WriteLine(
@"Empty string is invalid");
                Console.Write(i_Prompt);
                inputStr = Console.ReadLine();
            }

            return inputStr;
        }

        // get a number selection from the user, the number must be between the given min and max values
        internal static ushort GetNumberInputFromUserInRange(ushort i_MinValidSelection, ushort i_MaxValidSelection)
        {
            bool isValidInput = false;
            ushort userSelection = 0;

            while (!isValidInput)
            {
                while (!ushort.TryParse(Console.ReadLine(), out userSelection))
                {
                    // TODO use exception?
                    Console.WriteLine(
@"Input format error please input a number");
                }

                if (userSelection >= i_MinValidSelection && userSelection <= i_MaxValidSelection)
                {
                    isValidInput = true;
                }
                else
                {
                    // TODO use exception?
                    Console.WriteLine(
@"Please input a number between {0} and {1}",
i_MinValidSelection,
i_MaxValidSelection);
                }
            }

            return userSelection;
        }

        // returns Y or N from user, displays "i_Prompt"
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

        // display a list of all enum values, return the user's selection
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
                Console.WriteLine(
@"{0}. {1}", 
item.Key, 
item.Value);
            }

            userSelection = GetNumberInputFromUserInRange(1, (ushort)(enumCounter - 1));

            return enumDictinary[userSelection];
        }

        // display "press any key to continue" and wait for user to hit a key
        internal static void PressAnyKeyToContinue()
        {
            Console.WriteLine(
@"(press any key to continue)
");
            Console.ReadKey();
        }

        // get a list of pairs of desriptions and type, and return the user's selection
        internal static Type SelectTypeFromListOfDescriptionAndTypePair(List<KeyValuePair<string, Type>> i_TypleList, string i_ListHeader)
        {
            Console.WriteLine(
@"Please select the {0} type:",
i_ListHeader);
            // print all type descriptions in the given list
            for (int i = 0; i < i_TypleList.Count; i++)
            {
                Console.WriteLine(
@"{0}. {1}",
i + 1,
i_TypleList[i].Key);
            }

            ushort input = GetNumberInputFromUserInRange(1, (ushort)i_TypleList.Count);

            //return the matching type selected bt the user
            return i_TypleList[input - 1].Value;
        }
    }
}
