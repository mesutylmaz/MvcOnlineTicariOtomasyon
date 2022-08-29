using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class YapilacakListesi
    {
        [Key]
        public int YapilacakID { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string Baslik { get; set; }


        [Column(TypeName = "bit")]
        public bool Durumu { get; set; }
    }
}
