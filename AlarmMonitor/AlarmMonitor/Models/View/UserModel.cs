using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlarmMonitor.Models.View
{
    public class UserModel
    {
        public int ID { get; set; }
        public string UName { get; set; }
        public string UID { get; set; }
        public string PWD { get; set; }
        public DateTime LogInTime { get; set; }
    }
}