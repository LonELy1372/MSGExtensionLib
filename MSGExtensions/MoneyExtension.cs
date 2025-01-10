using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace MSGExtensions
{
    public static class MoneyExtension
    {
        public static long RoundUp(this long Number, long roundBase)
        {
            return (long)Math.Ceiling((double)Number / roundBase) * roundBase;
        }
        public static long RoundDown(this long Number, long roundBase)
        {
            return (long)Math.Floor((double)Number / roundBase) * roundBase;
        }
        public static long RoundMid(this long Number, long roundBase)
        {
            return (long)Math.Round((double)Number / roundBase) * roundBase;
        }
    }
}