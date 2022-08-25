using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Cari
    {
        [Key]
        public int CariID { get; set; }


        [Column(TypeName = "Varchar")][Required(ErrorMessage ="Cari adını boş geçemezsiniz!")]
        [StringLength(30,ErrorMessage ="Cari adı en fazla 30 karakter girilebilir!")]
        public string CariAdi { get; set; }


        [Column(TypeName = "Varchar")][Required(ErrorMessage = "Cari soyadını boş geçemezsiniz!")]
        [StringLength(30, ErrorMessage = "Cari soyadı en fazla 30 karakter girilebilir!")]
        public string CariSoyadi { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(13, ErrorMessage = "Cari şehri en fazla 13 karakter girilebilir!")]
        public string CariSehir{ get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(50, ErrorMessage = "Cari maili en fazla 50 karakter girilebilir!")]
        public string CariMaili { get; set; }





        private bool _ilkDurumDegeri = true;
        public bool CariDurumu
        {
            get
            {
                return _ilkDurumDegeri;
            }
            set
            {
                _ilkDurumDegeri = value;
            }
        }



        public ICollection<SatisHareket> SatisHarekets { get; set; }              //1 Carinin 1'den fazla(Çok) SatisHareketi olabilir
    }
}