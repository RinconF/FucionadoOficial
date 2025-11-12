using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DCL
{
    public class objSearch<T>
    {
        private object _obj;

        private string _PropertyName;

        public objSearch(string PropertyName, object obj)
        {
            _obj = obj;
            _PropertyName = PropertyName;
        }

        public object obj
        {
            get { return _obj; }
            set { _obj = value; }
        }

        public string PropertyName
        {
            get { return _PropertyName; }
            set { _PropertyName = value; }
        }

        public bool Search(T obj)
        {
            bool bolReturn = false;

            object oblPri = obj.GetType().GetProperty(_PropertyName).GetValue(obj, null);

            if (Equals(oblPri, _obj))
            {
                return true;
            }

            return bolReturn;
        }
    }

    public class GenericSortList<T> : List<T>
    {
        private bool mbooAscending;
        private string mstrProperty;

        public void Sort(string strProperty)
        {
            if (strProperty != string.Empty)
            {
                bool booAscending = false;
                if (strProperty.IndexOf(" ASC") > 0)
                {
                    booAscending = true;
                    strProperty = strProperty.Remove(strProperty.IndexOf(" ASC"));
                }
                else
                {
                    booAscending = false;
                    strProperty = strProperty.Remove(strProperty.IndexOf(" DESC"));
                }
                if (mstrProperty == strProperty && mbooAscending == booAscending)
                {
                    mbooAscending = !booAscending;
                }
                else
                {
                    mstrProperty = strProperty;
                    mbooAscending = booAscending;
                }

                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                PropertyDescriptor propertyDesc = properties.Find(strProperty, true);
            }
        }
    }
}