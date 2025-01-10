using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MSGExtensions
{
    public static class ObjectExtension
    {
        public static string Ext_ToString(this object obj)
        {
            if (obj != null)
            {
                if (obj.ToString().ToLower() == "null")
                    return string.Empty;
                else
                    return obj.ToString();
            }
            else
                return string.Empty;
        }

        public static string NotNullToString(this object value)
        {
            if (value != null)
            {
                if (value.ToString().ToLower() == "null")
                    return string.Empty;
                else
                    return value.ToString();
            }
            else
                return string.Empty;
        }

        public static bool IsNumeric(this object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;


            double res;
            if (Expression is string)
            {
                var isSuccess = double.TryParse(Expression as string, out res);
                return isSuccess;
            }
            else
            {
                var isSuccess = double.TryParse(Expression.Ext_ToString(), out res);
                return isSuccess;
            }
        }

        public static string ValidNumericValue(this object obj,string DefaultNumber)
        {
            return obj.IsNumeric() ? obj.Ext_ToString() : DefaultNumber;
        }

        public static string ValidNumericValue(this object obj)
        {
            return obj.IsNumeric() ? obj.Ext_ToString() : "0";
        }
        public static T GetValueFromObject<T>(this object value)
        {
            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(value.NotNullToString());
        }
    }
}

