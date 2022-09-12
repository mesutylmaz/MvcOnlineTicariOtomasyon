using MvcOnlineTicariOtomasyon.Models.Siniflar;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;       //ToPagedList için

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {

        Context context = new Context();


        // GET: Kategori
        //public ActionResult KategorileriListele()
        //{
        //    var degerler = context.Kategoriler.Where(x=>x.KategoriDurumu==true).ToList();
        //    return View(degerler);
        //}
        public ActionResult KategorileriListele(int sayfa = 1)      //Listelemeyi 1. sayfa için yap
        {
            var degerler = context.Kategoriler.Where(x => x.KategoriDurumu == true).ToList().ToPagedList(sayfa, 10);      //ilgili sayfada sadece ilk 10 kategoriyi göster
            return View(degerler);
        }





        public ActionResult PasifKategorileriListele(int sayfa = 1)
        {
            var degerler = context.Kategoriler.Where(x => x.KategoriDurumu == false).ToList().ToPagedList(sayfa, 10);      //ilgili sayfada sadece ilk 10 kategoriyi göster
            return View(degerler);
        }





        public ActionResult KategoriyiAktifEt(int id)
        {
            var deger = context.Kategoriler.Find(id);
            deger.KategoriDurumu = true;
            context.SaveChanges();
            return RedirectToAction("KategorileriListele");
        }







        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(Kategori kategori)
        {
            context.Kategoriler.Add(kategori);
            context.SaveChanges();
            return RedirectToAction("KategorileriListele");
        }






        [HttpGet]
        public ActionResult KategoriGuncelle(int id)
        {
            var deger = context.Kategoriler.Find(id);
            return View("KategoriGuncelle", deger);         //Bu ID'ye sahip olanın bilgileriyle birlikte KategoriGuncelle Sayfasını Getir
        }

        [HttpPost]
        public ActionResult KategoriGuncelle(Kategori kategori)
        {
            var deger = context.Kategoriler.Find(kategori.KategoriID);
            deger.KategoriAdi = kategori.KategoriAdi;
            context.SaveChanges();
            return RedirectToAction("KategorileriListele");
        }









        
        public ActionResult KategoriSil(int id)
        {
            var deger = context.Kategoriler.Find(id);
            deger.KategoriDurumu = false;
            context.SaveChanges();
            return RedirectToAction("KategorileriListele");
        }










        public ActionResult CascadingKategoriUrun()
        {
            Cascading cas = new Cascading();
            cas.Kategoriler = new SelectList(context.Kategoriler, "KategoriID", "KategoriAdi");
            cas.Urunler = new SelectList(context.Urunler, "UrunID", "UrunAdi");

            return View(cas);
        }




        public JsonResult KategoriyeGoreUrunGetir(int p)
        {
            var urunList = (from x in context.Urunler
                            join y in context.Kategoriler
                            on x.Kategori.KategoriID equals y.KategoriID
                            where x.Kategori.KategoriID == p
                            select new
                            {
                                Text = x.UrunAdi,
                                Value = x.UrunID.ToString()
                            }).ToList();

            return Json(urunList, JsonRequestBehavior.AllowGet);
        }
    }
}