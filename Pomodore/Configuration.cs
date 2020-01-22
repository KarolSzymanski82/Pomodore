using System;
using Pomodore.Models;

namespace Pomodore
{
    public class Configuration
    {
        private readonly AppSettings _appSettings;
        public Configuration()
        {
            _appSettings = new AppSettings();
        }

        public TimeRange GetPomodoreTime()
        {
            var currentDate = DateTime.Now.Date.AddHours(DateTime.Now.Hour);
            return new TimeRange
            {
                StartTime = currentDate.AddMinutes(_appSettings.StartMinute),
                EndTime = currentDate.AddMinutes(_appSettings.EndMinute)
            };
        }
    }
}
