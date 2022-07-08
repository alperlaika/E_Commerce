using OnlineShopp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopp.Controllers
{
    public class CargoController : Controller
    {
        E_CommerceEntities db = new E_CommerceEntities();
        public ActionResult Add(int ? ID)
        {
            CargoCompany cargocompany = new CargoCompany();

            var oku = (from veri in db.CargoCompany where veri.CargoID == ID select veri).FirstOrDefault();
           
            if (oku != null)
            {
                cargocompany = oku;
            }
            else
            {
                var okuu = (from veri in db.CargoCompany select veri).OrderByDescending(p => p.CargoID).FirstOrDefault();
                int idcount = okuu.CargoID + 1;
                cargocompany.CargoID = idcount;
            }
           
            return View(cargocompany);
        }
        [HttpPost]

        public ActionResult Add(CargoCompany data)
        {
            try
            {
                var oku = (from veri in db.CargoCompany where veri.CargoID == data.CargoID select veri).FirstOrDefault();
                if (oku != null)
                {
                    oku.CargoID = data.CargoID;
                    oku.Company = data.Company;
                    oku.CargoAddress = data.CargoAddress;
                    oku.PhoneNumber = data.PhoneNumber;
                    oku.WebSite = data.WebSite;
                    oku.Email = data.Email;
                    oku.TaxNumber = data.TaxNumber;

                    db.SaveChanges();
                }
                else
                {
                    db.CargoCompany.Add(data);
                    db.SaveChanges();
                }
                //if (result==1)
                //{
                //    TempData["result"] = 1;

                //}
                return RedirectToAction("List");
            }
            catch (Exception)
            {

                return RedirectToAction("Add");

            }

           
        }

        public ActionResult List()
        {

            var cargo = db.CargoCompany.ToList();
            return View(cargo);
        }

        public ActionResult Delete(int ID)
        {
            try
            {
                var deletecargo = db.CargoCompany.Find(ID);
                db.CargoCompany.Remove(deletecargo);
                int result=db.SaveChanges();
                if (result==1)
                {

                    TempData["result"] = 1;
                }
                return RedirectToAction("List");
            }
            catch (Exception)
            {

                return RedirectToAction("List");
            }
        }

        public ActionResult Edit(int ID)
        {
            try
            {
                CargoCompany cargocompany = db.CargoCompany.Where(x=>x.CargoID==ID).FirstOrDefault();
                return View(cargocompany);
                            
            }
            catch (Exception)
            {

                return RedirectToAction("List");
            }
        }

        [HttpPost]

        public ActionResult Edit(CargoCompany data)
        {
            try
            {
                var editcargo = db.CargoCompany.Where(x => x.CargoID == data.CargoID).FirstOrDefault();
                //var editcargo = db.CargoCompanies.Find(data.CargoID);
                editcargo.CargoAddress = data.CargoAddress;
                editcargo.Email = data.Email;
                editcargo.Company=data.Company;
                editcargo.PhoneNumber = data.PhoneNumber;
                editcargo.TaxNumber = data.TaxNumber;
                editcargo.WebSite = data.WebSite;

                int result = db.SaveChanges();
                if (result==1)
                {

                    TempData["result"] = 1;
                }
                return RedirectToAction("List");
            }
            catch (Exception)
            {

                return RedirectToAction("List");
            }
        }

    }
}