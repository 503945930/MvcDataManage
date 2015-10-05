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
using PetaPoco;

namespace MvcDataManage.Controllers
{
     [AuthorizeEx]
    public class UsersController : Controller
    {
        //
        // GET: /Users/
        /// <summary>
        /// 用户管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.UserList = BllBuilder.BuilUser().QueryAllUser();
            return View();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifyPass(int id=0)
        {
            if (id<=0)
            {
                int id1 = Convert.ToInt32(Session["User"]);
                ww_User list = BllBuilder.BuilUser().QueryById(id1);
                return View(list);
            }
            else
            {
               // int id1 = Convert.ToInt32(Session["User"]);
                ww_User list = BllBuilder.BuilUser().QueryById(id);
                return View(list);
            }
            
        }
        /// <summary>
        /// 确认修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [LoginAction(LogMessage = 6)]
        public ActionResult DoModifyPass(ww_User model)
        {
            model.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password, "MD5");
            if (BllBuilder.BuilUser().UpdateUser(model))
            {
                return RedirectToAction("Alert", "Login", new { alert = "修改成功！！！" });
            }
            return RedirectToAction("Alert", "Login", new { alert = "修改失败，请重新修改！！！" });
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <returns></returns>
         [HttpPost]
        public string ValidateUser(string userName)
        {
            List<ww_User> list = BllBuilder.BuilUser().CheckUser(userName);
            if (list.Count>0)
            {
                
                return "true";
            }
            return "false";
        }
        /// <summary>
        /// 注册到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
         [HttpPost]
         [LoginAction(LogMessage = 5)]
        public ActionResult Register(ww_User model)
        {
            model.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password, "MD5");
            if (BllBuilder.BuilUser().AddUser(model))
            {
                return RedirectToAction("Alert", "Login", new { alert = "注册成功！！！" });
            }
            return RedirectToAction("Alert", "Login", new { alert = "注册失败，请重新注册！！！" });
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [LoginAction(LogMessage = 12)]
         public ActionResult DelUser(int id)
        {
            if (BllBuilder.BuilUser().DelUser(id))
            {
                return RedirectToAction("Alert", "Login", new { alert = "删除成功！！！" });
            }
            return RedirectToAction("Alert", "Login", new { alert = "删除失败，请重新删除！！！" });
        }
         /// <summary>
         /// 操作日志
         /// </summary>
         /// <returns></returns>
        public ActionResult Log(int page = 1)
         {
             int take = 20; //每页显示的数量
             Page<UserOperateModel> mPage = BllBuilder.BuilLog().SelectLog(page, take);
             ViewBag.TotalPage = mPage.TotalPages;
             ViewBag.UserOperateModel = mPage.Items;
             return View();
         }
        public JsonResult QueryPage(int id = 1)
        {
            int take = 20; //每页显示的数量
            Page<UserOperateModel> mPage = BllBuilder.BuilLog().SelectLog(id, take);
            //ViewBag.TotalPage = mPage.TotalPages;
            //ViewBag.QueryModellist = mPage.Items;
            return Json(mPage.Items, JsonRequestBehavior.AllowGet);
        }
  
    }
}
