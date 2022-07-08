using OnlineShopp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopp.Controllers
{
    [UseAuthorize]
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {

            if(Request.Cookies["UserName"]==null)
            {
                return RedirectToAction("Index","Login");   

            }
            else
            {
                ViewBag.UserName = Request.Cookies["UserName"].Value;
            }


            return View();
        }

        public PartialViewResult situation()
        {

            return PartialView(new Situation().situationmmodel());
        }
    }
}