using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DefaultConnection;
using MvcDataManage.Bll;
using MvcDataManage.Filter;
using MvcDataManage.Models;
using MvcDataManage.Utilities;
using PetaPoco;


namespace MvcDataManage.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        //PetaPoco.Database db = new PetaPoco.Database("DefaultConnection");
        public ActionResult Login()
        {
            //var test = db.Query<ww_Cabinet>("select * from ww_Cabinet").ToList();
            
            return View();
        }
        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [LoginAction(LogMessage = 1)]
        public ActionResult DoLogin(string userName, string password)
        {
           // var test = db.Query<ww_Cabinet>("select * from ww_Cabinet").ToList();
         // string  password= FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");

          //List<ww_User> list = BllBuilder.BuilUser().CheckUser(userName.Trim(), FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5"));
            List<ww_User> list = BllBuilder.BuilUser().CheckUser(userName.Trim(), FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5"));
            if (list.Count > 0)
            {
                Session["User"] = list[0].UserId;
                Session["RoseId"] = list[0].RoleId;
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Alert", "Login", new { alert = "登陆失败，密码错误，请重新输入！！！", con = "Login", ac = "Login" });
        
           
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        //[LoginAction(LogMessage = 2)]
        public ActionResult LogOff()
        {
            ww_UserOperate userOperate = new ww_UserOperate()
            {
                UserId = (int?)Session["User"],
                OperateId = 2,
                Dtime = DateTime.Now.ToString(),
                IP = GetIp.getIp(),
            };
            BllBuilder.BuilLog().AddLog(userOperate);


            Session.RemoveAll();
            return RedirectToAction("Login", "Login");
        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(int page = 1)
        {
            int take = 50; //每页显示的数量
            Page<QueryModel> mPage = BllBuilder.BuilRes().Pagination(page, take);
            ViewBag.TotalPage = mPage.TotalPages;
            ViewBag.QueryModellist = mPage.Items;
            return View();
        }       
        public JsonResult QueryPage(int id = 1)
        {
            int take = 50; //每页显示的数量
            Page<QueryModel> mPage = BllBuilder.BuilRes().Pagination(id, take);
            //ViewBag.TotalPage = mPage.TotalPages;
            //ViewBag.QueryModellist = mPage.Items;
            return Json(mPage.Items, JsonRequestBehavior.AllowGet);
        }
  
        /// <summary>
        /// 添加提示
        /// </summary>
        /// <param name="alert"></param>
        /// <returns></returns>
        public ActionResult Alert(string alert, string con = "Home", string ac = "Index")
        {
            ViewBag.con = con;
            ViewBag.ac = ac;
            ViewBag.alert = alert;
            return View();

        }

        public ActionResult TestResult()
        {
            return View();
        }

      
    }
}
