using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class SinifGrup
    {
        public String Sehir { get; set; }       //Cariler tablosundaki CariSehir sütunundaki veriler string tipinde olduğu için buraya Sehir'i string türünde yazdık

        public int Sayi { get; set; }
    }
}
