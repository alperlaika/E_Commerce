using OnlineShopp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopp.Controllers
{
    public class DeliveryController : Controller
    {
        E_CommerceEntities db = new E_CommerceEntities();
        public ActionResult Index()
        {
            ViewBag.delivery=db.DeliveryInformation.ToList();

            return View("Index");
        }
    }

}