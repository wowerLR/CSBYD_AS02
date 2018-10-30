using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlarmMonitor.Models.View;
using AlarmMonitor.Models.DB;
using System.Configuration;
using AlarmMonitor.Extension;

namespace AlarmMonitor.Controllers
{
    [UserPermission]
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            UserModel user = GetCurrentUser();
            return View(user);
        }
        public ActionResult IntFilter()
        {
            using (ANDONEntities entities = new ANDONEntities())
            {
                //AlarmClass
                List<AlarmClassView> alarmClassList = entities.AlarmClassView.ToList();
                List<string> alarmClasses = new List<string>();
                alarmClasses.Add("全部");
                foreach (AlarmClassView alarmClass in alarmClassList)
                {
                    alarmClasses.Add(alarmClass.value);
                }

                //AlarmArea
                string areasStr=ConfigurationManager.AppSettings["AlarmArea"];
                List<string> alarmAreas=new List<string>();
                foreach(string area in areasStr.Split(','))
                {
                    alarmAreas.Add(area);
                }

                //Division_of_respon
                string Division_of_respon_str = ConfigurationManager.AppSettings["Division_of_respon"];
                List<string> Division_of_respons = new List<string>();
                foreach (string d in Division_of_respon_str.Split(','))
                {
                    Division_of_respons.Add(d);
                }

                //Responsible
                string ResponsibleStr = ConfigurationManager.AppSettings["Responsible"];
                List<string> Responsibles = new List<string>();
                foreach (string r in ResponsibleStr.Split(','))
                {
                    Responsibles.Add(r);
                }


                return Json(new
                {
                    alarmClasses = alarmClasses,
                    alarmAreas = alarmAreas,
                    Division_of_respons = Division_of_respons,
                    Responsibles=Responsibles
                }
, JsonRequestBehavior.AllowGet);
            }
        }

    }
}