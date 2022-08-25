using MvcOnlineTicariOtomasyon.Models.Siniflar;
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
        public ActionResult CariListesi()
        {
            var degerler = context.Cariler.Where(x=>x.CariDurumu==true).ToList();
            return View(degerler);
        }





        public ActionResult PasifCariListesi()
        {
            var degerler = context.Cariler.Where(x => x.CariDurumu == false).ToList();
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
            var degerler = context.SatisHareketleri.Where(x => x.Cariid == id).ToList();
            var cr = context.Cariler.Where(x => x.CariID == id).Select(y => y.CariAdi + " " + y.CariSoyadi).FirstOrDefault();
            ViewBag.Cari = cr;
            return View(degerler);
        }
    }
}
