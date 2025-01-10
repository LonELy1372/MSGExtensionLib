using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MSGExtensions
{
    public static class EncryptionExtension
    {
        public static string ToSHA256(this string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // محاسبه هش
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // تبدیل آرایه بایت به رشته هگزادسیمال
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // فرمت هگزادسیمال (دو رقمی)
                }
                return builder.ToString();
            }
            // return encodedBytes;
        }
    }
}
