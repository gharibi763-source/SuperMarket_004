using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SuperMarket_004
{
    internal class ShamsiDate
    {
        public static string miladitoshamsi(DateTime date) 
        {
            PersianCalendar persian = new PersianCalendar();
            StringBuilder builder = new StringBuilder();

            builder.Append(persian.GetDayOfMonth(date).ToString("00"));
            builder.Append(" / ");

            builder.Append(persian.GetMonth(date).ToString("00"));
            builder.Append(" / ");

            builder.Append(persian.GetYear(date).ToString("0000"));
                                

            
            return builder.ToString();
        }
    }
}
