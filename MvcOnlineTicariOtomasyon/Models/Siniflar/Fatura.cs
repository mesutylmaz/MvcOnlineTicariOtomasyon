using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Fatura
    {
        [Key]
        public int FaturaID { get; set; }


        [Column(TypeName = "Char")]
        [StringLength(1)]
        public string FaturaSeriNo { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(6)]
        public string FaturaSiraNo { get; set; }


        public DateTime FaturaTarih { get; set; }

        [Column(TypeName = "Char")]
        [StringLength(5)]
        public string FaturaSaat { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string FaturaTeslimEden { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string FaturaTeslimAlan { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(60)]
        public string FaturaVergiDairesi { get; set; }


        public decimal ToplamTutar { get; set; }


        public ICollection<FaturaKalem>FaturaKalems { get; set; }           //1 Faturanın 1'den fazla(Çok) FaturaKalemi olabilir

    }
}