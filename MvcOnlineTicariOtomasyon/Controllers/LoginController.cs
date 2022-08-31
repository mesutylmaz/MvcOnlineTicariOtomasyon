using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class LoginController : Controller
    {


        Context context = new Context();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }









        [HttpGet]
        public PartialViewResult Partial1()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult Partial1(Cari cari)
        {
            cari.CariDurumu = true;
            context.Cariler.Add(cari);
            context.SaveChanges();
            return PartialView();
        }













        [HttpGet]
        public ActionResult Partial2()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Partial2(Cari cari)
        {
            return View();
        }












        [HttpGet]
        public ActionResult CariLogin1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CariLogin1(Cari cari)
        {
            var bilgiler = context.Cariler.FirstOrDefault(x => x.CariMaili == cari.CariMaili && x.CariSifresi == cari.CariSifresi);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.CariMaili, false);
                Session["CariMaili"] = bilgiler.CariMaili.ToString();
                return RedirectToAction("Index", "CariPanel");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }





        
    }
}
