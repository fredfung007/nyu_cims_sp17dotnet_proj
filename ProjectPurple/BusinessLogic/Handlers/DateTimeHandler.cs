using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Handlers
{
    public static class DateTimeHandler
    {
        private static DateTime _currentTime = new DateTime();

        public static DateTime GetCurrentTime()
        {
            return _currentTime;
        }

        public static void SetCurrentTime(DateTime newDate)
        {
            _currentTime = newDate;
        }
    }
}
