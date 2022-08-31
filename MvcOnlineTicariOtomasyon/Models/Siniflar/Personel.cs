using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Personel
    {
        [Key]
        public int PersonelID { get; set; }


        //[Display(Name ="Personel Adı")]
        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string PersonelAdi { get; set; }


        //[Display(Name ="Personel Soyadı")]
        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string PersonelSoyadi { get; set; }


        //[Display(Name ="Personel Görseli")]
        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string PersonelGorseli { get; set; }

        //[Display(Name ="Personel Durumu")]
        public bool PersonelDurumu { get; set; }




        public ICollection<SatisHareket> SatisHarekets { get; set; }//1 Personelin 1'den fazla(Çok) SatisHareketi olabilir
        public virtual Departman Departman { get; set; }            //1 Personelin 1 Departmanı olabilir. Virtual yapmazsam View'da Departman bilgisini alamam

        public int Departmanid { get; set; }        //Bu değişkeni Personele istenilen departmanı ekleyebilmek için kullanıcaz. Aksi halde, View kısmında departman ekleme için Departman.DepartmanID denilirse sisteme en son eklenen Departman'ın DepartmanID değerine göre personel DepartmanID'yi otomatik atıyor.
    }
}