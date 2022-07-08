using OnlineShopp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopp.Controllers
{
    public class PaymentMethodController : Controller
    {
        E_CommerceEntities db = new E_CommerceEntities();
        public ActionResult Add(int? id)
        {
            var oku = (from veri in db.PaymentMethod where veri.PaymentMethodID == id select veri).FirstOrDefault();
            PaymentMethod paymentmethod = new PaymentMethod();
            if (oku != null)
            {
                paymentmethod = oku;
            }
            else
            {
                var okuu = (from veri in db.PaymentMethod select veri).OrderByDescending(p => p.PaymentMethodID).FirstOrDefault();
                int idcount = okuu.PaymentMethodID + 1;
                paymentmethod.PaymentMethodID = idcount;
            }
            return View(paymentmethod);
        }

        [HttpPost]
        public ActionResult Add(PaymentMethod data)
        {

            try
            {
                var oku = (from veri in db.PaymentMethod where veri.PaymentMethodID == data.PaymentMethodID select veri).FirstOrDefault();
                if (oku != null)
                {
                    oku.PaymentMethodID = data.PaymentMethodID;
                    oku.PaymentMethodDetails = data.PaymentMethodDetails.Trim();
                    oku.PaymentMethodDescription = data.PaymentMethodDescription;
                    db.SaveChanges();
                }
                else
                {
                    db.PaymentMethod.Add(data);
                    db.SaveChanges();
                }
                //if (result==1)
                //{
                //    TempData["result"] = 1;

                //}
                return RedirectToAction("Listt");
            }
            catch (Exception)
            {

                return RedirectToAction("Add");

            }
        }
      
        [HttpPost]



        public ActionResult List()
        {
            var list = db.PaymentMethod.ToList();
            return View(list);
        }

        public ActionResult Delete(int ID)
        {
            try
            {

                var deletepaymentmethod = db.PaymentMethod.Find(ID);
                db.PaymentMethod.Remove(deletepaymentmethod);
                int result=db.SaveChanges();

                if (result==1)
                {
                    TempData["result"] = 1;

                }
                return RedirectToAction("Listt");

            }
            catch (Exception)
            {

                return RedirectToAction("Listt");
            }
        }



     }
}