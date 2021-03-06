﻿using System;

namespace BusinessLogic.Handlers
{
    public static class DateTimeHandler
    {
        private static DateTime _currentTime = DateTime.UtcNow;
        public static bool Enabled { get; set; }

        public static DateTime GetCurrentTime()
        {
            return Enabled ? _currentTime : DateTime.UtcNow;
        }

        /// <summary>
        ///     Return the time point when reservations can be checked in on the current day
        /// </summary>
        /// <returns></returns>
        public static DateTime GetCurrentStartTime()
        {
            return Enabled ? _currentTime.Date.AddHours(12) : DateTime.UtcNow.Date.AddHours(12);
        }

        /// <summary>
        ///     Return the time point when reservations must be checked out on the current day
        /// </summary>
        /// <returns></returns>
        public static DateTime GetCurrentEndTime()
        {
            return Enabled ? _currentTime.Date.AddHours(14) : DateTime.UtcNow.Date.AddHours(14);
        }

        public static DateTime GetCurrentDate()
        {
            return Enabled ? _currentTime.Date : DateTime.UtcNow.Date;
        }

        public static void SetCurrentTime(DateTime newDate)
        {
            _currentTime = newDate;
        }
    }
}