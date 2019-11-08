using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoteliTest.Models
{
    public class Gost
    {
        public int GostID { get; set; }

        [StringLength(60, MinimumLength=2)]
        public string Ime { get; set; }

        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string Prezime { get; set; }

        [StringLength(60, MinimumLength = 2)]
        public string Email { get; set; }

        [StringLength(60, MinimumLength = 2)]
        public string Adresa { get; set; }

        public virtual ICollection<Rezervacija> Rezervacije { get; set; } 
    }
}