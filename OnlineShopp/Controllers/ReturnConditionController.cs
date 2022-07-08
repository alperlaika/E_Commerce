using OnlineShopp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopp.Controllers
{
    public class ReturnConditionController : Controller
    {
        E_CommerceEntities db = new E_CommerceEntities();

        public ActionResult Index()
        {
            ViewBag.returncondition = db.ReturnConditions.ToList();

            return View("Index");
        }
        [HttpPost]

        public ActionResult Index(ReturnConditions data)
        {
            var updatereturnpolicy = (from x in db.ReturnConditions where x.ReturnConditionsID == 1 select x).FirstOrDefault();
            //ReturnCondition updatereturnpolicy = db.ReturnConditions.Where(x => x.ReturnConditionsID == 1).ToList();

            updatereturnpolicy.ReturnConditionsTitle = data.ReturnConditionsTitle;
            updatereturnpolicy.ReturnConditionsContent = data.ReturnConditionsContent;
            updatereturnpolicy.Return_Keyword=data.Return_Keyword;
            updatereturnpolicy.Return_Topic=data.Return_Topic;

            int result = db.SaveChanges();
            if (result == 1)
            {
                TempData["result"] = 1;

            }

            return RedirectToAction("Index");

        }
    }
}