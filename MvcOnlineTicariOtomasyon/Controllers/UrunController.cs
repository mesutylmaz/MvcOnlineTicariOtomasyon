using MvcOnlineTicariOtomasyon.Models.Siniflar;
using PagedList;    //ToPagedList için
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {

        Context context = new Context();

        // GET: Urun
        //public ActionResult UrunlerListesi()
        //{
        //    var urunler = context.Urunler.Where(x=>x.UrunDurumu==true).ToList();        //Ürün Durumu True(Aktif) olanları Listele
        //    return View(urunler);
        //}

        public ActionResult UrunlerListesi(string aranacakKelime)
        {
            var urunler = from x in context.Urunler select x;
            if (!string.IsNullOrEmpty(aranacakKelime))
            {
                urunler = urunler.Where(y => y.UrunAdi.Contains(aranacakKelime) || y.UrunMarka.Contains(aranacakKelime));
            }
            return View(urunler.ToList());
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
        [ValidateAntiForgeryToken]
        public ActionResult UrunEkle(/*[Bind(Include = "UrunID, UrunAdi, UrunMarka, UrunStok, UrunAlisFiyati, UrunSatisFiyati, UrunDurumu, UrunGorseli, Kategoriid, Kategori")] Urun urun, IEnumerable<HttpPostedFileBase> UrunGorseli*/ Urun urun)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Images/" + dosyaAdi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                urun.UrunGorseli = "/Images/" + dosyaAdi + uzanti;
            }
            urun.UrunDurumu = true;
            context.Urunler.Add(urun);
            context.SaveChanges();
            return RedirectToAction(nameof(UrunlerListesi));





            //if (ModelState.IsValid)
            //{
            //    foreach (var item in UrunGorseli)
            //    {
            //        if (item.ContentLength > 0)
            //        {
            //            var image = Path.GetFileName(item.FileName);
            //            var path = Path.Combine(Server.MapPath("~/Images"), image);
            //            item.SaveAs(path);
            //            urun.UrunGorseli = "/Images/" + image;
            //            urun.UrunDurumu = true;
            //            context.Urunler.Add(urun);
            //            context.SaveChanges();
            //            return RedirectToAction(nameof(UrunlerListesi));
            //        }
            //        return HttpNotFound();
            //    }
            //}
            //return HttpNotFound();
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
        public ActionResult UrunGuncelle(/*[Bind(Include = "UrunID, UrunAdi, UrunMarka, UrunStok, UrunAlisFiyati, UrunSatisFiyati, UrunDurumu, UrunGorseli, Kategoriid, Kategori")] Urun urun, IEnumerable<HttpPostedFileBase> UrunGorseli*/ Urun urun)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Images/" + dosyaAdi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                urun.UrunGorseli = "/Images/" + dosyaAdi + uzanti;
            }
            var deger = context.Urunler.Find(urun.UrunID);
            deger.UrunAdi = urun.UrunAdi;
            deger.UrunStok = urun.UrunStok;
            deger.UrunAlisFiyati = urun.UrunAlisFiyati;
            deger.UrunSatisFiyati = urun.UrunSatisFiyati;
            deger.UrunGorseli = urun.UrunGorseli;
            deger.UrunMarka = urun.UrunMarka;
            deger.Kategoriid = urun.Kategoriid;
            urun.UrunDurumu = true;
            context.SaveChanges();
            return RedirectToAction("UrunlerListesi");



            //if (ModelState.IsValid)
            //{
            //    foreach (var item in UrunGorseli)
            //    {
            //        if (item.ContentLength > 0)
            //        {
            //            var image = Path.GetFileName(item.FileName);
            //            var path = Path.Combine(Server.MapPath("~/Images"), image);
            //            item.SaveAs(path);
            //            urun.UrunGorseli = "/Images/" + image;

            //            var deger = context.Urunler.Find(urun.UrunID);
            //            deger.UrunAdi = urun.UrunAdi;
            //            deger.UrunStok = urun.UrunStok;
            //            deger.UrunAlisFiyati = urun.UrunAlisFiyati;
            //            deger.UrunSatisFiyati = urun.UrunSatisFiyati;
            //            deger.UrunGorseli = urun.UrunGorseli;
            //            deger.UrunMarka = urun.UrunMarka;
            //            deger.Kategoriid = urun.Kategoriid;
            //            urun.UrunDurumu = true;
            //            context.SaveChanges();
            //            return RedirectToAction("UrunlerListesi");
            //        }
            //        return HttpNotFound();
            //    }
            //}
            //return HttpNotFound();
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