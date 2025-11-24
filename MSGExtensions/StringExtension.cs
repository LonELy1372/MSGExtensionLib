using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace MSGExtensions
{
    public static class StringExtension
    {
        public static string EncodeBase64(this string value)
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(valueBytes);
        }

        public static string DecodeBase64(this string value)
        {
            var valueBytes = System.Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(valueBytes);
        }

        public static IEnumerable<string> SplitByLength(this string str, int maxLength)
        {
            for (int index = 0; index < str.Length; index += maxLength)
            {
                yield return str.Substring(index, Math.Min(maxLength, str.Length - index));
            }
        }
        public static int ToInt32(this string value)
        {
            try
            {
                return Int32.Parse(value);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static long ToInt64(this string value)
        {
            try
            {
                return Int64.Parse(value);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static double ToDouble(this string value)
        {
            try
            {
                return Double.Parse(value);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static Guid ToGuid(this string value)
        {
            try
            {
                return Guid.Parse(value);
            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }
        }

        public static Guid? ToNullGuid(this string value)
        {
            try
            {
                return Guid.Parse(value);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string ToEnglishNumber(this string PersianNumbers)
        {
            string[] persian = new string[10] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };

            for (int j = 0; j < persian.Length; j++)
                PersianNumbers = PersianNumbers.Replace(persian[j], j.Ext_ToString());

            return PersianNumbers;
        }

        public static string RemoveLastOccurrence(this string query, string regexPattern)
        {
            return Regex.Replace(query, regexPattern, "", RegexOptions.IgnoreCase);
        }

        public static T GetValue<T>(this string value)
        {
            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(value);
        }

        public static string NumberToPersianWord(this int number)
        {
            Dictionary<int, string> persianWords = new Dictionary<int, string>()
                {
                    {0, "صفر"},
                    {1, "اول"},
                    {2, "دوم"},
                    {3, "سوم"},
                    {4, "چهارم"},
                    {5, "پنجم"},
                    {6, "ششم"},
                    {7, "هفتم"},
                    {8, "هشتم"},
                    {9, "نهم"},
                    {10, "دهم"},
                    {11, "یازدهم"},
                    {12, "دوازدهم"},
                    {13, "سیزدهم"},
                    {14, "چهاردهم"},
                    {15, "پانزدهم"},
                    {16, "شانزدهم"},
                    {17, "هفدهم"},
                    {18, "هجدهم"},
                    {19, "نوزدهم"},
                    {20, "بیستم"}
                };

            if (persianWords.ContainsKey(number))
                return persianWords[number];
            else
                return number + "ام";
        }

        public static string NumberToWord(this string Number)
        {
            string[] Units = { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه", "ده" };
            string[] ThousandsGroups = { "هزار", "میلیون", "میلیارد" };

            long number = Number.ValidNumericValue().ToInt64();
            if (number == 0)
                return Units[0];

            var words = "";

            int thousandCounter = 0;

            while (number > 0)
            {
                long chunk = number % 1000;
                if (chunk > 0)
                {
                    string chunkWords = ConvertChunkToWords(chunk);
                    if (thousandCounter > 0)
                    {
                        chunkWords += " " + ThousandsGroups[thousandCounter - 1];
                    }
                    words = chunkWords + " " + words;
                }

                number /= 1000;
                thousandCounter++;
            }

            return words.Trim();
        }

        private static string ConvertChunkToWords(long number)
        {
            string[] Units = { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" , "ده"};
            string[] Teens = { "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
            string[] Tens = { "ده", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
            string[] Hundreds = { "صد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };

            var chunkWords = "";

            if (number >= 100)
            {
                chunkWords += Hundreds[number / 100 - 1] + " ";
                number %= 100;
            }

            if (number >= 20)
            {
                chunkWords += Tens[number / 10 - 1] + " ";
                number %= 10;
            }
            else if (number >= 11 && number <= 19)
            {
                chunkWords += Teens[number - 11] + " ";
                return chunkWords.Trim();
            }

            if (number > 0)
            {
                chunkWords += Units[number] + " ";
            }

            return chunkWords.Trim();
        }
    }
}
