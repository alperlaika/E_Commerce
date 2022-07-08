using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopp.Models;
using MoreLinq;

namespace OnlineShopp.Controllers
{
    public class OrdersController : Controller
    {
        E_CommerceEntities db = new E_CommerceEntities();

        // GET: Orders
        public ActionResult OrderList()
        {
            var oku = (from veri in db.Orders
                       select new SiparisUst
                       {
                           CustomerID = veri.CustomerID,
                           DateOfSale = veri.DateOfSale,
                           OrderCargoID = veri.OrderCargoID,
                           OrderCargoName = (from kargo in db.CargoCompany where kargo.CargoID == veri.OrderCargoID select kargo.Company).FirstOrDefault(),
                           OrderDeliveryAddress = veri.OrderDeliveryAdress,
                           OrderID = veri.OrderID,
                           OrderStatusID = veri.OrderStatusID,
                           OrderStatusName = (from sipdurum in db.OrderStatus where sipdurum.OrderStatusID == veri.OrderStatusID select sipdurum.OrderStatusDescription).FirstOrDefault(),
                           OrderTotalPrice = veri.OrderTotalPrice,
                           ParcelTracking = veri.ParcelTracking,
                           PaymentMethodID = veri.PaymentMethodID,
                           KullaniciAdi = (from kuladi in db.Customers where kuladi.CustomerID == veri.CustomerID select kuladi.CustomerName).FirstOrDefault(),
                           KullaniciSoyadi = (from kuladi in db.Customers where kuladi.CustomerID == veri.CustomerID select kuladi.CustomerSurname).FirstOrDefault(),
                           PaymentMethodName = (from odemetipi in db.PaymentMethod where odemetipi.PaymentMethodID == veri.PaymentMethodID select odemetipi.PaymentMethodDescription).FirstOrDefault(),
                       }).ToList();
            return View(oku);

        }
        public ActionResult OrderEdit(int id)
        {
            ViewBag.SipDurum = (from veri in db.OrderStatus select veri).ToList();
            var oku = (from veri in db.Orders where veri.OrderID == id select veri).FirstOrDefault();
            
            return View(oku);
        }
        [HttpPost]
        public ActionResult OrderEditt(Orders orders)
        {
            var oku = (from veri in db.Orders where veri.OrderID == orders.OrderID select veri).FirstOrDefault();
            oku.OrderStatusID = orders.OrderStatusID;
            oku.ParcelTracking = orders.ParcelTracking;
            db.SaveChanges();
            return RedirectToAction("OrderList");
        }
        public ActionResult OrderDetailList(int id)
        {
            var oku = (from veri in db.OrderDetails
                       where veri.OrderDetailsOrderID == id
                       select new SiparisSatirlarim
                       {
                           OrderDetailsAmount = veri.OrderDetailsAmount,
                           OrderDetailsID = veri.OrderDetailsID,
                           OrderDetailsOrderID = veri.OrderDetailsOrderID,
                           OrderDetailsPrice = veri.OrderDetailsPrice,
                           ProductID = veri.ProductID,
                           UrunBirimFiyat = (from birimfiyat in db.Products where birimfiyat.ProductID == veri.ProductID select birimfiyat.ProductPrice).FirstOrDefault(),
                           UrunIsmi = (from urunismi in db.Products where urunismi.ProductID == veri.ProductID select urunismi.ProductName).FirstOrDefault(),
                           UrunResim = (from urunresim in db.Products where urunresim.ProductID == veri.ProductID select urunresim.ProductImage).FirstOrDefault(),
                       }).ToList();
            return View(oku);

        }

    }
}