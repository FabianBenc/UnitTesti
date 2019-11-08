using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HoteliTest.Models
{
    public class StavkaRacuna
    {
        [Display(Name = "Stavka RacunaID")]
        public int StavkaRacunaID { get; set; }
        public int Kolicina { get; set; }

        public int RacunID { get; set; }

        public int UslugaID { get; set; }

        public virtual Racun Racun { get; set; }
        public virtual Usluga Usluga { get; set; }
    }
}