using AlarmMonitor.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlarmMonitor.Models.View
{
    public class AlarmDetailViewModel
    {
        public Nullable<System.DateTime> Start_time { get; set; }
        public Nullable<System.DateTime> End_time { get; set; }
        public string Duration { get; set; }
        public string Division_of_respon { get; set; }
        public string Station { get; set; }
        public string Position { get; set; }
        public string Responsible { get; set; }
        public string Reason { get; set; }
        public string Recorder { get; set; }
        public string Alarm_class { get;  set; }
        public string DurationStr
        {
            get
            {
                return Untils.ConvertSecond(Duration);
            }
        }
        public string Start_timeStr
        {
            get
            {
                if (Start_time.HasValue)
                {
                    return Start_time.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    return string.Empty;
                }

            }
        }
        public string End_timeStr
        {
            get
            {
                if (End_time.HasValue)
                {
                    return End_time.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    return string.Empty;
                }

            }
        }
    }
}