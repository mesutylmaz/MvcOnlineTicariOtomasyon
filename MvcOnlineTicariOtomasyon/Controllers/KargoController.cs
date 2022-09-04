using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {

        Context context = new Context();

        
        public ActionResult Index(string aranacakKelime)
        {
            var kargolar = from x in context.KargoDetaylari select x;
            if (!string.IsNullOrEmpty(aranacakKelime))
            {
                kargolar = kargolar.Where(y => y.TakipKodu.Contains(aranacakKelime) || y.Personel.Contains(aranacakKelime) || y.Alici.Contains(aranacakKelime));
            }
            return View(kargolar.ToList());
        }








        [HttpGet]
        public ActionResult YeniKargo()
        {
            Random rnd = new Random();

            string[] karakterler = { "A", "B", "C", "D", "E", "F", "G" };

            int k1, k2, k3;
            k1 = rnd.Next(0, karakterler.Length);
            k2 = rnd.Next(0, karakterler.Length);
            k3 = rnd.Next(0, karakterler.Length);
            
            int s1, s2, s3;
            s1 = rnd.Next(100, 1000);
            s2 = rnd.Next(10, 99);
            s3 = rnd.Next(10, 99);

            string kod = s1.ToString() + "-" + karakterler[k1] + "-" + s2.ToString() + "-" + karakterler[k2] + "-" + s3.ToString() + "-" + karakterler[k3];
            ViewBag.takipKodu = kod;


            List<SelectListItem> deger = (from x in context.Personeller.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.PersonelAdi + " " + x.PersonelSoyadi,
                                              Value = x.PersonelID.ToString()
                                          }).ToList();
            ViewBag.dgr = deger;

            return View();
        }

        [HttpPost]
        public ActionResult YeniKargo(KargoDetay kargo)
        {
            context.KargoDetaylari.Add(kargo);
            context.SaveChanges();
            return RedirectToAction("Index");
        }











        public ActionResult KargoTakip(string id)
        {
            var degerler = context.KargoTakipleri.Where(x => x.KargoTakipKodu == id).ToList();
            return View(degerler);
        }
    }
}