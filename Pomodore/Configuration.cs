using System;
using Pomodore.Models;

namespace Pomodore
{
    public class Configuration
    {
        private int startMinute = 0;
        private int endMinute = 15;
        public TimeRange GetPomodoreTime()
        {
            var currentDate = DateTime.Now.Date.AddHours(DateTime.Now.Hour);
            return new TimeRange
            {
                StartTime = currentDate.AddMinutes(startMinute),
                EndTime = currentDate.AddMinutes(endMinute)
            };
        }
    }
}
