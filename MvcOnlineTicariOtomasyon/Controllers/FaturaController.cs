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









        public ActionResult DinamikFaturalar()
        {
            DinamikFatura dinamikFatura = new DinamikFatura();
            dinamikFatura.deger1 = context.Faturalar.ToList();
            dinamikFatura.deger2 = context.FaturaKalemleri.ToList();
            return View(dinamikFatura);
        }



        public ActionResult FaturayiKaydet(string FaturaSeriNo, string FaturaSiraNo, DateTime FaturaTarih, string FaturaVergiDairesi, string FaturaSaat, string FaturaTeslimEden, string FaturaTeslimAlan, string ToplamTutar, FaturaKalem [] kalemler)
        {
            Fatura fatura = new Fatura();
            fatura.FaturaSeriNo = FaturaSeriNo;
            fatura.FaturaSiraNo = FaturaSiraNo;
            fatura.FaturaTarih = FaturaTarih;
            fatura.FaturaVergiDairesi = FaturaVergiDairesi;
            fatura.FaturaSaat = FaturaSaat;
            fatura.FaturaTeslimEden = FaturaTeslimEden;
            fatura.FaturaTeslimAlan = FaturaTeslimAlan;
            fatura.ToplamTutar = decimal.Parse(ToplamTutar);
            context.Faturalar.Add(fatura);

            foreach (var item in kalemler)
            {
                FaturaKalem kalem = new FaturaKalem();
                kalem.FaturaKalemAciklama = item.FaturaKalemAciklama;
                kalem.FaturaKalemBirimFiyat = item.FaturaKalemBirimFiyat;
                kalem.FaturaKalemTutar = item.FaturaKalemTutar;
                kalem.Faturaid = item.Faturaid;
                kalem.FaturaKalemMiktar = item.FaturaKalemMiktar;
                context.FaturaKalemleri.Add(kalem);
            }

            context.SaveChanges();
            return Json("Kayıt işlemi başarılı.", JsonRequestBehavior.AllowGet);
        }
    }
}