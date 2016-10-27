using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopscale.Shared.Helpers
{
    public static class DateHelper
    {
        public static DateTime GetUTCDate(DateTime date)
        {
            DateTime convertedDate = DateTime.SpecifyKind(DateTime.Parse(date.ToString()), DateTimeKind.Utc);
            var kind = convertedDate.Kind;

            return convertedDate.ToLocalTime();
        }

        public static DateTimeSpan GetAge(DateTime dateOfBirth, DateTime tillDate)
        {
            DateTime dob = DateTime.Parse(dateOfBirth.ToLongDateString());
            DateTime now = DateTime.Parse(tillDate.ToLongDateString());

            var dateSpan = DateTimeSpan.CompareDates(dob, now);

            return dateSpan;

            /*
                Console.WriteLine("Years: " + dateSpan.Years);
                Console.WriteLine("Months: " + dateSpan.Months);
                Console.WriteLine("Days: " + dateSpan.Days);
                Console.WriteLine("Hours: " + dateSpan.Hours);
                Console.WriteLine("Minutes: " + dateSpan.Minutes);
                Console.WriteLine("Seconds: " + dateSpan.Seconds);
                Console.WriteLine("Milliseconds: " + dateSpan.Milliseconds);
             */
        }
    }
}
