using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DefaultConnection;
using MvcDataManage.Bll;
using MvcDataManage.Utilities;

namespace MvcDataManage.Filter
{
    public class LoginAction : ActionFilterAttribute
    {
        public int LogMessage { get; set; }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
           // filterContext.HttpContext.Response.Write(LogMessage);
            if (filterContext.HttpContext.Session != null)
            {
                ww_UserOperate userOperate=new ww_UserOperate()
                {
                    UserId = (int?) filterContext.HttpContext.Session["User"],
                    OperateId = LogMessage,
                    Dtime=DateTime.Now.ToString(),
                    IP = GetIp.getIp(),
                };
                BllBuilder.BuilLog().AddLog(userOperate);
            }
            else
            {
                filterContext.HttpContext.Response.Redirect("/Login/Login");
            }
            base.OnResultExecuted(filterContext);
        }
    }
}