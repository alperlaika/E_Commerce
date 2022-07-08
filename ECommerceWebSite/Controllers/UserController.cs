using ECommerceWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceWebSite.Controllers
{
    public class UserController : Controller
    {
        E_CommerceEntities db = new E_CommerceEntities();

       // GET: User
       //kullanıcı adını kontrol ettik hatalı veya yanlış girildiyse indexe yönlendir.üye ıd null değilse striing olarak atıyoruz
        public ActionResult Index(Guid? id)
        {
            var customers = (from veri in db.Customers where veri.CustomerID == id select veri).FirstOrDefault();

            return View(customers);
        }
//        public ActionResult Index()
//        { 

//            if (Request.Cookies["UserName"]==null)//çerezlerden kullanıcı adını getiriyoruz
//            {
//                return RedirectToAction("Index","Home");
//    }
//            else
//            {
//                if (Session["UserID"] !=null)//customerID almamızın nedeni hesap kısmına geçisinde hangi üyeye ait olduğunu tespit için kullanıyoruz.
//                {
//                    ViewBag.UserName = Session["UserID"].ToString();//ıdsini aldık usernameye atadık.
//                                                                    //var list = db.Customers.ToList();
//                                                                    //return View(list);

//}
//                else
//{
//    TempData["result"] = 0;
//}
//            }
//            return View();
//        }
//        //get
//        public ActionResult Edit(Guid? Id) //gelen ıd ile kontrol sağlanıp ona göre güncelleme yapılacak;
//{
//    try
//    {
//        if (Id == null)
//        {
//            return RedirectToAction("Index", "Home");

//        }
//        var editcustomer = db.Customers.Where(x => x.CustomerID == Id).FirstOrDefault();
//        if (editcustomer == null)
//        {
//            return RedirectToAction("Index", "Home");

//        }
//        return View(editcustomer);

//    }
//    catch (Exception)
//    {

//        return RedirectToAction("Index", "Home");
//    }
//}
//[HttpPost]
////set
//public ActionResult Edit(Customers data)
//{
//    Customers editing = db.Customers.FirstOrDefault(x => x.CustomerID == data.CustomerID);
//    editing.CustomerName = data.CustomerName;
//    editing.Email = data.Email;
//    editing.BillingAddress = data.BillingAddress;
//    editing.CustomerSurname = data.CustomerSurname;
//    editing.DeliveryAddress = data.DeliveryAddress;
//    int result = db.SaveChanges();
//    if (result == 1)
//    {
//        TempData["result"] = "Güncelleme İşlemi Başarılı Bir Şekilde Gerçekleştirilmiştir.";
//    }
//    else
//    {
//        TempData["result"] = "Güncelleme İşlemi Sırasında Bilinmeyen Bir Hata İle Oluştu.";
//    }
//    return View(editing);

//}

    }
}