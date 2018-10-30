using AlarmMonitor.Common;
using AlarmMonitor.Extension;
using AlarmMonitor.Models.DB;
using AlarmMonitor.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlarmMonitor.Controllers
{
    [UserPermission]
    public class AlarmController : BaseController
    {
        // GET: Alarm
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AlarmEdit()
        {
            return View();
            //return new JsonResult { Data = null, JsonRequestBehavior=JsonRequestBehavior.AllowGet };
        }
        public ActionResult AlarmDetail()
        {
            return View();
        }
        public ActionResult GetAlarmEditList(FilterForAlarmEdit filter)
        {
            using (ANDONEntities entities = new ANDONEntities())
            {
                var alarm_edits = entities.Alarm_edit.Where(t => t.Delete_flag == false); ;
                if (filter.Start_time != null && filter.End_time != null)
                {
                    DateTime startTime = Convert.ToDateTime(filter.Start_time);
                    DateTime endTime = Convert.ToDateTime(filter.End_time);
                    alarm_edits = alarm_edits.Where(t => t.Start_time >= startTime && t.End_time <= endTime);
                }
                if (!String.IsNullOrEmpty(filter.Alarm_class) && !(filter.Alarm_class == "全部"))
                {
                    alarm_edits = alarm_edits.Where(t => t.Alarm_class == filter.Alarm_class);
                }
                if (!String.IsNullOrEmpty(filter.Alarm_area) && !(filter.Alarm_area == "全部"))
                {
                    alarm_edits = alarm_edits.Where(t => t.Alarm_area == filter.Alarm_area);
                }
                int totalCount = alarm_edits.Count();
                alarm_edits = alarm_edits.OrderBy(t => t.Start_time).Skip(filter.PageSize * (filter.PageIndex - 1)).Take(filter.PageSize);
                var result = from a in alarm_edits
                           select new ALarmEditViewModel
                           {
                             ID = a.ID,
                             Start_time=a.Start_time,
                             End_time=a.End_time,
                             Station=a.Station,
                             Alarm_area=a.Alarm_area,
                             Alarm_class=a.Alarm_class,
                             Division_of_respon=a.Division_of_respon,
                             Position=a.Position,
                             Reason=a.Reason,
                             Responsible=a.Responsible,
                             Duration=a.Duration
                           };
                                                      ;
                return Json(new
                {
                    iTotalDisplayRecords = totalCount,
                    aaData = result.ToList()
                }
                 , JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAlarmDetailList(FilterForAlarmDetail filter)
        {
            using (ANDONEntities entities = new ANDONEntities())
            {
                var alarm_edits = entities.Alarm_edit.Where(t => t.Delete_flag == false);
                if (filter.Start_time != null && filter.End_time != null)
                {
                    DateTime startTime = Convert.ToDateTime(filter.Start_time);
                    DateTime endTime = Convert.ToDateTime(filter.End_time);
                    alarm_edits = alarm_edits.Where(t => t.Start_time >= startTime && t.End_time <= endTime);
                }
                if (!String.IsNullOrEmpty(filter.Alarm_area))
                {
                    alarm_edits = alarm_edits.Where(t => t.Alarm_area == filter.Alarm_area);
                }
                if (!String.IsNullOrEmpty(filter.Division_of_respon))
                {
                    alarm_edits = alarm_edits.Where(t => t.Division_of_respon == filter.Division_of_respon);
                }
                if (!String.IsNullOrEmpty(filter.Responsible))
                {
                    alarm_edits = alarm_edits.Where(t => t.Responsible == filter.Responsible);
                }
                if (!String.IsNullOrEmpty(filter.Recorder))
                {
                    alarm_edits = alarm_edits.Where(t => t.Recorder.Contains(filter.Recorder));
                }

                int totalCount = alarm_edits.Count();
                alarm_edits = alarm_edits.OrderBy(t => t.Start_time).Skip(filter.PageSize * (filter.PageIndex - 1)).Take(filter.PageSize);
                var result = from a in alarm_edits
                             select new AlarmDetailViewModel
                             {
                                 Start_time = a.Start_time,
                                 End_time = a.End_time,
                                 Station = a.Station,
                                 Reason = a.Reason,
                                 Responsible = a.Responsible,
                                 Duration = a.Duration,
                                 Division_of_respon=a.Division_of_respon,
                                 Position=a.Position,
                                 Recorder=a.Recorder,
                                 Alarm_class = a.Alarm_class
                             };
                return Json(new
                {
                    iTotalDisplayRecords = totalCount,
                    aaData = result.ToList()
                }
                 , JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AlarmEditSaveChange(ALarmEditViewModel model)
        {
            using (ANDONEntities entities = new ANDONEntities())
            {
                bool result = false;
                try
                {
                    Alarm_edit alarm = entities.Alarm_edit.Where(t => t.ID == model.ID).FirstOrDefault();
                    alarm.Start_time = model.Start_time;
                    alarm.End_time = model.End_time;
                    alarm.Reason = model.Reason;
                    alarm.Responsible = model.Responsible;
                    alarm.Division_of_respon = model.Division_of_respon;
                    UserModel user = GetCurrentUser();
                    alarm.Recorder = user.UName + "(" + user.UID + ")";

                    DateTime startTime, endTime;
                    if (model.Start_time.HasValue && model.End_time.HasValue)
                    {
                        startTime = model.Start_time.Value;
                        endTime = model.End_time.Value;
                        TimeSpan ts = endTime - startTime;
                        alarm.Duration = ts.TotalSeconds.ToString();
                    }
                    entities.SaveChanges();
                    result = true;
                }
                catch(Exception e)
                {
                    
                }

                return Json(result);
            }
        }
        public ActionResult DeleteAlarmEidt(int ID)
        {
            bool result = false;
            using (ANDONEntities entities = new ANDONEntities())
            {
                Alarm_edit a = entities.Alarm_edit.Find(ID);
                a.Delete_flag = true;
                entities.SaveChanges();
                result = true;
            }
            return Json(result);
        }
    }
}