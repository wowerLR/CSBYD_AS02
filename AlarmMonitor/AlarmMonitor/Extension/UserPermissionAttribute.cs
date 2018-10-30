using AlarmMonitor.Models.View;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlarmMonitor.Extension
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class UserPermissionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserModel admin = (UserModel)SessionHelper.GetObj("adminInfo");
            if (admin == null)
            {
                filterContext.Result = new RedirectResult("/User/Login");
            }
        }
    }
}