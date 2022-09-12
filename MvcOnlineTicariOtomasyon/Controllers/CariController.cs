using MvcOnlineTicariOtomasyon.Models.Siniflar;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {

        Context context = new Context();


        // GET: Cari
        public ActionResult CariListesi(int sayfa = 1)      //Listelemeyi 1. sayfa için yap
        {
            var degerler = context.Cariler.Where(x=>x.CariDurumu==true).ToList().ToPagedList(sayfa, 10);      //ilgili sayfada sadece ilk 10 cariyi göster
            return View(degerler);
        }





        public ActionResult PasifCariListesi(int sayfa = 1)      //Listelemeyi 1. sayfa için yap
        {
            var degerler = context.Cariler.Where(x => x.CariDurumu == false).ToList().ToPagedList(sayfa, 10);      //ilgili sayfada sadece ilk 10 cariyi göster
            return View(degerler);
        }





        public ActionResult CariyiAktifEt(int id)
        {
            var deger = context.Cariler.Find(id);
            deger.CariDurumu = true;
            context.SaveChanges();
            return RedirectToAction("CariListesi");
        }






        [HttpGet]
        public ActionResult CariEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CariEkle(Cari cari)
        {
            if (!ModelState.IsValid)
            {
                return View("CariEkle");
            }
            else
            {
                //cari.CariDurumu = true;           //Class'da Durum için yazılan kodlar yerine sadece bu satırı da yazabiliriz.
                context.Cariler.Add(cari);
                context.SaveChanges();
                return RedirectToAction("CariListesi");
            }
        }







        

        
        public ActionResult CariSil(int id)
        {
            var deger = context.Cariler.Find(id);
            deger.CariDurumu = false;
            context.SaveChanges();
            return RedirectToAction("CariListesi");
        }








        public ActionResult CariGetir(int id)
        {
            var deger = context.Cariler.Find(id);
            return View("CariGetir", deger);        
        }


        public ActionResult CariGuncelle(Cari cari)
        {
            if (!ModelState.IsValid)
            {
                return View("CariGetir");
            }
            else
            {
                var deger = context.Cariler.Find(cari.CariID);
                deger.CariAdi = cari.CariAdi;
                deger.CariSoyadi = cari.CariSoyadi;
                deger.CariMaili = cari.CariMaili;
                deger.CariSehir = cari.CariSehir;
                context.SaveChanges();
                return RedirectToAction("CariListesi");
            }
        }








        public ActionResult CariSatisGecmisi(int id)
        {
            var degerler = context.SatisHareketleri.Where(x => x.Cariid == id).ToList().ToPagedList(id, 10);      //ilgili sayfada sadece ilk 10 satışı göster
            var cr = context.Cariler.Where(x => x.CariID == id).Select(y => y.CariAdi + " " + y.CariSoyadi).FirstOrDefault();
            ViewBag.Cari = cr;
            return View(degerler);
        }
    }
}
