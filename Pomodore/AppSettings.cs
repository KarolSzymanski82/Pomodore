using System.Configuration;

namespace Pomodore
{
    class AppSettings
    {
        public int StartMinute => int.Parse(ConfigurationManager.AppSettings["StartMinute"]);
        public int EndMinute => int.Parse(ConfigurationManager.AppSettings["EndMinute"]);
    }
}
