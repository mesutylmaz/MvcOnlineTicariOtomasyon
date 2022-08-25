using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        Context context = new Context();
        // GET: Fatura
        public ActionResult Index()
        {
            var liste = context.Faturalar.ToList();
            return View(liste);
        }






        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FaturaEkle(Fatura fatura)
        {
            context.Faturalar.Add(fatura);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }








        public ActionResult FaturaGetir(int id)
        {
            var fatura = context.Faturalar.Find(id);
            return View("FaturaGetir", fatura);
        }








        public ActionResult FaturaGuncelle(Fatura fatura)
        {
            var fat = context.Faturalar.Find(fatura.FaturaID);
            fat.FaturaSeriNo = fatura.FaturaSeriNo;
            fat.FaturaSiraNo = fatura.FaturaSiraNo;
            fat.FaturaTarih = fatura.FaturaTarih;
            fat.FaturaSaat = fatura.FaturaSaat;
            fat.FaturaVergiDairesi = fatura.FaturaVergiDairesi;
            fat.FaturaTeslimAlan = fatura.FaturaTeslimAlan;
            fat.FaturaTeslimEden = fatura.FaturaTeslimEden;
            context.SaveChanges();
            return RedirectToAction("Index");
        }








        public ActionResult FaturaDetay(int id)
        {
            var degerler = context.FaturaKalemleri.Where(x => x.Faturaid == id).ToList();
            return View(degerler);
        }






        [HttpGet]
        public ActionResult FaturaKalemiEkle()
        {
            return View();
        }


        [HttpPost]
        public ActionResult FaturaKalemiEkle(FaturaKalem faturaKalem)
        {
            context.FaturaKalemleri.Add(faturaKalem);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}