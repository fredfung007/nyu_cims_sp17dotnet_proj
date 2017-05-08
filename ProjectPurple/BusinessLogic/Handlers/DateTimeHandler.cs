using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Handlers
{
    public static class DateTimeHandler
    {
        public static bool Enabled { get; set; }
        private static DateTime _currentTime = DateTime.Now;

        public static DateTime GetCurrentTime()
        {
            return Enabled? _currentTime : DateTime.Now;
        }

        /// <summary>
        /// Return the time point when reservations can be checked in on the current day
        /// </summary>
        /// <returns></returns>
        public static DateTime GetCurrentStartTime()
        {
            return Enabled? _currentTime.Date.AddHours(12) : DateTime.Now.Date.AddHours(12);
        }

        /// <summary>
        /// Return the time point when reservations must be checked out on the current day
        /// </summary>
        /// <returns></returns>
        public static DateTime GetCurrentEndTime()
        {
            return Enabled? _currentTime.Date.AddHours(14) : DateTime.Now.Date.AddHours(14);
        }

        public static DateTime GetCurrentDate()
        {
            return Enabled? _currentTime.Date : DateTime.Now.Date;
        }

        public static void SetCurrentTime(DateTime newDate)
        {
            _currentTime = newDate;
        }
    }
}
