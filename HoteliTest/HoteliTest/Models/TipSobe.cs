using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoteliTest.Models
{
    public class TipSobe
    {
        public int TipSobeID { get; set; }

        [Display(Name = "Opis Sobe")]
        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string OpisSobe { get; set; }

        [Display(Name="Cijena Po Noci")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal CijenaPoNoci { get; set; }

        public virtual ICollection<Soba> Soba { get; set; }
    }
}