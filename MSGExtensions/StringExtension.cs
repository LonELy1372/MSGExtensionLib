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
        public static Boolean IsValidNationalCode(this String nationalCode)
        {
            //در صورتی که کد ملی وارد شده تهی باشد

            if (String.IsNullOrEmpty(nationalCode))
                throw new Exception("لطفا کد ملی را صحیح وارد نمایید");


            if (nationalCode.Length != 10)
                throw new Exception("طول کد ملی باید ده کاراکتر باشد");

            var regex = new Regex(@"\d{10}");
            if (!regex.IsMatch(nationalCode))
                throw new Exception("کد ملی تشکیل شده از ده رقم عددی می‌باشد؛ لطفا کد ملی را صحیح وارد نمایید");

            var allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
            if (allDigitEqual.Contains(nationalCode)) return false;


            var chArray = nationalCode.ToCharArray();
            var num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
            var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
            var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
            var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
            var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
            var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
            var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
            var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
            var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
            var a = Convert.ToInt32(chArray[9].ToString());

            var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
            var c = b % 11;

            return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
        }

        public static bool IsValidMacAddress(this string MacAddress)
        {
            var addMacReg = new Regex("^([0-9A-F]{2}[:-]){5}([0-9A-F]{2})$");
            return addMacReg.IsMatch(MacAddress.ToUpper());
        }

        public static string ValidNumericValue(this string Value)
        {
            return Value.IsNumeric() ? Value : "0";
        }

        public static string ValidNumericValue(this string Value, string DefaultValue)
        {
            return Value.IsNumeric() ? Value : DefaultValue;
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
