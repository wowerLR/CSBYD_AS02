using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlarmMonitor.Models.View
{
    public class FilterForAlarmEdit
    {
        public DateTime? Start_time { get; set; }
        public DateTime? End_time { get; set; }
        public string Alarm_class { get; set; }
        public string Alarm_area { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}