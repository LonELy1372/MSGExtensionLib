using System;
using System.Collections.Generic;

namespace MSGExtensions
{
    public enum StringFormatType { Money }
    public static class Extensions
    {
        #region List
        public static void Move<T>(this List<T> list, T item, int newIndex)
        {
            if (item != null)
            {
                var oldIndex = list.IndexOf(item);
                if (oldIndex > -1)
                {
                    list.RemoveAt(oldIndex);

                    if (newIndex > oldIndex) newIndex--;

                    list.Insert(newIndex, item);
                }
            }
        }
        #endregion List

        #region Object
        public static string FormatString(this object obj, StringFormatType type)
        {
            switch (type)
            {
                case StringFormatType.Money:
                    try
                    {
                        return obj.ValidNumericValue().Length > 3 ? string.Format("{0:0,000}", obj) : obj.ValidNumericValue();
                    }
                    catch
                    {
                        return "";
                    }
                default:
                    return "";
            }
        }
        #endregion Object

        

        


    }
}