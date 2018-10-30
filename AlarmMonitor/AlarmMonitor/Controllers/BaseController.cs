using AlarmMonitor.Models.View;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlarmMonitor.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnException(ExceptionContext filterContext)
        {
            //AdminInfoModel user = (AdminInfoModel)SessionHelper.GetObj("adminInfo");
            //if (user != null)
            //{
            //    string msg = filterContext.Exception.Message;
            //    if (filterContext.Exception.InnerException != null)
            //    {
            //        msg += filterContext.Exception.InnerException.Message;
            //    }
            //    //LogModel model = new LogModel
            //    //{
            //    //    logContent = filterContext.RouteData.Values["controller"].ToString() + "/" + filterContext.RouteData.Values["action"].ToString(),
            //    //    logDetail = msg,
            //    //    logType = "Error",
            //    //    ctime = DateTime.Now,
            //    //    cId = user.userId,
            //    //    cName = user.userName
            //    //};
            //    //ILogService service = new LogService();
            //    //service.Insert(model);
            //}
        }
        protected UserModel GetCurrentUser()
        {
            UserModel model = (UserModel)SessionHelper.GetObj("adminInfo");
            return model;
        }
    }
}