using MvcOnlineTicariOtomasyon.Models.Siniflar;
using PagedList;
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
        public ActionResult Index(int sayfa =1)
        {
            var liste = context.Faturalar.ToList().ToPagedList(sayfa, 10);      //ilgili sayfada sadece ilk 10 faturayı göster
            return View(liste);
        }






        [HttpGet]
        public ActionResult FaturaEkle()
        {
            List<SelectListItem> deger1 = (from x in context.Personeller.ToList()       //Kategori Listesinden dropdown ile listeleme yapması için Linq                                                                                   sorgusunu yazdım
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAdi + " " + x.PersonelSoyadi,                    //Formda seçilen kategori adını text'e basıcak
                                               Value = x.PersonelID.ToString()          //Text'e basılan Kategorinin ID'sini Value olarak saklayacak.
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from x in context.Cariler.ToList()       //Kategori Listesinden dropdown ile listeleme yapması için Linq                                                                                   sorgusunu yazdım
                                           select new SelectListItem
                                           {
                                               Text = x.CariAdi + " " + x.CariSoyadi,                    //Formda seçilen kategori adını text'e basıcak
                                               Value = x.CariID.ToString()          //Text'e basılan Kategorinin ID'sini Value olarak saklayacak.
                                           }).ToList();
            ViewBag.dgr2 = deger2;

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
            List<SelectListItem> deger1 = (from x in context.Personeller.ToList()       //Kategori Listesinden dropdown ile listeleme yapması için Linq                                                                                   sorgusunu yazdım
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAdi + " " + x.PersonelSoyadi,                    //Formda seçilen kategori adını text'e basıcak
                                               Value = x.PersonelID.ToString()          //Text'e basılan Kategorinin ID'sini Value olarak saklayacak.
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from x in context.Cariler.ToList()       //Kategori Listesinden dropdown ile listeleme yapması için Linq                                                                                   sorgusunu yazdım
                                           select new SelectListItem
                                           {
                                               Text = x.CariAdi + " " + x.CariSoyadi,                    //Formda seçilen kategori adını text'e basıcak
                                               Value = x.CariID.ToString()          //Text'e basılan Kategorinin ID'sini Value olarak saklayacak.
                                           }).ToList();
            ViewBag.dgr2 = deger2;

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
            var degerler = context.FaturaKalemleri.Where(x => x.Faturaid == id).ToList().ToPagedList(id, 10);      //ilgili sayfada sadece ilk 10 fatura detayını göster
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