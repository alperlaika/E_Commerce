using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceWebSite.Models;
using MoreLinq;

namespace ECommerceWebSite.Controllers
{
    public class HomeController : Controller
    {
        E_CommerceEntities db = new E_CommerceEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Category()
        {
            var list = (from veri in db.Categories select veri).ToList();

            return PartialView(list);

        }
        public ActionResult Arama(string id)
        {
            var oku = (from veri in db.Products where veri.ProductName.Contains(id) select veri).ToList();
            return View(oku);
        }
        public PartialViewResult ProductList()
        {
            
            var list = (from veri in db.Products select veri).ToList();
            return PartialView(list);
        }

        public PartialViewResult Slayt()
        {
            E_CommerceEntities db = new E_CommerceEntities();
            var list = (from veri in db.Products select veri).ToList();


            return PartialView(list);
        }

        public ActionResult Cart()
        {
            var kullaniciid = Session["UserID"];
            string kullaniciidstring = kullaniciid.ToString();
            Guid kullaniciidguid = Guid.Parse(kullaniciidstring);
            ViewBag.KargoFirmalari = (from veri in db.CargoCompany select veri).ToList();
            ViewBag.OdemeTipi = (from veri in db.PaymentMethod where veri.PaymentMethodID == 2 select veri).ToList();
            var oku = (from veri in db.ShoppingCart
                       where veri.ShoppingCartCustomerID == kullaniciidguid &&veri.ShoppingCartStatus == 0
                       select new SepetUrunOku
                       {
                          ShoppingCartProductID = veri.ShoppingCartProductID,
                          ShoppingCartAmount = veri.ShoppingCartAmount,
                          ShoppingCartCustormerID = veri.ShoppingCartCustomerID,
                          ShoppingCartTotal = veri.ShoppingCartTotal,
                          UrunResim = (from urunresim in db.Products where urunresim.ProductID == veri.ShoppingCartProductID select urunresim.ProductImage).FirstOrDefault(),
                          UrunBirimFiyat = (from birimfiyat in db.Products where birimfiyat.ProductID == veri.ShoppingCartProductID select birimfiyat.ProductPrice).FirstOrDefault(),
                          UrunIsmi = (from urunisim in db.Products where urunisim.ProductID == veri.ShoppingCartProductID select urunisim.ProductName).FirstOrDefault(),
                          ShoppingCartID = veri.ShoppingCartID,
                       }).ToList();
            return View(oku);
        }

        public JsonResult AlisverisiTamamla(int OdemeTipi,int KargoFirmasi, string Adres)
        {
            var kullaniciid = Session["UserID"];
            string kullaniciidstring = kullaniciid.ToString();
            Guid kullaniciidguid = Guid.Parse(kullaniciidstring);
            var oku = (from veri in db.ShoppingCart
                       where veri.ShoppingCartCustomerID == kullaniciidguid && veri.ShoppingCartStatus == 0
                       select veri).ToList();
            double? totalprice = 0;
            foreach(var item in oku)
            {
                totalprice = totalprice + item.ShoppingCartTotal; 
            }
            Orders orders = new Orders();
            var sipustid = (from veri in db.Orders select veri).OrderByDescending(p => p.OrderID).FirstOrDefault();

            orders.OrderID = sipustid.OrderID + 1;
            orders.OrderCargoID = KargoFirmasi;
            orders.CustomerID = kullaniciidguid;
            orders.OrderStatusID = 3;
            orders.OrderTotalPrice = totalprice;
            orders.PaymentMethodID = OdemeTipi;
            orders.OrderDeliveryAdress = Adres;
            orders.DateOfSale = DateTime.Now;
            db.Orders.Add(orders);
            db.SaveChanges();

            foreach(var items in oku)
            {
                OrderDetails orderDetails = new OrderDetails();
                orderDetails.OrderDetailsPrice = items.ShoppingCartTotal;
                orderDetails.OrderDetailsOrderID = orders.OrderID;
                orderDetails.ProductID = items.ShoppingCartProductID;
                orderDetails.OrderDetailsAmount = items.ShoppingCartAmount;
                db.OrderDetails.Add(orderDetails);
                db.SaveChanges();

                

                var sepetsatir = db.ShoppingCart.Find(items.ShoppingCartID);
                sepetsatir.ShoppingCartStatus = 1;
                db.SaveChanges();
            }
            return Json(true);
        }

        public ActionResult Siparislerim()
        {
            var kullaniciid = Session["UserID"];
            string kullaniciidstring = kullaniciid.ToString();
            Guid kullaniciidguid = Guid.Parse(kullaniciidstring);

            var oku = (from veri in db.Orders where veri.CustomerID == kullaniciidguid select new SiparisUst
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
                PaymentMethodName = (from odemetipi in db.PaymentMethod where odemetipi.PaymentMethodID == veri.PaymentMethodID select odemetipi.PaymentMethodDescription).FirstOrDefault(),

            }).ToList();
            return View(oku);
        }
        public ActionResult SiparisSatirlarim(int id)
        {
            var oku = (from veri in db.OrderDetails where veri.OrderDetailsOrderID == id select new SiparisSatirlarim
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
