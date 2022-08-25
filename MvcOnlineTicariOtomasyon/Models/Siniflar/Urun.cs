using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Urun
    {
        [Key]
        public int UrunID { get; set; }


        [Column(TypeName = "varchar")]
        [StringLength(30)]
        public string UrunAdi { get; set; }


        [Column(TypeName = "varchar")]
        [StringLength(30)]
        public string UrunMarka { get; set; }


        public short UrunStok { get; set; }
        public decimal UrunAlisFiyati { get; set; }
        public decimal UrunSatisFiyati { get; set; }
        public bool UrunDurumu { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string UrunGorseli { get; set; }


        public int Kategoriid {get; set;}           //Bu değişkeni Ürüne istenilen kategoriyi ekleyebilmek için kullanıcaz. Aksi halde, View kısmında kategori ekleme için Kategori.KategoriID denilirse sisteme en son eklenen Kategori'nin KategoriID değerine göre ürüne KategoriID'yi otomatik atıyor.


        public virtual Kategori Kategori { get; set; }                   //1 Ürünün 1 Kategorisi olabilir. Virtual yapmazsam View'da Kategori bilgisini alamam
        public ICollection<SatisHareket> SatisHarekets { get; set; }     //1 Ürünün 1'den fazla(Çok) SatisHareketi olabilir

    }
}