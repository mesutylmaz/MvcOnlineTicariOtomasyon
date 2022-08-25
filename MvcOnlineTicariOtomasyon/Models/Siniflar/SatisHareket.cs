using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class SatisHareket
    {
        [Key]
        public int SatisHareketID { get; set; }
        public DateTime SatisHareketTarihi { get; set; }
        public int SatisHareketAdedi { get; set; }
        public decimal SatisHareketFiyati { get; set; }
        public decimal SatisHareketToplamTutari { get; set; }


        public int Urunid { get; set; }
        public virtual Urun Urun { get; set; }                     //1 SatisHareketinin 1 Ürünü olabilir

        public int Cariid { get; set; }
        public virtual Cari Cari { get; set; }                    //1 SatisHareketinin 1 Carisi olabilir

        public int Personelid { get; set; }
        public virtual Personel Personel { get; set; }             //1 SatisHareketinin 1 Personeli olabilir

    }
}