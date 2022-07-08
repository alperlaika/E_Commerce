using ECommerceWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ECommerceWebSite.Controllers
{
    public class LoginController : Controller
    {

        E_CommerceEntities db = new E_CommerceEntities();

        public ActionResult MemberRegistration(Guid? id)
        {
            Customers customers = new Customers();

            if (id != null)
            {
                var oku = (from veri in db.Customers where veri.CustomerID == id select veri).FirstOrDefault();
                customers = oku;
            }
            else
            {
                customers.CustomerID = Guid.NewGuid();
            }
            return View(customers);
        }
        [HttpPost]
        //SET: Login
        public ActionResult MemberRegistration(Customers data)
        {
            try
            {
                data.BirthDate = DateTime.Now;
                db.Customers.Add(data);
                db.SaveChanges();
                return RedirectToAction("MemberRegistration");
            }
            catch (Exception)
            {

                return RedirectToAction("MemberRegistration");

            }
        }


        public JsonResult Login(string email,string password)
        {
            Customers login = db.Customers.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
            if (login == null)
            {
                ViewBag.hata = "Kullanıcı Adı veya Şifre Hatalı";
                return Json(false);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(login.Email, true);
                HttpCookie cookies = new HttpCookie("UserName", login.CustomerName);
                cookies.Expires.AddDays(5);
                Response.Cookies.Add(cookies);
                Session.Add("UserID", login.CustomerID);
                Session.Add("user", login.CustomerName);
                return Json(true);
            }
        }
       
     
        public JsonResult SignOut()
        {
            try
            {
                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-5);
                Session.Remove("customerID");
                Session.Remove("user");

                FormsAuthentication.SignOut();
                return Json(true);
            }
            catch (Exception e)
            {

                return Json(false);
            }

          
        }
    }
}