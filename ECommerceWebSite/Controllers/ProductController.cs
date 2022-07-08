using ECommerceWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceWebSite.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        E_CommerceEntities db = new E_CommerceEntities();
        
        public ActionResult ProductDetail(Guid? id)
        {
            Products products = new Products();
            var urun = (from veri in db.Products where veri.ProductID == id select veri).FirstOrDefault();
            
            if (urun == null)
            {
               return RedirectToAction("Index","Home");
            }
            else
            {
                products = urun;
            }

            return View(products);
        }

        //[HttpPost]//post deme nednimiz sepete ekle dediğimizde ürünün değerlerin, bilgilrini getirecek
        public ActionResult ProductList(Guid? id)
        {
            var oku = (from veri in db.Products where veri.CategoryID == id select veri).ToList();
            var altkategorioku = (from veri in db.Categories where veri.TopCategoryID == id select veri).ToList();
            if(altkategorioku.Count > 0)
            {
                foreach(var item in altkategorioku)
                {
                    var okuu = (from veri in db.Products where veri.CategoryID == item.CategoryID select veri).ToList();
                    oku = oku.Union(okuu).ToList();

                }
            }
            return View(oku);
        }

        public JsonResult SepeteEkle(Guid ProductID,int Miktar,double Fiyat)
        {
            var kullaniciid = Session["UserID"];
            string kullaniciidstring = kullaniciid.ToString();
            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart.ShoppingCartProductID = ProductID;
            shoppingCart.ShoppingCartTotal = Miktar * Fiyat;
            shoppingCart.ShoppingCartAmount = Miktar;
            shoppingCart.ShoppingCartCustomerID = Guid.Parse(kullaniciidstring);
            shoppingCart.ShoppingCartStatus = 0;
            db.ShoppingCart.Add(shoppingCart);

            //Here, all the changes (additions) made in the basket have been added to the database.
            db.SaveChanges();
            return Json(true);
        }

        public JsonResult SepetSatirSil(int SatirID)
        {
            var oku = (from veri in db.ShoppingCart where veri.ShoppingCartID == SatirID select veri).FirstOrDefault();
            if(oku != null)
            {
                db.ShoppingCart.Remove(oku);
                db.SaveChanges();
            }
            return Json(true);
        }

    }
}