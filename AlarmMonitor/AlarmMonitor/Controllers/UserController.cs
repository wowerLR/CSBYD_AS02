using AlarmMonitor.Common;
using AlarmMonitor.Models.DB;
using AlarmMonitor.Models.View;
using AutoMapper;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlarmMonitor.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            using (ANDONEntities entities = new ANDONEntities())
            {
                string pwd=Untils.MD5Encrypt(model.PWD);
                user_log user = entities.user_log.Where(t => t.UID == model.UID && t.PWD == pwd).FirstOrDefault();
                if (user != null)
                {
                    user.LogInTime = DateTime.Now;
                    entities.SaveChanges();
                    Mapper.CreateMap<user_log, UserModel>(); // 配置
                    model = Mapper.Map<user_log, UserModel>(user); // 使用AutoMapper自动映射
                    SessionHelper.Add("adminInfo", model, 120);
                    return Json(true); 
                }
                else
                {
                    return Json(false);
                }
                
            }
        }
    }
}