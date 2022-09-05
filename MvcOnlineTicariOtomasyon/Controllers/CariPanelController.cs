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















        public ActionResult GelenMesajlar()
        {
            //var musteriler = context.Cariler.Where(
            //    x => x.CariMaili == (context.Mesajlar.Select(c => c.Gonderici))
            //    .(x.CariAdi + " " + x.CariSoyadi)).ToList();



            var mail = (string)Session["CariMaili"];
            var degerler = context.Mesajlar.Where(x=>x.Alici==mail).OrderByDescending(x=>x.MesajID).ToList();

            var gelenMesajSayisi = context.Mesajlar.Count(x => x.Alici == mail).ToString();
            ViewBag.GelenMesajlar = gelenMesajSayisi;

            var gidenMesajSayisi = context.Mesajlar.Count(x => x.Gonderici == mail).ToString();
            ViewBag.GidenMesajlar = gidenMesajSayisi;

            return View(degerler);
        }









        public ActionResult GidenMesajlar()
        {
            //var musteriler = context.Cariler.Where(
            //    x => x.CariMaili == (context.Mesajlar.Select(c => c.Gonderici))
            //    .(x.CariAdi + " " + x.CariSoyadi)).ToList();



            var mail = (string)Session["CariMaili"];
            var degerler = context.Mesajlar.Where(x => x.Gonderici == mail).OrderByDescending(x => x.MesajID).ToList();

            var gelenMesajSayisi = context.Mesajlar.Count(x => x.Alici == mail).ToString();
            ViewBag.GelenMesajlar = gelenMesajSayisi;

            var gidenMesajSayisi = context.Mesajlar.Count(x => x.Gonderici == mail).ToString();
            ViewBag.GidenMesajlar = gidenMesajSayisi;

            return View(degerler);
        }








        public ActionResult MesajDetay(int id)
        {
            var degerler = context.Mesajlar.Where(x => x.MesajID == id).ToList();

            var mail = (string)Session["CariMaili"];
            var gelenMesajSayisi = context.Mesajlar.Count(x => x.Alici == mail).ToString();
            ViewBag.GelenMesajlar = gelenMesajSayisi;
            var gidenMesajSayisi = context.Mesajlar.Count(x => x.Gonderici == mail).ToString();
            ViewBag.GidenMesajlar = gidenMesajSayisi;

            return View(degerler);
        }







        [HttpGet]
        public ActionResult YeniMesajlar()
        {
            var mail = (string)Session["CariMaili"];

            var gelenMesajSayisi = context.Mesajlar.Count(x => x.Alici == mail).ToString();
            ViewBag.GelenMesajlar = gelenMesajSayisi;

            var gidenMesajSayisi = context.Mesajlar.Count(x => x.Gonderici == mail).ToString();
            ViewBag.GidenMesajlar = gidenMesajSayisi;

            return View();
        }


        [HttpPost]
        public ActionResult YeniMesajlar(Mesaj mesaj)
        {
            var mail = (string)Session["CariMaili"];

            mesaj.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            mesaj.Gonderici = mail;
            context.Mesajlar.Add(mesaj);
            context.SaveChanges();

            return View();
        }
    }
}