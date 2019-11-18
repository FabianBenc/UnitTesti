using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HoteliTest.Models
{
    public class Rezervacija
    {

      
        [Key]
        public int RezervacijaID {get; set;}

        [ForeignKey("Gost")]
        public int GostID { get; set; }

        [ForeignKey("Soba")]
        public int SobaID { get; set; }
        public decimal Popust { get; set; }
        public bool Rezervirano { get; set; }

        [Required,Display(Name ="Prijava")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Prijava { get; set; }

        [Required,Display(Name =("Odjava"))]
        [DataType(DataType.Date)]
       // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Odjava { get; set; }


        public virtual Soba Soba { get; set; }
        public virtual Gost Gost { get; set; }
        public virtual ICollection<Racun> Racun { get; set; }

       
    }

}