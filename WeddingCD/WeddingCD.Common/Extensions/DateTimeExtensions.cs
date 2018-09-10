using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.Extensions
{
    /// <summary>
    /// Extension class for DateTime
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Gets the day number from 1 (monday) to 7 (sunday).
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The day number</returns>
        public static int GetDayNumber(this DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return 1;

                case DayOfWeek.Tuesday:
                    return 2;

                case DayOfWeek.Wednesday:
                    return 3;

                case DayOfWeek.Thursday:
                    return 4;

                case DayOfWeek.Friday:
                    return 5;

                case DayOfWeek.Saturday:
                    return 6;

                case DayOfWeek.Sunday:
                default:
                    return 7;
            }
        }

        /// <summary>
        /// Converts an unix time to an actual DateTime.
        /// </summary>
        /// <param name="unixTime">The unix time.</param>
        /// <returns>The DateTime</returns>
        public static DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        /// <summary>
        /// Converts the specified date to Unix time.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// The Unix time
        /// </returns>
        public static long ToUnixTime(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date.ToUniversalTime() - epoch).TotalSeconds);
        }

        /// <summary>
        /// Gets the next date from the currentDate that corresponds to target day/hour.
        /// </summary>
        /// <param name="currentDate">The current date.</param>
        /// <param name="targetDay">The target day.</param>
        /// <param name="targetHour">The target hour.</param>
        /// <returns>The next date</returns>
        public static DateTime GetNextDate(this DateTime currentDate, DayOfWeek targetDay, TimeSpan targetHour)
        {
            // If current date is same week day than target day, compute hours only
            if (currentDate.DayOfWeek == targetDay)
            {
                var sameDayTargetHour = new DateTime(
                        currentDate.Year,
                        currentDate.Month,
                        currentDate.Day,
                        targetHour.Hours,
                        targetHour.Minutes,
                        targetHour.Seconds,
                        DateTimeKind.Utc);

                if (currentDate.TimeOfDay < targetHour)
                {
                    // Target hour will be today
                    return sameDayTargetHour;
                }
                else
                {
                    // Target will be next week
                    return sameDayTargetHour.AddDays(7);
                }
            }
            else
            {
                // Gets the next day of week that corresponds to the target
                var daysUntilTargetDay = ((int)targetDay - (int)currentDate.DayOfWeek + 7) % 7;
                var date = new DateTime(
                        currentDate.Year,
                        currentDate.Month,
                        currentDate.Day,
                        targetHour.Hours,
                        targetHour.Minutes,
                        targetHour.Seconds,
                        DateTimeKind.Utc);
                return date.AddDays(daysUntilTargetDay);
            }
        }
    }
}