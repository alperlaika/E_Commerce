using OnlineShopp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopp.Controllers
{
    public class CategoriesController : Controller
    {

        E_CommerceEntities db = new E_CommerceEntities();
        // GET: Categories
        public ActionResult Add(Guid? id)
        {
            Categories category = new Categories();

            if (id != null)
            {
                var oku = (from veri in db.Categories where veri.CategoryID == id select veri).FirstOrDefault();
                category = oku;
            }
            else
            {
                category.CategoryID = Guid.NewGuid();
            }
            return View(category);
        }
        [HttpPost]

        
        public ActionResult Add(Categories model) 
        {

            try
            {
                var oku = (from veri in db.Categories where veri.CategoryID == model.CategoryID select veri).FirstOrDefault();
                if(oku == null)
                {
                    db.Categories.Add(model);
                }
                else
                {
                    oku.CategoryID = model.CategoryID;
                    oku.TopCategoryID = model.TopCategoryID;
                    oku.Description = model.Description;
                    db.Categories.Append(oku);
                }
                int result=db.SaveChanges();    

                if(result==0)
                {
                    TempData["result"] = "1";
                }
                else
                {
                    TempData["result"] = "0";

                }
                return RedirectToAction("Add");
            }
            catch (Exception e)
            {
                return RedirectToAction("Add");

            }

        }

        public ActionResult List()
        {
            try
            {



                var list = (from veri in db.Categories
                            select new OnlineShopp.Models.UserModel.CategoriesWithTopName
                            {
                                CategoryAdi = veri.Description,
                                CategoryID = veri.CategoryID,
                                TopCategoryID = veri.TopCategoryID,
                                TopCategoryAdi = (from ustkat in db.Categories where ustkat.CategoryID == veri.TopCategoryID select ustkat.Description).FirstOrDefault()
                            }).ToList();
                return View(list);  
            }
            catch (Exception)
            {
                return RedirectToAction("Add");

            }
               
        }
        public PartialViewResult categoryList()
        {

            var list = db.Categories.ToList();
            return PartialView(list);
        }
        public JsonResult CategoryDelete(Guid? id)
        {
            try
            {
                var oku = (from veri in db.Categories where veri.CategoryID == id select veri).FirstOrDefault();
                if(oku != null)
                {
                    var urunoku = (from veri in db.Products where veri.CategoryID == oku.CategoryID select veri).FirstOrDefault();
                    
                    if(urunoku == null)
                    {
                        var altkategoriler = (from veri in db.Categories where veri.TopCategoryID == oku.CategoryID select veri).FirstOrDefault();
                        if (altkategoriler == null)
                        {
                            db.Categories.Remove(oku);
                            db.SaveChanges();
                            return Json("Kategori başarıyla silinmiştir.",JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json("Bu kategoriye bağlı alt kategori mevcut.",JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        return Json("Bu kategoriye bağlı ürünler mevcut.",JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("Böyle bir kategori bulunmamaktadır.", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult SubCategory()
        {
            var category=db.Categories.ToList().Select(x=> new
            SelectListItem
            {
                Selected = false,
                Text=x.Description,
                Value=x.CategoryID.ToString()
            }).ToList();

            ViewBag.category = category;

            Categories categories = new Categories();
            categories.CategoryID = Guid.NewGuid();
            return View(categories);

        }

        [HttpPost]
        public ActionResult SubCategory(Categories model)
        {

            try
            {
                db.Categories.Add(model);
                db.SaveChanges();   

                return RedirectToAction("Add");
            }
            catch (Exception)
            {

                return View("Add");
            }
        }
    }
}