using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSGExtensions
{
    public static class DateTimeExtension
    {
        public enum PersianDateType { NumberMode, MonthString, FullText, Digit8, TimeOnly, YearOnly, DayMonthString, FullString }

        public enum HourType { Hour12, Hour24, Hour12WithSec, Hour24WithSec }
        public static String PersionDayOfWeek(this DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return "شنبه";
                case DayOfWeek.Sunday:
                    return "یکشنبه";
                case DayOfWeek.Monday:
                    return "دوشنبه";
                case DayOfWeek.Tuesday:
                    return "سه شنبه";
                case DayOfWeek.Wednesday:
                    return "چهارشنبه";
                case DayOfWeek.Thursday:
                    return "پنجشنبه";
                case DayOfWeek.Friday:
                    return "جمعه";
                default:
                    throw new Exception();
            }
        }
        public static string ToPersianDateString(this DateTime date, PersianDateType Format)
        {
            PersianCalendar pc = new PersianCalendar();
            int year, month, day;
            year = pc.ToFourDigitYear(pc.GetYear(date));
            month = pc.GetMonth(date);
            day = pc.GetDayOfMonth(date);
            int hour, min, sec;
            hour = pc.GetHour(date);
            min = pc.GetMinute(date);
            sec = pc.GetSecond(date);
            string _date = "";
            string _monthString = "";
            if (Format == PersianDateType.YearOnly)
            {
                _date = year.ToString();
            }
            if (Format == PersianDateType.NumberMode)
            {
                _date = year + "/" + (month < 10 ? "0" + month : month.ToString()) + "/" + (day < 10 ? "0" + day : day.ToString());
            }
            else if (Format == PersianDateType.MonthString)
            {// _date = day + 
                switch (month)
                {
                    case 1: _monthString = "فروردین"; break;
                    case 2: _monthString = "اردیبهشت"; break;
                    case 3: _monthString = "خرداد"; break;
                    case 4: _monthString = "تیر"; break;
                    case 5: _monthString = "مرداد"; break;
                    case 6: _monthString = "شهریور"; break;
                    case 7: _monthString = "مهر"; break;
                    case 8: _monthString = "آبان"; break;
                    case 9: _monthString = "آذر"; break;
                    case 10: _monthString = "دی"; break;
                    case 11: _monthString = "بهمن"; break;
                    case 12: _monthString = "اسفند"; break;
                }
                _date = day + " " + _monthString + " " + year;
            }
            else if (Format == PersianDateType.FullText)
            {
                _date = year + "/" + (month < 10 ? "0" + month : month.ToString()) + "/" + (day < 10 ? "0" + day : day.ToString()) + " " + (hour < 10 ? "0" + hour : hour.ToString()) + ":" + (min < 10 ? "0" + min : min.ToString()) + ":" + (sec < 10 ? "0" + sec : sec.ToString());
            }
            else if (Format == PersianDateType.Digit8)
            {
                _date = year + "" + (month < 10 ? "0" + month : month.ToString()) + "" + (day < 10 ? "0" + day : day.ToString());
            }
            else if (Format == PersianDateType.TimeOnly)
            {
                _date = (hour < 10 ? "0" + hour : hour.ToString()) + ":" + (min < 10 ? "0" + min : min.ToString()) + ":" + (sec < 10 ? "0" + sec : sec.ToString());
            }
            else if (Format == PersianDateType.DayMonthString)
            {
                switch (month)
                {
                    case 1: _monthString = "فروردین"; break;
                    case 2: _monthString = "اردیبهشت"; break;
                    case 3: _monthString = "خرداد"; break;
                    case 4: _monthString = "تیر"; break;
                    case 5: _monthString = "مرداد"; break;
                    case 6: _monthString = "شهریور"; break;
                    case 7: _monthString = "مهر"; break;
                    case 8: _monthString = "آبان"; break;
                    case 9: _monthString = "آذر"; break;
                    case 10: _monthString = "دی"; break;
                    case 11: _monthString = "بهمن"; break;
                    case 12: _monthString = "اسفند"; break;
                }
                _date = day + " " + _monthString;
            }
            else if (Format == PersianDateType.FullString)
            {
                _date = ToPersianDateString(date, PersianDateType.MonthString);
            }
            return _date;
        }
        public static string PersianDateToString(this string persianDate)
        {
            string[] dateParts = persianDate.Split('/');
            int year = int.Parse(dateParts[0]);
            int month = int.Parse(dateParts[1]);
            int day = int.Parse(dateParts[2]);

            // استفاده از PersianCalendar

            // لیست نام ماه‌ها
            string[] persianMonths = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };

            // تبدیل روز و سال به حروف
            string dayInWords = ConvertDayToWords(day);
            string yearInWords = ConvertYearToWords(year);

            // نمایش تاریخ به حروف
            string result = $"{dayInWords} {persianMonths[month - 1]} {yearInWords}";

            return result;
        }
        public static DateTime PersianDateStringToDateTime(this string PersianDateTime, bool endOfDay = false)
        {
            if (PersianDateTime == "")
            {
                return DateTime.MaxValue;
            }
            else
            {
                try
                {
                    var splited = PersianDateTime.ToEnglishNumber().Split('/', '-');
                    int year, month, day;
                    Int32.TryParse(splited[0], out year);
                    Int32.TryParse(splited[1], out month);
                    Int32.TryParse(splited[2], out day);
                    PersianCalendar pc = new PersianCalendar();
                    if (endOfDay)
                        return pc.ToDateTime(year, month, day, 23, 59, 59, 999);
                    else
                        return pc.ToDateTime(year, month, day, 0, 0, 0, 1);
                }
                catch (Exception)
                {
                    return DateTime.MaxValue;
                }
            }
        }

        public static DateTime FullPersianDateTimeStringToDateTime(this string FSPersianDateTime)
        {
            if (FSPersianDateTime == "")
            {
                return DateTime.MaxValue;
            }
            else
            {
                var DateAndTime = FSPersianDateTime.Split(' ');
                string FaDate = DateAndTime[0];
                var FaTime = DateAndTime[1].Split(':');
                var Result = FaDate.PersianDateStringToDateTime()
                                    .AddHours(int.Parse(FaTime[0]))
                                    .AddMinutes(int.Parse(FaTime[1]))
                                    .AddSeconds(int.Parse(FaTime[2]));
                return Result;
            }
        }

        public static string CustomTimeStamp(this DateTime now)
        {
            PersianCalendar pc = new PersianCalendar();
            var start = pc.ToDateTime(1394, 1, 1, 0, 0, 0, 0);
            var result = ((long)(now - start).TotalSeconds).NotNullToString();
            return result;
        }

        public static string TimeAgo(this DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("حدود {0} {1} قبل",
                years, years == 1 ? "سال" : "سال");
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("حدود {0} {1} قبل",
                months, months == 1 ? "ماه" : "ماه");
            }
            if (span.Days > 0)
                return String.Format("حدود {0} {1} قبل",
                span.Days, span.Days == 1 ? "روز" : "روز");
            if (span.Hours > 0)
                return String.Format("حدود {0} {1} قبل",
                span.Hours, span.Hours == 1 ? "ساعت" : "ساعت");
            if (span.Minutes > 0)
                return String.Format("حدود {0} {1} قبل",
                span.Minutes, span.Minutes == 1 ? "دقیقه" : "دقیقه");
            if (span.Seconds > 5)
                return String.Format("حدود {0} ثانیه قبل", span.Seconds);
            if (span.Seconds <= 5)
                return "هم اکنون";
            return string.Empty;
        }

        public static string TimeAgo(this DateTime dt, DateTime dtNow)
        {
            TimeSpan span = dtNow - dt;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("حدود {0} {1} ",
                years, years == 1 ? "سال" : "سال");
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("حدود {0} {1} ",
                months, months == 1 ? "ماه" : "ماه");
            }
            if (span.Days > 0)
                return String.Format("حدود {0} {1} ",
                span.Days, span.Days == 1 ? "روز" : "روز");
            if (span.Hours > 0)
                return String.Format("حدود {0} {1} ",
                span.Hours, span.Hours == 1 ? "ساعت" : "ساعت");
            if (span.Minutes > 0)
                return String.Format("حدود {0} {1} ",
                span.Minutes, span.Minutes == 1 ? "دقیقه" : "دقیقه");
            if (span.Seconds > 5)
                return String.Format("حدود {0} ثانیه ", span.Seconds);
            if (span.Seconds <= 5)
                return "هم اکنون";
            return string.Empty;
        }

        public static string GetHour(this DateTime dt, HourType ht)
        {
            switch (ht)
            {
                case HourType.Hour12:
                    return dt.ToString("hh:mm");
                case HourType.Hour24:
                    return dt.ToString("HH:mm");
                case HourType.Hour12WithSec:
                    return dt.ToString("hh:mm:ss");
                case HourType.Hour24WithSec:
                    return dt.ToString("HH:mm:ss");
                default:
                    return "";
            }
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }

        public static DateTime EndOfWeek(this DateTime date, DayOfWeek startOfWeek)
        {
            DateTime ldowDate = date.StartOfWeek(startOfWeek).AddDays(6);
            return ldowDate;
        }

        public static DateTime StartOfMonth(this DateTime dt)
        {
            var pc = new System.Globalization.PersianCalendar();
            var CurrentMonth = pc.GetMonth(dt);
            var CurrentYear = pc.GetYear(dt);
            return pc.ToDateTime(CurrentYear, CurrentMonth, 1, 0, 0, 0, 0);
        }

        public static DateTime EndOfMonth(this DateTime dt)
        {
            var pc = new System.Globalization.PersianCalendar();
            var FirstDayOfMonth = dt.StartOfMonth();
            return pc.AddDays(pc.AddMonths(FirstDayOfMonth, 1), -1).AddHours(23).AddMinutes(59).AddSeconds(59);
        }

        public static DateTime StartOfSeason(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            var CurrentMonth = pc.GetMonth(date);
            var CurrentYear = pc.GetYear(date);
            int currQuarter = (CurrentMonth - 1) / 3 + 1;
            switch (currQuarter)
            {
                case 1:
                    return pc.ToDateTime(CurrentYear, 1, 1, 0, 0, 0, 0);
                case 2:
                    return pc.ToDateTime(CurrentYear, 4, 1, 0, 0, 0, 0);
                case 3:
                    return pc.ToDateTime(CurrentYear, 7, 1, 0, 0, 0, 0);
                default:
                    return pc.ToDateTime(CurrentYear, 10, 1, 0, 0, 0, 0);
            }
        }

        public static DateTime EndOfSeason(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            var CurrentMonth = pc.GetMonth(date);
            var CurrentYear = pc.GetYear(date);
            int currQuarter = (CurrentMonth - 1) / 3 + 1;
            switch (currQuarter)
            {
                case 1:
                    return pc.ToDateTime(CurrentYear, 3, 31, 23, 59, 59, 0);
                case 2:
                    return pc.ToDateTime(CurrentYear, 6, 31, 23, 59, 59, 0);
                case 3:
                    return pc.ToDateTime(CurrentYear, 9, 30, 23, 59, 59, 0);
                default:
                    return pc.ToDateTime(CurrentYear, 12, pc.IsLeapYear(CurrentYear) ? 30 : 29, 23, 59, 59, 0);
            }
        }

        public static DateTime StartOfYear(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            var CurrentYear = pc.GetYear(date);
            return pc.ToDateTime(CurrentYear, 1, 1, 0, 0, 0, 0);
        }

        public static DateTime EndOfYear(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            var CurrentYear = pc.GetYear(date);
            return pc.ToDateTime(CurrentYear, 12, pc.IsLeapYear(CurrentYear) ? 30 : 29, 23, 59, 59, 0);
        }

        public static string PersianAddMonth(this string date, int count)
        {
            var part = date.Split('/');
            int y = int.Parse(part[0]);
            int m = int.Parse(part[1]);
            int d = int.Parse(part[2]);
            if (count >= 0)
            {
                int m1 = m + count;
                m = m1 % 12 == 0 ? 12 : m1 % 12;
                y = y + (m1 / 12) - (m1 % 12 == 0 ? 1 : 0);
                return y + "/" + m.ToString().PadLeft(2, '0') + "/" + d.ToString().PadLeft(2, '0');
            }
            else
            {
                int m1 = m + count;
                if (m1 == 0)
                {
                    y = y - 1;
                    m = 12;
                }
                else if (m1 > 0)
                {
                    m = m1;
                }
                else
                {
                    int yd = Math.Abs(m1) / 12 + 1;
                    y = y - yd;
                    m = 12 - Math.Abs(m1) % 12;
                }

                return y + "/" + m.ToString().PadLeft(2, '0') + "/" + d.ToString().PadLeft(2, '0');
            }

        }

        private static string ConvertDayToWords(int day)
        {
            string[] numbersInWords = { "", "یکم", "دوم", "سوم", "چهارم", "پنجم", "ششم", "هفتم", "هشتم", "نهم", "دهم", "یازدهم", "دوازدهم", "سیزدهم", "چهاردهم", "پانزدهم", "شانزدهم", "هفدهم", "هجدهم", "نوزدهم", "بیستم", "بیست و یکم", "بیست و دوم", "بیست و سوم", "بیست و چهارم", "بیست و پنجم", "بیست و ششم", "بیست و هفتم", "بیست و هشتم", "بیست و نهم", "سی‌ام", "سی و یکم" };

            return numbersInWords[day];
        }

        private static string ConvertYearToWords(int year)
        {
            string[] digits = { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };

            // تبدیل هر چهار رقم سال به حروف
            string yearInWords = $"هزار و {digits[year / 100 % 10]}صد و {digits[year / 10 % 10]}{digits[year % 10]}";

            // بهبود بیشتر برای ظاهر خواناتر
            if (year % 100 == 0)
            {
                yearInWords = $"هزار و {digits[year / 100 % 10]}صد";
            }

            return yearInWords.Replace("صفر", "");
        }

        public static string ToGregorianString(this DateTime dateTime)
        {
            return dateTime.ToString("M/d/yyyy h:mm:ss tt",
                System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
