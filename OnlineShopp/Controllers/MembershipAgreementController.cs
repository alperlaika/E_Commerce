using OnlineShopp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopp.Controllers
{
    public class MembershipAgreementController : Controller
    {
        E_CommerceEntities db = new E_CommerceEntities();

        public ActionResult Index()
        {
            ViewBag.membershipagreements = db.MembershipAgreement.ToList();

            return View("Index");
        }
        [HttpPost]

        public ActionResult Index(MembershipAgreement data)
        {
            var updatemembership = db.MembershipAgreement.Where(x => x.MembershipAgreementID == 1).FirstOrDefault();

            updatemembership.Member_Keyword = data.Member_Keyword;
            updatemembership.MembershipAgreementContent = data.MembershipAgreementContent;
            updatemembership.Member_Topic = data.Member_Topic;
            updatemembership.MembershipAgreementTitle = data.MembershipAgreementTitle;

            int result = db.SaveChanges();
            if (result == 1)
            {
                TempData["result"] = 1;

            }
            return RedirectToAction("Index");
        }
    }
}