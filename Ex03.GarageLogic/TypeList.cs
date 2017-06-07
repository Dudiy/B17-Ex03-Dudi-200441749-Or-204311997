//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text;

//namespace Ex03.GarageLogic
//{
//    public class TypeList : IEnumerable
//    {
//        private List<KeyValuePair<string,Type>> m_List = new List<KeyValuePair<string, Type>>();

//        public TypeList(List<KeyValuePair<string, Type>> i_List)
//        {
//            m_List = i_List;
//        }

//        public void Add(Type i_Input)
//        {
//            m_List.Add(new KeyValuePair<string, Type>(i_Input.Name, i_Input));
//        }

//        public void Add(string i_Description, Type i_Input)
//        {
//            m_List.Add(new KeyValuePair<string, Type>(i_Description, i_Input));
//        }

//        public int Count
//        {
//            get { return m_List.Count; }
//        }

//        public KeyValuePair<string, Type> this[int i]
//        {
//            get { return m_List[i]; }
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return m_List.GetEnumerator();
//        }
//    }
//}
