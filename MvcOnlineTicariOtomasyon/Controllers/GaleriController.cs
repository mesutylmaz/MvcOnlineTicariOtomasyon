using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GaleriController : Controller
    {

        Context content = new Context();

        // GET: Image
        public ActionResult Index()
        {
            var degerler = content.Urunler.Where(x=>x.UrunDurumu==true).ToList();
            return View(degerler);
        }
    }
}