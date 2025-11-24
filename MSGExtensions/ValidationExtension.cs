using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MSGExtensions
{
    public static class ValidationExtension
    {
        public static bool IsValidIranianMobile(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
                return false;

            mobile = mobile.Trim();

            // حذف +98 یا 0098
            if (mobile.StartsWith("+98"))
                mobile = mobile.Substring(3);
            else if (mobile.StartsWith("0098"))
                mobile = mobile.Substring(4);

            // اگر با 0 شروع نشده بود، خودش را اضافه میکنیم
            if (!mobile.StartsWith("0"))
                mobile = "0" + mobile;

            // بعد از نرمال‌سازی باید دقیقا 11 رقم باشد
            if (mobile.Length != 11)
                return false;

            // فقط عدد باشد
            if (!mobile.All(char.IsDigit))
                return false;

            // الگوی رسمی اپراتورها (همراه اول، ایرانسل، رایتل، شاتل، …)
            // 09 + (0 تا 9) + 8 رقم
            return Regex.IsMatch(mobile, @"^09\d{9}$");
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
    }
}
