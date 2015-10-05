using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using System.Web.Security;
using System.Web.UI.WebControls;


namespace MvcDataManage.Filter
{
    public class AuthorizeExAttribute : AuthorizeAttribute

    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session != null && httpContext.Session.Count < 1)
            {
                httpContext.Response.Redirect("/Login/Login");
                
            }
            
               // return base.AuthorizeCore(httpContext);
            return true;

        }
    }
}