using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {

        Context context = new Context();


        // GET: Personel
        public ActionResult PersonelListesi()
        {
            var degerler = context.Personeller.Where(x => x.PersonelDurumu == true).ToList();
            return View(degerler);
        }




        public ActionResult PasifPersonelListesi()
        {
            var degerler = context.Personeller.Where(x => x.PersonelDurumu == false).ToList();
            return View(degerler);
        }




        public ActionResult PersoneliAktifYap(int id)
        {
            var deger = context.Personeller.Find(id);
            deger.PersonelDurumu = true;           //Bu satır yerine Personel Class'da Durum için yazılan kodu get-set ederek de yazabiliriz.
            context.SaveChanges();
            return RedirectToAction("PersonelListesi");
        }





        public ActionResult PasifPersonelYap(int id)
        {
            var deger = context.Personeller.Find(id);
            deger.PersonelDurumu = false;           //Bu satır yerine Personel Class'da Durum için yazılan kodu get-set ederek de yazabiliriz.
            context.SaveChanges();
            return RedirectToAction("PersonelListesi");
        }






        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> deger1 = (from x in context.Departmanlar.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAdi,
                                               Value = x.DepartmanID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }

        [HttpPost]
        public ActionResult PersonelEkle(Personel personel)
        {
            personel.PersonelDurumu = true;           //Bu satır yerine Personel Class'da Durum için yazılan kodu get-set ederek de yazabiliriz.
            context.Personeller.Add(personel);
            context.SaveChanges();
            return RedirectToAction("PersonelListesi");

        }






        public ActionResult PersonelGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in context.Departmanlar.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAdi,
                                               Value = x.DepartmanID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var deger = context.Personeller.Find(id);
            return View("PersonelGetir", deger);
        }


        public ActionResult PersonelGuncelle(Personel personel)
        {

            var deger = context.Personeller.Find(personel.PersonelID);
            deger.PersonelGorseli = personel.PersonelGorseli;
            deger.PersonelAdi = personel.PersonelAdi;
            deger.PersonelSoyadi = personel.PersonelSoyadi;
            deger.Departmanid = personel.Departmanid;
            context.SaveChanges();
            return RedirectToAction("PersonelListesi");

        }
    }
}