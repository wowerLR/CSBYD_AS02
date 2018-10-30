using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlarmMonitor.Models.View
{
    public class FilterForAlarmDetail
    {
        public DateTime? Start_time { get; set; }
        public DateTime? End_time { get; set; }
        public string Alarm_area { get; set; }
        public string Division_of_respon { get; set; }
        public string Responsible { get; set; }
        public string Recorder { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}