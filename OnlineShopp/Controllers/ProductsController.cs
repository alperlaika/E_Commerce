using OnlineShopp.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineShopp.Controllers
{
    public class ProductsController : Controller
    {
        string resimyolu = "";

        // GET: Products

        E_CommerceEntities db = new E_CommerceEntities();
        public ActionResult Add(Guid? id)
        {
            Products products = new Products();

            var urun = (from veri in db.Products where veri.ProductID == id select veri).FirstOrDefault();
            if(urun == null)
            {
                products.ProductID = Guid.NewGuid();
            }
            else
            {
                products = urun;
            }
            ViewBag.category = (from veri in db.Categories select veri).ToList();


            return View(products);
        }

        [HttpPost]

        public ActionResult ProductAdd(Products model)
        {
            try
            {
                var urun = (from veri in db.Products where veri.ProductID == model.ProductID select veri).FirstOrDefault();
                if(urun == null)
                {
                    
                    model.ProductAddedDate = DateTime.Today;
                    db.Products.Add(model);
                }
                
                else
                {
                    urun.ProductNumber = model.ProductNumber;
                    urun.ProductName = model.ProductName;
                    urun.ProductDetails = model.ProductDetails;
                    urun.ProductImage = model.ProductImage;
                    urun.ProductPrice = model.ProductPrice;
                    urun.ProductStock_Code = model.ProductStock_Code;
                    urun.CategoryID = model.CategoryID;
                }
                //foreach (var item in model.ProductVariant)
                //{
                //    item.ProductID = variants.ProductID;
                //    db.ProductVariant.Add(item);
                //}
                int result = db.SaveChanges();

                if (result == 1)
                {
                    TempData["result"] = 1;

                }
                else
                {
                    TempData["result"] = 0;

                }
                return RedirectToAction("List");

            }
            catch (Exception)
            {

                return RedirectToAction("List");
            }
        }

        public ActionResult Category(Guid CategoryID)
        {
            var list = db.Categories.Where(x => x.CategoryID == CategoryID).Select(x => new
            {
                x.CategoryID,
                x.Description
            }).ToList();


            JavaScriptSerializer javaScript = new JavaScriptSerializer();
            string result = javaScript.Serialize(list);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveUploadedFile()
        {
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    fName = "/Media/Images/" + file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        resimyolu = "";
                        var originalDirectory = new DirectoryInfo(string.Format("{0}Media\\Images", Server.MapPath(@"\")));
                        var originalDirectory2 = new DirectoryInfo(string.Format("C:\\Users\\Laika\\source\\repos\\OnlineShopp\\ECommerceWebSite\\Media\\Images"));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");
                        string pathString2 = System.IO.Path.Combine(originalDirectory2.ToString(), "imagepath");

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);
                        bool isExists2 = System.IO.Directory.Exists(pathString2);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        if (!isExists2)
                            System.IO.Directory.CreateDirectory(pathString2);

                        var path = string.Format("{0}\\{1}", originalDirectory, file.FileName);
                        var path2 = string.Format("{0}\\{1}", originalDirectory2, file.FileName);
                        file.SaveAs(path);
                        file.SaveAs(path2);
                        resimyolu = "/Media/Images/" + file.FileName;
 
                    }

                }
                return Json(resimyolu);

            }
            catch (Exception ex)
            {
                return Json(ex.Message);

            }
        }
        public ActionResult List()
        {
            try
            {
                var list = db.Products.ToList();
                return View(list);

            }
            catch (Exception)
            {

                return RedirectToAction("List");
            }
        }

        public ActionResult ProductDelete(Guid ID)
        {
            try
            {

                var deletecategory = db.Products.Find(ID);
                db.Products.Remove(deletecategory);
                int result = db.SaveChanges();

                if (result == 1)
                {

                    TempData["result"] = "1";
                }
                //else
                //{
                //    TempData["result"] = "0";
                //}
                //return RedirectToAction("List");
            }
            catch (Exception)
            {

                return View("List");
            }
            return RedirectToAction("List");
        }

    }
 }
