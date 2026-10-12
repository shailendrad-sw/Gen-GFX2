using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
//namespace graphicserver.Classes
//{
    public static class UtilityCls
    {
        public static TimeSpan GetTimeDifference(string dateTime1, string dateTime2)
        {
            string[] formats =
            {
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-dd|HH:mm:ss",
            "yyyy-MM-dd HH:mm",
            "yyyy-MM-dd|HH:mm"
        };

            DateTime dt1 = DateTime.ParseExact(
                dateTime1,
                formats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None);

            DateTime dt2 = DateTime.ParseExact(
                dateTime2,
                formats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None);

            return dt2 - dt1;
        }
    }
//}
