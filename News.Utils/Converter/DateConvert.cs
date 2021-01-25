using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace News.Utilities.Converter
{
    public static class DateConvert
    {
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" + pc.GetDayOfMonth(value).ToString("00");
        }
    }
}
