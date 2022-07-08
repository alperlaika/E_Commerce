using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopp.Models
{
    public class UseAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            if(httpContext.Request.Cookies["UserName"]!=null)
            {
                return true;
            }
            else
            {
                httpContext.Response.Redirect("/login/Index");

                return false;

            }
        }

    }
}