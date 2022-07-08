using OnlineShopp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopp.Controllers
{
    public class OrderStatusController : Controller
    {
        E_CommerceEntities db = new E_CommerceEntities();

        public ActionResult Add(int? id)
        {
            var oku = (from veri in db.OrderStatus where veri.OrderStatusID == id select veri).FirstOrDefault();
            OrderStatus orderStatus = new OrderStatus();
            if(oku != null)
            {
                orderStatus = oku;
            }
            else
            {
                var okuu = (from veri in db.OrderStatus select veri).OrderByDescending(p => p.OrderStatusID).FirstOrDefault();
                int idcount = okuu.OrderStatusID + 1;
                orderStatus.OrderStatusID = idcount;
            }
            return View(orderStatus);
        }

        [HttpPost]
        public ActionResult Add(OrderStatus data)
        {

            try
            {
                var oku = (from veri in db.OrderStatus where veri.OrderStatusID == data.OrderStatusID select veri).FirstOrDefault();
                if (oku != null)
                {
                    oku.OrderStatusID = data.OrderStatusID;
                    oku.OrderStatusDetails = data.OrderStatusDetails.Trim();
                    oku.OrderStatusDescription = data.OrderStatusDescription;
                    db.SaveChanges();
                }
                else
                {
                    db.OrderStatus.Add(data);
                    db.SaveChanges();
                }
                //if (result==1)
                //{
                //    TempData["result"] = 1;

                //}
                return RedirectToAction("List");
            }
            catch (Exception)
            {

                return RedirectToAction("Add");

            }
        }
        public ActionResult List()
        {
            var list=db.OrderStatus.ToList();
            return View(list);
        }

         public ActionResult Delete(int ID)
        {
            try
            {

                var deleteorderstatus = db.OrderStatus.Find(ID);
                db.OrderStatus.Remove(deleteorderstatus);
                int result=db.SaveChanges();

                if (result==1)
                {
                    TempData["result"] = 1;

                }
                return RedirectToAction("List");

            }
            catch (Exception)
            {

                return RedirectToAction("List");
            }
        }



    }
}