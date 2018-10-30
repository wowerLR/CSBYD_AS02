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
    public class ScheduleController : Controller
    {
        // GET: Schedule
        public ActionResult WorkSchedule()
        {
            return View();
        }
        public ActionResult GetShcedule()
        {
            List<DateScheduleViewModel> result = new List<DateScheduleViewModel>();
            using (ANDONEntities entities = new ANDONEntities())
            {
                List<ShiftSet> list = entities.Database.SqlQuery<ShiftSet>(@"SELECT [1] _1,[2] _2,[3] _3,[4] _4,[5] _5,[6] _6,[7] _7,[8] _8,[9] _9,[10] _10,[11] _11,[12] _12,[13] _13,[14] _14,[15] _15,[16] _16,[17] _17,[18] _18,[19] _19,[20] _20,shift FROM shift_set").ToList();

                ShiftSet dayS = list.Where(t => t.shift.Trim() == "1").FirstOrDefault();
                ShiftSet nightS = list.Where(t => t.shift.Trim() == "2").FirstOrDefault();


                //1
                DaySchedule day = new DaySchedule { StartTime = dayS._1.Trim() + ":" + dayS._2.Trim(), EndTime = dayS._3.Trim() + ":" + dayS._4.Trim() };
                NightSchedule night = new NightSchedule { StartTime = nightS._1.Trim() + ":" + nightS._2.Trim(), EndTime = nightS._3.Trim() + ":" + nightS._4.Trim() };
                DateScheduleViewModel model = new DateScheduleViewModel { Day = day, Night = night, Name = "开停线" };
                result.Add(model);
                //2
                day = new DaySchedule { StartTime = dayS._5.Trim() + ":" + dayS._6.Trim(), EndTime = dayS._7.Trim() + ":" + dayS._8.Trim() };
                night = new NightSchedule { StartTime = nightS._5.Trim() + ":" + nightS._6.Trim(), EndTime = nightS._7.Trim() + ":" + nightS._8.Trim() };
                model = new DateScheduleViewModel { Day = day, Night = night, Name = "休息一" };
                result.Add(model);
                //3
                day = new DaySchedule { StartTime = dayS._9.Trim() + ":" + dayS._10.Trim(), EndTime = dayS._11.Trim() + ":" + dayS._12.Trim() };
                night = new NightSchedule { StartTime = nightS._9.Trim() + ":" + nightS._10.Trim(), EndTime = nightS._11.Trim() + ":" + nightS._12.Trim() };
                model = new DateScheduleViewModel { Day = day, Night = night, Name = "休息二" };
                result.Add(model);
                //4
                day = new DaySchedule { StartTime = dayS._13.Trim() + ":" + dayS._14.Trim(), EndTime = dayS._15.Trim() + ":" + dayS._16.Trim() };
                night = new NightSchedule { StartTime = nightS._13.Trim() + ":" + nightS._14.Trim(), EndTime = nightS._15.Trim() + ":" + nightS._16.Trim() };
                model = new DateScheduleViewModel { Day = day, Night = night, Name = "休息三" };
                result.Add(model);
                //5
                day = new DaySchedule { StartTime = dayS._17.Trim() + ":" + dayS._18.Trim(), EndTime = dayS._19.Trim() + ":" + dayS._20.Trim() };
                night = new NightSchedule { StartTime = nightS._17.Trim() + ":" + nightS._18.Trim(), EndTime = nightS._19.Trim() + ":" + nightS._20.Trim() };
                model = new DateScheduleViewModel { Day = day, Night = night, Name = "休息四" };
                result.Add(model);
                //entities
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateSchedule(List<DateScheduleViewModel> list)
        {
            bool result = false;
            using (ANDONEntities entities = new ANDONEntities())
            {
                ShiftSet s1 = new ShiftSet
                {
                    shift = "1",
                    _1 = list[0].Day.StartTime.Split(':')[0],
                    _2 = list[0].Day.StartTime.Split(':')[1],
                    _3 = list[0].Day.EndTime.Split(':')[0],
                    _4 = list[0].Day.EndTime.Split(':')[1],
                    _5 = list[1].Day.StartTime.Split(':')[0],
                    _6 = list[1].Day.StartTime.Split(':')[1],
                    _7 = list[1].Day.EndTime.Split(':')[0],
                    _8 = list[1].Day.EndTime.Split(':')[1],
                    _9 = list[2].Day.StartTime.Split(':')[0],
                    _10 = list[2].Day.StartTime.Split(':')[1],
                    _11 = list[2].Day.EndTime.Split(':')[0],
                    _12 = list[2].Day.EndTime.Split(':')[1],
                    _13 = list[3].Day.StartTime.Split(':')[0],
                    _14 = list[3].Day.StartTime.Split(':')[1],
                    _15 = list[3].Day.EndTime.Split(':')[0],
                    _16 = list[3].Day.EndTime.Split(':')[1],
                    _17 = list[4].Day.StartTime.Split(':')[0],
                    _18 = list[4].Day.StartTime.Split(':')[1],
                    _19 = list[4].Day.EndTime.Split(':')[0],
                    _20 = list[4].Day.EndTime.Split(':')[1],
                };
                ShiftSet s2 = new ShiftSet
                {
                    shift = "2",
                    _1 = list[0].Night.StartTime.Split(':')[0],
                    _2 = list[0].Night.StartTime.Split(':')[1],
                    _3 = list[0].Night.EndTime.Split(':')[0],
                    _4 = list[0].Night.EndTime.Split(':')[1],
                    _5 = list[1].Night.StartTime.Split(':')[0],
                    _6 = list[1].Night.StartTime.Split(':')[1],
                    _7 = list[1].Night.EndTime.Split(':')[0],
                    _8 = list[1].Night.EndTime.Split(':')[1],
                    _9 = list[2].Night.StartTime.Split(':')[0],
                    _10 = list[2].Night.StartTime.Split(':')[1],
                    _11 = list[2].Night.EndTime.Split(':')[0],
                    _12 = list[2].Night.EndTime.Split(':')[1],
                    _13 = list[3].Night.StartTime.Split(':')[0],
                    _14 = list[3].Night.StartTime.Split(':')[1],
                    _15 = list[3].Night.EndTime.Split(':')[0],
                    _16 = list[3].Night.EndTime.Split(':')[1],
                    _17 = list[4].Night.StartTime.Split(':')[0],
                    _18 = list[4].Night.StartTime.Split(':')[1],
                    _19 = list[4].Night.EndTime.Split(':')[0],
                    _20 = list[4].Night.EndTime.Split(':')[1],
                };

                List<ShiftSet> sets = new List<ShiftSet> { s1, s2 };
                foreach(ShiftSet set in sets)
                {
                    string sql = string.Format(@"UPDATE [dbo].[shift_set]
   SET [1] = '{0}'
      ,[2] = '{1}'
      ,[3] = '{2}'
      ,[4] = '{3}'
      ,[5] = '{4}'
      ,[6] = '{5}'
      ,[7] = '{6}'
      ,[8] = '{7}'
      ,[9] = '{8}'
      ,[10] = '{9}'
      ,[11] = '{10}'
      ,[12] = '{11}'
      ,[13] = '{12}'
      ,[14] = '{13}'
      ,[15] = '{14}'
      ,[16] = '{15}'
      ,[17] = '{16}'
      ,[18] = '{17}'
      ,[19] = '{18}'
      ,[20] = '{19}'
 WHERE [shift]='{20}'", set._1, set._2, set._3, set._4, set._5, set._6, set._7, set._8, set._9, set._10, set._11, set._12, set._13, set._14, set._15, set._16, set._17, set._18, set._19, set._20, set.shift);
                    entities.Database.ExecuteSqlCommand(sql);
                }
                entities.SaveChanges();
                result = true;
            }
            return Json(result);
        }
    }
}