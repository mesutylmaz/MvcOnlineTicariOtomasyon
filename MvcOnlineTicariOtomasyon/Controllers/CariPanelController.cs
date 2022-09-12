using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;          //FormsAuthentication için

namespace MvcOnlineTicariOtomasyon.Controllers
{
    //[Authorize]         //WebConfig'deki authentication kısıtlaması bu controllerda uygulanacak.
    public class CariPanelController : Controller
    {

        Context context = new Context();

        // GET: CariPanel
        //[Authorize]     //Action üzerine Authorize yazarak, Sadece o action'a Url'den erişimi engelleyebiliriz
        public ActionResult Index()
        {
            var mail = (string)Session["CariMaili"];        //Cari'nin mailini Session ile sakla(taşı)
            var sehir = (string)Session["CariSehir"];
            var degerler = context.Mesajlar.Where(x => x.Alici == mail).ToList();    //Giriş yapan Maili database'de varsa bunu al
            ViewBag.mail = mail;    //Session'daki maili ViewBag'e sakla
            ViewBag.Sehir = sehir;

            var mailID = context.Cariler.Where(x => x.CariMaili == mail).Select(y => y.CariID).FirstOrDefault();
            ViewBag.MailID = mailID;

            var toplamSatis = context.SatisHareketleri.Where(c => c.Cariid == mailID).Count();
            ViewBag.ToplamSatis = toplamSatis;

            var satilanToplamUrunSayisi = context.SatisHareketleri.Where(d => d.Cariid == mailID).Sum(f => f.SatisHareketAdedi);
            ViewBag.SatilanToplamUrunSayisi = satilanToplamUrunSayisi;

            var toplamTutar = context.SatisHareketleri.Where(d => d.Cariid == mailID).Sum(f => f.SatisHareketToplamTutari);
            ViewBag.ToplamTutar = toplamTutar;

            var adSoyad = context.Cariler.Where(x => x.CariMaili == mail).Select(y => y.CariAdi + " " + y.CariSoyadi).FirstOrDefault();
            ViewBag.AdSoyad = adSoyad;

            return View(degerler);
        }







        
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMaili"];
            var id = context.Cariler.Where(x => x.CariMaili == mail.ToString()).Select(y => y.CariID).FirstOrDefault();
            var degerler = context.SatisHareketleri.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }














        
        public ActionResult GelenMesajlar()
        {
            //var musteriler = context.Cariler.Where(
            //    x => x.CariMaili == (context.Mesajlar.Select(c => c.Gonderici))
            //    .(x.CariAdi + " " + x.CariSoyadi)).ToList();



            var mail = (string)Session["CariMaili"];
            var degerler = context.Mesajlar.Where(x => x.Alici == mail).OrderByDescending(x => x.MesajID).ToList();

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




        //Ab

        public ActionResult KargoTakip(string aranacakKelime)       //ab
        {
            //var deger1 = aranacakKelime.ToLower().Replace('ı','i');          //AB

            //var kargolar = from x in context.KargoDetaylari select x;
            //kargolar = kargolar.Where(y => y.TakipKodu.ToLower().Replace('ı','i').Contains(deger1));

            var kargolar = from x in context.KargoDetaylari select x;
            kargolar = kargolar.Where(y => y.TakipKodu.Contains(aranacakKelime));


            return View(kargolar.ToList());
        }





        
        public ActionResult CariKargoTakip(string id)
        {
            var degerler = context.KargoTakipleri.Where(x => x.KargoTakipKodu == id).ToList();
            return View(degerler);
        }







        //public ActionResult Yardim()
        //{
        //    return View();
        //}








        
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();      //İstekleri terk et
            return RedirectToAction("Index", "Login");
        }








        public ActionResult PartialView1()
        {
            var mail = (string)Session["CariMaili"];
            var id = context.Cariler.Where(x => x.CariMaili == mail).Select(y => y.CariID).FirstOrDefault();

            var cariBul = context.Cariler.Find(id);

            return PartialView("PartialView1", cariBul);
        }














        public ActionResult PartialView2()
        {
            var veriler = context.Mesajlar.Where(x => x.Gonderici == "admin").ToList();

            return PartialView(veriler);
        }











        public ActionResult CariBilgiGuncelle(Cari cari)
        {
            var deger = context.Cariler.Find(cari.CariID);
            deger.CariAdi = cari.CariAdi;
            deger.CariSoyadi = cari.CariSoyadi;
            deger.CariSehir = cari.CariSehir;
            deger.CariSifresi = cari.CariSifresi;
            deger.CariMaili = cari.CariMaili;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
