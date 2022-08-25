using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {

        Context context = new Context();

        // GET: Urun
        public ActionResult UrunlerListesi()
        {
            var urunler = context.Urunler.Where(x=>x.UrunDurumu==true).ToList();        //Ürün Durumu True(Aktif) olanları Listele
            return View(urunler);
        }





        public ActionResult PasifUrunListesi()
        {
            var degerler = context.Urunler.Where(x => x.UrunDurumu == false).ToList();
            return View(degerler);
        }






        public ActionResult UrunuAktifYap(int id)
        {
            var deger = context.Urunler.Find(id);
            deger.UrunDurumu = true;           //Bu satır yerine Personel Class'da Durum için yazılan kodu get-set ederek de yazabiliriz.
            context.SaveChanges();
            return RedirectToAction("UrunlerListesi");
        }





        public ActionResult UrunuPasifYap(int id)
        {
            var deger = context.Urunler.Find(id);
            deger.UrunDurumu = false;           //Bu satır yerine Personel Class'da Durum için yazılan kodu get-set ederek de yazabiliriz.
            context.SaveChanges();
            return RedirectToAction("PasifUrunListesi");
        }








        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> deger1 = (from x in context.Kategoriler.ToList()       //Kategori Listesinden dropdown ile listeleme yapması için Linq                                                                                   sorgusunu yazdım
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAdi,                    //Formda seçilen kategori adını text'e basıcak
                                               Value = x.KategoriID.ToString()          //Text'e basılan Kategorinin ID'sini Value olarak saklayacak.
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(Urun urun)
        {
            urun.UrunDurumu = true;
            context.Urunler.Add(urun);
            context.SaveChanges();
            return RedirectToAction(nameof(UrunlerListesi));
        }








        [HttpGet]
        public ActionResult UrunGuncelle(int id)
        {
            List<SelectListItem> deger1 = (from x in context.Kategoriler.ToList()       //Kategori Listesinden dropdown ile listeleme yapması için Linq                                                                                   sorgusunu yazdım
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAdi,                    //Formda seçilen kategori adını text'e basıcak
                                               Value = x.KategoriID.ToString()          //Text'e basılan Kategorinin ID'sini Value olarak saklayacak.
                                           }).ToList();
            ViewBag.dgr1 = deger1;


            var deger = context.Urunler.Find(id);
            return View("UrunGuncelle", deger);         //Bu ID'ye sahip olanın bilgileriyle birlikte UrunGuncelle Sayfasını Getir
        }

        [HttpPost]
        public ActionResult UrunGuncelle(Urun urun)
        {
            var deger = context.Urunler.Find(urun.UrunID);
            deger.UrunAdi = urun.UrunAdi;
            deger.UrunStok = urun.UrunStok;
            deger.UrunAlisFiyati = urun.UrunAlisFiyati;
            deger.UrunSatisFiyati = urun.UrunSatisFiyati;
            deger.UrunGorseli = urun.UrunGorseli;
            deger.UrunMarka = urun.UrunMarka;
            deger.UrunDurumu = urun.UrunDurumu;
            deger.Kategoriid = urun.Kategoriid;
            context.SaveChanges();
            return RedirectToAction("UrunlerListesi");
        }









        [HttpGet]
        public ActionResult UrunleriSil()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UrunleriSil(int id)
        {
            var deger = context.Urunler.Find(id);
            deger.UrunDurumu = false;
            context.SaveChanges();
            return RedirectToAction("UrunlerListesi");
        }









        public ActionResult UrunListesi()
        {
            var degerler = context.Urunler.Where(x => x.UrunDurumu == true).ToList();
            return View(degerler);
        }


    }
}