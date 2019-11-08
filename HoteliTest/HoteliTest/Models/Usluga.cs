using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoteliTest.Models
{
    public class Usluga
    {
        public int UslugaID { get; set; }

        [Display(Name = "Cijena Usluge")]
        [DataType(DataType.Currency)]
        public double CijenaUsluge { get; set; }

        [Display(Name = "Ime Usluge")]
        [StringLength(60, MinimumLength = 2)]
        public string ImeUsluge { get; set; }


        [Display(Name = "Stavka Racuna")]
        public ICollection<StavkaRacuna> StavkaRacuna { get; set; }
    }
}