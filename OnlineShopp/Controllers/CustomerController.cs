using OnlineShopp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopp.Controllers
{
    public class CustomerController : Controller
    {
        E_CommerceEntities db = new E_CommerceEntities();

        public ActionResult List()
        {
            var customer = db.Customers.ToList();
            return View(customer);
        }

        public ActionResult Edit(Guid ID)
        {
            //if (ID == null)
            //{

            //    return RedirectToAction("list");
            //}
            //var editcustomer = db.Customers.Where(x => x.CustomerID == ID).ToList();
            //if (editcustomer == null)
            //{
            //    TempData["sonuc"] = "0";
            //    return RedirectToAction("Edit");
            //}
            //return View(editcustomer);
            try
            {
                if (ID == null)
                {

                    return RedirectToAction("list");
                }
                var editcustomer = db.Customers.Where(x => x.CustomerID == ID).FirstOrDefault();
                if (editcustomer == null)
                {
                    TempData["sonuc"] = "0";
                    return RedirectToAction("Edit");
                }
                return View(editcustomer);
            }
            catch (Exception)
            {

                return RedirectToAction("list");
            }
           
        }

        [HttpPost]


        public ActionResult Edit(Customers data)
        {
            try
            {
                
                Customers editcustomers = db.Customers.FirstOrDefault(x => x.CustomerID == data.CustomerID);

                editcustomers.CustomerName=data.CustomerName;
                editcustomers.CustomerSurname = data.CustomerSurname; 
                editcustomers.Email = data.Email;
                editcustomers.BillingAddress = data.BillingAddress;              
                editcustomers.Password = data.Password;
                editcustomers.DeliveryAddress = data.DeliveryAddress;               
                editcustomers.CommentID = data.CommentID;


                int result = db.SaveChanges();

                if (result == 1)
                {

                    TempData["result"] = "1";
                }
                else
                {
                    TempData["result"] = "0";
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Edit");
            }

            return View();

           
        }
    }
}