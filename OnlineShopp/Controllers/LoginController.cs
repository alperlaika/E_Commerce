using OnlineShopp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShopp.Controllers
{
    public class LoginController : Controller
    {
        E_CommerceEntities db = new E_CommerceEntities();

        public ActionResult Index()
        {
            return View();  
        }
        [HttpPost]
        public ActionResult Index(Login model)
        {

            var login = db.Login.Where(x => x.UserName == model.UserName && x.Password == model.Password).FirstOrDefault();
           
            if(login==null)
            {
                ViewBag.message = "Yanlış girdiniz, lütfen bilgilerinizi kontrol ediniz.";
                   
                return View("Index");   
            }
            else
            {
                if(login.IsAdmin==true)
                {
                    
                    FormsAuthentication.SetAuthCookie(login.UserName, true);
                    HttpCookie cookies = new HttpCookie("UserName", login.UserName);
                    cookies.Expires.AddDays(10);
                    Response.Cookies.Add(cookies);  

                    return RedirectToAction("Index","Home");   

                }
                else
                {
                   
                   
                    TempData["return"] = 0;
                    return View("Index");
                }
            }
        }


        public ActionResult SignOut()
        {
            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-5);
            FormsAuthentication.SignOut();


            return RedirectToAction("Index","Login");
        }
    }
}