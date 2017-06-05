using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ex03.GarageLogic
{
    public class UserInputFunctions : IEnumerable
    {
        private static readonly Dictionary<string, string> sr_UserInputFunctions = new Dictionary<string, string>();

        internal void Add (string i_Description, string i_MethodName)
        {
            sr_UserInputFunctions.Add(i_Description, i_MethodName);
        }

        public int Count
        {
            get { return sr_UserInputFunctions.Count; }
        }

        public IEnumerator GetEnumerator()
        {
            return sr_UserInputFunctions.GetEnumerator();
        }
    }
}
