using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Handlers
{
    public static class DateTimeHandler
    {
        private static DateTime _currentTime = DateTime.Now;

        public static DateTime GetCurrentTime()
        {
            return _currentTime;
        }

        /// <summary>
        /// Return the time point when reservations can be checked in on the current day
        /// </summary>
        /// <returns></returns>
        public static DateTime GetCurrentStartTime()
        {
            return _currentTime.Date.AddHours(12);
        }

        /// <summary>
        /// Return the time point when reservations must be checked out on the current day
        /// </summary>
        /// <returns></returns>
        public static DateTime GetCurrentEndTime()
        {
            return _currentTime.Date.AddHours(14);
        }

        public static DateTime GetCurrentDate()
        {
            return _currentTime.Date;
        }

        public static void SetCurrentTime(DateTime newDate)
        {
            _currentTime = newDate;
        }
    }
}
