using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Departman
    {
        [Key]
        public int DepartmanID { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string DepartmanAdi { get; set; }




        private bool _ilkDurumDegeri = true;
        public bool DepartmanDurumu { 
            get{
                return _ilkDurumDegeri;
            }
            set{
                _ilkDurumDegeri = value;
            } 
        }




        public ICollection<Personel>Personels { get; set; }                 ///1 SatisHareketinin 1'den fazla(Çok) Ürünü olabilir
    }
}