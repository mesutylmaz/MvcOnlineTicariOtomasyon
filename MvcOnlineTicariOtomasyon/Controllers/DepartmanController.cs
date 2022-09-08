using MvcOnlineTicariOtomasyon.Models.Siniflar;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class DepartmanController : Controller
    {

        Context context = new Context();

        // GET: Departman
        public ActionResult DepartmanListesi(int sayfa = 1)
        {
            var degerler = context.Departmanlar.Where(x=>x.DepartmanDurumu==true).ToList().ToPagedList(sayfa, 10);      //ilgili sayfada sadece ilk 4 kategoriyi göster;
            return View(degerler);
        }





        public ActionResult PasifDepartmanListesi()
        {
            var degerler = context.Departmanlar.Where(x => x.DepartmanDurumu == false).ToList();
            return View(degerler);
        }






        public ActionResult DepartmaniAktifEt(int id)
        {
            var deger = context.Departmanlar.Find(id);
            deger.DepartmanDurumu = true;
            context.SaveChanges();
            return RedirectToAction("DepartmanListesi");
        }







        [HttpGet][Authorize(Roles ="A")]
        public ActionResult DepartmanEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DepartmanEkle(Departman departman)
        {
            context.Departmanlar.Add(departman);
            context.SaveChanges();
            return RedirectToAction(nameof(DepartmanListesi));
        }







        public ActionResult DepartmanGetir(int id)
        {
            //List<SelectListItem> deger1 = (from x in context.Departmanlar.ToList()       //Departman Listesinden dropdown ile listeleme yapması için Linq                                                                                   sorgusunu yazdım
            //                               select new SelectListItem
            //                               {
            //                                   Text = x.DepartmanAdi,                    //Formda seçilen departman adını text'e basıcak
            //                                   Value = x.DepartmanID.ToString()          //Text'e basılan departmanın ID'sini Value olarak saklayacak.
            //                               }).ToList();
            //ViewBag.dgr1 = deger1;


            var deger = context.Departmanlar.Find(id);
            return View("DepartmanGetir", deger);         //Bu ID'ye sahip olanın bilgileriyle birlikte DepartmanGuncelle Sayfasını Getir
        }

        
        public ActionResult DepartmanGuncelle(Departman departman)
        {
            var deger = context.Departmanlar.Find(departman.DepartmanID);
            deger.DepartmanAdi = departman.DepartmanAdi;
            deger.DepartmanDurumu = departman.DepartmanDurumu;
            context.SaveChanges();
            return RedirectToAction("DepartmanListesi");
        }






        //[HttpGet]
        //public ActionResult DepartmanSil()
        //{
        //    return View();
        //}

        //[HttpPost]
        public ActionResult DepartmanSil(int id)
        {
            var deger = context.Departmanlar.Find(id);
            deger.DepartmanDurumu = false;
            context.SaveChanges();
            return RedirectToAction("DepartmanListesi");
        }






        public ActionResult DepartmanDetayi(int id)
        {
            var degerler = context.Personeller.Where(x => x.Departmanid == id).ToList();
            var departman = context.Departmanlar.Where(x => x.DepartmanID == id).Select(y => y.DepartmanAdi).FirstOrDefault();
            ViewBag.DepartmanAdi = departman;
            return View(degerler);
        }






        public ActionResult DepartmanPersonelSatis(int id)
        {
            var degerler = context.SatisHareketleri.Where(x => x.Personelid == id).ToList();
            var personel = context.Personeller.Where(x => x.PersonelID == id).Select(y => y.PersonelAdi + " " + y.PersonelSoyadi).FirstOrDefault();
            var departman = context.Personeller.Where(x => x.PersonelID == id).Select(y => y.Departman.DepartmanAdi).FirstOrDefault();
            ViewBag.DepartmanAdi = departman;
            ViewBag.DepartmanPersoneliSatisi = personel;
            return View(degerler);
        }
    }
}
