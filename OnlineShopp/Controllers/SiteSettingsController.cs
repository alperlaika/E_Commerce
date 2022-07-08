using OnlineShopp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopp.Controllers
{
    public class SiteSettingsController : Controller
    {
        E_CommerceEntities db = new E_CommerceEntities();
        public ActionResult Add()
        {
            ViewBag.mailsettings = db.MailSetting.ToList();
            ViewBag.socialmedias = db.SocialMedia.ToList();
            ViewBag.seo = db.Seos.ToList();
            ViewBag.contact = db.SiteSettings.ToList();



            return View("Add");
        }
        [HttpPost]
        public ActionResult seo(Seos data)
        {
            Seos updateseo = db.Seos.Where(x => x.SeosID == 1).FirstOrDefault();
            updateseo.SeosTitle = data.SeosTitle;
            updateseo.Seos_Keyword = data.Seos_Keyword;
            updateseo.SeosMap = data.SeosMap;
            updateseo.Seos_Topic = data.Seos_Topic;
            int result = db.SaveChanges();
            if (result == 1)
            {
                TempData["result"] = 1;

            }

            return RedirectToAction("Add");
        }
        [HttpPost]
        public ActionResult socialmedia(SocialMedia data)
        {
            SocialMedia updatesocialMedia = db.SocialMedia.Where(x => x.SocialMediaID == 1).FirstOrDefault();

            updatesocialMedia.facebook = data.facebook;
            updatesocialMedia.instagram = data.instagram;
            updatesocialMedia.twitter = data.twitter;
            updatesocialMedia.YouTube = data.YouTube;
            int result = db.SaveChanges();
            if (result == 1)
            {
                TempData["result"] = 1;

            }

            return RedirectToAction("Add");
        }


        [HttpPost]
        public ActionResult contact(SiteSettings data)
        {
            SiteSettings updatesitesetting = db.SiteSettings.Where(x => x.SiteSettingsID == 1).FirstOrDefault();

            updatesitesetting.Address = data.Address;
            updatesitesetting.Email = data.Email;
            updatesitesetting.PhoneNumber = data.PhoneNumber;
            updatesitesetting.WhatsApp = data.WhatsApp;
            int result = db.SaveChanges();
            if (result == 1)
            {
                TempData["result"] = 1;

            }

            return RedirectToAction("Add");
        }



        [HttpPost]
        public ActionResult mailsettings(MailSetting data)
        {
            MailSetting updatemailsettings = db.MailSetting.Where(x => x.MailSettingsID == 1).FirstOrDefault();

            updatemailsettings.Mail_Email = data.Mail_Email;
            updatemailsettings.Mail_Email_Password = data.Mail_Email_Password;
            updatemailsettings.Sender_Info = data.Sender_Info;
            updatemailsettings.Mail_Sender = data.Mail_Sender;
            updatemailsettings.Smtp_server = data.Smtp_server;
            updatemailsettings.Smpt_port = data.Smpt_port;

            int result = db.SaveChanges();
            if (result == 1)
            {
                TempData["result"] = 1;

            }

            return RedirectToAction("Add");
        }

    }
}
