using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HoteliTest.Models
{
    public class Racun
    {
        [Key]
        [Column(Order=1)]
        public int RacunID { get; set; }
        public bool Placeno { get; set; }
        [Display(Name = "Iznos Ukupno")]
        [DataType(DataType.Currency)]
        public double IznosUkupno { get; set; }

        [Required]
        [Column(Order=2)]
        public int RezervacijaID { get; set; }

        [ForeignKey("RezervacijaID")]
        public virtual Rezervacija Rezervacija { get; set; }
        public ICollection<StavkaRacuna> StavkaRacuna { get; set; }
    }
}