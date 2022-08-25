using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class FaturaKalem
    {
        [Key]
        public int FaturaKalemID { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string FaturaKalemAciklama { get; set; }
        public int FaturaKalemMiktar { get; set; }
        public decimal FaturaKalemBirimFiyat { get; set; }
        public decimal FaturaKalemTutar { get; set; }


        public int Faturaid { get; set; }
        public virtual Fatura Fatura { get; set; }              //1 FaturaKalemi 1 Faturaya olabilir
    }
}