using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlarmMonitor.Models.View
{
    public class DateScheduleViewModel
    {
        public string Name { get; set; }
        public DaySchedule Day { get; set; }
        public NightSchedule Night { get; set;}
    }
    public class DaySchedule
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
    public class NightSchedule
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}