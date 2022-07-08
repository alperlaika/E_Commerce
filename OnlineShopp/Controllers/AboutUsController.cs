using OnlineShopp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace OnlineShopp.Controllers
{
    public class AboutUsController : Controller
    {
        E_CommerceEntities db = new E_CommerceEntities();
        
        public ActionResult Index()
        {
            ViewBag.aboutus = db.AboutUs.ToList();
            return View("Index");
        }

        [HttpPost]

        public ActionResult Index(AboutUs data)
        {
            // var updateaboutus=db.AboutUs.Where(x=>x.AboutUsID==data.AboutUsID).ToList();

            var updateaboutus = (from x in db.AboutUs where x.AboutUsID == 1 select x).FirstOrDefault();

            updateaboutus.AboutUsTitle = data.AboutUsTitle;
            updateaboutus.AboutUsContent = data.AboutUsContent;
            updateaboutus.AboutUs_Keyword = data.AboutUs_Keyword;
            updateaboutus.AboutUs_Topic = data.AboutUs_Topic;

            int result = db.SaveChanges();
            if (result == 1)
            {
                TempData["result"] = 1;

            }

            return RedirectToAction("Index");

        }
    }
}