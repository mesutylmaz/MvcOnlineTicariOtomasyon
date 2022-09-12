using MvcOnlineTicariOtomasyon.Models.Siniflar;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        Context context = new Context();

        // GET: Satis
        public ActionResult SatisListesi()
        {
            var degerler = context.SatisHareketleri.ToList();      
            return View(degerler);
        }





        [HttpGet]
        public ActionResult SatisEkle()
        {
            List<SelectListItem> deger1 = (from x in context.Urunler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAdi,
                                               Value = x.UrunID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;


            List<SelectListItem> deger2 = (from x in context.Cariler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAdi + " " + x.CariSoyadi,
                                               Value = x.CariID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;


            List<SelectListItem> deger3 = (from x in context.Personeller.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAdi + " " + x.PersonelSoyadi,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;

            return View();
        }

        [HttpPost]
        public ActionResult SatisEkle(SatisHareket satis)
        {
            satis.SatisHareketTarihi = DateTime.Parse(DateTime.Now.ToShortDateString());
            context.SatisHareketleri.Add(satis);
            context.SaveChanges();
            return RedirectToAction("SatisListesi");

        }





        public ActionResult SatisGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in context.Urunler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAdi,
                                               Value = x.UrunID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;


            List<SelectListItem> deger2 = (from x in context.Cariler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAdi + " " + x.CariSoyadi,
                                               Value = x.CariID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;


            List<SelectListItem> deger3 = (from x in context.Personeller.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAdi + " " + x.PersonelSoyadi,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            var deger = context.SatisHareketleri.Find(id);
            return View("SatisGetir", deger);
        }







        

       
        public ActionResult SatisGuncelle(SatisHareket satisHareket)
        {
            var deger = context.SatisHareketleri.Find(satisHareket.SatisHareketID);
            deger.Cariid = satisHareket.Cariid;
            deger.SatisHareketAdedi = satisHareket.SatisHareketAdedi;
            deger.SatisHareketFiyati = satisHareket.SatisHareketFiyati;
            deger.Personelid = satisHareket.Personelid;
            deger.SatisHareketTarihi = satisHareket.SatisHareketTarihi;
            deger.SatisHareketToplamTutari = satisHareket.SatisHareketToplamTutari;
            deger.Urunid = satisHareket.Urunid;
            context.SaveChanges();
            return RedirectToAction("SatisListesi");
        }






        public ActionResult SatisDetayi(int id)
        {
            var deger = context.SatisHareketleri.Where(x => x.SatisHareketID == id).ToList();
            return View(deger);
        }
    }
}
