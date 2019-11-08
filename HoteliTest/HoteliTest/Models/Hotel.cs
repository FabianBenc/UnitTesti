using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoteliTest.Models
{
    public class Hotel
    {
        [Key]
        public int HotelID { get; set; }

        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string Ime { get; set; }

        [StringLength(60, MinimumLength = 2)]
        public string Adresa { get; set; }

        [StringLength(60, MinimumLength = 2)]
        public string Lokacija { get; set; }

        public virtual ICollection<Soba> Sobe { get; set; }
    }
}