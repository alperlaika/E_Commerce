using OnlineShopp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopp.Controllers
{
    public class PrivacyPolicyController : Controller
    {
        E_CommerceEntities db = new E_CommerceEntities();
        public ActionResult Index()
        {

            ViewBag.privacypolicy = db.PrivacyPolicy.ToList();

            return View("Index");
        }

        
        [HttpPost]

        public ActionResult Index(PrivacyPolicy data)
        {
            var updateprivacypolicy=db.PrivacyPolicy.Where(x=> x.PrivacyPolicyID==1).FirstOrDefault();

            updateprivacypolicy.Privacy_Keyword = data.Privacy_Keyword;
            updateprivacypolicy.PrivacyPolicyContent = data.PrivacyPolicyContent;
            updateprivacypolicy.Privacy_Topic = data.Privacy_Topic;
            updateprivacypolicy.PrivacyPolicyTitle=data.PrivacyPolicyTitle;

            int result = db.SaveChanges();
            if (result==1)
            {
                TempData["result"] = 1;

            }
            return RedirectToAction("Index");
        }
    }

}