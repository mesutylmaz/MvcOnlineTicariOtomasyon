using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {

        Context context = new Context();

        // GET: CariPanel
        [Authorize]     //WebConfig'deki authentication kısıtlaması bu controllerda uygulanacak.
        public ActionResult Index()
        {
            var mail = (string)Session["CariMaili"];        //Cari'nin mailini Session ile sakla(taşı)
            var degerler = context.Cariler.FirstOrDefault(x => x.CariMaili == mail);    //Giriş yapan Maili database'de varsa bunu al
            ViewBag.mail = mail;    //Session'daki maili ViewBag'e sakla
            return View(degerler);
        }








        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMaili"];        
            var id = context.Cariler.Where(x => x.CariMaili == mail.ToString()).Select(y=>y.CariID).FirstOrDefault();
            var degerler = context.SatisHareketleri.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }
    }
}