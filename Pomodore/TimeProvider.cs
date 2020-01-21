using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodore
{
    public class TimeProvider
    {
        private Configuration _configuration = new Configuration();
        public bool IsPomodoreTime(DateTime lastClearTime)
        {
            var currentDate = DateTime.Now;
            var pomodoreTime = _configuration.GetPomodoreTime();
            if (lastClearTime > pomodoreTime.StartTime)
            {
                return false;
            }

            if (currentDate >= pomodoreTime.StartTime && currentDate < pomodoreTime.EndTime)
            {
                return IsOutlookCalendarFree();
            }

            return false;
        }

        private bool IsOutlookCalendarFree()
        {
            return true;
        }
    }
}
