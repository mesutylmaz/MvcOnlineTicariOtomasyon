﻿using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections;       //ArrayList için
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;       //Chart için
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GrafikController : Controller
    {

        

        // GET: Grafik
        public ActionResult Index()
        {
            return View();
        }






        public ActionResult Index2()
        {
            var grafikCiz = new Chart(600, 600);
            grafikCiz.AddTitle("Kategori - Ürün Stok Sayısı").AddLegend("Stok").AddSeries("Değerler", xValue: new[] { "Mobilya","Ofis Eşyaları","Bilgisayar" }, yValues: new[] { 85, 66, 98 }).Write();
            return File(grafikCiz.ToWebImage().GetBytes(), "image/jpeg");
        }




        Context context = new Context();



        public ActionResult Index3()
        {
            ArrayList xDegeri = new ArrayList();
            ArrayList yDegerleri = new ArrayList();

            var sonuclar = context.Urunler.ToList();
            sonuclar.ToList().ForEach(x => xDegeri.Add(x.UrunAdi));
            sonuclar.ToList().ForEach(x => yDegerleri.Add(x.UrunStok));

            var grafik = new Chart(width:800, height:800).AddTitle("Stoklar").AddSeries(chartType:"Pie", name:"Stoklar", xValue: xDegeri, yValues: yDegerleri);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }









        public ActionResult Index4()
        {
            return View();
        }



        
        public ActionResult VisualizeUrunResult()
        {
            return Json(UrunList(), JsonRequestBehavior.AllowGet);
        }



        public List<Sinif1> UrunList()
        {
            List<Sinif1> snf = new List<Sinif1>();
            snf.Add(new Sinif1(){
                UrunAd = "Bilgisayar",
                Stok = 120
            });
            snf.Add(new Sinif1(){
                UrunAd = "Beyaz Eşya",
                Stok = 78
            });
            snf.Add(new Sinif1(){
                UrunAd = "Mobilya",
                Stok = 90
            });
            snf.Add(new Sinif1(){
                UrunAd = "Küçük Ev Aletleri",
                Stok = 45
            });
            snf.Add(new Sinif1(){
                UrunAd = "Cep Telefonu",
                Stok = 236
            });
            return snf;
        }










        public ActionResult VisualizeUrunResult2()
        {
            return Json(UrunList2(), JsonRequestBehavior.AllowGet);
        }



        public List<Sinif2> UrunList2()
        {
            List<Sinif2> snf = new List<Sinif2>();
            using (var c = new Context())
            {
                snf = c.Urunler.Select(x => new Sinif2
                {
                    urunAd = x.UrunAdi,
                    stok = x.UrunStok
                }).ToList();
            }
            
            return snf;
        }





        public ActionResult Index5()
        {
            return View();
        }





        public ActionResult Index6()
        {
            return View();
        }



        public ActionResult Index7()
        {
            return View();
        }
    }
}
