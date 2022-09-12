using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;       //SelectListItem için

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Cascading
    {
        public IEnumerable<SelectListItem> Kategoriler { get; set; }
        public IEnumerable<SelectListItem> Urunler { get; set; }
        public IEnumerable<SelectListItem> UrunFiyatlari { get; set; }
        public IEnumerable<Fatura> deger1 { get; set; }
    }
}